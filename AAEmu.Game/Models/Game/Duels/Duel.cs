using AAEmu.Game.Core.Managers.World;
using AAEmu.Game.Core.Network.Connections;
using AAEmu.Game.Core.Network.Game;
using AAEmu.Game.Models.Game.Char;
using AAEmu.Game.Models.Game.Units;
using AAEmu.Game.Models.Tasks.Duels;

namespace AAEmu.Game.Models.Game.Duels
{
    public class Duel : BaseUnit
    {

        public GameConnection Connection { get; set; }
        public uint ChallengerId { get; set; }
        public uint ChallengedId { get; set; }
        public uint ChallengerObjId { get; set; }
        public uint ChallengedObjId { get; set; }
        public DuelFuncTask FuncTask { get; set; }

        public Duel()
        {
        }

        public Duel(Duel owner, GameConnection connection, uint challengerId, uint challengedId, uint challengerObjId, uint challengedObjId)
        {
            Connection = connection;
            ChallengerId = challengerId;
            ChallengedId = challengedId;
            ChallengerObjId = challengerObjId;
            ChallengedObjId = challengedObjId;
        }

        public override void BroadcastPacket(GamePacket packet, bool self)
        {
            foreach (var character in WorldManager.Instance.GetAround<Character>(this))
            {
                character.SendPacket(packet);
            }
        }
    }
}
