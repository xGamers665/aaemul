using AAEmu.Commons.Network;
using AAEmu.Game.Core.Managers;
using AAEmu.Game.Core.Network.Game;
using AAEmu.Game.Core.Packets.G2C;

namespace AAEmu.Game.Core.Packets.C2G
{
    public class CSChallengeDuelPacket : GamePacket
    {
        public CSChallengeDuelPacket() : base(0x050, 1)
        {
        }

        public override void Read(PacketStream stream)
        {
            var challengedId = stream.ReadUInt32(); // Id кого вызываем на дуэль
            var challengerId = Connection.ActiveChar.Id; // это тот кто вызвал на дуэль

            _log.Warn("CSChallengeDuelPacket: challengerId = {0}, challengedId = {1}", challengerId, challengedId);
            DuelManager.Instance.DuelRequest(Connection.ActiveChar, challengedId); // посылаем свой Id
        }
    }
}
