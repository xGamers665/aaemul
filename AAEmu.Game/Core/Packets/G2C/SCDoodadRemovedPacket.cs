using AAEmu.Commons.Network;
using AAEmu.Game.Core.Network.Game;

namespace AAEmu.Game.Core.Packets.G2C
{
    public class SCDoodadRemovedPacket : GamePacket
    {
        private readonly uint _id;
        private readonly byte _es;

        public SCDoodadRemovedPacket(uint id, byte es) : base(SCOffsets.SCDoodadRemovedPacket, 1)
        {
            _id = id;
            _es = es;
        }

        public override PacketStream Write(PacketStream stream)
        {
            stream.WriteBc(_id);
            stream.Write(_es); // e
            return stream;
        }
    }
}
