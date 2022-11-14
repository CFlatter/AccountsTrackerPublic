using AccountsTracker.Shared.Interfaces.Repositories;
using AccountsTracker.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Diagnostics.Contracts;
using System.Reflection;

namespace AccountsTracker.Web.Controllers
{
    public class PersonalOutgoingsController : Controller
    {
        private readonly IPersonalOutgoingRepository _personalOutgoingRepository;

        public PersonalOutgoingsController(IPersonalOutgoingRepository personalOutgoingRepository)
        {
            _personalOutgoingRepository = personalOutgoingRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int personId)
        {
            var model = new PersonalOutgoingsViewModel();
            model.AddEditPersonalOutgoingsViewModel = new AddEditPersonalOutgoingsViewModel();

            model.AddEditPersonalOutgoingsViewModel.PersonId = personId;
            model.PersonalOutgoings = await _personalOutgoingRepository.GetPersonalOutgoingByPersonId(personId);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SavePersonalOutgoing(PersonalOutgoingsViewModel model)
        {
            if(model.AddEditPersonalOutgoingsViewModel.Id == 0)
            {
                await _personalOutgoingRepository.SavePersonalOutgoing(model.AddEditPersonalOutgoingsViewModel.Description, model.AddEditPersonalOutgoingsViewModel.Amount, model.AddEditPersonalOutgoingsViewModel.PersonId);
            }
            else
            {
                await _personalOutgoingRepository.SavePersonalOutgoing(model.AddEditPersonalOutgoingsViewModel.Id,model.AddEditPersonalOutgoingsViewModel.Description, model.AddEditPersonalOutgoingsViewModel.Amount, model.AddEditPersonalOutgoingsViewModel.PersonId);
            }
            
            return RedirectToAction("Index", new { personId = model.AddEditPersonalOutgoingsViewModel.PersonId });
        }

        [HttpPost]
        public async Task<IActionResult> DeletePersonalOutgoing(int id, int personId)
        {
            await _personalOutgoingRepository.DeletePersonalOutgoing(id);
            return RedirectToAction("Index", new { personId = personId });
        }

        [HttpPost]
        public async Task<IActionResult> AddEditPersonalOutgoing(int personId, int id = 0)
        {
            var model = new PersonalOutgoingsViewModel();
            model.AddEditPersonalOutgoingsViewModel = new AddEditPersonalOutgoingsViewModel();

            model.AddEditPersonalOutgoingsViewModel.PersonId = personId;
            if(id != 0)
            {
                var returnFromDb = await _personalOutgoingRepository.GetPersonalOutgoingById(id);
                model.AddEditPersonalOutgoingsViewModel.Id = id;
                model.AddEditPersonalOutgoingsViewModel.Description = returnFromDb.Description;
                model.AddEditPersonalOutgoingsViewModel.Amount = returnFromDb.Amount;
            }

            return PartialView("_AddEditOutgoing", model);
        }
    }
}
