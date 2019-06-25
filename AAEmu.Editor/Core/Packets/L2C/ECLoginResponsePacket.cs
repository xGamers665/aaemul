using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Network.Editor;

namespace AAEmu.Editor.Core.Packets.L2C
{
    public class ECEditorResponsePacket : EditorPacket
    {
        private readonly uint _accountId;
        private readonly byte[] _wsk;
        private readonly byte _slotCount;

        public ECEditorResponsePacket(uint accountId, byte slotCount) : base(0x01)
        {
            _accountId = accountId;
            _wsk = new byte[32];
            _slotCount = slotCount;
        }

        public override PacketStream Write(PacketStream stream)
        {
            stream.Write(_accountId); // round
            stream.Write("c:\\aa\\Archeage1.2\\bin32\\Editor.exe");
            //stream.Write(new byte[32]); // hc


            //stream.Write(_accountId);
            //stream.Write(_wsk, true);
            //stream.Write(_slotCount);

            return stream;
        }
    }
}
