using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public abstract class Session
    {
        #region Variables
        int connectFlag = 0; // 0 : Connect, 1: Disconnect

        Socket connectSocket;
        ReceiveBuffer receiveBuffer;
        SocketAsyncEventArgs receiveArgs = new SocketAsyncEventArgs();
        SocketAsyncEventArgs sendArgs = new SocketAsyncEventArgs();
        #endregion

        // Connect, Disconnect, Send, Receive
        #region Abstract CallBack Methods
        public abstract void OnCompletedConnect();
        public abstract void OnCompletedDisconnect(EndPoint _endPoint);
        public abstract void OnCompletedSendPacket();
        public abstract int OnCompletedReceivePacket(ArraySegment<byte> _bufferSegment);
        #endregion

        #region Connect & Disconnect
        public void Connect(Socket _connectSocket)
        {
            connectSocket = _connectSocket;
            receiveBuffer = new ReceiveBuffer(4096);

            receiveArgs.Completed += AfterReceivePacket;
            sendArgs.Completed += AfterSendPacket;
            OnReceivePacket();
        }

        public void OnDisconnect()
        {
            if (Interlocked.Exchange(ref connectFlag, 1) == 1)
                return;
            OnCompletedDisconnect(connectSocket.RemoteEndPoint);
            connectSocket.Shutdown(SocketShutdown.Both);
            connectSocket.Close();
        }
        #endregion

        #region Receive
        void OnReceivePacket()
        {
            receiveBuffer.Clean();
            ArraySegment<byte> bufferSegment = receiveBuffer.GetWriteSegment;
            receiveArgs.SetBuffer(bufferSegment.Array, bufferSegment.Offset, bufferSegment.Count);            
            bool isProgress = connectSocket.ReceiveAsync(receiveArgs);
            if (isProgress == false)
                AfterReceivePacket(null, receiveArgs);
        }

        void AfterReceivePacket(object _sender, SocketAsyncEventArgs _args)
        {
            int byteSize = receiveArgs.BytesTransferred;
            if (byteSize > 0 && receiveArgs.SocketError == SocketError.Success)
            {
                try
                {
                    if(receiveBuffer.OnWrite(byteSize) == false)
                    {
                        Console.WriteLine("[Error] : Can't write packet / Over Size");
                        OnDisconnect();
                        return;
                    }

                    int receiveSize = OnCompletedReceivePacket(receiveBuffer.GetReadSegment);
                    if(receiveSize == 0 || receiveBuffer.ReadSize < receiveSize)
                    {
                        Console.WriteLine("[Error] : Receive Over Size Packet");
                        OnDisconnect();
                        return;
                    }

                    if(receiveBuffer.OnRead(byteSize) == false)
                    {
                        Console.WriteLine("[Error] : Can't read packet / Over Size");
                        OnDisconnect();
                        return;
                    }

                    OnReceivePacket();
                }
                catch (Exception ex)
                {
                    // To Do Change 
                    Console.WriteLine(ex.ToString());
                    OnDisconnect();
                    return;
                }
            }
            else
            {
                // To Do Change 
                OnDisconnect();
                return;
            }
        }
        #endregion

        #region Send
        void OnSendPacket()
        {

        }

        void AfterSendPacket(object _sender, SocketAsyncEventArgs _args)
        {

        }
        #endregion
    }
}
