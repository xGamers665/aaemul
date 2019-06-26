using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Controllers;
using AAEmu.Editor.Core.Network.Editor;

namespace AAEmu.Editor.Core.Packets.C2E
{
    public class CEBatchRequestPacket : EditorPacket
    {
        public CEBatchRequestPacket() : base(0x0A)
        {
        }

        public override void Read(PacketStream stream)
        {
            var requestId = stream.ReadUInt32();
            var worldName = stream.ReadString();
            var cellCount = stream.ReadInt32();
            for (var i = 0; i < cellCount; i++)
            {
                var cell = stream.ReadUInt64(); //Times2D
            }
            var flags = stream.ReadUInt32();

            //EditorController.Editor(Connection, account);
        }
    }
}
