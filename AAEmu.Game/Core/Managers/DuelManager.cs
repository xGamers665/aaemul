using System;
using AAEmu.Commons.Utils;
using AAEmu.Game.Core.Managers.World;
using AAEmu.Game.Core.Network.Connections;
using AAEmu.Game.Core.Packets.G2C;
using AAEmu.Game.Models.Game.Char;
using AAEmu.Game.Models.Game.DoodadObj;
using AAEmu.Game.Models.Tasks.Doodads;
using AAEmu.Game.Models.Tasks.Duels;
using NLog;

namespace AAEmu.Game.Core.Managers
{
    public class DuelManager : Singleton<DuelManager>
    {
        protected static readonly Logger _log = LogManager.GetCurrentClassLogger();

        private uint _challengerObjId;
        private uint _challengedObjId;
        private Character _challenger;
        private DoodadSpawner _combatFlag;
        private int _delay;
        private uint _funcGroupId;

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
            _challengedObjId = connection.ActiveChar.ObjId;
            _challenger = WorldManager.Instance.GetCharacterById(challengerId);
            _challengerObjId = _challenger.ObjId;

            connection.ActiveChar.BroadcastPacket(new SCDuelStartedPacket(_challengerObjId, _challengedObjId), true);
            connection.ActiveChar.BroadcastPacket(new SCAreaChatBubblePacket(true, connection.ActiveChar.ObjId, 543), true);
            connection.ActiveChar.BroadcastPacket(new SCDuelStartCountdownPacket(), true);

            _combatFlag = new DoodadSpawner();
            _combatFlag.Id = 0;
            _combatFlag.UnitId = 5014u; // Combat Flag;
            //_combatFlag.Last.GrowthTime = DateTime.Now.AddMilliseconds(2850); // 5 min
            //_combatFlag.Last.FuncGroupId = 12829;
            //_combatFlag.Last.Despawn = DateTime.Now.AddMilliseconds(120000); // 5 min
            _combatFlag.Position = connection.ActiveChar.Position.Clone();
            _combatFlag.Position.X = connection.ActiveChar.Position.X - (connection.ActiveChar.Position.X - _challenger.Position.X) / 2;
            _combatFlag.Position.Y = connection.ActiveChar.Position.Y - (connection.ActiveChar.Position.Y - _challenger.Position.Y) / 2;
            _combatFlag.Position.Z = connection.ActiveChar.Position.Z - (connection.ActiveChar.Position.Z - _challenger.Position.Z) / 2;
            _combatFlag.Spawn(0); // set CombatFlag

            connection.ActiveChar.BroadcastPacket(new SCDuelStatePacket(_challengerObjId, _combatFlag.Last.ObjId), true);
            connection.ActiveChar.BroadcastPacket(new SCDuelStatePacket(_challengedObjId, _combatFlag.Last.ObjId), true);

            const uint NextPhase = 12830u;  // TODO
            const uint SkillId = 12850u;    // TODO создать смерч
            _combatFlag.Last.GrowthTime = DateTime.Now.AddMilliseconds(60000); // 5 min
            //_combatFlag.Last.FuncGroupId = 12830;
            //_combatFlag.Last.Despawn = DateTime.Now.AddMilliseconds(_delay);
            //_combatFlag.Last.BroadcastPacket(new SCDoodadPhaseChangedPacket(_combatFlag.Last), false);

            _combatFlag.Last.FuncTask = new DoodadFuncTimerTask(connection.ActiveChar, _combatFlag.Last, SkillId, NextPhase);
            TaskManager.Instance.Schedule(_combatFlag.Last.FuncTask, TimeSpan.FromMilliseconds(_delay));


            connection.SendPacket(new SCCombatEngagedPacket(_challengerObjId));
            connection.ActiveChar.BroadcastPacket(new SCCombatEngagedPacket(_challengedObjId), false);
        }

        public void StopDuel()
        {
        }

        public bool DistanceСheck()
        {
            return true;
        }
    }
}
