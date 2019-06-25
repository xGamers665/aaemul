using System;
using System.Collections.Generic;
using System.Net;
using AAEmu.Commons.Models;
using AAEmu.Commons.Network;
using AAEmu.Editor.Core.Network.Editor;

namespace AAEmu.Editor.Core.Network.Connections
{
    public class EditorConnection
    {
        private Session _session;

        public uint Id => _session.Id;
        public IPAddress Ip => _session.Ip;
        public InternalConnection InternalConnection { get; set; }
        public PacketStream LastPacket { get; set; }

        public uint AccountId { get; set; }
        public string AccountName { get; set; }
        public DateTime LastEditor { get; set; }
        public IPAddress LastIp { get; set; }

        public Dictionary<byte, List<LoginCharacterInfo>> Characters;

        public EditorConnection(Session session)
        {
            _session = session;

            Characters = new Dictionary<byte, List<LoginCharacterInfo>>();
        }

        public void SendPacket(EditorPacket packet)
        {
            SendPacket(packet.Encode());
        }

        public void SendPacket(byte[] packet)
        {
            _session?.SendPacket(packet);
        }

        public void OnConnect()
        {
        }

        public void Shutdown()
        {
            _session?.Close();
        }

        public List<LoginCharacterInfo> GetCharacters()
        {
            var res = new List<LoginCharacterInfo>();
            foreach (var characters in Characters.Values)
                if (characters != null)
                    res.AddRange(characters);
            return res;
        }

        public void AddCharacters(byte gsId, List<LoginCharacterInfo> characterInfos)
        {
            foreach (var character in characterInfos)
                character.GsId = gsId;
            Characters.Add(gsId, characterInfos);
        }
    }
}
