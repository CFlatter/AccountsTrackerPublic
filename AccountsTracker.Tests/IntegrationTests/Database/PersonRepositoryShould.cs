
using AccountsTracker.Data.Repositories;
using AccountsTracker.Models.PersonModels;
using AccountsTracker.Shared.Interfaces.Repositories;
using AccountsTracker.Shared.Interfaces.Services;
using AccountsTracker.Shared.Services;
using AccountsTracker.Shared.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;
using System.Transactions;
using System.Xml.Linq;

namespace AccountsTracker.Tests.IntegrationTests.Database
{
    //TODO - Test Xunit dependency injection
    [Collection("Database")]
    public class PersonRepositoryShould
    {
        private readonly IPersonService _personService;
        private readonly IPersonalOutgoingRepository _personalOutgoingRepository;
        public PersonRepositoryShould()
        {

            var appSettings = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.Development.json", false, false).Build();
            ConnectionStrings connectionString = new ConnectionStrings();
            connectionString.DefaultConnection = appSettings.GetConnectionString("DefaultConnection");
            var options = Options.Create<ConnectionStrings>(connectionString);


            var personRepository = new PersonRepository(options);
            _personalOutgoingRepository = new PersonalOutgoingRepository(options);

            _personService = new PersonService(personRepository, _personalOutgoingRepository);

        }

        [Fact]
        public async void SavePersonToDatabase()
        {
            //Arrange
            TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var name = "Tester";
            var grossIncome = 1000m;
            var netIncome = 800m;

            //Act
            var sut = await _personService.SavePerson(name, grossIncome, netIncome);
            transaction.Dispose();

            //Assert
            Assert.True(sut);
            
        }

        [Fact]
        public async void GetAllPeopleFromDatabase()
        {
            TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var testPerson = new Person("Tester", 1000m, 800m);

            await _personService.SavePerson(testPerson.Name, testPerson.GrossIncome, testPerson.NetIncome);

            var sut = await _personService.GetAllPeople();
            transaction.Dispose();

            Assert.NotEmpty(sut);
            
        }

        [Fact]
        public async void DeletePersonFromDatabase()
        {
            TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var name = "Tester";
            var grossIncome = 1000m;
            var netIncome = 800m;

            var savePersonResult = await _personService.SavePerson(name, grossIncome, netIncome);
            var peopleInDb = await _personService.GetAllPeople();
            var personRetrievedFromDb = peopleInDb.Find(x => x.Name == name);

            var sut = await _personService.DeletePerson(personRetrievedFromDb.Id);
            transaction.Dispose();

            Assert.True(sut);

        }

        [Fact]
        public async void GetAPersonAndCorrespondingOutgoing()
        {
            TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var name = "Tester";
            var grossIncome = 1000m;
            var netIncome = 800m;

            await _personService.SavePerson(name, grossIncome, netIncome);
            var peopleInDb = await _personService.GetAllPeople();
            var testPerson = peopleInDb.Find(x => x.Name == name);
            await _personalOutgoingRepository.SavePersonalOutgoing("Test Outgoing", 10m, testPerson.Id);

            peopleInDb = await _personService.GetAllPeople();
            var sut = peopleInDb.Find(x => x.Name == name);
            transaction.Dispose();

            
            Assert.Equal("Test Outgoing", sut.PersonalOutgoings.First().Description);

            
        }

        [Fact]
        public async void GetPersonByIdFromDatabase()
        {
            //Arrange
            TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var name = "Tester";
            var grossIncome = 1000m;
            var netIncome = 800m;

            //Act
            await _personService.SavePerson(name, grossIncome, netIncome);
            var peopleInDb = await _personService.GetAllPeople();
            var testPerson = peopleInDb.Find(x => x.Name == name);
       

            var sut = await _personService.GetPersonById(testPerson.Id);
            transaction.Dispose();

            //Assert
            Assert.NotNull(sut);

        }



    }
}
