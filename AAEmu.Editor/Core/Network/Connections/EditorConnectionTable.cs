using System.Collections.Concurrent;
using System.Collections.Generic;
using AAEmu.Commons.Utils;

namespace AAEmu.Editor.Core.Network.Connections
{
    public class EditorConnectionTable : Singleton<EditorConnectionTable>
    {
        private ConcurrentDictionary<uint, EditorConnection> _connections;

        private EditorConnectionTable()
        {
            _connections = new ConcurrentDictionary<uint, EditorConnection>();
        }

        public void AddConnection(EditorConnection con)
        {
            _connections.TryAdd(con.Id, con);
        }

        public EditorConnection GetConnection(uint id)
        {
            _connections.TryGetValue(id, out var con);
            return con;
        }

        public EditorConnection RemoveConnection(uint id)
        {
            _connections.TryRemove(id, out var con);
            return con;
        }

        public List<EditorConnection> GetConnections()
        {
            return new List<EditorConnection>(_connections.Values);
        }
    }
}
