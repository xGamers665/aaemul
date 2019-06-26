using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Controllers;
using AAEmu.Editor.Core.Network.Editor;

namespace AAEmu.Editor.Core.Packets.C2E
{
    public class CEIncreaseVersionPacket : EditorPacket
    {
        public CEIncreaseVersionPacket() : base(0x05)
        {
        }

        public override void Read(PacketStream stream)
        {
            var worldName = stream.ReadString();
            var subCell = stream.ReadUInt64(); // Times2D
            var category = stream.ReadByte();

            //EditorController.Editor(Connection, account);
        }
    }
}
