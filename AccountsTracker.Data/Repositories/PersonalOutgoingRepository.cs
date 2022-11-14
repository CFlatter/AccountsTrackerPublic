using AccountsTracker.Models.PersonModels;
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
    public class PersonalOutgoingRepository : IPersonalOutgoingRepository
    {
        private readonly string _connectionString;

        public PersonalOutgoingRepository(IOptions<ConnectionStrings> connectionString)
        {
            _connectionString = connectionString.Value.DefaultConnection;
        }
        public async Task<bool> DeletePersonalOutgoing(int id)
        {
            var p = new
            {
                Id = id
            };

            using (var conn = new SqlConnection(_connectionString))
            {
                var result = await conn.ExecuteAsync("DeletePersonalOutgoing", p, commandType: System.Data.CommandType.StoredProcedure);

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

        public async Task<List<PersonalOutgoings>> GetAllPersonalOutgoings()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var result = await conn.QueryAsync<PersonalOutgoings>("GetAllPersonalOutgoings", commandType: System.Data.CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<bool> SavePersonalOutgoing(string description, decimal amount, int personId)
        {
            var p = new
            {
                Description = description,
                Amount = amount,
                PersonId = personId,
            };

            using (var conn = new SqlConnection(_connectionString))
            {
                var result = await conn.ExecuteAsync("InsertPersonalOutgoing", p, commandType: System.Data.CommandType.StoredProcedure);

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

        public async Task<bool> SavePersonalOutgoing(int id, string description, decimal amount, int personId)
        {
            var p = new
            {
                Id = id,
                Description = description,
                Amount = amount,
                PersonId = personId,
            };

            using (var conn = new SqlConnection(_connectionString))
            {
                var result = await conn.ExecuteAsync("UpdatePersonalOutgoing", p, commandType: System.Data.CommandType.StoredProcedure);

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

        public async Task<List<PersonalOutgoings>> GetPersonalOutgoingByPersonId(int personId)
        {
            var p = new
            {
                PersonId = personId
            };
            using (var conn = new SqlConnection(_connectionString))
            {
                var results = await conn.QueryAsync<PersonalOutgoings>("GetPersonalOutgoingByPersonId", p, commandType: System.Data.CommandType.StoredProcedure);
                return results.ToList();
            }
        }

        public async Task<PersonalOutgoings> GetPersonalOutgoingById(int id)
        {
            var p = new
            {
                Id = id
            };
            using (var conn = new SqlConnection(_connectionString))
            {
                var results = await conn.QueryAsync<PersonalOutgoings>("GetPersonalOutgoingById", p, commandType: System.Data.CommandType.StoredProcedure);
                return results.FirstOrDefault();
            }
        }
    }
}
