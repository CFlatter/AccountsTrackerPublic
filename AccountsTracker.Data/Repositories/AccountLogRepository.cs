using AccountsTracker.Models.AccountModels;
using AccountsTracker.Shared.Interfaces.Repositories;
using AccountsTracker.Shared.Settings;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsTracker.Data.Repositories
{
    public class AccountLogRepository : IAccountLogRepository
    {
        private readonly string _connectionString;

        public AccountLogRepository(IOptions<ConnectionStrings> connectionString)
        {

            if (connectionString.Value == null)
            {
                throw new Exception("No connection string defined! Fix it lazy bones");
            }

            _connectionString = connectionString.Value.DefaultConnection;

        }
        public async Task<bool> DeleteAccountLog(int id)
        {
            var p = new
            {
                Id = id
            };

            using (var conn = new SqlConnection(_connectionString))
            {
                var result = await conn.ExecuteAsync("DeleteAccountLog", p, commandType: System.Data.CommandType.StoredProcedure);

                if (result == 1)
                {
                    return true;
                }
                else if (result == 0)
                {
                    return false;
                }
                else
                {
                    throw new Exception($"The operation affected {result} rows in the database. This should be 0 or 1. An error has occured");
                }
            }    
        }

        public async Task<bool> DeleteAccountLogsByAccountId(int id)
        {
            var p = new
            {
                AccountId = id
            };

            using(var conn = new SqlConnection(_connectionString))
            {
                var result = await conn.ExecuteAsync("DeleteAccountLogsByAccountId",p, commandType: System.Data.CommandType.StoredProcedure);

                if (result > 0)
                {
                    return true;
                }
                else if (result == 0)
                {
                    return false;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<List<AccountLog>> GetAllAccountLogs(int accountId)
        {
            var p = new
            {
                AccountId = accountId
            };

            using (var conn = new SqlConnection(_connectionString))
            {
                var result = await conn.QueryAsync<AccountLog>("GetAllAccountLogsByAccountId", p, commandType: System.Data.CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<bool> SaveAccountLog(decimal accountBalance, DateTime logDate, int accountId)
        {
            var p = new
            {
                AccountBalance = accountBalance,
                Logdate = logDate,
                AccountId = accountId
            } ;

            using (var conn = new SqlConnection(_connectionString))
            {
                var result = await conn.ExecuteAsync("InsertAccountLog", p, commandType: System.Data.CommandType.StoredProcedure);

                if (result == 1)
                {
                    return true;
                }
                else if (result == 0)
                {
                    return false;
                }
                else
                {
                    throw new Exception($"The operation affected {result} rows in the database. This should be 0 or 1. An error has occured");
                }
            }
        }
    }
}
