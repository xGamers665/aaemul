using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Network.Internal;

namespace AAEmu.Editor.Core.Packets.L2G
{
    public class LGPlayerReconnectPacket : InternalPacket
    {
        private readonly uint _token;

        public LGPlayerReconnectPacket(uint token) : base(0x02)
        {
            _token = token;
        }

        public override PacketStream Write(PacketStream stream)
        {
            stream.Write(_token);
            return stream;
        }
    }
}
