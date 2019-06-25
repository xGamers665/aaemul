using AAEmu.Game.Core.Managers;

namespace AAEmu.Game.Models.Tasks.LaborPower
{
    public class LaborPowerTickStartTask : Task
    {
        protected uint _id;
        public LaborPowerTickStartTask(uint id)
        {
            _id = id;
        }

        public override void Execute()
        {
            LaborPowerManager.Instance.LaborPowerTickStart(_id);
        }
    }
}
