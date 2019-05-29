using System;
using System.Collections.Generic;
using System.Text;
using AAEmu.Game.Core.Managers;
using AAEmu.Game.Models.Game.NPChar;
using AAEmu.Game.Models.Game.Skills;
using AAEmu.Game.Models.Game.Units.Route;
using AAEmu.Game.Utils;

namespace AAEmu.Game.Models.Game.Units
{
    internal class Combat : Patrol
    {
        float distance = 1.0f;
        public override void Execute(Npc npc)
        {
            // 先判断距离
            // Judge the distance first
            var move = false;
            var x = npc.Position.X - npc.CurrentTarget.Position.X;
            var y = npc.Position.Y - npc.CurrentTarget.Position.Y;
            var z = npc.Position.Z - npc.CurrentTarget.Position.Z;
            //var MaxXYZ = Math.Max(Math.Max(Math.Abs(x), Math.Abs(y)), Math.Abs(z));

            var MaxXYZ = MathUtil.CalculateDistance(npc.Position, npc.CurrentTarget.Position, true);

            // 如果最大值超过distance 则放弃攻击转而进行追踪
            // If the maximum value exceeds distance, abandon the attack and track it
            if (MaxXYZ > distance)
            {
                var track = new Track();
                track.Pause(npc);
                track.LastPatrol = LastPatrol;
                LastPatrol = track;
                Stop(npc);
            }
            else
            {
                LoopDelay = 3000;

                var skillId = 2u; // TODO

                var skillCasterType = SkillCasterType.Unit; // who applies / кто применяет
                var skillCaster = SkillCaster.GetByType(skillCasterType);
                skillCaster.ObjId = npc.ObjId;

                var skillCastTargetType = 0; // on whom apply / на кого применяют
                var skillCastTarget = SkillCastTarget.GetByType((SkillCastTargetType)skillCastTargetType);
                skillCastTarget.ObjId = npc.CurrentTarget.ObjId;

                var flag = 0;
                var flagType = flag & 15;
                var skillObject = SkillObject.GetByType((SkillObjectType)flagType);
                if (flagType > 0)
                {
                    skillObject.Flag = SkillObjectType.None;
                }

                var skill = new Skill(SkillManager.Instance.GetSkillTemplate(skillId)); // TODO переделать...
                skill.Use(npc, skillCaster, skillCastTarget, skillObject);

                LoopAuto(npc);
            }
        }
    }
}
