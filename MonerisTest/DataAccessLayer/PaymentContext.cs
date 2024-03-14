



namespace MonerisTest.DataAccessLayer
{
    public class PaymentContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

      

        public PaymentContext()
        {
            SQLitePCL.Batteries.Init();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "moneristest.db3");
            optionsBuilder.UseSqlite($"Filename={dbPath}");
           
        }

        
    }
}
