using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Network.Editor;

namespace AAEmu.Editor.Core.Packets.C2L
{
    public class CACancelEnterWorldPacket : EditorPacket
    {
        public CACancelEnterWorldPacket() : base(0x0c)
        {
        }

        public override void Read(PacketStream stream)
        {
            var wId = stream.ReadByte(); // diw -> world id
        }
    }
}
