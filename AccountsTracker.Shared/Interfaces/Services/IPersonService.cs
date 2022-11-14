using AccountsTracker.Models.PersonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsTracker.Shared.Interfaces.Services
{
    public interface IPersonService
    {
        Task<bool> DeletePerson(int id);
        Task<List<Person>> GetAllPeople();
        Task<Person> GetPersonById(int id);
        Task<bool> SavePerson(string name, decimal grossIncome, decimal netIncome);
        Task<bool> SavePerson(int id, string name, decimal grossIncome, decimal netIncome);
    }
}
