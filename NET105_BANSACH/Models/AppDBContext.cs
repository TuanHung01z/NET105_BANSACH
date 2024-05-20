using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace NET105_BANSACH.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext()
        {

        }
        public AppDBContext(DbContextOptions<AppDBContext> Options) : base(Options)
        {

        }

        public DbSet<Account>? Accounts { get; set; }
        public DbSet<Bill>? Bills { get; set; }
        public DbSet<BillDetails>? BillDetails { get; set; }
        public DbSet<Cart>? Carts { get; set; }
        public DbSet<CartDetails>? CartsDetails { get; set; }
        public DbSet<Book>? Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder OptionsBuilder)
        {
            OptionsBuilder.UseSqlServer("Data Source=DESKTOP-QM5ES3L\\HUNGTUAN;Initial Catalog=Ontap320021;Integrated Security=True; TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
