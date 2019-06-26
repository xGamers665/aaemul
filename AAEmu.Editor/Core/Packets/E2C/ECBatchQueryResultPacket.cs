using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Network.Editor;

namespace AAEmu.Editor.Core.Packets.E2C
{
    public class ECBatchQueryResultPacket : EditorPacket
    {
        private readonly string _botAccount;
        private readonly byte _command;
        private readonly uint _jobId;
        private readonly byte _status;
        private readonly int _queuedCount;

        public ECBatchQueryResultPacket(string botAccount, byte command, uint jobId, byte status, int queuedCount) : base(0x0A)
        {
            _botAccount = botAccount;
            _command = command;
            _jobId = jobId;
            _status = status;
            _queuedCount = queuedCount;
        }

        public override PacketStream Write(PacketStream stream)
        {
            stream.Write(_botAccount);
            stream.Write(_command);
            stream.Write(_jobId);
            stream.Write(_status);
            stream.Write(_queuedCount);

            return stream;
        }
    }
}
