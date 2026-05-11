using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterInspectionDB
{
    public class SyncRepository
    {
        private readonly IDbConnection _db;

        public SyncRepository(IDbConnection db)
        {
            _db = db;
        }


        public async Task ExecuteSyncAllTablesAsync()
        {
            await _db.ExecuteAsync(
                sql: "[dbo].[Sync_All_Tables]",
                commandType: CommandType.StoredProcedure
            );
        }


       
    }
}
