using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class ServerOption
    {
        public const int Port = 9000;
        public const int MaxPlayerCount = 100;
        public int ReceiveBufferSize { get; set; } = 4096; // Limit 4kb
        public int MaxPacketSize { get; set; } = 1024; // Limit 1kb

        public void LogSize()
        {
            Console.WriteLine($"Recv Buffer Size : {ReceiveBufferSize}");
            Console.WriteLine($"Max Packet Size : {MaxPacketSize}");
        }
    }
}
