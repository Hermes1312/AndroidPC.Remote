using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Network;
using Network.Packets;
using Network.RSA;
using Packets;

namespace PC
{
    public class Listener
    {
        public ServerConnectionContainer Server;

        public Listener(int port)
        {
            Server = ConnectionFactory.CreateServerConnectionContainer(port, start: false);
            Server.ConnectionEstablished += ConnectionEstablished;
            Server.ConnectionLost += ConnectionLost;
            Server.AllowUDPConnections = true;
        }

        public void Start() 
        {
            Server.Start();
            Console.WriteLine("Server started!");
        }
        

        private void ConnectionLost(Connection connection, Network.Enums.ConnectionType connectionType, Network.Enums.CloseReason closeReason)
        {

        }

        private void ConnectionEstablished(Connection connection, Network.Enums.ConnectionType connectionType)
        {
            connection.EnableLogging = true;
            connection.TIMEOUT = 60000;

            // 0 = Tcp
            if (connectionType == 0)
            {
                connection.RegisterStaticPacketHandler<VolumeRequest>(VolumeRequestHandler);
                connection.RegisterStaticPacketHandler<PausePlayRequest>(PausePlayRequestHandler);
                connection.RegisterStaticPacketHandler<ShutdownRequest>(ShutdownRequestHandler);
                connection.RegisterStaticPacketHandler<TimerRequest>(TimerRequestHandler);
            }
        }

        private static void TimerRequestHandler(TimerRequest packet, Connection connection) 
            => Functions.SetTimer(packet.Time * 1000);

        private static void ShutdownRequestHandler(ShutdownRequest packet, Connection connection) 
            => Functions.Shutdown();

        private static void PausePlayRequestHandler(PausePlayRequest packet, Connection connection) 
            => Functions.PausePlay();

        private static void VolumeRequestHandler(VolumeRequest packet, Connection connection)
            => Functions.SetVolume(packet.Direction);

    }
}
