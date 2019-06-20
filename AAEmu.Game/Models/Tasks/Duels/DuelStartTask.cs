using AAEmu.Game.Core.Managers;
using AAEmu.Game.Core.Network.Connections;
using AAEmu.Game.Models.Game.Duels;

namespace AAEmu.Game.Models.Tasks.Duels
{
    public class DuelStartTask : Task
    {
        protected uint _challengerId;
        public DuelStartTask(uint challengerId)
        {
            _challengerId = challengerId;
        }

        public override void Execute()
        {
            DuelManager.Instance.DuelStart(_challengerId);
        }
    }
}
