using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Controllers;
using AAEmu.Editor.Core.Network.Editor;

namespace AAEmu.Editor.Core.Packets.C2L
{
    public class CARequestReconnectPacket : EditorPacket
    {
        public CARequestReconnectPacket() : base(0x0d)
        {}

        public override void Read(PacketStream stream)
        {
            var pFrom = stream.ReadUInt32();
            var pTo = stream.ReadUInt32();
            var accountId = stream.ReadUInt32();
            var gsId = stream.ReadByte();
            var cookie = stream.ReadUInt32();
            var macLength = stream.ReadUInt16();
            var mac = stream.ReadBytes(macLength);

            EditorController.Instance.Reconnect(Connection, gsId, accountId, cookie);
        }
    }
}
