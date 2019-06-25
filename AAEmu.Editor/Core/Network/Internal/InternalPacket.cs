using System;
using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Network.Connections;

namespace AAEmu.Editor.Core.Network.Internal
{
    public abstract class InternalPacket : PacketBase<InternalConnection>
    {
        protected InternalPacket(ushort typeId) : base(typeId)
        {
        }

        public override PacketStream Encode()
        {
            var ps = new PacketStream();
            try
            {
                ps.Write(new PacketStream().Write(TypeId).Write(this));
            }
            catch (Exception ex)
            {
                _log.Fatal(ex);
                throw;
            }

            return ps;
        }

        public override PacketBase<InternalConnection> Decode(PacketStream ps)
        {
            try
            {
                Read(ps);
            }
            catch (Exception ex)
            {
                _log.Fatal(ex);
                throw;
            }

            return this;
        }
    }
}
