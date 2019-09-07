using System;
using System.Collections.Generic;
using AAEmu.Game.Core.Managers;
using AAEmu.Game.Core.Managers.Id;
using AAEmu.Game.Core.Network.Connections;
using AAEmu.Game.Core.Network.Game;
using AAEmu.Game.Core.Packets.G2C;
using AAEmu.Game.Models.Game.Char;
using AAEmu.Game.Models.Game.Error;
using AAEmu.Game.Models.Game.Expeditions;
using AAEmu.Game.Models.Game.NPChar;
using AAEmu.Game.Models.Game.Skills;
using AAEmu.Game.Models.Tasks;
using AAEmu.Game.Models.Tasks.Skills;

namespace AAEmu.Game.Models.Game.Units
{
    public class Unit : BaseUnit
    {
        //protected static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private Task _regenTask;
        public uint ModelId { get; set; }
        public byte Level { get; set; }
        public int Hp { get; set; }
        public virtual int MaxHp { get; set; }
        public virtual int HpRegen { get; set; }
        public virtual int PersistentHpRegen { get; set; }
        public int Mp { get; set; }
        public virtual int MaxMp { get; set; }
        public virtual int MpRegen { get; set; }
        public virtual int PersistentMpRegen { get; set; }
        public virtual float LevelDps { get; set; }
        public virtual int Dps { get; set; }
        public virtual int DpsInc { get; set; }
        public virtual int OffhandDps { get; set; }
        public virtual int RangedDps { get; set; }
        public virtual int RangedDpsInc { get; set; }
        public virtual int MDps { get; set; }
        public virtual int MDpsInc { get; set; }
        public virtual int Armor { get; set; }
        public virtual int MagicResistance { get; set; }
        public BaseUnit CurrentTarget { get; set; }
        public virtual byte RaceGender => 0;
        public virtual UnitCustomModelParams ModelParams { get; set; }
        public byte ActiveWeapon { get; set; }
        public bool IdleStatus { get; set; }
        public bool ForceAttack { get; set; }
        public bool Invisible { get; set; }
        public uint OwnerId { get; set; }
        public SkillTask SkillTask { get; set; }
        public SkillTask AutoAttackTask { get; set; }
        public Dictionary<uint, List<Bonus>> Bonuses { get; set; }
        public Expedition Expedition { get; set; }
        public bool IsInBattle { get; set; }
        public int SummarizeDamage { get; set; }
        public bool IsAutoAttack = false;
        public uint SkillId;
        public ushort TlId { get; set; }
        public GameConnection Connection { get; set; }
        public Dictionary<uint, DateTime> Cooldowns { get; set; }


        /// <summary>
        /// Unit巡逻
        /// Unit patrol
        /// 指明Unit巡逻路线及速度、是否正在执行巡逻等行为
        /// Indicates the route and speed of the Unit patrol, whether it is performing patrols, etc.
        /// </summary>
        public Patrol Patrol { get; set; }

        public Unit()
        {
            Bonuses = new Dictionary<uint, List<Bonus>>();
            Cooldowns = new Dictionary<uint, DateTime>();
            IsInBattle = false;
        }

        public virtual void ReduceCurrentHp(Unit attacker, int value)
        {

            if (attacker.CurrentTarget is Npc npc)
            {
                if (npc.Hp <= 0) // не дасть выполнить Dodie
                    return;

                npc.Hp = Math.Max(npc.Hp - value, 0);
                if (npc.Hp <= 0)
                {
                    StopRegen();
                    DoDie(npc);
                    return;
                }

                StartRegen();
                BroadcastPacket(new SCUnitPointsPacket(npc.ObjId, npc.Hp, npc.Hp > 0 ? npc.Mp : 0), true);
            }
            else if (attacker.CurrentTarget is Character character)
            {
                if (character.Hp <= 0) // не дасть выполнить Dodie
                    return;

                character.Hp = Math.Max(character.Hp - value, 0);
                if (character.Hp <= 0)
                {
                    StopRegen();
                    DoDie(character);
                    return;
                }

                StartRegen();
                BroadcastPacket(new SCUnitPointsPacket(character.ObjId, character.Hp, character.Hp > 0 ? character.Mp : 0), true);
            }

        }
        public virtual void ReduceCurrentMp(Unit attacker, int value)
        {
            //if (Hp <= 0) // если юнит мертв, то не надо менять MP
            //    return;

            attacker.Mp = Math.Max(attacker.Mp - value, 0);
            if (attacker.Mp >= attacker.MaxMp)
            {
                //StopRegen(); // нельзя останавливать реген, в этот момент может быть 0 < HP < MaxHp
                return;
            }
            StartRegen();
            BroadcastPacket(new SCUnitPointsPacket(attacker.ObjId, attacker.Hp, attacker.Hp > 0 ? attacker.Mp : 0), true);
        }

