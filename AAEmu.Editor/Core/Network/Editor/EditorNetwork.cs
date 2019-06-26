using System;
using System.Net;
using AAEmu.Commons.Network.Type;
using AAEmu.Commons.Utils;
using AAEmu.Editor.Core.Packets.C2E;
using AAEmu.Editor.Models;
using NLog;

namespace AAEmu.Editor.Core.Network.Editor
{
    public class EditorNetwork : Singleton<EditorNetwork>
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        private Server _server;
        private EditorProtocolHandler _handler;

        private EditorNetwork()
        {
            _handler = new EditorProtocolHandler();

            RegisterPacket(0x01, typeof(CELoginPacket)); // шлет Editor первым пакетом
            RegisterPacket(0x02, typeof(CERequestCellLockInfoPacket));
            RegisterPacket(0x03, typeof(CERequestLockPacket));
            RegisterPacket(0x04, typeof(CERequestMultipleLocksPacket));
            RegisterPacket(0x05, typeof(CEIncreaseVersionPacket));
            RegisterPacket(0x06, typeof(CERequestCellLockPacket));
            RegisterPacket(0x07, typeof(CEPongPacket));
            RegisterPacket(0x08, typeof(CEChatPacket));
            RegisterPacket(0x0a, typeof(CEBatchRequestPacket));
            RegisterPacket(0x0c, typeof(CEBatchQueryPacket));
            RegisterPacket(0x0e, typeof(CECommandPacket));
        }

        public void Start()
        {
            var config = AppConfiguration.Instance.Network;
            _server = new Server(
                new IPEndPoint(config.Host.Equals("*") ? IPAddress.Any : IPAddress.Parse(config.Host), config.Port),
                config.PlayerNumber);
            _server.SetHandler(_handler);
            _server.Start();

            _log.Info("Network started with Player Count of: " + config.PlayerNumber);
        }

        public void Stop()
        {
            if (_server.IsStarted)
                _server.Stop();

            _log.Info("Network stoped");
        }

        private void RegisterPacket(uint type, Type classType)
        {
            _handler.RegisterPacket(type, classType);
        }
    }
}
