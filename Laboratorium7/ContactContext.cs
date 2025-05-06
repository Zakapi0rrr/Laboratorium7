using Microsoft.EntityFrameworkCore;

namespace Laboratorium7
{//niepotrzebna, context przeniesiony do mainwindow
    public class ContactContext : DbContext
    {

        public DbSet<Person> Contacts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=contacts.db");
        }
        
        }
    }


