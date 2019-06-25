using System;
using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Network.Editor;
using AAEmu.Editor.Core.Packets.L2C;

namespace AAEmu.Editor.Core.Packets.C2L
{
    public class CAChallengeResponsePacket : EditorPacket
    {
        public CAChallengeResponsePacket() : base(0x060)
        {
        }

        public override void Read(PacketStream stream)
        {
            for (var i = 0; i < 4; i++)
                stream.ReadUInt32(); // responses
            var password = stream.ReadBytes(); // TODO or bytes? length 32
            var bytes = Convert.FromBase64String("jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=");
            Connection.SendPacket(new ACEditorDeniedPacket(3));
        }
    }
}
