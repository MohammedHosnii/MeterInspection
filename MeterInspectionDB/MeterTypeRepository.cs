using Dapper;
using MeterInspectionDB.Model;
using System.Data;

namespace MeterInspectionDB
{
  
    public class MeterTypeRepository
    {
        private readonly IDbConnection _db;

        public MeterTypeRepository(IDbConnection db)
        {
            _db = db;
        }

        // Get All
        public async Task<IEnumerable<MeterType>> GetAllAsync()
        {
            var sql = "SELECT * FROM MeterTypes ORDER BY Id";
            return await _db.QueryAsync<MeterType>(sql);
        }

        // Get By Id
        public async Task<MeterType?> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM MeterTypes WHERE Id = @Id";

            return await _db.QueryFirstOrDefaultAsync<MeterType>(
                sql,
                new { Id = id });
        }

        // Add
        public async Task<MeterType> AddAsync(MeterType model)
        {
            var sql = @"
        INSERT INTO MeterTypes
        (
            MeterTypeCode,
            MeterTypeName
        )
        VALUES
        (
            @MeterTypeCode,
            @MeterTypeName
        );

        SELECT CAST(SCOPE_IDENTITY() AS INT);";

            int id = await _db.QuerySingleAsync<int>(sql, model);

            return await GetByIdAsync(id);
        }

        // Update
        public async Task<MeterType?> UpdateAsync(MeterType model)
        {
            var sql = @"
        UPDATE MeterTypes
        SET
            MeterTypeCode = @MeterTypeCode,
            MeterTypeName = @MeterTypeName
        WHERE Id = @Id";

            var rows = await _db.ExecuteAsync(sql, model);

            if (rows == 0)
                return null;

            return await GetByIdAsync(model.Id);
        }

        // Delete
        public async Task<bool> DeleteAsync(int id)
        {
            var sql = "DELETE FROM MeterTypes WHERE Id = @Id";

            var rows = await _db.ExecuteAsync(
                sql,
                new { Id = id });

            return rows > 0;
        }
    }
}
