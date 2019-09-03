using System;
using AAEmu.Game.Core.Packets.G2C;
using AAEmu.Game.Models.Game.Char;
using AAEmu.Game.Models.Game.DoodadObj.Funcs;
using AAEmu.Game.Models.Game.NPChar;
using AAEmu.Game.Models.Game.Units;

namespace AAEmu.Game.Models.Tasks
{
    public class UnitPointsRegenTask : Task
    {
        private Unit _unit;

        public UnitPointsRegenTask(Unit unit)
        {
            _unit = unit;
        }

        public override void Execute()
        {
            switch (_unit)
            {
                case Character chr:
                    {
                        if (chr.Hp < _unit.MaxHp && chr.Hp > 0)
                        {
                            if (chr.IsInBattle)
                                chr.Hp += chr.PersistentHpRegen;
                            else
                                chr.Hp += chr.HpRegen;
                        }
                        if (chr.Mp < chr.MaxMp && chr.Hp >= 0)
                        {
                            if (chr.IsInBattle)
                                chr.Hp += chr.PersistentMpRegen;
                            else
                                chr.Mp += chr.MpRegen;
                        }
                        _unit = chr;
                        break;
                    }

                case Npc npc:
                    {
                        if (npc.Hp < _unit.MaxHp && npc.Hp > 0)
                        {
                            if (npc.IsInBattle)
                                npc.Hp += npc.PersistentHpRegen;
                            else
                                npc.Hp += npc.HpRegen;
                        }
                        if (npc.Mp < npc.MaxMp && npc.Hp >= 0)
                        {
                            if (npc.IsInBattle)
                                npc.Hp += npc.PersistentMpRegen;
                            else
                                npc.Mp += npc.MpRegen;
                        }
                        _unit = npc;
                        break;
                    }

                case Mount mnt:
                    {
                        if (mnt.Hp < _unit.MaxHp && mnt.Hp > 0)
                        {
                            if (mnt.IsInBattle)
                                mnt.Hp += mnt.PersistentHpRegen;
                            else
                                mnt.Hp += mnt.HpRegen;
                        }
                        if (mnt.Mp < mnt.MaxMp && mnt.Hp >= 0)
                        {
                            if (mnt.IsInBattle)
                                mnt.Hp += mnt.PersistentMpRegen;
                            else
                                mnt.Mp += mnt.MpRegen;
                        }
                        _unit = mnt;
                        break;
                    }

                default:
                    {
                        if (_unit.Hp < _unit.MaxHp && _unit.Hp > 0)
                        {
                            _unit.Hp += _unit.HpRegen; // TODO at battle _unit.PersistentHpRegen
                        }

                        if (_unit.Mp < _unit.MaxMp && _unit.Hp >= 0)
                        {
                            _unit.Mp += _unit.MpRegen; // TODO at battle _unit.PersistentMpRegen
                        }
                        break;
                    }
            }
            _unit.Hp = Math.Min(_unit.Hp, _unit.MaxHp);
            _unit.Mp = Math.Min(_unit.Mp, _unit.MaxMp);

            _unit.BroadcastPacket(new SCUnitPointsPacket(_unit.ObjId, _unit.Hp, _unit.Mp), true);

            if (_unit.Hp >= _unit.MaxHp && _unit.Mp >= _unit.MaxMp)
            {
                _unit.StopRegen();
            }
        }
    }
}
