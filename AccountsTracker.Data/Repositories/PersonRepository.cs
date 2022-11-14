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
    public class PersonRepository : IPersonRepository

    {
        private readonly string _connectionString;

        public PersonRepository(IOptions<ConnectionStrings> connectionString)
        {
            _connectionString = connectionString.Value.DefaultConnection;
        }
        public async Task<bool> DeletePerson(int id)
        {
            var p = new
            {
                Id = id
            };

            using (var conn = new SqlConnection(_connectionString))
            {
                var result = await conn.ExecuteAsync("DeletePerson", p, commandType: System.Data.CommandType.StoredProcedure);

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

        public async Task<List<Person>> GetAllPeople()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var result = await conn.QueryAsync<Person>("GetAllPeople", commandType: System.Data.CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<bool> SavePerson(string name, decimal grossIncome, decimal netIncome)
        {
            var p = new
            {
                Name = name,
                GrossIncome = grossIncome,
                NetIncome = netIncome
            };


            using (var conn = new SqlConnection(_connectionString))
            {

                var result = await conn.ExecuteAsync("InsertPerson", p, commandType: System.Data.CommandType.StoredProcedure);

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
        public async Task<bool> SavePerson(int id, string name, decimal grossIncome, decimal netIncome)
        {
            var p = new
            {
                Id = id,
                Name = name,
                GrossIncome = grossIncome,
                NetIncome = netIncome
            };


            using (var conn = new SqlConnection(_connectionString))
            {

                var result = await conn.ExecuteAsync("UpdatePerson", p, commandType: System.Data.CommandType.StoredProcedure);

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

        public async Task<Person> GetPersonById(int id)
        {
            var p = new
            {
                Id = id
            };

            using (var conn = new SqlConnection(_connectionString))
            {
                var result = await conn.QueryAsync<Person>("GetPersonById", p, commandType: System.Data.CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }
    }
}
