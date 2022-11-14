using AccountsTracker.Shared.Interfaces.Repositories;
using AccountsTracker.Shared.Interfaces.Services;
using AccountsTracker.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AccountsTracker.Web.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var people = await _personService.GetAllPeople();
            var model = new PersonViewModel();
            model.AddEditPersonViewModel = new AddEditPersonViewModel();
            model.People = people;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SavePerson(PersonViewModel person)
        {
            if(person.AddEditPersonViewModel.Id != 0)
            {
                await _personService.SavePerson(person.AddEditPersonViewModel.Id,person.AddEditPersonViewModel.Name, person.AddEditPersonViewModel.GrossIncome, person.AddEditPersonViewModel.NetIncome);
            }
            else
            {
                await _personService.SavePerson(person.AddEditPersonViewModel.Name, person.AddEditPersonViewModel.GrossIncome, person.AddEditPersonViewModel.NetIncome);
            }           
           return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> AddEditPerson(int personId = 0)
        {
            var model = new PersonViewModel();
            model.AddEditPersonViewModel = new AddEditPersonViewModel();

            if (personId != 0)
            {                
                var returnFromDb = await _personService.GetPersonById(personId);
                if(returnFromDb.Id != 0)
                {
                    model.AddEditPersonViewModel.Id = returnFromDb.Id;
                    model.AddEditPersonViewModel.Name = returnFromDb.Name;
                    model.AddEditPersonViewModel.GrossIncome = returnFromDb.GrossIncome;
                    model.AddEditPersonViewModel.NetIncome = returnFromDb.NetIncome;
                }  

            }

            return PartialView("_AddEditPerson", model);

            
        }

    }
}
