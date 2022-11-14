using AccountsTracker.Data.EFCore.Repositories;
using AccountsTracker.Data.Repositories;
using AccountsTracker.Shared.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AccountsTracker.Tests.IntegrationTests.Database
{
    [Collection("Database")]
    public class AccountRepositoryShould
    {
        private readonly AccountEFRepository _accountRepository;

        public AccountRepositoryShould()
        {
            var appSettings = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.Development.json", false, false).Build();
            ConnectionStrings connectionString = new ConnectionStrings();
            connectionString.DefaultConnection = appSettings.GetConnectionString("DefaultConnection");
            var options = Options.Create<ConnectionStrings>(connectionString);


            _accountRepository = new AccountRepository(options);
        }

        [Fact]
        public async void SaveAccountToDatabase()
        {
            TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);


            var sut = await _accountRepository.SaveAccount("TestAccount");
            transaction.Dispose();

            Assert.True(sut);
            
        }

        [Fact]
        public async void GetAllAccountsFromDatabase()
        {
            TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            await _accountRepository.SaveAccount("TestAccount1");
            await _accountRepository.SaveAccount("TestAccount2");

            var sut = await _accountRepository.GetAllAccounts();
            transaction.Dispose();

            Assert.NotEmpty(sut);
            
        }

        [Fact]
        public async void GetAccountByIdFromDatabase()
        {
            TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            await _accountRepository.SaveAccount("TestAccount");
            var accountsInDb = await _accountRepository.GetAllAccounts();
            var testAccount = accountsInDb.Find(x => x.AccountName == "TestAccount");

            var sut = await _accountRepository.GetAccountById(testAccount.Id);
            transaction.Dispose();

            Assert.NotNull(sut);
            
        }

        [Fact]
        public async void DeleteAccountFromDatabase()
        {
            TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            await _accountRepository.SaveAccount("TestAccount");
            var accountsInDb = await _accountRepository.GetAllAccounts();
            var testAccount = accountsInDb.Find(x => x.AccountName == "TestAccount");

            var sut = await _accountRepository.DeleteAccount(testAccount.Id);
            transaction.Dispose();

            Assert.True(sut);
        }
    }
}
