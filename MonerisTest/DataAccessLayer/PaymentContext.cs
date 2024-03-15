



namespace MonerisTest.DataAccessLayer
{
    public class PaymentContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public PaymentContext()
        {
            // will uncomment the below code if required
            //SQLitePCL.Batteries_V2.Init();
            Database.EnsureCreated();
        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "moneristest.db3");
            optionsBuilder.UseSqlite($"Filename={dbPath}");

        }


    }
}
