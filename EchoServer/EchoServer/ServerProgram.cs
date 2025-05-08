using System.Net.Sockets;
using System.Net;
using System;
using System.Threading;
using Core;

namespace EchoServer
{
    internal class ServerProgram
    {
        static void Main(string[] args)
        {
            string hostName = Dns.GetHostName();
            IPHostEntry entry = Dns.GetHostEntry(hostName);
            IPAddress address = entry.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(address, ServerOption.Port);   

            Listener listener = new Listener();
            listener.Init(endPoint);

            while (true)
            {
                ;
            }
        }
    }
}
