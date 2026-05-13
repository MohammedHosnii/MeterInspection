
using Shared;
using System.Data;
using System.Data.SqlClient;

namespace MeterInspectionDB
{
    public class OFFline_Online
    {
        private readonly GpiConfig _config;

        public OFFline_Online(GpiConfig config)
        {
            _config = config;
        }

        public async Task<ConnectionType> GetConnectionStatusAsync()
        {
            var serverTask = Task.Run(() =>
                CanConnect(_config.ConnString_Server));

            var localTask = Task.Run(() =>
                CanConnect(_config.ConnString_Local));

            await Task.WhenAll(serverTask, localTask);

            if (!localTask.Result)
            {
                Shared.GPI.Status = ConnectionType.Offline.ToString();
                return ConnectionType.Offline;
            }

            if (serverTask.Result)
            {
                Shared.GPI.Status = ConnectionType.Server.ToString();
                return ConnectionType.Server;
            }
                       

            Shared.GPI.Status = ConnectionType.Local.ToString();
            return ConnectionType.Local;
        }

        public async Task<bool> GetConnectionStatus()
        {
            var serverTask = Task.Run(() =>
                CanConnect(_config.ConnString_Server));

            var localTask = Task.Run(() =>
                CanConnect(_config.ConnString_Local));

            await Task.WhenAll(serverTask, localTask);

            if (!localTask.Result)
            {
                return false;
            }

            if (serverTask.Result)
            {
                
                return true;
            }


            
            return false;
        }

        private bool CanConnect(string connectionString)
        {
            try
            {
                var builder =
                    new SqlConnectionStringBuilder(
                        connectionString)
                    {
                        ConnectTimeout = 1
                    };

                using var conn =
                    new SqlConnection(
                        builder.ConnectionString);

                conn.Open();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}