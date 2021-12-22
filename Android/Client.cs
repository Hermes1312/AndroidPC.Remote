using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Network;
using Packets;

namespace Android
{
    public class Client
    {
        public ClientConnectionContainer ClientConnectionContainer;
        private readonly string _ip;

        public Client(string ip)
        {
            _ip = ip;
            Connect();
        }

        public void Connect()
        {
            ClientConnectionContainer = ConnectionFactory.CreateSecureClientConnectionContainer(_ip, 1122);

            ClientConnectionContainer.AutoReconnect = false;
            ClientConnectionContainer.ReconnectInterval = 10000;
            ClientConnectionContainer.ConnectionEstablished += ClientConnectionContainer_ConnectionEstablished;
            ClientConnectionContainer.ConnectionLost += ClientConnectionContainer_ConnectionLost;
        }


        private void ClientConnectionContainer_ConnectionLost(Connection connection, Network.Enums.ConnectionType type, Network.Enums.CloseReason reason)
        {
            Connect();
        }

        private static void ClientConnectionContainer_ConnectionEstablished(Connection connection, Network.Enums.ConnectionType connectionType)
        {
            connection.KeepAlive = true;
            connection.EnableLogging = true;
            connection.TIMEOUT = 60000;

            if (connectionType == Network.Enums.ConnectionType.TCP)
            {

            }
        }

        public void SendShutdown() 
            => ClientConnectionContainer.Send(new ShutdownRequest(), ClientConnectionContainer);

        public void SendVolume(int direction) 
            => ClientConnectionContainer.Send(new VolumeRequest(direction), ClientConnectionContainer);

        public void PausePlay()
            => ClientConnectionContainer.Send(new PausePlayRequest(), ClientConnectionContainer);
    }
}
