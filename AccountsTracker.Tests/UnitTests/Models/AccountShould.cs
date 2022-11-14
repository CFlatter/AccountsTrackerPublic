using AccountsTracker.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsTracker.Tests.UnitTests.Models
{
    public class AccountShould
    {
        [Fact]
        public void ThrowExceptionIfNameIsTooLong()
        {
            var longName = "qwertyuiopqwertyuiopqwertyuiopqwertyuiopqqqqqqwwwwertttttyyyyyyyyuuuuuuiuyiq3riqh";
            Assert.Throws<Exception>(() => new Account(longName));
        }
    }
}
