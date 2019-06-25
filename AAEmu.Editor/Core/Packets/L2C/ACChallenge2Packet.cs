using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Network.Editor;

namespace AAEmu.Editor.Core.Packets.L2C
{
    public class ACChallenge2Packet : EditorPacket
    {
        public ACChallenge2Packet() : base(0x04)
        {

        }

        public override PacketStream Write(PacketStream stream)
        {
            stream.Write(5000); // round
            stream.Write("xnDekI2enmWuAvwL"); // salt; length 16?
            stream.Write(new byte[32]); // hc

            return stream;
        }
    }
}
