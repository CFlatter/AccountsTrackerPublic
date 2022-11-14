using AccountsTracker.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsTracker.Shared.Interfaces.Services
{
    public interface IAccountService
    {
        Task<bool> DeleteAccount(int id);
        Task<Account> GetAccountById(int Id);
        Task<List<Account>> GetAllAccounts();
        Task<bool> SaveAccount(string accountName);
        Task<bool> SaveAccount(int id, string accountName);
    }
}
