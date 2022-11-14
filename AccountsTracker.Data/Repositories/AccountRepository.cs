using AccountsTracker.Models.AccountModels;
using AccountsTracker.Shared.Interfaces.Repositories;
using AccountsTracker.Shared.Settings;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AccountsTracker.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly string _connectionString;

        public AccountRepository(IOptions<ConnectionStrings> connectionString)
        {
            _connectionString = connectionString.Value.DefaultConnection;
        }
        public async Task<Account> GetAccountById(int id)
        {
            var p = new
            {
                Id = id
            };

            using (var conn = new SqlConnection(_connectionString))
            {
                var result = await conn.QueryAsync<Account>("GetAccountById", p, commandType: System.Data.CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }

        public async Task<List<Account>> GetAllAccounts()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var result = await conn.QueryAsync<Account>("GetAllAccounts", commandType: System.Data.CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<bool> SaveAccount(string accountName)
        {
            var p = new
            {
                AccountName = accountName
            };

            using (var conn = new SqlConnection(_connectionString))
            {
                var result = await conn.ExecuteAsync("InsertAccount", p, commandType: System.Data.CommandType.StoredProcedure);

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
        public async Task<bool> SaveAccount(int id, string accountName)
        {
            var p = new
            {
                Id = id,
                AccountName = accountName
            };

            using (var conn = new SqlConnection(_connectionString))
            {
               var result = await conn.ExecuteAsync("UpdateAccount", p, commandType: System.Data.CommandType.StoredProcedure);
                
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

        public async Task<bool> DeleteAccount(int id)
        {
            var p = new
            {
                Id = id
            };

            using (var conn = new SqlConnection(_connectionString))
            {
                var result = await conn.ExecuteAsync("DeleteAccount", p, commandType: System.Data.CommandType.StoredProcedure);

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
