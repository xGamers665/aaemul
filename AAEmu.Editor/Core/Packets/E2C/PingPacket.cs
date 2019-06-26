using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Network.Editor;

namespace AAEmu.Editor.Core.Packets.E2C
{
    public class PingPacket : EditorPacket
    {

        public PingPacket() : base(0x07)
        {
        }

        public override PacketStream Write(PacketStream stream)
        {
            return stream;
        }
    }
}
