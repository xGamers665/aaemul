using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Network.Editor;

namespace AAEmu.Editor.Core.Packets.L2C
{
    public class ACEnterPcCertPacket : EditorPacket
    {
        public ACEnterPcCertPacket() : base(0x07)
        {
        }

        public override PacketStream Write(PacketStream stream)
        {
            stream.Write((int) 0); // mt
            stream.Write((int) 0); // ct

            return stream;
        }
    }
}
