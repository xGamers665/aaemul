using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Network.Editor;

namespace AAEmu.Editor.Core.Packets.L2C
{
    public class ACAccountWarnedPacket : EditorPacket
    {
        public ACAccountWarnedPacket() : base(0x0D)
        {
        }

        public override PacketStream Write(PacketStream stream)
        {
            stream.Write((byte) 0); // source
            stream.Write(""); // msg

            return stream;
        }
    }
}
