using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class SendBufferHandler
    {
        public static ThreadLocal<SendBuffer> localSendBuffer = new ThreadLocal<SendBuffer>();
        
        /// <summary>
        /// Default size : 524288 (= 512kb)
        /// </summary>
        /// <param name="_bufferSize"></param>
        public SendBufferHandler(int _bufferSize = 524288)
        {

        }
    }

    public class SendBuffer
    {
        byte[] buffer;

        public SendBuffer(int _bufferSize)
        {

        }
    }
}
