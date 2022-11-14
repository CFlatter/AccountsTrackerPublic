using Microsoft.Extensions.Options;

namespace AccountsTracker.Shared.Settings
{
    public class SavingPercentages
    {

        public double SavingsPercentage { get; set; }

        public double SharesPercentage { get; set; }

        public double HouseFundPercentage { get; set; }

        public double HolidayFundPercentage { get; set; }

    }
}
