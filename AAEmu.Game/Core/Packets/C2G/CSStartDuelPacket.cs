using AAEmu.Commons.Network;
using AAEmu.Game.Core.Managers;
using AAEmu.Game.Core.Network.Game;
using AAEmu.Game.Models.Game.Error;

namespace AAEmu.Game.Core.Packets.C2G
{
    public class CSStartDuelPacket : GamePacket
    {
        public CSStartDuelPacket() : base(0x051, 1)
        {
        }

        public override void Read(PacketStream stream)
        {
            var challengerId = stream.ReadUInt32();  // ID the one who called us to a duel
            var errorMessage = stream.ReadInt16();  // 0 = "NoErrorMessage" - accepted a duel, 507 = "TargetRejectedDuel" - refused duel

            _log.Warn("CSStartDuelPacket: challengerId = {0}", challengerId);

            if (errorMessage == 0)
            {
                DuelManager.Instance.DuelAccepted(Connection.ActiveChar, challengerId);
            }
            else
            {
                DuelManager.Instance.DuelCancel(challengerId, (ErrorMessageType)errorMessage);
            }
        }
    }
}
