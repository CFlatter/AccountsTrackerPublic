using AccountsTracker.Models.AccountModels;
using AccountsTracker.Shared.Interfaces.Repositories;
using AccountsTracker.Shared.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsTracker.Shared.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountLogRepository _accountLogRepository;

        public AccountService(IAccountRepository accountRepository, IAccountLogRepository accountLogRepository)
        {
            _accountRepository = accountRepository;
            _accountLogRepository = accountLogRepository;
        }

        public async Task<bool> DeleteAccount(int id)
        {
            await _accountRepository.DeleteAccount(id);
            return true; //TODO better exception handling
        }
        public async Task<Account> GetAccountById(int Id)
        {
            return await _accountRepository.GetAccountById(Id);
        }
        public async Task<List<Account>> GetAllAccounts()
        {
            return await _accountRepository.GetAllAccounts();
        }
        public async Task<bool> SaveAccount(string accountName)
        {
            return await _accountRepository.SaveAccount(accountName);
        }
        public async Task<bool> SaveAccount(int id, string accountName)
        {
            return await _accountRepository.SaveAccount(id, accountName);
        }
    }
}
