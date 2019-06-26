using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Controllers;
using AAEmu.Editor.Core.Network.Editor;

namespace AAEmu.Editor.Core.Packets.C2E
{
    public class CERequestCellLockInfoPacket : EditorPacket
    {
        public CERequestCellLockInfoPacket() : base(0x02)
        {
        }

        public override void Read(PacketStream stream)
        {
            var worldName = stream.ReadString();
            var cellCount = stream.ReadInt32();
            for (var i = 0; i < cellCount; i++)
            {
                var cell = stream.ReadUInt64(); //Times2D
            }

            //EditorController.Editor(Connection, account);
        }
    }
}
