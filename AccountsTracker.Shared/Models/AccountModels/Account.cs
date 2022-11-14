using System.Xml.Linq;

namespace AccountsTracker.Models.AccountModels
{
    public class Account
    {
        public int Id { get; set; }
        public string AccountName { get; set; }

        public Account(string accountName)
        {
            if (accountName.Length > 50)
            {
                throw new Exception("Name is too long");
            }
            AccountName = accountName;
        }

        public Account(int id, string accountName)
        {
            Id = id;
            AccountName = accountName;
        }
    }
}
