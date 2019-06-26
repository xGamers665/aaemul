using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Controllers;
using AAEmu.Editor.Core.Network.Editor;

namespace AAEmu.Editor.Core.Packets.C2E
{
    public class CELoginPacket : EditorPacket
    {
        public CELoginPacket() : base(0x01)
        {
        }

        public override void Read(PacketStream stream)
        {
            var pFrom = stream.ReadUInt32();
            var pTo = stream.ReadUInt32();
            var account = stream.ReadString();

            EditorController.Editor(Connection, account);
        }
    }
}
