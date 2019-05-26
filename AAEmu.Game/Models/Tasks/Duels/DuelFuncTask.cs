using AAEmu.Game.Models.Game.DoodadObj;
using AAEmu.Game.Models.Game.Units;

namespace AAEmu.Game.Models.Tasks.Duels
{
    public abstract class DuelFuncTask : Task
    {
        protected Unit _caster;
        protected Doodad _owner;
        protected uint _skillId;

        protected DuelFuncTask(Unit caster, Doodad owner, uint skillId)
        {
            _caster = caster;
            _owner = owner;
            _skillId = skillId;
        }
    }
}
