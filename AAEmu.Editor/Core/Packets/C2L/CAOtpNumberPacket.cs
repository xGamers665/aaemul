using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Network.Editor;

namespace AAEmu.Editor.Core.Packets.C2L
{
    public class CAOtpNumberPacket : EditorPacket
    {
        public CAOtpNumberPacket() : base(0x07)
        {
        }

        public override void Read(PacketStream stream)
        {
            var num = stream.ReadString(); // TODO but on old client length const 8
        }
    }
}
