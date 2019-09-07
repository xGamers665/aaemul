using System;
using AAEmu.Game.Core.Managers;
using AAEmu.Game.Models.Game.Units;
using AAEmu.Game.Models.Tasks.Skills;
using NLog;

namespace AAEmu.Game.Models.Game.Skills.Effects.SpecialEffects
{
    public class Combo : ISpecialEffect
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
            int comboSkillId,
            int timeFromNow,
            int value3,
            int value4)
        {
            _log.Warn("Special effects: Combo");
            //TODO: this should not auto cast the skill, just make it so that the skill on the hotbar changes to the next skill temporarily (for timefromnow amount of time)
        }
    }
}
