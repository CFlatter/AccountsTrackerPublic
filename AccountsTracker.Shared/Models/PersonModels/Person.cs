using System.Net;

namespace AccountsTracker.Models.PersonModels
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<PersonalOutgoings>? PersonalOutgoings { get; set; }

        public decimal GrossIncome { get; set; }

        public decimal NetIncome { get; set; }

        public Person(string name, decimal grossIncome, decimal netIncome)
        {
            if (name.Length > 50)
            {
                throw new Exception("Name is too long");
            }
            if (netIncome > grossIncome)
            {
                throw new Exception("Net income cannot be larger than gross income");
            }
            Name = name;
            GrossIncome = grossIncome;
            NetIncome = netIncome;
        }

        public Person(int id, string name, decimal grossIncome, decimal netIncome)
        {
            Id = id;
            Name = name;
            GrossIncome = grossIncome;
            NetIncome = netIncome;
        }

    }
}
