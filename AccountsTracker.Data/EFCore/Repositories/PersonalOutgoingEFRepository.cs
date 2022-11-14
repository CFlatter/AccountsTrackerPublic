using AccountsTracker.Data.EFCore.Contexts;
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
    public class PersonalOutgoingEFRepository : IPersonalOutgoingRepository
    {
        private readonly AccountTrackerContext _context;

        public PersonalOutgoingEFRepository(AccountTrackerContext context)
        {
            _context = context;
        }
        public async Task<bool> DeletePersonalOutgoing(int id)
        {
            var outgoingToDelete = await _context.PersonalOutgoings.FirstOrDefaultAsync(x => x.Id == id);
            _context.PersonalOutgoings.Remove(outgoingToDelete);
            var dbOperation = await _context.SaveChangesAsync();
            if(dbOperation != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<PersonalOutgoings>> GetAllPersonalOutgoings()
        {
            return await _context.PersonalOutgoings.ToListAsync();
        }

        public async Task<bool> SavePersonalOutgoing(string description, decimal amount, int personId)
        {
            PersonalOutgoings personalOutgoing = new PersonalOutgoings(description, amount, personId);
            await _context.PersonalOutgoings.AddAsync(personalOutgoing);
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

        public async Task<bool> SavePersonalOutgoing(int id, string description, decimal amount, int personId)
        {
            PersonalOutgoings personalOutgoing = new PersonalOutgoings(id, description, amount, personId);
                _context.Update(personalOutgoing);
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

        public async Task<List<PersonalOutgoings>> GetPersonalOutgoingByPersonId(int personId)
        {

            var outgoings = await _context.PersonalOutgoings.ToListAsync();
            return outgoings.Where(x => x.PersonId == personId).ToList();
   

            
        }

        public async Task<PersonalOutgoings> GetPersonalOutgoingById(int id)
        {
            return await _context.PersonalOutgoings.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
