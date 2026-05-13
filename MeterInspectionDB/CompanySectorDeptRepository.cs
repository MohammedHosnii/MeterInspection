using Dapper;
using MeterInspectionDB.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MeterInspectionDB
{
    public class CompanySectorDeptRepository
    {
        private readonly IDbConnection _db;

        public CompanySectorDeptRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<IEnumerable<CompanySectorDept>> GetAllAsync()
        {
            string sql = @"SELECT * FROM [dbo].[CompanySectorDept]";
            return await _db.QueryAsync<CompanySectorDept>(sql);
        }

        public async Task<CompanySectorDept> GetByIdAsync(int id)
        {
            string sql = @"SELECT * FROM [dbo].[CompanySectorDept]
                           WHERE Id = @Id";

            return await _db.QueryFirstOrDefaultAsync<CompanySectorDept>(sql, new { Id = id });
        }

        public async Task<IEnumerable<CompanySectorDept>> GetALLCompany()
        {
            string sql = @"SELECT [Id] 
            ,[CompanySectorDeptName] 
            FROM [dbo].[CompanySectorDept]
            WHERE CompanySectorDept_Level = 1";

            return await _db.QueryAsync<CompanySectorDept>(sql);
        }
        public async Task<IEnumerable<CompanySectorDept>> GetALLSectors(int ParentId)
        {
            string sql = @"SELECT [Id] 
            ,[CompanySectorDeptName] 
            FROM [dbo].[CompanySectorDept]
            WHERE ParentId = @ParentId and  CompanySectorDept_Level = 2";

            return await _db.QueryAsync<CompanySectorDept>(sql, new { ParentId = ParentId });

        }

        public async Task<IEnumerable<CompanySectorDept>> GetALLDepartments(int ParentId)
        {
            string sql = @"SELECT [Id] 
            ,[CompanySectorDeptName] 
            FROM [dbo].[CompanySectorDept]
            WHERE ParentId = @ParentId and  CompanySectorDept_Level = 3";

            return await _db.QueryAsync<CompanySectorDept>(sql, new { ParentId = ParentId });

        }
        public async Task<IEnumerable<CompanySectorDept>> GetByParentAsync(int parentId)
        {
            string sql = @"SELECT * FROM [dbo].[CompanySectorDept]
                           WHERE ParentId = @ParentId";

            return await _db.QueryAsync<CompanySectorDept>(sql, new { ParentId = parentId });
        }

        public async Task<int> AddAsync(CompanySectorDept dept)
        {
            string sql = @"
                INSERT INTO [dbo].[CompanySectorDept]
                (CompanySectorDeptCode, CompanySectorDeptName,
                 CompanySectorDept_Level, ParentId)
                VALUES
                (@CompanySectorDeptCode, @CompanySectorDeptName,
                 @CompanySectorDept_Level, @ParentId);

                SELECT CAST(SCOPE_IDENTITY() as int);";

            return await _db.ExecuteScalarAsync<int>(sql, dept);
        }

        public async Task<CompanySectorDept?> UpdateAsync(CompanySectorDept dept)
        {
            string sql = @"
                UPDATE [dbo].[CompanySectorDept]
                SET
                    CompanySectorDeptCode = @CompanySectorDeptCode,
                    CompanySectorDeptName = @CompanySectorDeptName,
                    CompanySectorDept_Level = @CompanySectorDept_Level,
                    ParentId = @ParentId
                WHERE Id = @Id"
            ;

            var rows = await _db.ExecuteAsync(sql, dept);

            if (rows == 0)
                return null;

            return await GetByIdAsync(dept.Id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            string sql = @"DELETE FROM [dbo].[CompanySectorDept]
                           WHERE Id = @Id";

            var rows = await _db.ExecuteAsync(
                sql,
                new { Id = id });

            return rows > 0;
        }
    }
}
