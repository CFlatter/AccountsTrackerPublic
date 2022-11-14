using AccountsTracker.Models.PersonModels;

namespace AccountsTracker.Web.ViewModels
{
    public class PersonalOutgoingsViewModel
    {
        public List<PersonalOutgoings> PersonalOutgoings { get; set; }

        public AddEditPersonalOutgoingsViewModel AddEditPersonalOutgoingsViewModel { get; set; }
    }
}
