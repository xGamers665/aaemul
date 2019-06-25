using System;
using System.Threading;
using System.Threading.Tasks;
using AAEmu.Editor.Core.Controllers;
using AAEmu.Editor.Core.Network.Internal;
using AAEmu.Editor.Core.Network.Editor;
using Microsoft.Extensions.Hosting;
using NLog;

namespace AAEmu.Editor
{
    public class EditorService : IHostedService, IDisposable
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _log.Info("Starting daemon: AAEmu.Editor");
            GameController.Instance.Load();
            EditorNetwork.Instance.Start();
            InternalNetwork.Instance.Start();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _log.Info("Stopping daemon.");
            EditorNetwork.Instance.Stop();
            InternalNetwork.Instance.Stop();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _log.Info("Disposing....");
            LogManager.Flush();
        }
    }
}
