using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Network.Packets;

namespace Packets
{
    public class HandshakeRequest : RequestPacket
    {
        public HandshakeRequest(string welcome)
        {
            Welcome = welcome;
        }

        public string Welcome { get; set; }
    }
}
