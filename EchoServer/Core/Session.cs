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

        public void Init(Socket _connectSocket)
        {
            connectSocket = _connectSocket; 

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

        }

        #endregion
    }
}
