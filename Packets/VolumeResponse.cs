using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Network.Attributes;
using Network.Packets;

namespace Packets
{
    [PacketRequest(typeof(VolumeRequest))]
    public class VolumeResponse : ResponsePacket
    {
        public VolumeResponse(bool Succeed, RequestPacket request) : base(request)
        {
            this.Succeed = Succeed;
        }

        public bool Succeed { get; set; }
    }
}
