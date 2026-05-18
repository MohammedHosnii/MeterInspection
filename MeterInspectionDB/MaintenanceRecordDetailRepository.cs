using Dapper;
using MeterInspectionDB.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterInspectionDB
{
    public class MaintenanceRecordDetailRepository
    {
        private readonly IDbConnection _db;

        public MaintenanceRecordDetailRepository(IDbConnection db)
        {
            _db = db;
        }

        // Get All
        public async Task<IEnumerable<MaintenanceRecordDetail>> GetAllAsync()
        {
            var sql = "SELECT * FROM MaintenanceRecord_Detail ORDER BY Id";
            return await _db.QueryAsync<MaintenanceRecordDetail>(sql);
        }

        // Get By Id
        public async Task<MaintenanceRecordDetail?> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM MaintenanceRecord_Detail WHERE Id = @Id";

            return await _db.QueryFirstOrDefaultAsync<MaintenanceRecordDetail>(
                sql,
                new { Id = id });
        }


        // Get By MaintenanceRecord_id
        public async Task<MaintenanceRecordDetail?> GetByMaintenanceRecordIdAsync(int id)
        {
            var sql = "SELECT * FROM MaintenanceRecord_Detail WHERE MaintenanceRecord_id = @Id";

            return await _db.QueryFirstOrDefaultAsync<MaintenanceRecordDetail>(
                sql,
                new { Id = id });
        }

        // Add
        public async Task<MaintenanceRecordDetail> AddAsync(MaintenanceRecordDetail model)
        {
            var sql = @$"
        INSERT INTO MaintenanceRecord_Detail
        (
            MaintenanceRecord_id,
            MeterNumber,
            MeterType_id,
            TestResultCode,
            CorrectiveActionCode,
            ErrorNumber,
            CreationDateTime,
            Notes,
            ModificationDateTime,
            ISSync,
            IsDeleted,
            DeletedDate,
            DeletedUserId
        )
        VALUES
        (
            @MaintenanceRecord_id,
            @MeterNumber,
            @MeterType_id,
            @TestResultCode,
            @CorrectiveActionCode,
            @ErrorNumber,
            @CreationDateTime,
            @Notes,
            @ModificationDateTime,
            @ISSync,
           {0},
            NULL,
           Null
        );

        SELECT CAST(SCOPE_IDENTITY() AS INT);";

            int id = await _db.QuerySingleAsync<int>(sql, model);

            return await GetByIdAsync(id);
        }

        // Update
        public async Task<MaintenanceRecordDetail?> UpdateAsync(MaintenanceRecordDetail model)
        {
            var sql = @"
        UPDATE MaintenanceRecord_Detail
        SET
            MaintenanceRecord_id = @MaintenanceRecord_ids,
            MeterNumber = @MeterNumber,
            MeterType_id=@MeterType_id,
            TestResultCode = @TestResultCode,
            CorrectiveActionCode = @CorrectiveActionCode,
            ErrorNumber = @ErrorNumber,
            CreationDateTime = @CreationDateTime,
            Notes = @Notes,
            ModificationDateTime = @ModificationDateTime,
            ISSync = @ISSync,
            IsDeleted =0,
            DeletedDate = NULL,
            DeletedUserId = NULL
        WHERE Id = @Id AND  IsDeleted = 0 ";

            var rows = await _db.ExecuteAsync(sql, model);

            if (rows == 0)
                return null;

            return await GetByIdAsync(model.Id);
        }

        // Delete
        public async Task<bool> DeleteAsync(int id, int? deletedUserId = null)
        {
            var sql = @"
            UPDATE MaintenanceRecord_Detail
            SET 
                IsDeleted = 1,
                DeletedDate = GETDATE(),
                DeletedUserId = @DeletedUserId
            WHERE Id = @Id AND IsDeleted = 0";

            var rows = await _db.ExecuteAsync(sql, new
            {
                Id = id,
                DeletedUserId = deletedUserId
            });

            return rows > 0;
        }
    }
}
