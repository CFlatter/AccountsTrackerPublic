using AccountsTracker.Models.PersonModels;
using AccountsTracker.Shared.Interfaces.Repositories;
using AccountsTracker.Shared.Interfaces.Services;
using AccountsTracker.Shared.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsTracker.Shared.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IPersonalOutgoingRepository _personalOutgoingRepository;

        public PersonService(IPersonRepository personRepository, IPersonalOutgoingRepository personalOutgoingRepository)
        {
            _personRepository = personRepository;
            _personalOutgoingRepository = personalOutgoingRepository;
        }
        public async Task<bool> DeletePerson(int id)
        {
            return await _personRepository.DeletePerson(id);
        }

        public async Task<List<Person>> GetAllPeople()
        {
            var allPeople = await _personRepository.GetAllPeople();
            
            foreach (var person in allPeople)
            {
                person.PersonalOutgoings = await _personalOutgoingRepository.GetPersonalOutgoingByPersonId(person.Id);
            }
   
            return allPeople;

        }

        public async Task<bool> SavePerson(string name, decimal grossIncome, decimal netIncome)
        {
            return await _personRepository.SavePerson(name, grossIncome, netIncome);
        }

        public async Task<bool> SavePerson(int id, string name, decimal grossIncome, decimal netIncome)
        {
            return await _personRepository.SavePerson(id, name, grossIncome, netIncome);
        }

        public async Task<Person> GetPersonById(int id)
        {
            return await _personRepository.GetPersonById(id);
        }
    }
}
