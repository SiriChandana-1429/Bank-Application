using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccessLayer
{
    public class BankDataBaseContext:DbContext
    {
        public DbSet<Bank> Banks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }  
        public DbSet<AcceptedCurrency> AcceptedCurrencies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=BankDB;Trusted_Connection=True;");
        }
        
    }
}