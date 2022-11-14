using AccountsTracker.Models.PersonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsTracker.Tests.UnitTests.Models
{
    public class PersonalOutgoingShould
    {
        [Fact]
        public void ThrowExceptionIfDescriptionIsTooLong()
        {
            var longDescription = "qwertyuiopqwertyuiopqwertyuiopqwertyuiopqwertyuiopqwertyuiopqwertyuiop";
            Assert.Throws<Exception>(() => new PersonalOutgoings(longDescription, 10m));
        }
    }
}
