using System;
using AAEmu.Game.Core.Managers.World;
using AAEmu.Game.Core.Network.Connections;
using AAEmu.Game.Core.Network.Game;
using AAEmu.Game.Core.Packets.G2C;
using AAEmu.Game.Models.Game.Char;
using AAEmu.Game.Models.Game.DoodadObj;
using AAEmu.Game.Models.Game.Units;
using AAEmu.Game.Models.Tasks.Duels;

namespace AAEmu.Game.Models.Game.Duels
{
    public enum DuelDetType : byte
    {
        // det 00=lose, 01=win, 02=surrender (Fled beyond the flag action border), 03=draw
        Lose = 0,
        Win = 1,
        Surrender = 2,
        Draw = 3
    }
    public enum DuelDistance : sbyte
    {
        Error = -1,
        Near = 0,
        ChallengerFar = 1,
        ChallengedFar = 2,
    }

    public class Duel
    {
        public Character Challenger { get; set; }
        public Character Challenged { get; set; }
        public Doodad DuelFlag { get; set; }
        public DuelStartTask DuelStartTask { get; set; }
        public DuelEndTimerTask DuelEndTimerTask { get; set; }
        public DuelDistanceСheckTask DuelDistanceСheckTask { get; set; }
        public bool DuelStarted { get; set; } = false;
        public bool DuelAllowed { get; set; } = false;

        public Duel(Character challenger, Character challenged)
        {
            Challenger = challenger; // это персонаж который вызвал нас на дуэль
            Challenged = challenged; // это наш персонаж (т.е. connection.ActiveChar)
        }

        public void SendPacketsBoth(GamePacket packet)
        {
            // нужен когда пакеты одинаковы у обоих персонажей
            Challenger.SendPacket(packet); // по типу Broadcast только тем, кто в дуэли
            Challenged.SendPacket(packet);
        }
        public void SendPacketChallenger(GamePacket packet)
        {
            Challenger.SendPacket(packet); // только вызвавшему дуэль
        }
        public void SendPacketChallenged(GamePacket packet)
        {
            Challenged.SendPacket(packet); // только вызываемому на дуэль
        }
    }
}
