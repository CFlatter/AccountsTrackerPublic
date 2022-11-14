using AccountsTracker.Models.Calculator;
using AccountsTracker.Models.PersonModels;
using AccountsTracker.Shared.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsTracker.Shared.Models.Calculator
{
    public class PersonalAccount
    {
        public Person Person { get; }
        public decimal PensionContributionAmount { get; }
        public decimal IncomeAfterPensionDeduction { get; }
        public double PercentageOfTotalIncome { get; set; }

        public decimal AmountToPayHouseholdBills { get; set; }

        public PersonalTransfers PersonalTransfers { get; set; }
        public SharedTransfers SharedTransfers { get; set; }

        public SavingAmounts SavingAmounts { get; set; }

        public PersonalAccount(Person person, PensionContributionPercentage pensionContributionPercentage, PersonalTransfers personalTransfers, SharedTransfers sharedTransfers)
        {
            if (person == null)
            {
                throw new Exception("Please provide a person");
            }
            Person = person;
            PensionContributionAmount = Person.GrossIncome * (decimal)pensionContributionPercentage.Percentage;
            IncomeAfterPensionDeduction = Person.NetIncome - PensionContributionAmount;
            PercentageOfTotalIncome = 100.00;
            PersonalTransfers = new PersonalTransfers()
            {
                JointGiftPot = personalTransfers.JointGiftPot,
                IndividualGiftPot = personalTransfers.IndividualGiftPot,
                DisposableIncome = personalTransfers.DisposableIncome
                
            };
            SharedTransfers = sharedTransfers;
            SavingAmounts = new SavingAmounts();
        }

    }
}
