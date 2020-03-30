using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using RemoteObjects;
using System.Threading;

namespace ChatClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // using TCP protocol 
            // running both client and server on same machines
            TcpChannel chan = new TcpChannel();
            ChannelServices.RegisterChannel(chan, false);
            // Create an instance of the remote object
            RemoteObject remoteObject = (RemoteObject)Activator.GetObject(
              typeof(RemoteObject), "tcp://localhost:5000/HelloWorld");

            Console.WriteLine("Waiting...");
            Console.ReadKey();
            
            remoteObject.SendMessage("ola");

            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}
