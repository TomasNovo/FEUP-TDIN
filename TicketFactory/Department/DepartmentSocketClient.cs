using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TTService;

namespace Department
{
    public class DepartmentSocketClient
    {

        public List<SecondaryTicket> secondaryTickets = new List<SecondaryTicket>();
        public string department;
        private string ticketFilePath;

        public delegate void AfterReceive();
        public AfterReceive afterReceive = null;

        // ManualResetEvent instances signal completion.  
        private ManualResetEvent receiveDone =
            new ManualResetEvent(false);

        public StringBuilder sb = new StringBuilder();

        public DepartmentSocketClient(string department)
        {
            this.department = department;
            ticketFilePath = $"tickets-{department}.json";
            GetSecondaryTicketsFile();
        }

        public void StartClient(string message)
        {
            StartClient(message, null);
        }

        public void StartClient(string message, AfterReceive ar)
        {
            // Connect to a remote device.  
            try
            {
                afterReceive = ar;

                receiveDone = new ManualResetEvent(false);

                int port = SocketConstants.port;

                IPAddress ipAddress = IPAddress.Parse(DepartmentQueue.GetLocalIPAddress());
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                // Create a TCP/IP socket.  
                Socket client = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect to the remote endpoint.  
                client.BeginConnect(remoteEP,
                    new AsyncCallback(ConnectCallback), client);
                //connectDone.WaitOne();

                // Send test data to the remote device.  
                Send(client, $"{message} <EOF>");
                //sendDone.WaitOne();

                // Receive the response from the remote device.  
                Receive(client);
                receiveDone.WaitOne();

                Console.WriteLine("Closed socket");

                // Release the socket.  
                client.Shutdown(SocketShutdown.Both);
                client.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.  
                client.EndConnect(ar);

                Console.WriteLine("Socket connected to {0}",
                    client.RemoteEndPoint.ToString());

                // Signal that the connection has been made.  
                //connectDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void Receive(Socket client)
        {
            try
            {
                // Begin receiving the data from the remote device.
                byte[] buffer = new byte[SocketConstants.BUFFER_SIZE];
                client.BeginReceive(buffer, 0, SocketConstants.BUFFER_SIZE, 0,
                    new AsyncCallback(ReceiveCallback), new Tuple<byte[], Socket>(buffer, client));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket
                // from the asynchronous state object.  
                Tuple<byte[], Socket> tuple = (Tuple<byte[], Socket>)ar.AsyncState;
                byte[] buffer = tuple.Item1;
                Socket client = tuple.Item2;

                // Read data from the remote device.  
                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // There might be more data, so store the data received so far.  

                    sb.Append(Encoding.ASCII.GetString(buffer, 0, bytesRead));

                    // Get the rest of the data.  
                    client.BeginReceive(buffer, 0, SocketConstants.BUFFER_SIZE, 0,
                        new AsyncCallback(ReceiveCallback), new Tuple<byte[], Socket>(buffer, client));
                }
                else
                {
                    string content = sb.ToString();
                    content = content.Replace(SocketConstants.EOF, "");
                    content = content.Trim();

                    // All the data has arrived; clear.  
                    sb.Clear();

                    if (content.StartsWith(SocketConstants.SECONDARYTICKETS))
                    {
                        content = content.Replace(SocketConstants.SECONDARYTICKETS, "");
                        content = content.Trim();

                        List<SecondaryTicket> receivedTickets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SecondaryTicket>>(content);

                        // Update existing SecondaryTickets and add new ones
                        for (int i = 0; i < receivedTickets.Count; i++)
                        {
                            SecondaryTicket newTicket = receivedTickets[i];
                            int index = secondaryTickets.FindIndex((x) => x.Id == newTicket.Id);

                            if (index == -1)
                            {
                                secondaryTickets.Add(newTicket);
                            }
                            else
                            {
                                secondaryTickets[index] = newTicket;
                            }
                        }

                        UpdateSecondaryTicketsFile();

                    }

                    if (content.StartsWith(SocketConstants.OK))
                    {

                    }

                    if (afterReceive != null)
                    {
                        afterReceive();
                        afterReceive = null;
                    }

                    // Signal that all bytes have been received.  
                    receiveDone.Set();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void Send(Socket client, String data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), client);
        }

        public void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = client.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);

                //sendDone.Set();
                // Signal that all bytes have been sent.  
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void UpdateSecondaryTicketsFile()
        {
            FileStream fs;

            // Create a file to write to.
            if (!File.Exists(ticketFilePath))
                fs = File.Create(ticketFilePath);
            else
                fs = File.Open(ticketFilePath, FileMode.Truncate);

            byte[] stream = Encoding.Unicode.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(secondaryTickets));
            fs.Write(stream, 0, stream.Length);
            fs.Close();
        }

        public void GetSecondaryTicketsFile()
        {
            if (File.Exists(ticketFilePath))
            {
                string json = File.ReadAllText(ticketFilePath, Encoding.Unicode);

                if (json != "")
                    secondaryTickets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SecondaryTicket>>(json);
            }
        }

    }
}
