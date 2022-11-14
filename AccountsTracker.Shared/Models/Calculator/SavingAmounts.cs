using AccountsTracker.Shared.Settings;

namespace AccountsTracker.Models.Calculator
{
    public class SavingAmounts
    {

        public decimal SavingsAmount { get;  }

        public decimal SharesAmount { get; }

        public decimal HouseFundAmount { get;  }

        public decimal HolidayFundAmount { get; }

        public SavingAmounts(decimal availableBalance, SavingPercentages savingPercentages)
        {
            if(availableBalance > 0)
            {
                SavingsAmount = availableBalance * (decimal)savingPercentages.SavingsPercentage;
                SharesAmount = availableBalance * (decimal)savingPercentages.SharesPercentage;
                HouseFundAmount = availableBalance * (decimal)savingPercentages.HouseFundPercentage;
                HolidayFundAmount = availableBalance * (decimal)savingPercentages.HolidayFundPercentage;
            }
            else
            {
                SavingsAmount = 0;
                SharesAmount = 0;
                HouseFundAmount = 0;
                HolidayFundAmount = 0;
            }


        }

        public SavingAmounts()
        {

        }
    }
}
