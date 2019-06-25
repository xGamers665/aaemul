using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Network.Internal;
using AAEmu.Editor.Models;

namespace AAEmu.Editor.Core.Packets.L2G
{
    public class LGRegisterGameServerPacket : InternalPacket
    {
        private readonly GSRegisterResult _result;

        public LGRegisterGameServerPacket(GSRegisterResult result) : base(0x00)
        {
            _result = result;
        }

        public override PacketStream Write(PacketStream stream)
        {
            stream.Write((byte) _result);
            return stream;
        }
    }
}
