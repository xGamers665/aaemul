using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Network.Editor;

namespace AAEmu.Editor.Core.Packets.L2C
{
    public class ACWorldQueuePacket: EditorPacket
    {
        public ACWorldQueuePacket() : base(0x09)
        {
        }

        public override PacketStream Write(PacketStream stream)
        {
            stream.Write((byte) 0); // diw -> world id
            stream.Write(false); // isPremium
            stream.Write((ushort) 0); // myTurn
            stream.Write((ushort) 0); // normalLength
            stream.Write((ushort) 0); // premiumLength
            return stream;
        }
    }
}
