using AccountsTracker.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsTracker.Shared.Interfaces.Repositories
{
    public interface IAccountLogRepository
    {
        Task<bool> SaveAccountLog(decimal accountBalance, DateTime LogDate, int accountId);
        Task<bool> DeleteAccountLog(int Id);
        Task<List<AccountLog>> GetAllAccountLogs(int accountId);
        Task<bool> DeleteAccountLogsByAccountId(int id);
    }
}
