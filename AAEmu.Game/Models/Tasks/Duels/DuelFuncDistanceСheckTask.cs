using AAEmu.Game.Core.Managers;
using AAEmu.Game.Core.Network.Connections;
using AAEmu.Game.Models.Game.Duels;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AAEmu.Game.Models.Tasks.Duels
{
    public class DuelFuncDistanceСheckTask : DuelFuncTask
    {
        protected Duel Owner;
        protected GameConnection Connection;
        protected uint ChallengerId;
        protected uint ChallengedId;
        protected uint ChallengerObjId;
        protected uint ChallengedObjId;
        protected uint FlagObjId;
        protected byte Det;

        public DuelFuncDistanceСheckTask(Duel owner, GameConnection connection, uint challengerId, uint challengedId, uint challengerObjId, uint challengedObjId, uint flagObjId) : base(connection, challengerId, challengedId)
        {
            Owner = owner;
            Connection = connection;
            ChallengerId = challengerId;
            ChallengedId = challengedId;
            ChallengerObjId = challengerObjId;
            ChallengedObjId = challengedObjId;
            FlagObjId = flagObjId;
        }

        public override void Execute()
        {
            var res = DuelManager.Instance.DistanceСheck(Connection, ChallengerId, ChallengedId, ChallengerObjId, ChallengedObjId);

            if (res == 0)
            {
                return;
            }

            if (res == 1)
            {
                Det = 1; // win
            }
            else if (res == -1)
            {
                Det = 2; // surrender
            }
            DuelManager.Instance.StopDuel(Connection, ChallengerId, ChallengedId, ChallengerObjId, ChallengedObjId, FlagObjId, Det);
            Owner.FuncTask = null;
        }
    }
}
