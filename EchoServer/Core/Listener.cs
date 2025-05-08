using System.Net.Sockets;
using System.Net;
using System;
using System.Threading;

namespace Core
{
    public class Listener
    {
        Socket listenerSocket;
        SocketAsyncEventArgs acceptArgs;

        public void Init(IPEndPoint _endPoint)
        {
            acceptArgs = new SocketAsyncEventArgs();

            listenerSocket = new Socket(_endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listenerSocket.Bind(_endPoint);
            listenerSocket.Listen(ServerOption.MaxPlayerCount);

            acceptArgs.Completed += new EventHandler<SocketAsyncEventArgs>(AfterAccept);
            OnAccept();
        }

        void OnAccept()
        {
            acceptArgs.AcceptSocket = null;
            bool isProgress = listenerSocket.AcceptAsync(acceptArgs);
            if (isProgress == false)
                AfterAccept(null, acceptArgs);
        }

        void AfterAccept(object _sender, SocketAsyncEventArgs _args)
        {
            if(acceptArgs.SocketError == SocketError.Success)
            {
                // To Do Make Session
            }
            else
            {
                Console.WriteLine("[Error] : Can not accept");
            }
            OnAccept();
        }
    }
}
