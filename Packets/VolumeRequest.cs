using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Network.Packets;

namespace Packets
{
    public class VolumeRequest : RequestPacket
    {
        public VolumeRequest(int direction) => Direction = direction;
        public int Direction { get; set; }
    }
}
