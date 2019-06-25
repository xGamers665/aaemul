using System.Net;
using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Network.Internal;
using AAEmu.Editor.Models;

namespace AAEmu.Editor.Core.Network.Connections
{
    public class InternalConnection
    {
        private Session _session;

        public uint Id => _session.Id;
        public IPAddress Ip => _session.Ip;
        public GameServer GameServer { get; set; }
        public bool Block { get; set; }
        public PacketStream LastPacket { get; set; }

        public InternalConnection(Session session)
        {
            _session = session;
        }

        public void OnConnect()
        {
        }

        public void SendPacket(InternalPacket packet)
        {
            if (Block)
                return;
            packet.Connection = this;
            byte[] buf = packet.Encode();
            _session.SendPacket(buf);
        }

        public void AddAttribute(string name, object value)
        {
            _session.AddAttribute(name, value);
        }
    }
}
