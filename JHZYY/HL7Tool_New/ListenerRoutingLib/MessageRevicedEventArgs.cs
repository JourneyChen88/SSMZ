using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace MediII.Adapter.ListenerRouting
{
    public class MessageRevicedEventArgs:EventArgs
    {
        public byte[] Contents { get; set; }
        public Socket SocketHandler { get; set; }
    }
}
