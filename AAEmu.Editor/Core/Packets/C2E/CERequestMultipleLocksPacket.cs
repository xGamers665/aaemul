using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Controllers;
using AAEmu.Editor.Core.Network.Editor;

namespace AAEmu.Editor.Core.Packets.C2E
{
    public class CERequestMultipleLocksPacket : EditorPacket
    {
        public CERequestMultipleLocksPacket() : base(0x04)
        {
        }

        public override void Read(PacketStream stream)
        {
            var worldName = stream.ReadString();
            var subCellCount = stream.ReadInt32();
            for (var i = 0; i < subCellCount; i++)
            {
                var subCell = stream.ReadUInt64(); //Times2D
                var category = stream.ReadByte();
            }
            var type = stream.ReadByte();
            var lok = stream.ReadBoolean(); // lock

            //EditorController.Editor(Connection, account);
        }
    }
}
