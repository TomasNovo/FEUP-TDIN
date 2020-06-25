using MongoDB.Bson.IO;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TTService
{
    public class DepartmentQueue
    {
        // Stores secondaryTickets using secondarySolvers as keys
        private ConcurrentDictionary<string, List<SecondaryTicket>> secondaryTickets = new ConcurrentDictionary<string, List<SecondaryTicket>>();
        private Thread t;
        public Database db;

        private List<int> usedPorts;
        
        public ManualResetEvent allDone = new ManualResetEvent(false);
        StringBuilder sb = new StringBuilder();

        public DepartmentQueue()
        {

        }

        public void FetchSecondaryTickets()
        {
            List<SecondaryTicket> tickets = db.GetSecondaryTickets();

            for (int i = 0; i < tickets.Count; i++)
            {
                SecondaryTicket ticket = tickets[i];

                //if (!ticket.received)
                    AddSecondaryTicket(ticket);
            }
        }

        public bool AddSecondaryTicket(SecondaryTicket st)
        {
            if (secondaryTickets.ContainsKey(st.secondarySolver))
            {
                if (!secondaryTickets.TryGetValue(st.secondarySolver, out List<SecondaryTicket> tickets))
                    return false;

                List<SecondaryTicket> old = new List<SecondaryTicket>(tickets);
                int index = old.FindIndex(x => x.Id == st.Id);

                if (index == -1)
                    tickets.Add(st);
                else
                    tickets[index] = st;

                if (!secondaryTickets.TryUpdate(st.secondarySolver, tickets, old))
                    return false;
            }
            else
            {
                List<SecondaryTicket> list = new List<SecondaryTicket>();
                list.Add(st);

                if (!secondaryTickets.TryAdd(st.secondarySolver, list))
                    return false;
            }

            return true;
        }

        public void Listen()
        {
            Console.WriteLine("In Main: Creating the Child thread");
            t = new Thread(new ThreadStart(StartListening));
            t.Start();
        }

        private void StartListening()
        {
            try
            {
                IPAddress ipAddress = IPAddress.Parse(GetLocalIPAddress());
                //int port = new Random().Next(SocketConstants.lowerPortBound, SocketConstants.higherPortBound);
                int port = SocketConstants.port;

                Console.WriteLine($"Port: {port}");
                IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, port);

                // Create a TCP/IP socket.
                Socket listener = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (true)
                {
                    // Set the event to nonsignaled state.  
                    allDone.Reset();

                    // Start an asynchronous socket to listen for connections.
                    Console.WriteLine("Waiting for a connection...");
                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        listener);

                    // Wait until a connection is made before continuing.  
                    allDone.WaitOne();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }

        public void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.  
            allDone.Set();

            // Get the socket that handles the client request.  
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            // Create the state object.  
            byte[] buffer = new byte[SocketConstants.BUFFER_SIZE];
            handler.BeginReceive(buffer, 0, SocketConstants.BUFFER_SIZE, 0,
                new AsyncCallback(ReadCallback), new Tuple<byte[], Socket>(buffer, handler));
        }

        public void ReadCallback(IAsyncResult ar)
        {
            // Retrieve the state object and the handler socket  
            // from the asynchronous state object.  
            Tuple<byte[], Socket> tuple = (Tuple<byte[], Socket>)ar.AsyncState;
            byte[] buffer = tuple.Item1;
            Socket handler = tuple.Item2;

            // Read data from the client socket.
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far.  
                sb.Append(Encoding.ASCII.GetString(
                    buffer, 0, bytesRead));

                // Check for end-of-file tag. If it is not there, read
                // more data.  
                string content = sb.ToString();

                if (content.IndexOf(SocketConstants.EOF) > -1)
                {
                    sb.Clear();
                    content = content.Replace(SocketConstants.EOF, "");
                    content = content.Trim();

                    // All the data has been read from the
                    // client. Display it on the console.  
                    Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                        content.Length, content);

                    // Handle request
                    if (content.StartsWith(SocketConstants.ID))
                    {
                        content = content.Replace(SocketConstants.ID, "");
                        content = content.Trim();

                        SendTickets(handler, content);
                    }

                    if (content.StartsWith(SocketConstants.ANSWER))
                    {
                        content = content.Replace(SocketConstants.ANSWER, "");
                        content = content.Trim();

                        string[] args = content.Split(' ');

                        string ticketId = args[0];
                        content = content.Replace(ticketId, "");
                        content = content.Trim();

                        db.ChangeSecondaryTicketAnswer(ticketId, content);

                        FetchSecondaryTickets();

                        // TODO: Setup event to receive answer

                        Send(handler, SocketConstants.OK);
                    }
                }
                else
                {
                    // Not all data received. Get more.  
                    byte[] buffer2 = new byte[SocketConstants.BUFFER_SIZE];
                    handler.BeginReceive(buffer2, 0, SocketConstants.BUFFER_SIZE, 0,
                        new AsyncCallback(ReadCallback), new Tuple<byte[], Socket>(buffer2, handler));
                }
            }
        }

        public void SendTickets(Socket handler, string departmentId)
        {
            Console.WriteLine($"Sending tickets to {departmentId}");

            List<SecondaryTicket> tickets;
            if (!secondaryTickets.TryGetValue(departmentId, out tickets))
                tickets = new List<SecondaryTicket>();

            Console.WriteLine(tickets.Count);

            List<SecondaryTicket> departmentTickets = new List<SecondaryTicket>();

            for (int i = 0; i < tickets.Count; i++)
            {
                SecondaryTicket ticket = tickets[i];
                  
                departmentTickets.Add(ticket);

                db.SetSecondaryTicketReceived(ticket.Id.ToString(), true);
            }

            string jsonString = $"SECONDARYTICKETS {Newtonsoft.Json.JsonConvert.SerializeObject(departmentTickets)}";
         
            Send(handler, jsonString);
        }

        public void Send(Socket handler, String data)
        {
            data += "" + SocketConstants.EOF;

            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        public void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);


                handler.Shutdown(SocketShutdown.Both);
                handler.Close();

                Console.WriteLine("Closed socket");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static string GetLocalIPAddress()
        {
            string localIP;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                localIP = endPoint.Address.ToString();

                return localIP;
            }
        }
    }
}
