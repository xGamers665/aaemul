using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Network.Editor;

namespace AAEmu.Editor.Core.Packets.E2C
{
    public class ECNoticePacket : EditorPacket
    {
        private readonly byte _noticeType;
        private readonly string _msg;

        public ECNoticePacket(byte noticeType, string msg) : base(0x0F)
        {
            _noticeType = noticeType;
            _msg = msg;
        }

        public override PacketStream Write(PacketStream stream)
        {
            stream.Write(_noticeType);
            stream.Write(_msg);

            return stream;
        }
    }
}
