using AAEmu.Commons.Network;
using AAEmu.Commons.Utils;
using AAEmu.Game.Core.Network.Game;
using AAEmu.Game.Models.Game.Char.Templates;
using AAEmu.Game.Models.Game.Units;
using AAEmu.Game.Utils;

namespace AAEmu.Game.Core.Packets.G2C
{
    public class SCPlotEndedPacket : GamePacket
    {
        private readonly ushort _tl;

        public SCPlotEndedPacket(ushort tl) : base(SCOffsets.SCPlotEventPacket, 1)
        {
            _tl = tl;
        }

        public override PacketStream Write(PacketStream stream)
        {
            stream.Write(_tl);

            return stream;
        }
    }
}
