using Dapper;
using MeterInspectionDB.Model;
using System.Data;
namespace MeterInspectionDB
{
  
    public class MaintenanceRecordRepository
    {
        private readonly IDbConnection _db;

        public MaintenanceRecordRepository(IDbConnection db)
        {
            _db = db;
        }

        // Get all
        public async Task<IEnumerable<MaintenanceRecord>> GetAllAsync()
        {
            string sql = @"SELECT * FROM [dbo].[MaintenanceRecord]";
            return await _db.QueryAsync<MaintenanceRecord>(sql);
        }


        // Get by Id
        public async Task<MaintenanceRecord> GetByIdAsync(int id)
        {
            string sql = @"
            SELECT * 
            FROM [dbo].[MaintenanceRecord]
            WHERE Id = @Id";

            return await _db.QueryFirstOrDefaultAsync<MaintenanceRecord>(
                sql,
                new { Id = id });
        }

        // Add
        public async Task<MaintenanceRecord> AddAsync(MaintenanceRecord model)
        {
            string sql = @"
            INSERT INTO [dbo].[MaintenanceRecord]
            (
                MaintenanceRecordDate,
                CompanySectorDept_Id,
                LabCenter_Id,
                MeterCount,
                WorkingMetersCount,
                RepairedMetersCount,
                RetiredMetersCount,
                MaintenanceRecordCode,
                ISSync,
                CompanySectorDept_Level,
                UserId,
                IsDeleted,
                DeletedDate,
                DeletedUserId
            )
            VALUES
            (
                @MaintenanceRecordDate,
                @CompanySectorDept_Id,
                @LabCenter_Id,
                @MeterCount,
                @WorkingMetersCount,
                @RepairedMetersCount,
                @RetiredMetersCount,
                @MaintenanceRecordCode,
                @ISSync,
                @CompanySectorDept_Level,
                @UserId,
                @IsDeleted,
                @DeletedDate,
                @DeletedUserId
            );

            SELECT CAST(SCOPE_IDENTITY() as int);";
            int id = await _db.QuerySingleAsync<int>(sql, model);

            return await GetByIdAsync(id);
           
        }

        // Update
        public async Task<MaintenanceRecord> UpdateAsync(MaintenanceRecord model)
        {
            string sql = @"
            UPDATE [dbo].[MaintenanceRecord]
            SET
                MaintenanceRecordDate = @MaintenanceRecordDate,
                CompanySectorDept_Id = @CompanySectorDept_Id,
                LabCenter_Id = @LabCenter_Id,
                MeterCount = @MeterCount,
                WorkingMetersCount = @WorkingMetersCount,
                RepairedMetersCount = @RepairedMetersCount,
                RetiredMetersCount = @RetiredMetersCount,
                MaintenanceRecordCode = @MaintenanceRecordCode,
                ISSync = @ISSync,
                CompanySectorDept_Level = @CompanySectorDept_Level,
                UserId = @UserId,
                IsDeleted = @IsDeleted,
                DeletedDate = @DeletedDate,
                DeletedUserId = @DeletedUserId
            WHERE Id = @Id";

            var rows = await _db.ExecuteAsync(sql, model);

            if (rows == 0)
                return null;

            return await GetByIdAsync(model.Id);

          
        }

        // Delete
        public async Task<bool> DeleteAsync(int id)
        {
            string sql = @"
            DELETE FROM [dbo].[MaintenanceRecord]
            WHERE Id = @Id";

            return await _db.ExecuteAsync(sql, new { Id = id }) > 0;
        }
    }
}
