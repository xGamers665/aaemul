using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using AAEmu.Commons.Utils;
using AAEmu.Game.Core.Network.Connections;
using AAEmu.Game.Models.Game.LaborPower;
using AAEmu.Game.Models.Tasks.LaborPower;
using NLog;

namespace AAEmu.Game.Core.Managers
{
    public class LaborPowerManager : Singleton<LaborPowerManager>
    {
        protected static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private readonly ConcurrentDictionary<uint, GameConnection> _accounts;

        private List<LaborPower> _onlineChar;
        private List<LaborPower> _offlineChar;


        private const double Delay = 5; // min

        public LaborPowerManager()
        {
            _onlineChar = new List<LaborPower>();
            _offlineChar = new List<LaborPower>();
        }

        public void LaborPowerTickStart(uint id)
        {
            Log.Warn("LaborPowerTickStart: Started");
            var LpTickStartTask = new LaborPowerTickStartTask(id);
            TaskManager.Instance.Schedule(LpTickStartTask, TimeSpan.FromMinutes(Delay));
        }

    }
}
