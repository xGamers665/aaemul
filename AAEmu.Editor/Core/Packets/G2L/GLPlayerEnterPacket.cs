using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Controllers;
using AAEmu.Editor.Core.Network.Connections;
using AAEmu.Editor.Core.Network.Internal;

namespace AAEmu.Editor.Core.Packets.G2L
{
    public class GLPlayerEnterPacket : InternalPacket
    {
        public GLPlayerEnterPacket() : base(0x01)
        {
        }

        public override void Read(PacketStream stream)
        {
            var connectionId = stream.ReadUInt32();
            var gsId = stream.ReadByte();
            var result = stream.ReadByte();

            var connection = EditorConnectionTable.Instance.GetConnection(connectionId);
            GameController.Instance.EnterWorld(connection, gsId, result);
        }
    }
}
