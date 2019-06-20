using AAEmu.Commons.Utils;
using AAEmu.Game.Core.Managers;
using AAEmu.Game.Core.Packets.G2C;
using AAEmu.Game.Models.Game.Char;
using AAEmu.Game.Models.Game.DoodadObj.Templates;
using AAEmu.Game.Models.Game.Units;
using AAEmu.Game.Utils;

namespace AAEmu.Game.Models.Game.DoodadObj.Funcs
{
    public class DoodadFuncUse : DoodadFuncTemplate
    {
        public uint SkillId { get; set; }

        public override void Use(Unit caster, Doodad owner, uint skillId)
        {
            var character = (Character)caster;
            if (character == null) return;

            var count = 1;
            uint itemId = 4056;

            var item = ItemManager.Instance.Create(itemId, count, 0);
            InventoryHelper.AddItemAndUpdateClient(character, item);
            _log.Debug("DoodadFuncUse");
        }
    }
}
