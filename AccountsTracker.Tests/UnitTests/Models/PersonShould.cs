using AccountsTracker.Models.PersonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsTracker.Tests.UnitTests.Models
{
    public class PersonShould
    {
        [Fact]
        public void ThrowExceptionIfNameIsTooLong()
        {
            var longName = "qwertyuiopqwertyuiopqwertyuiopqwertyuiopqwertyuiopqwertyuiopqwertyuiop";
            Assert.Throws<Exception>(() => new Person(longName, 1000m, 800m));
        }

        [Fact]
        public void ThrowExceptionIfNetIsGreaterThanGross()
        {
            Assert.Throws<Exception>(() => new Person("Test", 1000m, 8000m));
        }
    }
}
