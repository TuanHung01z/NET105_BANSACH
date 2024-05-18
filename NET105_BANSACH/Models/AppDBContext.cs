using Microsoft.EntityFrameworkCore;
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
            base.OnConfiguring(OptionsBuilder);
            // OptionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Practice1_Codefirst_EFCore_NET105;Integrated Security=True;Connect Timeout=10;Encrypt=True;");
        }

        // protected override void OnModelCreating(ModelBuilder ModelBuilder)
        // {
        //     ModelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        // }
    }
}
