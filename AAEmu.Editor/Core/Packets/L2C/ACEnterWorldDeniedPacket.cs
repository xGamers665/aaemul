using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Network.Editor;

namespace AAEmu.Editor.Core.Packets.L2C
{
    public class ACEnterWorldDeniedPacket : EditorPacket
    {
        private readonly byte _reason;
        
        public ACEnterWorldDeniedPacket(byte reason) : base(0x0B)
        {
            _reason = reason;
        }

        public override PacketStream Write(PacketStream stream)
        {
            stream.Write(_reason);

            return stream;
        }
    }
}
