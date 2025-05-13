using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class ReceiveBuffer
    {
        #region Variables
        // Values
        int readPosition;
        int writePosition;
        ArraySegment<byte> buffer;

        // Property
        public int ReadSize { get { return writePosition - readPosition; } }
        public int WriteSize { get { return buffer.Count - writePosition; } }
        public ArraySegment<byte> GetReadSegment { get { return new ArraySegment<byte>(buffer.Array, buffer.Offset + readPosition, ReadSize); } }
        public ArraySegment<byte> GetWriteSegment { get { return new ArraySegment<byte>(buffer.Array, buffer.Offset + writePosition, WriteSize); } }
        #endregion

        #region Creator
        /// <summary>
        /// Default Size = 4KB
        /// </summary>
        /// <param name="_bufferSize"></param>
        public ReceiveBuffer(int _bufferSize = 4096) 
        {
            buffer = new ArraySegment<byte>(new byte[_bufferSize], 0, _bufferSize);
            readPosition = writePosition = 0;
        }
        #endregion

        // Clean & Read & Write
        #region Fundamental Methods
        public void Clean()
        {
            if(readPosition == writePosition)
            {
                readPosition = writePosition = 0;
            }
            else
            {
                int diff = writePosition - readPosition;
                Array.Copy(buffer.Array, buffer.Offset + readPosition, buffer.Array, buffer.Offset, diff);
                readPosition = 0;
                writePosition = diff;
            }
        }

        /// <summary>
        /// if you can read return True
        /// </summary>
        /// <returns></returns>
        public bool OnRead(int _readSize)
        {
            if(ReadSize < _readSize)
                return false;
            readPosition += _readSize;
            return true;
        }

        /// <summary>
        /// if you can write return True
        /// </summary>
        /// <returns></returns>
        public bool OnWrite(int _writeSize)
        {
            if (WriteSize < _writeSize)
                return false;
            writePosition += _writeSize;    
            return false;
        }
        #endregion
    }
}
