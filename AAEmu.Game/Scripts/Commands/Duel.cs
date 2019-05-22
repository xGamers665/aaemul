using AAEmu.Game.Core.Managers;
using AAEmu.Game.Models.Game;
using AAEmu.Game.Models.Game.Char;
using AAEmu.Commons.Network;
using AAEmu.Game.Core.Managers.World;
using AAEmu.Game.Core.Network.Game;
using AAEmu.Game.Core.Packets.G2C;
using AAEmu.Game.Models.Game.Skills.Effects;
using AAEmu.Game.Models.Game.Skills.Templates;
using AAEmu.Game.Models.Game.Units.Movements;


namespace AAEmu.Game.Scripts.Commands
{
    public class TestDuel : ICommand
    {
        public void OnLoad()
        {
            CommandManager.Instance.Register("test_duel", this);
        }
        
        public void Execute(Character character, string[] args)
        {
            if (args.Length == 0)
            {
                character.SendMessage("[TestDuel] mods: engaged, cleared, first_hit");
                return;
            }

            switch (args[0])
            {
                case "engaged": // TODO Duel Start
                    if (character.CurrentTarget != null)
                    {
                        character.SendMessage("[TestDuel] start duel...");
                        character.SendPacket(new SCDuelStartedPacket(character.ObjId, character.CurrentTarget.ObjId));
                        character.SendPacket(new SCDuelChallengedPacket(character.ObjId));
                        character.SendPacket(new SCDuelStartCountdownPacket());
                    }
                    else
                        character.SendMessage("[TestDuel] not have target");

                    break;
                case "cleared": // TODO Battle End
                    if (character.CurrentTarget != null)
                    {
                        character.SendPacket(new SCCombatClearedPacket(character.ObjId));
                        character.SendPacket(new SCCombatClearedPacket(character.CurrentTarget.ObjId));
                    }
                    else
                        character.SendMessage("[TestCombat] not have target");

                    break;
                case "first_hit": 
                    if (character.CurrentTarget != null)
                        character.SendPacket(new SCCombatFirstHitPacket(character.ObjId, character.CurrentTarget.ObjId,
                            0));
                    else
                        character.SendMessage("[TestCombat] not have target");
                    break;
                case "text": // TODO Combat Effect
                    character.SendPacket(new SCCombatTextPacket(0, character.ObjId, 0));
                    break;
            }
        }
    }
} 