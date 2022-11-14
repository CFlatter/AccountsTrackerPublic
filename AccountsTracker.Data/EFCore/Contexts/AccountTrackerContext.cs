using AccountsTracker.Models.AccountModels;
using AccountsTracker.Models.PersonModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsTracker.Data.EFCore.Contexts
{
    public class AccountTrackerContext : DbContext
    {
        public AccountTrackerContext(DbContextOptions<AccountTrackerContext> options) :base(options)
        {
            Database.EnsureCreated();
        }


        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountLog> AccountLogs { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<PersonalOutgoings> PersonalOutgoings { get; set; }

    }
}
