using AccountsTracker.Models.AccountModels;

namespace AccountsTracker.Web.ViewModels
{
    public class AccountViewModel
    {
        public List<Account> Accounts { get; set; }
        public AccountLogViewModel AccountLogViewModel { get; set; }

    }
}
