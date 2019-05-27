using AAEmu.Commons.Network;
using AAEmu.Game.Core.Network.Game;

namespace AAEmu.Game.Core.Packets.G2C
{
    public class SCDuelEndedPacket : GamePacket
    {
        private readonly uint _challengerId;
        private readonly uint _challengedId;
        private readonly uint _challengerObjId;
        private readonly uint _challengedObjId;
        private readonly byte _det;

        public SCDuelEndedPacket(uint challengerId, uint challengedId, uint challengerObjId, uint challengedObjId, byte det)
            : base(SCOffsets.SCDuelEndedPacket, 1)
        {
            _challengerId = challengerId;
            _challengedId = challengedId;
            _challengerObjId = challengerObjId;
            _challengedObjId = challengedObjId;
            _det = det;
        }

        public override PacketStream Write(PacketStream stream)
        {
            stream.Write(_challengerId);         // challengerId
            stream.Write(_challengedId);        // challengedId
            stream.WriteBc(_challengerObjId);  // challengerObjId
            stream.WriteBc(_challengedObjId); // challengedObjId
            stream.Write((byte)_det);        // det 00=lose, 01=win, 02=surrender

            return stream;
        }
    }
}
