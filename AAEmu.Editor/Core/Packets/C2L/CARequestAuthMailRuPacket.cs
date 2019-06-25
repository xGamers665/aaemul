using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Controllers;
using AAEmu.Editor.Core.Network.Editor;

namespace AAEmu.Editor.Core.Packets.C2L
{
    public class CARequestAuthMailRuPacket : EditorPacket
    {
        public CARequestAuthMailRuPacket() : base(0x05)
        {
        }

        public override void Read(PacketStream stream)
        {
            var pFrom = stream.ReadUInt32();
            var pTo = stream.ReadUInt32();
            var dev = stream.ReadBoolean();
            var mac = stream.ReadBytes();
            var id = stream.ReadString();
            var token = stream.ReadBytes();

            EditorController.Editor(Connection, id, token);
        }
    }
}
