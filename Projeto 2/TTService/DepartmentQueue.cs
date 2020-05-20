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
      
        public ManualResetEvent allDone = new ManualResetEvent(false);
        StringBuilder sb = new StringBuilder();


        public DepartmentQueue()
        {


        }

        ~DepartmentQueue()
        {
            t.Interrupt();
        }

        public bool AddSecondaryTicket(SecondaryTicket st)
        {
            if (secondaryTickets.ContainsKey(st.secondarySolver))
            {
                if (!secondaryTickets.TryGetValue(st.secondarySolver, out List<SecondaryTicket> tickets))
                    return false;

                List<SecondaryTicket> old = new List<SecondaryTicket>(tickets);
                tickets.Add(st);

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
                // Establish the local endpoint for the socket.  
                // The DNS name of the computer  
                // running the listener is "host.contoso.com".

                // Bind the socket to the local endpoint and listen for incoming connections.  

                IPAddress ipAddress = IPAddress.Parse(GetLocalIPAddress());
                IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 5000);

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
                    if (content.StartsWith("ID"))
                    {
                        content = content.Replace("ID", "");
                        content = content.Trim();

                        SendTickets(handler, content);
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

            string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(departmentTickets);
         
            secondaryTickets.TryRemove(departmentId, out List<SecondaryTicket> foobar);

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

        void Heartline()
        {
            Console.WriteLine("I'm alive!");
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
