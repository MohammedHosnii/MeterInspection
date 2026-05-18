using Dapper;
using MeterInspectionDB.Model;
using System.Data;

namespace MeterInspectionDB
{

    public class ErrorRepository
    {
        private readonly IDbConnection _db;

        public ErrorRepository(IDbConnection db)
        {
            _db = db;
        }

        // Get All
        public async Task<IEnumerable<Error>> GetAllAsync()
        {
            var sql = "SELECT * FROM Errors ORDER BY Id";
            return await _db.QueryAsync<Error>(sql);
        }

        // Get By Id
        public async Task<Error?> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Errors WHERE Id = @Id";

            return await _db.QueryFirstOrDefaultAsync<Error>(
                sql,
                new { Id = id });
        }

        // Add
        public async Task<Error> AddAsync(Error model)
        {
            var sql = @"
        INSERT INTO Errors (ErrorNumber, ErrorName)
        VALUES (@ErrorNumber, @ErrorName);

        SELECT CAST(SCOPE_IDENTITY() AS INT);";

            int id = await _db.QuerySingleAsync<int>(sql, model);

            return await GetByIdAsync(id);
        }

        // Update
        public async Task<Error?> UpdateAsync(Error model)
        {
            var sql = @"
        UPDATE Errors
        SET ErrorNumber = @ErrorNumber,
            ErrorName = @ErrorName
        WHERE Id = @Id";

            var rows = await _db.ExecuteAsync(sql, model);

            if (rows == 0)
                return null;

            return await GetByIdAsync(model.Id);
        }

        // Delete
        public async Task<bool> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Errors WHERE Id = @Id";

            var rows = await _db.ExecuteAsync(
                sql,
                new { Id = id });

            return rows > 0;
        }
    }
}
