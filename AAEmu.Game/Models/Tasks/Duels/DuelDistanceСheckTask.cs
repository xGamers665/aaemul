using AAEmu.Game.Core.Managers;
using AAEmu.Game.Core.Network.Connections;
using AAEmu.Game.Models.Game.Duels;

namespace AAEmu.Game.Models.Tasks.Duels
{
    public class DuelDistanceСheckTask : DuelFuncTask
    {
        protected Duel Owner;
        protected GameConnection Connection;
        protected uint ChallengerId;
        protected uint ChallengedId;
        protected uint ChallengerObjId;
        protected uint ChallengedObjId;
        protected uint FlagObjId;
        protected byte Det;

        public DuelDistanceСheckTask(Duel owner, GameConnection connection, uint challengerId, uint challengedId, uint challengerObjId, uint challengedObjId, uint flagObjId) : base(connection, challengerId, challengedId)
        {
            Owner = owner;
            Connection = connection;
            ChallengerId = challengerId;
            ChallengedId = challengedId;
            ChallengerObjId = challengerObjId;
            ChallengedObjId = challengedObjId;
            FlagObjId = flagObjId;
            Owner = owner;
        }

        public override async void Execute()
        {
            var Challenger = DuelManager.Instance.DistanceСheckChallenger(Connection);
            var Challenged = DuelManager.Instance.DistanceСheckChallenged(Connection);

            if (!Challenger && !Challenged)
            {
                return;
            }

            Det = 2; // surrender
            if (Owner.FuncTask != null)
            {
                await Owner.FuncTask.Cancel();
                Owner.FuncTask = null;
            }
            if (Challenged)
            {
                DuelManager.Instance.StopDuel(Connection, ChallengerId, ChallengedId, ChallengerObjId, ChallengedObjId, FlagObjId, Det);
            }
            if (Challenger)
            {
                DuelManager.Instance.StopDuel(Connection, ChallengedId, ChallengerId, ChallengedObjId, ChallengerObjId, FlagObjId, Det);
            }
        }
    }
}
