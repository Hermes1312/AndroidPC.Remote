using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Network.Attributes;
using Network.Packets;

namespace Packets
{
    [PacketRequest(typeof(HandshakeRequest))]
    public class HandshakeResponse : ResponsePacket
    {
        public HandshakeResponse(bool succeed, RequestPacket packet) : base(packet) 
            => Succeed = succeed;

        public bool Succeed { get; set; }
    }
}
