using AccountsTracker.Models.PersonModels;

namespace AccountsTracker.Web.ViewModels
{
    public class PersonViewModel
    {
        public List<Person> People { get; set; }

        public AddEditPersonViewModel AddEditPersonViewModel { get; set; }
    }
}
