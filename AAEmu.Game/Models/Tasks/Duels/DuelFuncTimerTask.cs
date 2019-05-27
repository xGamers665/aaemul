﻿using AAEmu.Game.Core.Managers;
using AAEmu.Game.Core.Network.Connections;
using AAEmu.Game.Models.Game.Duels;

namespace AAEmu.Game.Models.Tasks.Duels
{
    public class DuelFuncTimerTask : DuelFuncTask
    {
        protected Duel Owner;
        protected GameConnection Connection;
        protected uint ChallengerId;
        protected uint ChallengedId;
        protected uint ChallengerObjId;
        protected uint ChallengedObjId;
        protected uint FlagObjId;
        protected byte Det;

        public DuelFuncTimerTask(Duel owner, GameConnection connection, uint challengerId, uint challengedId, uint challengerObjId, uint challengedObjId, uint flagObjId, byte det) : base(connection, challengerId, challengedId)
        {
            Owner = owner;
            Connection = connection;
            ChallengerId = challengerId;
            ChallengedId = challengedId;
            ChallengerObjId = challengerObjId;
            ChallengedObjId = challengedObjId;
            FlagObjId = flagObjId;
            Det = det = 1;
        }

        public override void Execute()
        {
            Owner.FuncTask = null;
            DuelManager.Instance.StopDuel(Connection, ChallengerId, ChallengedId, ChallengerObjId, ChallengedObjId, FlagObjId, Det);
        }
    }
}
