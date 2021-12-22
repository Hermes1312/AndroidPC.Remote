using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Network.Packets;

namespace Packets
{
    public class TimerRequest : RequestPacket
    {
        public TimerRequest(int time) => Time = time;
        public int Time { get; set; }
    }
}
