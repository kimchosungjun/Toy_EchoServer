using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public abstract class Session
    {
        Socket connectSocket;
        SocketAsyncEventArgs receiveArgs = new SocketAsyncEventArgs();
        SocketAsyncEventArgs sendArgs = new SocketAsyncEventArgs();

        int connectFlag = 0; // 0 : Connect, 1: Disconnect
        public void Init(Socket _connectSocket)
        {
            connectSocket = _connectSocket;
            receiveArgs.Completed += AfterReceivePacket;
            sendArgs.Completed += AfterSendPacket;
            OnReceivePacket();
        }

        // Connect, Disconnect, Send, Receive
        #region Abstract CallBack Methods
        public abstract void OnCompletedConnect();
        public abstract void OnCompletedDisconnect();
        public abstract void OnCompletedSendPacket();
        public abstract void OnCompletedReceivePacket();
        #endregion

        // Disconnect, Send, Receive
        #region Fundamental Methods
        
        public void OnDisconnect()
        {
            if (Interlocked.Exchange(ref connectFlag, 1) == 1)
                return;
            connectSocket.Shutdown(SocketShutdown.Both);
            connectSocket.Close();
            OnCompletedDisconnect();
        }

        void OnReceivePacket()
        {

        }

        void AfterReceivePacket(object _sender, SocketAsyncEventArgs _args)
        {

        }

        void OnSendPacket()
        {

        }

        void AfterSendPacket(object _sender, SocketAsyncEventArgs _args)
        {

        }
        #endregion
    }
}
