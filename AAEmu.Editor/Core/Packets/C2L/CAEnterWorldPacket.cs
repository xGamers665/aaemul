using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Controllers;
using AAEmu.Editor.Core.Network.Editor;

namespace AAEmu.Editor.Core.Packets.C2L
{
    public class CAEnterWorldPacket : EditorPacket
    {
        public CAEnterWorldPacket() : base(0x0b)
        {
        }

        public override void Read(PacketStream stream)
        {
            var flag = stream.ReadUInt64();
            var gsId = stream.ReadByte();

            GameController.Instance.RequestEnterWorld(Connection, gsId);
        }
    }
}
