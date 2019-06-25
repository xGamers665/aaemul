using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Controllers;
using AAEmu.Editor.Core.Network.Editor;

namespace AAEmu.Editor.Core.Packets.C2L
{
    public class CARequestAuthPacket : EditorPacket
    {
        public CARequestAuthPacket() : base(0x01)
        {
        }

        public override void Read(PacketStream stream)
        {
            var pFrom = stream.ReadUInt32();
            var pTo = stream.ReadUInt32();
            //var svc = stream.ReadByte();
            //var dev = stream.ReadBoolean();
            var account = stream.ReadString();
            //var mac = stream.ReadBytes();
            //var mac2 = stream.ReadBytes();
            //var cpu = stream.ReadUInt64();

            EditorController.Editor(Connection, account);

            // Connection.SendPacket(new ACChallengePacket()); // TODO ...
        }
    }
}
