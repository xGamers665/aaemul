using System.Collections.Generic;
using AAEmu.Commons.Network;
using AAEmu.Game.Core.Managers.World;
using AAEmu.Game.Core.Network.Game;
using AAEmu.Game.Models.Game.Items;

namespace AAEmu.Game.Core.Packets.C2G
{
    public class CSTakeAllSelectedAttachmentPacket : GamePacket
    {
        public CSTakeAllSelectedAttachmentPacket() : base(0x09f, 1)
        {
        }

        public override void Read(PacketStream stream)
        {
            _log.Debug("CSTakeAllSelectedAttachmentPacket");

            var mailId = stream.ReadInt64();

            _log.Debug("ReadMail, Id: {0}", mailId);
            Connection.ActiveChar.Mails.ReadMail(true, mailId);
        }
    }
}
