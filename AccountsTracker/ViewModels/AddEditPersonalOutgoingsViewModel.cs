namespace AccountsTracker.Web.ViewModels
{
    public class AddEditPersonalOutgoingsViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }

        public int PersonId { get; set; }
    }
}
