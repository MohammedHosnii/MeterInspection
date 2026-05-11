using System.Data;
using Dapper;
using global::MeterInspectionDB.Model;

namespace MeterInspectionDB
{

    public class LabCenterRepository
    {
        private readonly IDbConnection _db;

        public LabCenterRepository(IDbConnection db)
        {
            _db = db;
        }

        // Get All
        public async Task<IEnumerable<LabCenter>> GetAllAsync()
        {
            var sql = "SELECT * FROM LabCenter ORDER BY id";
            return await _db.QueryAsync<LabCenter>(sql);
        }

        // Get By Id
        public async Task<LabCenter?> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM LabCenter WHERE Id = @Id";

            return await _db.QueryFirstOrDefaultAsync<LabCenter>(
                sql,
                new { Id = id });
        }

        // Add
        public async Task<LabCenter> AddAsync(LabCenter model)
        {
            var sql = @"
            INSERT INTO LabCenter (LabCenterName)
            VALUES (@LabCenterName);

            SELECT CAST(SCOPE_IDENTITY() AS INT);";

            int id = await _db.QuerySingleAsync<int>(sql, model);

            return await GetByIdAsync(id);
        }

        // Update
        public async Task<LabCenter?> UpdateAsync(LabCenter model)
        {
            var sql = @"
            UPDATE LabCenter
            SET LabCenterName = @LabCenterName
            WHERE Id = @Id";

            var rows = await _db.ExecuteAsync(sql, model);

            if (rows == 0)
                return null;

            return await GetByIdAsync(model.Id);
        }

        // Delete
        public async Task<bool> DeleteAsync(int id)
        {
            var sql = "DELETE FROM LabCenter WHERE Id = @Id";

            var rows = await _db.ExecuteAsync(
                sql,
                new { Id = id });

            return rows > 0;
        }
    }
    
}
