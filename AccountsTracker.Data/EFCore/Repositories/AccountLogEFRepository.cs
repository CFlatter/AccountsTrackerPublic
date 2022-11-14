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
using System.Text;
using System.Threading.Tasks;

namespace AccountsTracker.Data.EFCore.Repositories
{
    public class AccountLogEFRepository : IAccountLogRepository
    {
        private readonly AccountTrackerContext _context;

        public AccountLogEFRepository(AccountTrackerContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteAccountLog(int id)
        {

            AccountLog accountLogToDelete = await _context.AccountLogs.FirstOrDefaultAsync(x => x.Id == id);
            _context.AccountLogs.Remove(accountLogToDelete);
            var dbOperation = await _context.SaveChangesAsync();
            if(dbOperation != 0 )
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<bool> DeleteAccountLogsByAccountId(int id)
        {
            AccountLog accountLogToDelete = await _context.AccountLogs.FirstOrDefaultAsync(x => x.AccountId == id);
            _context.AccountLogs.Remove(accountLogToDelete);
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

        public async Task<List<AccountLog>> GetAllAccountLogs(int accountId)
        {
            return await _context.AccountLogs.Where(x => x.AccountId == accountId).ToListAsync();
        }

        public async Task<bool> SaveAccountLog(decimal accountBalance, DateTime logDate, int accountId)
        {
            AccountLog accountLog = new AccountLog(accountBalance,accountId,logDate);
            await _context.AccountLogs.AddAsync(accountLog);
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
