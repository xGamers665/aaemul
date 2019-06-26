using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Network.Editor;

namespace AAEmu.Editor.Core.Packets.E2C
{
    public class ECEditorResponsePacket : EditorPacket
    {
        private readonly string _fileServerPath;
        private readonly byte _response;

        public ECEditorResponsePacket(byte response, string fileServerPath) : base(0x01)
        {
            _response = response;
            _fileServerPath = fileServerPath;
        }

        public override PacketStream Write(PacketStream stream)
        {
            stream.Write(_response);
            stream.Write(_fileServerPath);

            return stream;
        }
    }
}
