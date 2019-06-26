using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Controllers;
using AAEmu.Editor.Core.Network.Editor;

namespace AAEmu.Editor.Core.Packets.C2E
{
    public class CERequestLockPacket : EditorPacket
    {
        public CERequestLockPacket() : base(0x03)
        {
        }

        public override void Read(PacketStream stream)
        {
            var worldName = stream.ReadString();
            var subCell = stream.ReadUInt64(); // Times2D
            var category = stream.ReadByte();
            var type = stream.ReadByte();
            var lok = stream.ReadBoolean(); // lock

            //EditorController.Editor(Connection, account);
        }
    }
}
