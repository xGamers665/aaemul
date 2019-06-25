using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Network.Editor;

namespace AAEmu.Editor.Core.Packets.L2C
{
    public class ACChallengePacket : EditorPacket
    {
        public ACChallengePacket() : base(0x02)
        {
        }

        public override PacketStream Write(PacketStream stream)
        {
            stream.Write((uint) 0); // salt
            for (var i = 0; i < 4; i++)
                stream.Write((uint) 0); // hc

            return stream;
        }
    }
}
