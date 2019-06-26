using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Controllers;
using AAEmu.Editor.Core.Network.Editor;

namespace AAEmu.Editor.Core.Packets.C2E
{
    public class CERequestCellLockPacket : EditorPacket
    {
        public CERequestCellLockPacket() : base(0x06)
        {
        }

        public override void Read(PacketStream stream)
        {
            var worldName = stream.ReadString();
            var cell = stream.ReadUInt64(); // Times2D
            var type = stream.ReadByte();
            var lok = stream.ReadBoolean(); // lock

            //EditorController.Editor(Connection, account);
        }
    }
}
