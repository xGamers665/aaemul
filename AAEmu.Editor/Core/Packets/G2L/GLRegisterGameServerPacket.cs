using System.Collections.Generic;
using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Controllers;
using AAEmu.Editor.Core.Network.Internal;
using AAEmu.Editor.Core.Packets.L2G;
using AAEmu.Editor.Models;

namespace AAEmu.Editor.Core.Packets.G2L
{
    public class GLRegisterGameServerPacket : InternalPacket
    {
        public GLRegisterGameServerPacket() : base(0x00)
        {
        }

        public override void Read(PacketStream stream)
        {
            var secretKey = stream.ReadString();
            if (secretKey == AppConfiguration.Instance.SecretKey)
            {
                var gsId = stream.ReadByte();
                var additionalesCount = stream.ReadInt32();
                var mirrors = new List<byte>();
                for (var i = 0; i < additionalesCount; i++)
                    mirrors.Add(stream.ReadByte());

                GameController.Instance.Add(gsId, mirrors, Connection);
            }
            else
            {
                Connection.SendPacket(new LGRegisterGameServerPacket(GSRegisterResult.Error));
                _log.Error("Connection {0}, bad secret key", Connection.Ip);
            }
        }
    }
}
