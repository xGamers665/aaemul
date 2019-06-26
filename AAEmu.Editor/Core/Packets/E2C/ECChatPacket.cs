using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Network.Editor;

namespace AAEmu.Editor.Core.Packets.E2C
{
    public class ECChatPacket : EditorPacket
    {
        private readonly string _account;
        private readonly string _msg;

        public ECChatPacket(string account, string msg) : base(0x08)
        {
            _account = account;
            _msg = msg;
        }

        public override PacketStream Write(PacketStream stream)
        {
            stream.Write(_account);
            stream.Write(_msg);

            return stream;
        }
    }
}
