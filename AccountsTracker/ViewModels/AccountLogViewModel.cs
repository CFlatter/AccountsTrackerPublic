namespace AccountsTracker.Web.ViewModels
{
    public class AccountLogViewModel
    {
        public int Id { get; }
        public decimal AccountBalance { get; set; }
        public DateTime LogDate { get; set; }
        public int AccountId { get; set; }
    }
}
