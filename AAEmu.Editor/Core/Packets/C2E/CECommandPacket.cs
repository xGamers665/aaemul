using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Controllers;
using AAEmu.Editor.Core.Network.Editor;

namespace AAEmu.Editor.Core.Packets.C2E
{
    public class CECommandPacket : EditorPacket
    {
        public CECommandPacket() : base(0x0C)
        {
        }

        public override void Read(PacketStream stream)
        {
            var botAccounr = stream.ReadString();
            var command = stream.ReadByte();
            var jobId = stream.ReadUInt32();

            //EditorController.Editor(Connection, account);
        }
    }
}
