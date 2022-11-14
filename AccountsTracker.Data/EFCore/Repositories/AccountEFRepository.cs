using AccountsTracker.Data.EFCore.Contexts;
using AccountsTracker.Models.AccountModels;
using AccountsTracker.Shared.Interfaces.Repositories;
using AccountsTracker.Shared.Settings;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AccountsTracker.Data.EFCore.Repositories
{
    public class AccountEFRepository : IAccountRepository
    {
        private readonly AccountTrackerContext _context;

        public AccountEFRepository(AccountTrackerContext context)
        {
            _context = context;
        }
        public async Task<Account> GetAccountById(int id)
        {
            Account account = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == id);

            return account;
        }

        public async Task<List<Account>> GetAllAccounts()
        {
            return await _context.Accounts.ToListAsync();  
        }

        public async Task<bool> SaveAccount(string accountName)
        {
            Account account = new Account(accountName);
            await _context.Accounts.AddAsync(account);
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
        public async Task<bool> SaveAccount(int id, string accountName)
        {

            Account account = new Account(id, accountName);
            await _context.Accounts.AddAsync(account);
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

        public async Task<bool> DeleteAccount(int id)
        {
            Account accountToDelete = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == id);
            _context.Accounts.Remove(accountToDelete);
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
    }
}
