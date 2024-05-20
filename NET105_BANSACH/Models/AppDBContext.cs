using Microsoft.EntityFrameworkCore;

namespace NET105_BANSACH.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> Options) : base(Options)
        {

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillDetails> BillDetails { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartDetails> CartsDetails { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder OptionsBuilder)
        {
            base.OnConfiguring(OptionsBuilder);
            IConfigurationRoot Configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            OptionsBuilder.UseSqlServer(Configuration.GetConnectionString("BookstoreDatabase"));
        }

        // protected override void OnModelCreating(ModelBuilder ModelBuilder)
        // {
        //     ModelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        // }
    }
}
