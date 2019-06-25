using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Controllers;
using AAEmu.Editor.Core.Network.Internal;

namespace AAEmu.Editor.Core.Packets.G2L
{
    public class GLLoadPacket : InternalPacket
    {
        public GLLoadPacket() : base(0x03)
        {
        }

        public override void Read(PacketStream stream)
        {
            var gsId = stream.ReadByte();
            var load = stream.ReadByte();

            GameController.Instance.SetLoad(gsId, load);
        }
    }
}
