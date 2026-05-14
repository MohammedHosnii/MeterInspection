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
    public class TestResultRepository
    {
        private readonly IDbConnection _db;

        public TestResultRepository(IDbConnection db)
        {
            _db = db;
        }

        // Get all test results
        public async Task<IEnumerable<TestResult>> GetAllAsync()
        {
            string sql = @"SELECT * FROM [dbo].[TestResult]";
            return await _db.QueryAsync<TestResult>(sql);
        }

        // Get by Id
        public async Task<TestResult> GetByIdAsync(int id)
        {
            string sql = @"SELECT * FROM [dbo].[TestResult] WHERE Id = @Id";
            return await _db.QueryFirstOrDefaultAsync<TestResult>(sql, new { Id = id });
        }

        // Add new test result
        public async Task<int> AddAsync(TestResult result)
        {
            string sql = @"
                INSERT INTO [dbo].[TestResult] (TestResultCode, TestResultName)
                VALUES (@TestResultCode, @TestResultName);

                SELECT CAST(SCOPE_IDENTITY() as int);";

            return await _db.ExecuteScalarAsync<int>(sql, result);
        }

        // Update test result
        public async Task<bool> UpdateAsync(TestResult result)
        {
            string sql = @"
                UPDATE [dbo].[TestResult]
                SET TestResultCode = @TestResultCode,
                    TestResultName = @TestResultName
                WHERE Id = @Id";

            return await _db.ExecuteAsync(sql, result) > 0;
        }

        // Delete test result
        public async Task<bool> DeleteAsync(int id)
        {
            string sql = @"DELETE FROM [dbo].[TestResult] WHERE Id = @Id";
            return await _db.ExecuteAsync(sql, new { Id = id }) > 0;
        }
    }
}
