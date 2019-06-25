using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Controllers;
using AAEmu.Editor.Core.Network.Editor;

namespace AAEmu.Editor.Core.Packets.C2L
{
    public class CAListWorldPacket : EditorPacket
    {
        public CAListWorldPacket() : base(0x0b)
        {
        }

        public override void Read(PacketStream stream)
        {
            var flag = stream.ReadUInt64();

            GameController.Instance.RequestWorldList(Connection);
        }
    }
}
