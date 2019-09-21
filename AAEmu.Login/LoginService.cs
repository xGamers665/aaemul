using System;
using System.Threading;
using System.Threading.Tasks;
using AAEmu.Login.Core.Controllers;
using AAEmu.Login.Core.Network.Internal;
using AAEmu.Login.Core.Network.Login;
using Microsoft.Extensions.Hosting;
using NLog;

namespace AAEmu.Login
{
    public class LoginService : IHostedService, IDisposable
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _log.Info("Starting daemon: AAEmu.Login");
            try
            {
                RequestController.Instance.Initialize();
                GameController.Instance.Load();
                LoginNetwork.Instance.Start();
                InternalNetwork.Instance.Start();
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                return Task.FromException(e);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _log.Info("Stopping daemon.");
            try
            {
                LoginNetwork.Instance.Stop();
                InternalNetwork.Instance.Stop();
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                return Task.FromException(e);
            }
        }

        public void Dispose()
        {
            _log.Info("Disposing....");
            LogManager.Flush();
        }
    }
}
