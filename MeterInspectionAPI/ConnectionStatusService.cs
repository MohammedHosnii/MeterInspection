using Shared;
using MeterInspectionDB;

namespace MeterInspectionAPI
{
    public class ConnectionStatusService
    {
        private readonly IServiceScopeFactory
            _scopeFactory;

        private static bool _started = false;
        private static readonly object _lock = new();

        public ConnectionStatusService(
            IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;

            StartPolling();
        }

        public bool IsOnline()
        {
            return AppState.IsOnline;
        }

        private void StartPolling()
        {
            lock (_lock)
            {
                if (_started)
                    return;

                _started = true;

                Task.Run(async () =>
                {
                    while (true)
                    {
                        try
                        {
                            using var scope =
                                _scopeFactory
                                    .CreateScope();

                            var offlineOnline =
                                scope.ServiceProvider
                                .GetRequiredService<
                                    OFFline_Online>();

                            AppState.IsOnline =
                                await offlineOnline
                                    .GetConnectionStatus();

                            AppState.LastCheck =
                                DateTime.Now;
                        }
                        catch
                        {
                            AppState.IsOnline =
                                false;
                        }

                        await Task.Delay(30000);
                    }
                });
            }
        }
    }
}