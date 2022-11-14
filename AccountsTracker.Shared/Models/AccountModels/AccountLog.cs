using System.ComponentModel.DataAnnotations;

namespace AccountsTracker.Models.AccountModels
{
    public class AccountLog
    {

        public int Id { get; set;  }
        public decimal AccountBalance { get; set; }
        public DateTime LogDate { get; set; }
        public int AccountId { get; set;  }

        public Account Account { get; set; }

        public AccountLog(decimal accountBalance, int accountId, DateTime logDate)
        {
            AccountBalance = accountBalance;
            AccountId = accountId;
            LogDate = logDate;
        }

        public AccountLog(int id, decimal accountBalance, DateTime logDate, int accountId)
        {
            Id = id;
            AccountBalance = accountBalance;
            LogDate = logDate;
            AccountId = accountId;
        }
    }


}
