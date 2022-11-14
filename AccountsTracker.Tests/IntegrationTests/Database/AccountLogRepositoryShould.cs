
using AccountsTracker.Data.EFCore.Repositories;
using AccountsTracker.Data.Repositories;
using AccountsTracker.Shared.Interfaces.Repositories;
using AccountsTracker.Shared.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Runtime.CompilerServices;
using System.Transactions;

namespace AccountsTracker.Tests.IntegrationTests.Database
{
    [Collection("Database")]
    public class AccountLogRepositoryShould
    {
        private readonly IAccountLogRepository _accountLogRepository;
        private readonly AccountEFRepository _accountRepository;

        public AccountLogRepositoryShould()
        {

            var appSettings = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.Development.json", false, false).Build();
            ConnectionStrings connectionString = new ConnectionStrings();
            connectionString.DefaultConnection = appSettings.GetConnectionString("DefaultConnection");
            var options = Options.Create<ConnectionStrings>(connectionString);


            _accountLogRepository = new AccountLogRepository(options);
            _accountRepository = new AccountRepository(options);
        }

        [Fact]
        public async void SaveAccountLogToDatabase()
        {
            //Arrange
            TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            decimal accountBalance = 100m;
            DateTime dateTime = DateTime.UtcNow;

            //Act

            await _accountRepository.SaveAccount("TestAccount");
            var accountsInDb = await _accountRepository.GetAllAccounts();

            var testAccount = accountsInDb.Find(x => x.AccountName == "TestAccount");
            var sut = await _accountLogRepository.SaveAccountLog(accountBalance, dateTime, testAccount.Id);
            transaction.Dispose();

            //Assert
            Assert.True(sut);
            
        }

        [Fact(Skip = "Needs updating with new GetAllAccountLogs signature")]
        public async void GetAllAccountLogsFromDatabase()
        {
            TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            decimal accountBalance = 100m;
            DateTime dateTime = DateTime.UtcNow;

            await _accountRepository.SaveAccount("TestAccount");
            var accountsInDb = await _accountRepository.GetAllAccounts();
            var testAccount = accountsInDb.Find(x => x.AccountName == "TestAccount");
            await _accountLogRepository.SaveAccountLog(accountBalance, dateTime, testAccount.Id);

            //TODO Update this for new GetAllAccountLogs signature (should pass through Id not 1)
            var sut = await _accountLogRepository.GetAllAccountLogs(1);

            transaction.Dispose();

            Assert.NotEmpty(sut);
            
        }

        [Fact(Skip = "Needs updating with new GetAllAccountLogs signature")]
        public async void DeleteAccountLogFromDatabase()
        {
            TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            decimal accountBalance = 1m;
            DateTime dateTime = DateTime.UtcNow;

            await _accountRepository.SaveAccount("TestAccount");
            var accountsInDb = await _accountRepository.GetAllAccounts();
            var testAccount = accountsInDb.Find(x => x.AccountName == "TestAccount");
            var saveAccountLogResult = await _accountLogRepository.SaveAccountLog(accountBalance, dateTime, testAccount.Id);

            //TODO Update this for new GetAllAccountLogs signature (should pass through Id not 1)
            var accountLogsInDb = await _accountLogRepository.GetAllAccountLogs(1);
            var testAccountLog = accountLogsInDb.Find(x => x.AccountBalance == accountBalance);


            var sut = await _accountLogRepository.DeleteAccountLog(testAccountLog.Id);
            transaction.Dispose();


            Assert.True(sut);


            


        }




    }
}
