using AccountsTracker.Models.PersonModels;
using AccountsTracker.Shared.Models.Calculator;
using AccountsTracker.Shared.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsTracker.Tests.UnitTests.Models
{
    public class PersonalAccountShould
    {
        [Fact]
        public void ThrowException()
        {
            Person? person = null;
            var pensionContributionPercentage = new PensionContributionPercentage();
            pensionContributionPercentage.Percentage = 0.04;
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


            Assert.Throws<Exception>(() => new PersonalAccount(person, pensionContributionPercentage, personalTransfers, sharedTransfers));
        }
    }
}
