using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Network.Editor;
using AAEmu.Editor.Core.Packets.L2C;

namespace AAEmu.Editor.Core.Packets.C2L
{
    public class CAChallengeResponse2Packet : EditorPacket
    {
        public CAChallengeResponse2Packet() : base(0x06)
        {
        }

        public override void Read(PacketStream stream)
        {
            for (var i = 0; i < 8; i++)
                stream.ReadUInt32(); // hc

            Connection.SendPacket(new ACEditorDeniedPacket(2));
        }
    }
}
