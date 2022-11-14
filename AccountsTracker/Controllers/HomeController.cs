using AccountsTracker.Models.Calculator;
using AccountsTracker.Shared.Interfaces.Services;
using AccountsTracker.Shared.Models.Calculator;
using AccountsTracker.Shared.Settings;
using AccountsTracker.ViewModels;
using AccountsTracker.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace AccountsTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPersonService _personService;
        private readonly IOptions<PensionContributionPercentage> _pensionContributionPercentage;
        private readonly IOptions<PersonalTransfers> _personalTransfers;
        private readonly IOptions<SharedTransfers> _sharedTransfers;
        private readonly IOptions<SavingPercentages> _savingPercentages;

        public HomeController(ILogger<HomeController> logger, IPersonService personService, IOptions<PensionContributionPercentage> pensionContributionPercentage, IOptions<PersonalTransfers> personalTransfers, IOptions<SharedTransfers> sharedTransfers, IOptions<SavingPercentages> savingPercentages)
        {
            _logger = logger;
            _personService = personService;
            _pensionContributionPercentage = pensionContributionPercentage;
            _personalTransfers = personalTransfers;
            _sharedTransfers = sharedTransfers;
            _savingPercentages = savingPercentages;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomePageViewModel();
            model.PersonalAccounts = new List<PersonalAccount>();
            var people = await _personService.GetAllPeople();

            foreach(var person in people)
            {
                var personalAccount = new PersonalAccount(person, _pensionContributionPercentage.Value, _personalTransfers.Value, _sharedTransfers.Value);
                model.PersonalAccounts.Add(personalAccount);
            }

            var calculator = new BalanceCalculator(model.PersonalAccounts, _savingPercentages.Value);
            model.PersonalAccounts = calculator.HouseholdIncomes;

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}