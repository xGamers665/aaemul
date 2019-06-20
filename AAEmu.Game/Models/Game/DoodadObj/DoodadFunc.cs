using System;
using AAEmu.Game.Core.Managers;
using AAEmu.Game.Core.Managers.UnitManagers;
using AAEmu.Game.Core.Packets.G2C;
using AAEmu.Game.Models.Game.Char;
using AAEmu.Game.Models.Game.Units;
using AAEmu.Game.Utils;

namespace AAEmu.Game.Models.Game.DoodadObj
{
    public class DoodadFunc
    {
        public uint GroupId { get; set; }
        public uint FuncId { get; set; }
        public string FuncType { get; set; }
        public int NextPhase { get; set; }
        public uint SkillId { get; set; }
        public uint PermId { get; set; }
        public int Count { get; set; }

        public async void Use(Unit caster, Doodad owner, uint skillId)
        {
            owner.GrowthTime = DateTime.Now; //DateTime.MinValue;
            var template = DoodadManager.Instance.GetFuncTemplate(FuncId, FuncType);
            if (template == null)
            {
                return;
            }
            template.Use(caster, owner, skillId);

            //if (caster is Character chr)
            //{
            //    chr.Item = ItemManager.Instance.Create(owner.TemplateId, 1, (byte)1);
            //    if (chr.Item != null)
            //        InventoryHelper.AddItemAndUpdateClient(chr, chr.Item);
            //}

            if (NextPhase > 0)
            {
                if (owner.FuncTask != null)
                {
                    await owner.FuncTask.Cancel();
                    owner.FuncTask = null;
                }
                owner.FuncGroupId = (uint)NextPhase;
                var funcs = DoodadManager.Instance.GetPhaseFunc(owner.FuncGroupId);

                owner.BroadcastPacket(new SCDoodadPhaseChangedPacket(owner), false); // TODO добавил для работы вкл/выкл освещения и разрушения бочек/ящиков

                foreach (var func in funcs)
                {
                    func.Use(caster, owner, skillId);
                }
            }
        }
    }
}
