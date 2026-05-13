using Dapper;
using Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterInspectionDB
{
    public class SyncRepository
    {
        private readonly GpiConfig _config;

        public SyncRepository(GpiConfig config)
        {
            _config = config;
        }


        public async Task ExecuteSyncAllTablesAsync()
        {
            using IDbConnection _db =
             new SqlConnection(_config.ConnString_Local);
            await _db.ExecuteAsync(
                sql: "[dbo].[Sync_All_Tables]",
                commandType: CommandType.StoredProcedure
            );
        }


       
    }
}
