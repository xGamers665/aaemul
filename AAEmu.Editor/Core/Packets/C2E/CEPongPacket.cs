using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Controllers;
using AAEmu.Editor.Core.Network.Editor;

namespace AAEmu.Editor.Core.Packets.C2E
{
    public class CEPongPacket : EditorPacket
    {
        public CEPongPacket() : base(0x07)
        {
        }

        public override void Read(PacketStream stream)
        {

            //EditorController.Editor(Connection, account);
        }
    }
}
