using System.Data;
using Dapper;
using MeterInspectionDB.Model;

namespace MeterInspectionDB
{


    public class CorrectiveActionRepository
    {
        private readonly IDbConnection _db;

        public CorrectiveActionRepository(IDbConnection db)
        {
            _db = db;
        }

        // Get All
        public async Task<IEnumerable<CorrectiveAction>> GetAllAsync()
        {
            var sql = "SELECT * FROM CorrectiveAction order by id ";
            return await _db.QueryAsync<CorrectiveAction>(sql);
        }

        // Get By Id
        public async Task<CorrectiveAction?> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM CorrectiveAction WHERE Id = @Id";
            return await _db.QueryFirstOrDefaultAsync<CorrectiveAction>(sql, new { Id = id });
        }

        // Insert
        public async Task<CorrectiveAction> AddAsync(CorrectiveAction model)
        {
            var sql = @"
            INSERT INTO CorrectiveAction (CorrectiveActionCode, CorrectiveActionName)
            VALUES (@CorrectiveActionCode, @CorrectiveActionName);
            SELECT CAST(SCOPE_IDENTITY() as int);";

            int id = await _db.QuerySingleAsync<int>(sql, model);

            return await GetByIdAsync(id);
        }

        // Update
        public async Task<CorrectiveAction> UpdateAsync(CorrectiveAction model)
        {
            var sql = @"
            UPDATE CorrectiveAction
            SET CorrectiveActionCode = @CorrectiveActionCode,
            CorrectiveActionName = @CorrectiveActionName
            WHERE Id = @Id;

            SELECT * FROM CorrectiveAction WHERE Id = @Id;";

            return await _db.QueryFirstOrDefaultAsync<CorrectiveAction>(sql, model);
        }
        // Delete
        public async Task<bool> DeleteAsync(int id)
        {
            var sql = "DELETE FROM CorrectiveAction WHERE Id = @Id";
            var rows = await _db.ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }
    }
}
