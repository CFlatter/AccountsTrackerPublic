using AccountsTracker.Models.Calculator;
using AccountsTracker.Shared.Settings;
using Microsoft.Extensions.Options;

namespace AccountsTracker.Tests.UnitTests.Calculator
{
    public class SavingAmountsShould
    {
        [Fact]
        public void CalculateCorrectAmountsinConstructor()
        {
            //Arrange
            SavingPercentages savingPercentages = new SavingPercentages()
            {
                HouseFundPercentage = 0.1,
                SharesPercentage = 0.2,
                SavingsPercentage = 0.3,
                HolidayFundPercentage = 0.4                
            };

            decimal availableBalance = 500.00m;
           

            var expectedHouseFundAmount = 500m * 0.1m;
            var expectedSharesAmount = 500m * 0.2m;
            var expectedSavingsAmount = 500m * 0.3m;
            var expectedHolidayFund = 500m * 0.4m;

            SavingAmounts savingAmounts = new SavingAmounts(availableBalance, savingPercentages);

            var totalsavingAmount = savingAmounts.SavingsAmount + savingAmounts.SharesAmount + savingAmounts.HouseFundAmount + savingAmounts.HolidayFundAmount;

            //Act

            

            //Assert
            Assert.Equal(expectedHouseFundAmount, savingAmounts.HouseFundAmount);
            Assert.Equal(expectedSharesAmount, savingAmounts.SharesAmount);
            Assert.Equal(expectedSavingsAmount, savingAmounts.SavingsAmount);
            Assert.Equal(expectedHolidayFund, savingAmounts.HolidayFundAmount);

            Assert.Equal(availableBalance, totalsavingAmount);
        }
    }
}
