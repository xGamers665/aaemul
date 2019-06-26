using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Controllers;
using AAEmu.Editor.Core.Network.Editor;

namespace AAEmu.Editor.Core.Packets.C2E
{
    public class CEChatPacket : EditorPacket
    {
        public CEChatPacket() : base(0x08)
        {
        }

        public override void Read(PacketStream stream)
        {
            var msg = stream.ReadString();

            //EditorController.Editor(Connection, account);
        }
    }
}
