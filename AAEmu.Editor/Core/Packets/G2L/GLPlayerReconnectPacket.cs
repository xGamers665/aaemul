using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Controllers;
using AAEmu.Editor.Core.Network.Internal;

namespace AAEmu.Editor.Core.Packets.G2L
{
    public class GLPlayerReconnectPacket : InternalPacket
    {
        public GLPlayerReconnectPacket() : base(0x02)
        {
        }

        public override void Read(PacketStream stream)
        {
            var gsId = stream.ReadByte();
            var accountId = stream.ReadUInt32();
            var token = stream.ReadUInt32();

            EditorController.Instance.AddReconnectionToken(Connection, gsId, accountId, token);
        }
    }
}
