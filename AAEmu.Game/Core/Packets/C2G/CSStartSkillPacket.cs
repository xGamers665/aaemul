using AAEmu.Commons.Network;
using AAEmu.Game.Core.Managers;
using AAEmu.Game.Core.Network.Game;
using AAEmu.Game.Models.Game.Error;
using AAEmu.Game.Models.Game.Skills;
using AAEmu.Game.Utils;

namespace AAEmu.Game.Core.Packets.C2G
{
    public class CSStartSkillPacket : GamePacket
    {
        public CSStartSkillPacket() : base(0x052, 1)
        {
        }

        public override void Read(PacketStream stream)
        {
            Skill skill;
            var skillId = stream.ReadUInt32();

            var skillCasterType = stream.ReadByte(); // who applies
            var skillCaster = SkillCaster.GetByType((SkillCasterType)skillCasterType);
            skillCaster.Read(stream);

            var skillCastTargetType = stream.ReadByte(); // on whom apply
            var skillCastTarget = SkillCastTarget.GetByType((SkillCastTargetType)skillCastTargetType);
            skillCastTarget.Read(stream);

            var flag = stream.ReadByte();
            var flagType = flag & 15;
            var skillObject = SkillObject.GetByType((SkillObjectType)flagType);
            if (flagType > 0) skillObject.Read(stream);

            _log.Debug("StartSkill: Id {0}, flag {1}", skillId, flag);

            if (skillCaster is SkillItem)
            {
                var item = Connection.ActiveChar.Inventory.GetItem(((SkillItem)skillCaster).ItemId);
                if (item == null || skillId != item.Template.UseSkillId)
                {
                    Connection.ActiveChar.SendErrorMessage(ErrorMessageType.FailedToUseItem);
                    return;
                }
                Connection.ActiveChar.Quests.OnItemUse(item);
                Connection.ActiveChar.Item = item; // Item который используется персонажем в каких либо действиях
                skill = new Skill(SkillManager.Instance.GetSkillTemplate(skillId));

                //InventoryHelper.AddItemAndUpdateClient(Connection.ActiveChar, item);
                if (Connection.ActiveChar.Item != null)
                    InventoryHelper.RemoveItemAndUpdateClient(Connection.ActiveChar, Connection.ActiveChar.Item, 1);
            }
            else if (SkillManager.Instance.IsDefaultSkill(skillId) || SkillManager.Instance.IsCommonSkill(skillId))
            {
                skill = new Skill(SkillManager.Instance.GetSkillTemplate(skillId)); // TODO переделать...
                Connection.ActiveChar.IsInBattle = true;
            }
            else if (Connection.ActiveChar.Skills.Skills.ContainsKey(skillId))
            {
                skill = Connection.ActiveChar.Skills.Skills[skillId];
            }
            else
            {
                _log.Warn("StartSkill: Id {0}, undefined use type", skillId);
                Connection.ActiveChar.SendErrorMessage(ErrorMessageType.UnknownSkill);
                return;
            }

            skill.Use(Connection.ActiveChar, skillCaster, skillCastTarget, skillObject);

            //if (SkillManager.Instance.IsDefaultSkill(skillId) || SkillManager.Instance.IsCommonSkill(skillId))
            //{
            //    var skill = new Skill(SkillManager.Instance.GetSkillTemplate(skillId)); // TODO переделать...
            //    Connection.ActiveChar.IsInBattle = true;
            //    skill.Use(Connection.ActiveChar, skillCaster, skillCastTarget, skillObject);

            //    var item = Connection.ActiveChar.Inventory.GetItem(((SkillItem)skillCaster).ItemId); // TODO пробуем удалить вещь после использования
            //    if(item != null)
            //    {
            //        InventoryHelper.RemoveItemAndUpdateClient(Connection.ActiveChar, item, 1);
            //    }
            //}
            //else if (skillCaster is SkillItem) // перенести пораньше эту проверку
            //{
            //    var item = Connection.ActiveChar.Inventory.GetItem(((SkillItem)skillCaster).ItemId);
            //    if (item == null || skillId != item.Template.UseSkillId)
            //    {
            //        return;
            //    }
            //    Connection.ActiveChar.Quests.OnItemUse(item);
            //    var skill = new Skill(SkillManager.Instance.GetSkillTemplate(skillId));
            //    skill.Use(Connection.ActiveChar, skillCaster, skillCastTarget, skillObject);
            //}
            //else if (Connection.ActiveChar.Skills.Skills.ContainsKey(skillId))
            //{
            //    var skill = Connection.ActiveChar.Skills.Skills[skillId];
            //    skill.Use(Connection.ActiveChar, skillCaster, skillCastTarget, skillObject);
            //}
            //else
            //{
            //    _log.Warn("StartSkill: Id {0}, undefined use type", skillId);
            //}
        }
    }
}
