using AccountsTracker.Models.AccountModels;

namespace AccountsTracker.Web.ViewModels
{
    public class ChartDataViewModel
    {
        public int AccountId { get; set; }
        public string AccountName { get; set; }

        public List<AccountLog> AccountLogs { get; set; }
    }
}
