using AccountsTracker.Data.Repositories;
using AccountsTracker.Shared.Interfaces.Repositories;
using AccountsTracker.Shared.Interfaces.Services;
using AccountsTracker.Shared.Services;
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
    public class PersonalOutgoingRepositoryShould
    {
        private readonly IPersonalOutgoingRepository _personalOutgoingRepository;
        private readonly IPersonService _personService;
        public PersonalOutgoingRepositoryShould()
        {
            var appSettings = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.Development.json", false, false).Build();
            var connectionString = new ConnectionStrings();
            connectionString.DefaultConnection = appSettings.GetConnectionString("DefaultConnection");
            var options = Options.Create<ConnectionStrings>(connectionString);

            var personRepository = new PersonRepository(options);  

            _personalOutgoingRepository = new PersonalOutgoingRepository(options);
            _personService = new PersonService(personRepository, _personalOutgoingRepository);
        }

        [Fact]
        public async void SavePersonalOutgoing()
        {
            TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var name = "Tester";
            var grossIncome = 1000m;
            var netIncome = 800m;

            await _personService.SavePerson(name, grossIncome, netIncome);
            var peopleInDb = await _personService.GetAllPeople();
            var testPerson = peopleInDb.Find(x => x.Name == name);
            var sut = await _personalOutgoingRepository.SavePersonalOutgoing("Test Outgoing", 10m, testPerson.Id);
            transaction.Dispose();

            Assert.True(sut);
        }

        [Fact]
        public async void GetAllPersonalOutgoings()
        {
            TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var name = "Tester";
            var grossIncome = 1000m;
            var netIncome = 800m;

            await _personService.SavePerson(name, grossIncome, netIncome);
            var peopleInDb = await _personService.GetAllPeople();
            var testPerson = peopleInDb.Find(x => x.Name == name);
            await _personalOutgoingRepository.SavePersonalOutgoing("Test Outgoing", 10m, testPerson.Id);

            var sut = await _personalOutgoingRepository.GetAllPersonalOutgoings();
            transaction.Dispose();

            Assert.NotEmpty(sut);

        }

        [Fact]
        public async void DeletePersonalOutgoing()
        {
            TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var name = "Tester";
            var grossIncome = 1000m;
            var netIncome = 800m;

            await _personService.SavePerson(name, grossIncome, netIncome);
            var peopleInDb = await _personService.GetAllPeople();
            var testPerson = peopleInDb.Find(x => x.Name == name);
            await _personalOutgoingRepository.SavePersonalOutgoing("Test Outgoing", 10m, testPerson.Id);

            var outgoingsInDb = await _personalOutgoingRepository.GetAllPersonalOutgoings();
            var outgoingToDelete = outgoingsInDb.Find(x => x.Description == "Test Outgoing");

            var sut = await _personalOutgoingRepository.DeletePersonalOutgoing(outgoingToDelete.Id);
            transaction.Dispose();

            Assert.True(sut);

        }

    }
}