        public virtual void DoDie(Unit killer)
        {
            if (killer.CurrentTarget == null)
            {
                return;
            }
            Effects.RemoveEffectsOnDeath();
            killer.BroadcastPacket(new SCUnitDeathPacket(killer.ObjId, 1, killer), true);

            var lootDropItems = ItemManager.Instance.CreateLootDropItems(killer.ObjId);
            if (lootDropItems.Count > 0)
                killer.BroadcastPacket(new SCLootableStatePacket(killer.ObjId, true), true);

            if (killer.CurrentTarget != null)
            {
                killer.BroadcastPacket(new SCAiAggroPacket(killer.ObjId, 0), true);
                killer.SummarizeDamage = 0;

                killer.BroadcastPacket(new SCCombatClearedPacket(killer.CurrentTarget.ObjId), true);
                killer.BroadcastPacket(new SCCombatClearedPacket(killer.ObjId), true);
                killer.StartRegen();
                killer.BroadcastPacket(new SCTargetChangedPacket(killer.ObjId, 0), true);

                if (killer is Character character)
                {
                    character.StopAutoSkill(character);
                    character.IsInBattle = false; // нам надо, чтобы у персонажа стало "не в бою"
                }
                else if (killer.CurrentTarget is Character character2)
                {
                    character2.StopAutoSkill(character2);
                    character2.IsInBattle = false; // нам надо, чтобы у персонажа стало "не в бою"
                }

                killer.CurrentTarget = null;
            }
        }

        private async void StopAutoSkill(Unit character)
        {
            if (!(character is Character) || character.AutoAttackTask == null)
                return;

            await character.AutoAttackTask.Cancel();
            character.AutoAttackTask = null;
            character.IsAutoAttack = false; // turned off auto attack
            character.BroadcastPacket(new SCSkillEndedPacket(character.TlId), true);
            character.BroadcastPacket(new SCSkillStoppedPacket(character.ObjId, character.SkillId), true);
            TlIdManager.Instance.ReleaseId(character.TlId);
        }

        public void StartRegen()
        {
            if (_regenTask != null || Hp >= MaxHp && Mp >= MaxMp || Hp <= 0)
                return;

            _regenTask = new UnitPointsRegenTask(this);
            TaskManager.Instance.Schedule(_regenTask, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
        }

        public async void StopRegen()
        {
            if (_regenTask == null)
                return;

            await _regenTask.Cancel();
            _regenTask = null;
        }

        public void SetInvisible(bool value)
        {
            Invisible = value;
            BroadcastPacket(new SCUnitInvisiblePacket(ObjId, Invisible), true);
        }

        public void SetForceAttack(bool value)
        {
            ForceAttack = value;
            BroadcastPacket(new SCForceAttackSetPacket(ObjId, ForceAttack), true);
        }

        public override void AddBonus(uint bonusIndex, Bonus bonus)
        {
            var bonuses = Bonuses.ContainsKey(bonusIndex) ? Bonuses[bonusIndex] : new List<Bonus>();
            bonuses.Add(bonus);
            Bonuses[bonusIndex] = bonuses;
        }

        public override void RemoveBonus(uint bonusIndex, UnitAttribute attribute)
        {
            if (!Bonuses.ContainsKey(bonusIndex))
                return;

            var bonuses = Bonuses[bonusIndex];
            foreach (var bonus in new List<Bonus>(bonuses))
            {
                if (bonus.Template != null && bonus.Template.Attribute == attribute)
                    bonuses.Remove(bonus);
            }
        }

        public List<Bonus> GetBonuses(UnitAttribute attribute)
        {
            var result = new List<Bonus>();
            if (Bonuses == null)
                return result;

            foreach (var bonuses in new List<List<Bonus>>(Bonuses.Values))
            {
                foreach (var bonus in new List<Bonus>(bonuses))
                {
                    if (bonus.Template != null && bonus.Template.Attribute == attribute)
                        result.Add(bonus);
                }
            }
            return result;
        }

        public void SendPacket(GamePacket packet)
        {
            Connection?.SendPacket(packet);
        }

        public void SendErrorMessage(ErrorMessageType type)
        {
            SendPacket(new SCErrorMsgPacket(type, 0, true));
        }
    }
}
