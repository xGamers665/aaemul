using AAEmu.Commons.Network;
using AAEmu.Game.Core.Managers.World;
using AAEmu.Game.Core.Network.Game;
using AAEmu.Game.Core.Packets.G2C;
using AAEmu.Game.Models.Game.DoodadObj;

namespace AAEmu.Game.Core.Packets.C2G
{
    public class CSChallengeDuelPacket : GamePacket
    {
        public CSChallengeDuelPacket() : base(0x050, 1)
        {
        }

        public override void Read(PacketStream stream)
        {
            var challengedId = stream.ReadUInt32(); // Id того, кого мы вызвали на дуэль
            Connection.ActiveChar.BroadcastPacket(new SCDuelChallengedPacket(challengedId), true);
            //Connection.SendPacket(new SCDuelChallengedPacket(challengedId)); //только себе?

            //Connection.SendPacket(new SCAreaChatBubblePacket(true, Connection.ActiveChar.ObjId, 543));
            Connection.ActiveChar.BroadcastPacket(new SCAreaChatBubblePacket(true, Connection.ActiveChar.ObjId, 543), true);

            //Connection.SendPacket(new SCDuelStartCountdownPacket());
            Connection.ActiveChar.BroadcastPacket(new SCDuelStartCountdownPacket(), true);

            var doodadFlag = new DoodadSpawner();
            const uint unitId = 5014u; // Combat Flag
            doodadFlag.Id = 0;
            doodadFlag.UnitId = unitId;
            doodadFlag.Position = Connection.ActiveChar.Position.Clone();
            doodadFlag.Position.Y += 3;
            doodadFlag.Spawn(0);

            var challengerObjId = Connection.ActiveChar.ObjId;
            var challenged = WorldManager.Instance.GetCharacterById(challengedId);
            var challengedObjId = challenged.ObjId;


            Connection.ActiveChar.BroadcastPacket(new SCDuelStatePacket(challengerObjId, doodadFlag.Last.ObjId), true);
            //Connection.SendPacket(new SCDuelStatePacket(challengerObjId, doodadFlag.Last.ObjId));

            Connection.SendPacket(new SCDoodadPhaseChangedPacket(doodadFlag.Last));

            Connection.SendPacket(new SCCombatEngagedPacket(challengerObjId));



            _log.Warn("ChallengeDuel, challengedId: {0}", challengedId);
        }
    }
}
