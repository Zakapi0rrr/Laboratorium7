using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorium7
{
    public class ContactContext : DbContext
    {
        public void EnsureDbCreated()
        {
            using (var context = new ContactContext())
            {
                context.Database.EnsureCreated();
            }

        }
        public DbSet<Person> Contacts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=contacts.db");
        }
    }
    
}
