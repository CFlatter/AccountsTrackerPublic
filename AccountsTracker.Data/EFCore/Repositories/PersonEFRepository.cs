using AccountsTracker.Data.EFCore.Contexts;
using AccountsTracker.Models.AccountModels;
using AccountsTracker.Models.PersonModels;
using AccountsTracker.Shared.Interfaces.Repositories;
using AccountsTracker.Shared.Settings;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsTracker.Data.EFCore.Repositories
{
    public class PersonEFRepository : IPersonRepository

    {
        private readonly AccountTrackerContext _context;

        public PersonEFRepository(AccountTrackerContext context)
        {
            _context = context;
        }
        public async Task<bool> DeletePerson(int id)
        {
            var personToDelete = await _context.People.FirstOrDefaultAsync(x => x.Id == id);
            _context.People.Remove(personToDelete);
            var dbOperation = await _context.SaveChangesAsync();
            if (dbOperation != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Person>> GetAllPeople()
        {
            return await _context.People.ToListAsync();
        }

        public async Task<bool> SavePerson(string name, decimal grossIncome, decimal netIncome)
        {
            Person person = new Person(name,grossIncome,netIncome);
            await _context.People.AddAsync(person);
            var dbOperation = await _context.SaveChangesAsync();
            if (dbOperation != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<bool> SavePerson(int id, string name, decimal grossIncome, decimal netIncome)
        {
            Person person = new Person(id,name,grossIncome,netIncome);
            _context.Update(person);
            var dbOperation = await _context.SaveChangesAsync();
            if (dbOperation != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<Person> GetPersonById(int id)
        {
            return await _context.People.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
