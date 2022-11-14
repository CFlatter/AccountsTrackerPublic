using AccountsTracker.Models.PersonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsTracker.Shared.Interfaces.Repositories
{
    public interface IPersonalOutgoingRepository
    {
        Task<bool> DeletePersonalOutgoing(int id);

        Task<List<PersonalOutgoings>> GetAllPersonalOutgoings();
        Task<PersonalOutgoings> GetPersonalOutgoingById(int id);
        Task<List<PersonalOutgoings>> GetPersonalOutgoingByPersonId(int personId);
        Task<bool> SavePersonalOutgoing(string description, decimal amount, int personId);
        Task<bool> SavePersonalOutgoing(int id, string description, decimal amount, int personId);
    }
}
