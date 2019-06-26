using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Network.Editor;

namespace AAEmu.Editor.Core.Packets.E2C
{
    public class ECBatchRequestResponsePacket : EditorPacket
    {
        private readonly bool _succed;
        private readonly byte _error;
        private readonly string _botAccount;
        private readonly uint _requestId;
        private readonly uint _jobId;

        public ECBatchRequestResponsePacket(bool succed, byte error, string botAccount, uint requestId, uint jobId) : base(0x0C)
        {
            _succed = succed;
            _error = error;
            _botAccount = botAccount;
            _requestId = requestId;
            _jobId = jobId;
        }

        public override PacketStream Write(PacketStream stream)
        {
            stream.Write(_succed);
            stream.Write(_error);
            stream.Write(_botAccount);
            stream.Write(_requestId);
            stream.Write(_jobId);

            return stream;
        }
    }
}
