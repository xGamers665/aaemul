using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Network.Editor;

namespace AAEmu.Editor.Core.Packets.L2C
{
    public class ACShowArsPacket : EditorPacket
    {
        public ACShowArsPacket() : base(0x06)
        {
        }

        public override PacketStream Write(PacketStream stream)
        {
            stream.Write(""); // num
            stream.Write((uint) 0); // timeout

            return stream;
        }
    }
}
