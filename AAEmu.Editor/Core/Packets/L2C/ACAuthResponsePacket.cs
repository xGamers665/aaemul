using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Network.Editor;

namespace AAEmu.Editor.Core.Packets.L2C
{
    public class ACAuthResponsePacket : EditorPacket
    {
        private readonly uint _accountId;
        private readonly byte[] _wsk;
        private readonly byte _slotCount;

        public ACAuthResponsePacket(uint accountId, byte slotCount) : base(0x03)
        {
            _accountId = accountId;
            _wsk = new byte[32];
            _slotCount = slotCount;
        }

        public override PacketStream Write(PacketStream stream)
        {
            stream.Write(_accountId);
            stream.Write(_wsk, true);
            stream.Write(_slotCount);

            return stream;
        }
    }
}
