using System.ComponentModel.DataAnnotations.Schema;

namespace AccountsTracker.Models.PersonModels
{
    public class PersonalOutgoings
    {
        public int Id { get; set;  }
        public string Description { get; set;  }
        public decimal Amount { get; set; }
        public int PersonId { get; set; }

        public PersonalOutgoings(string description, decimal amount)
        {
            if(description.Length > 50)
            {
                throw new Exception("Description is too long");
            }
            Description = description;
            Amount = amount;
        }

        public PersonalOutgoings(string description, decimal amount, int personId)
        {
            Description = description;
            Amount = amount;
            PersonId = personId;
        }

        public PersonalOutgoings(int id, string description, decimal amount, int personId)
        {
            Id = id;
            Description = description;
            Amount = amount;
            PersonId = personId;
        }

    }
}
