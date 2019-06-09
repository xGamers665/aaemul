using System;
using AAEmu.Commons.Utils;
using AAEmu.Game.Core.Managers.World;
using AAEmu.Game.Core.Network.Connections;
using AAEmu.Game.Core.Packets.G2C;
using AAEmu.Game.Models.Game.Char;
using AAEmu.Game.Models.Game.DoodadObj;
using AAEmu.Game.Models.Game.Duels;
using AAEmu.Game.Models.Tasks.Doodads;
using AAEmu.Game.Models.Tasks.Duels;
using AAEmu.Game.Utils;
using NLog;

namespace AAEmu.Game.Core.Managers
{
    public class DuelManager : Singleton<DuelManager>
    {
        protected static readonly Logger _log = LogManager.GetCurrentClassLogger();

        private uint _challengerId;
        private uint _challengedId;
        private uint _challengerObjId;
        private uint _challengedObjId;
        private Character _challenger;
        private DoodadSpawner _combatFlag;
        private const double _delay = 1000; // 1 sec
        private uint _funcGroupId;
        private byte _det = 0;
        private const float DistanceForSurrender = 75;         // square 75 meters
        private const double DuelDurationTime = 5 * 60 *1000; // 5 min

        public bool Initialise()
        {
            _log.Info("DuelManager successfully initialized");

            return true;
        }

        public DuelManager()
        {

        }

        public void StartDuel(GameConnection connection, uint challengerId)
        {
            _challengerId = challengerId;
            _challengedObjId = connection.ActiveChar.ObjId;
            _challengedId = connection.ActiveChar.Id;
            _challenger = WorldManager.Instance.GetCharacterById(_challengerId);
            _challengerObjId = _challenger.ObjId;

            connection.ActiveChar.BroadcastPacket(new SCDuelStartedPacket(_challengerObjId, _challengedObjId), true);
            connection.ActiveChar.BroadcastPacket(new SCAreaChatBubblePacket(true, connection.ActiveChar.ObjId, 543), true);
            connection.ActiveChar.BroadcastPacket(new SCDuelStartCountdownPacket(), true);

            _combatFlag = new DoodadSpawner();
            _combatFlag.Id = 0;
            _combatFlag.UnitId = 5014u; // Combat Flag;

            _combatFlag.Position = connection.ActiveChar.Position.Clone();
            _combatFlag.Position.X = connection.ActiveChar.Position.X - (connection.ActiveChar.Position.X - _challenger.Position.X) / 2;
            _combatFlag.Position.Y = connection.ActiveChar.Position.Y - (connection.ActiveChar.Position.Y - _challenger.Position.Y) / 2;
            _combatFlag.Position.Z = connection.ActiveChar.Position.Z - (connection.ActiveChar.Position.Z - _challenger.Position.Z) / 2;
            _combatFlag.Spawn(0); // set CombatFlag

            connection.ActiveChar.BroadcastPacket(new SCDuelStatePacket(_challengerObjId, _combatFlag.Last.ObjId), true);
            connection.ActiveChar.BroadcastPacket(new SCDuelStatePacket(_challengedObjId, _combatFlag.Last.ObjId), true);

            const uint NextPhase = 12830u;
            const uint SkillId = 0u;

            // make the flag flutter in the wind
            _combatFlag.Last.FuncTask = new DoodadFuncGrowthTask(connection.ActiveChar, _combatFlag.Last, SkillId, NextPhase);
            TaskManager.Instance.Schedule(_combatFlag.Last.FuncTask, TimeSpan.FromMilliseconds(_delay));

            // after 5 minutes the flag is removed
            //_combatFlag.Last.FuncTask = new DoodadFuncFinalTask(connection.ActiveChar, _combatFlag.Last, SkillId, false);
            //TaskManager.Instance.Schedule(_combatFlag.Last.FuncTask, TimeSpan.FromMilliseconds(_delay));

            // Player can be attacked
            connection.SendPacket(new SCCombatEngagedPacket(_challengerObjId));
            connection.ActiveChar.BroadcastPacket(new SCCombatEngagedPacket(_challengedObjId), false);

            var duel = new Duel();
            // final operations after a duel
            duel.FuncTask = new DuelEndTimerTask(duel, connection, _challengerId, _challengedId, _challengerObjId, _challengedObjId, _combatFlag.Last.ObjId, _det);
            TaskManager.Instance.Schedule(duel.FuncTask, TimeSpan.FromMilliseconds(DuelDurationTime));

            duel.FuncTask = new DuelDistanceСheckTask(duel, connection, _challengerId, _challengedId, _challengerObjId, _challengedObjId, _combatFlag.Last.ObjId);
            TaskManager.Instance.Schedule(duel.FuncTask, TimeSpan.FromMilliseconds(_delay), TimeSpan.FromMilliseconds(_delay));
        }

        public void StopDuel(GameConnection connection, uint challengerId, uint challengedId, uint challengerObjId, uint challengedObjId, uint flagObjId, byte det)
        {
            // Duel is over, det 00=lose, 01=win, 02=surrender (Fled beyond the flag action border), 03=draw
            if (det == 3)
            {
                connection.ActiveChar.BroadcastPacket(new SCDuelEndedPacket(challengedId, challengerId, challengedObjId, challengerObjId, det), false);
                connection.SendPacket(new SCDuelEndedPacket(challengerId, challengedId, challengerObjId, challengedObjId, det));
            }
            else
            {
                connection.ActiveChar.BroadcastPacket(new SCDuelEndedPacket(challengerId, challengedId, challengerObjId, challengedObjId, det), true);
            }

            // Duel Status - Duel ended
            connection.ActiveChar.BroadcastPacket(new SCDuelStatePacket(challengerObjId, 0), true);
            connection.ActiveChar.BroadcastPacket(new SCDuelStatePacket(challengedObjId, 0), true);

            //Remove Flag
            connection.ActiveChar.BroadcastPacket(new SCDoodadRemovedPacket(flagObjId, 0), true);

            // Player cannot be attacked
            connection.SendPacket(new SCCombatClearedPacket(_challengerObjId));
            connection.ActiveChar.BroadcastPacket(new SCCombatClearedPacket(_challengedObjId), false);
        }

        public bool DistanceСheckChallenged(GameConnection connection)
        {
            // проверяем, убежали от флага или нет
            var currentDistance = MathUtil.CalculateDistance(_combatFlag.Position, connection.ActiveChar.Position, true);
            if(currentDistance >= DistanceForSurrender)
            {
                return true; // сдаемся, т.е. убежали от флага
            }

            return false;  // мы рядом с флагом
        }
        public bool DistanceСheckChallenger(GameConnection connection)
        {
            // проверяем, убежали от флага или нет
            var currentDistance = MathUtil.CalculateDistance(_combatFlag.Position, _challenger.Position, true);
            if(currentDistance >= DistanceForSurrender)
            {
                return true; // сдаемся, т.е. убежали от флага
            }

            return false;  // мы рядом с флагом
        }
    }
}
