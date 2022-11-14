using AccountsTracker.Models.Calculator;
using AccountsTracker.Models.PersonModels;
using AccountsTracker.Shared.Models.Calculator;
using AccountsTracker.Shared.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsTracker.Tests.UnitTests.Calculator
{
    public class BalanceCalculatorShould
    {
        [Fact]
        public void CalculateCorrectAmountsinConstructor()
        {
            //Arrange
            var testPerson1 = new Person("Foo", 2000m, 1500m);
            var testPerson2 = new Person("Bar", 4000m, 3000m);

            var personalTransfers = new PersonalTransfers()
            {
                IndividualGiftPot = 50m,
                JointGiftPot = 30m,
                DisposableIncome = 270m
            };

            var sharedTransfers = new SharedTransfers()
            {
                HouseholdBills = 2150m
            };

            SavingPercentages savingPercentages = new SavingPercentages()
            {
                HouseFundPercentage = 0.1,
                SharesPercentage = 0.2,
                SavingsPercentage = 0.3,
                HolidayFundPercentage = 0.4
            };

            var pensionContributionPercentage = new PensionContributionPercentage();
            pensionContributionPercentage.Percentage = 0.04;

            var person1Account = new PersonalAccount(testPerson1, pensionContributionPercentage, personalTransfers, sharedTransfers);
            var person2Account = new PersonalAccount(testPerson2, pensionContributionPercentage, personalTransfers, sharedTransfers);

            var listOfAccounts = new List<PersonalAccount>();
            listOfAccounts.Add(person1Account);
            listOfAccounts.Add(person2Account);

            var balanceCalculator = new BalanceCalculator(listOfAccounts, savingPercentages);

            //Act


            //Assert

            Assert.Equal(0.33, person1Account.PercentageOfTotalIncome,2);
            Assert.Equal(717m, person1Account.AmountToPayHouseholdBills, 0);
            Assert.Equal(106m, person1Account.SavingAmounts.SavingsAmount, 2);
            Assert.Equal(141m, person1Account.SavingAmounts.HolidayFundAmount, 2);
        }
    }
}
