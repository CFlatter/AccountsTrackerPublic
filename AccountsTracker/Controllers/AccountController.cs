using AccountsTracker.Models.AccountModels;
using AccountsTracker.Shared.Interfaces.Repositories;
using AccountsTracker.Shared.Interfaces.Services;
using AccountsTracker.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Nodes;
using System.Web.WebPages;

namespace AccountsTracker.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IAccountLogRepository _accountLogRepository;

        public AccountController(IAccountService accountService, IAccountLogRepository accountLogRepository)
        {
            _accountService = accountService;
            _accountLogRepository = accountLogRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new AccountViewModel();
            model.Accounts = await _accountService.GetAllAccounts();
            model.AccountLogViewModel = new AccountLogViewModel();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AccountLogHistory(int accountId)
        {
            var model = new AccountLogHistoryViewModel();
            model.AccountLogs = await _accountLogRepository.GetAllAccountLogs(accountId);

            ViewBag.AccountName = (await _accountService.GetAccountById(accountId)).AccountName;

            return View("AccountLogHistory", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddEditAccount(int id = 0)
        {
            var model = new AddEditAccountViewModel();
            if (id != 0)
            {
                var result = await _accountService.GetAccountById(id);
                model.AccountName = result.AccountName;
                model.Id = id;
            }
            return PartialView("_AddEditAccount", model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAccount(AddEditAccountViewModel model)
        {
            if (model.Id == 0)
            {
                await _accountService.SaveAccount(model.AccountName);
            }
            else
            {
                await _accountService.SaveAccount(model.Id, model.AccountName);
            }

            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> DeleteAccount(int id)  
        {
            await _accountService.DeleteAccount(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddEditAccountLog(int accountLogId)
        {
            var model = new AccountLogViewModel();
            model.AccountId = accountLogId;
            if(accountLogId != 0)
            {
                //TODO
                //var result = _accountLogRepository.GetAccountLogById(accountLogId);
                //model.Id = accountLogId;
                //model.AccountBalance = result.AccountBalance;
                //model.LogDate = result.LogDate
            }

            return PartialView("_AddEditAccountLog", model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAccountLog(AccountLogViewModel model)
        {
            var accountLog = new AccountLog(model.AccountBalance, model.AccountId, model.LogDate);
            await _accountLogRepository.SaveAccountLog(accountLog.AccountBalance, accountLog.LogDate, accountLog.AccountId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAccountLog(int accountLogId, int accountId)
        {
            await _accountLogRepository.DeleteAccountLog(accountLogId);
            return RedirectToAction("AccountLogHistory", new { accountId = accountId });
        }

        [HttpGet]
        public async Task<Dictionary<string, List<AccountLog>>> GetChartData()
        {            
            var dictionary = new Dictionary<string, List<AccountLog>>();
            var accounts = await _accountService.GetAllAccounts();

            foreach(var i in accounts)
            {
                var accountLogs = await _accountLogRepository.GetAllAccountLogs(i.Id);
                accountLogs = accountLogs.OrderBy(x => x.LogDate).ToList();
                dictionary.Add(i.AccountName, accountLogs);
            }

            //var chartJsonData = JsonConvert.SerializeObject(dictionary); 
            //return chartJsonData;

            return dictionary;


        }
    }
}
