using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TTService;

namespace Department
{
    public class DepartmentSocketClient
    {

        public List<SecondaryTicket> secondaryTickets;
        public string department;

        // ManualResetEvent instances signal completion.  
        private ManualResetEvent connectDone =
            new ManualResetEvent(false);
        private ManualResetEvent sendDone =
            new ManualResetEvent(false);
        private ManualResetEvent receiveDone =
            new ManualResetEvent(false);

        public StringBuilder sb = new StringBuilder();

        public DepartmentSocketClient()
        {

        }

        public void StartClient()
        {
            // Connect to a remote device.  
            try
            {
                // Establish the remote endpoint for the socket.  
                // The name of the remote device is "host.contoso.com".  
                //IPAddress ipAddress = IPAddress.Parse("http://localhost");
                IPAddress ipAddress = IPAddress.Parse(GetLocalIPAddress());

                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 5000);

                // Create a TCP/IP socket.  
                Socket client = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect to the remote endpoint.  
                client.BeginConnect(remoteEP,
                    new AsyncCallback(ConnectCallback), client);
                connectDone.WaitOne();

                // Send test data to the remote device.  
                Send(client, $"ID {department} <EOF>");
                sendDone.WaitOne();

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
                connectDone.Set();
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

                    secondaryTickets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SecondaryTicket>>(content);

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

                // Signal that all bytes have been sent.  
                sendDone.Set();
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
