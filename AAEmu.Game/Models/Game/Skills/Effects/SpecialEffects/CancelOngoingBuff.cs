using System;
using AAEmu.Game.Models.Game.Char;
using AAEmu.Game.Models.Game.Units;
using NLog;

namespace AAEmu.Game.Models.Game.Skills.Effects.SpecialEffects
{
    public class CancelOngoingBuff : ISpecialEffect
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();
        public void Execute(Unit caster,
            SkillCaster casterObj,
            BaseUnit target,
            SkillCastTarget targetObj,
            CastAction castObj,
            Skill skill,
            SkillObject skillObject,
            DateTime time,
            int value1,
            int value2,
            int value3,
            int value4)
        {
            // TODO caster.Effects.RemoveOngoingBuff();
            _log.Warn("Special effects: CancelOngoingBuff");
            caster.Effects.RemoveOngoingBuff();
        }
    }
}
