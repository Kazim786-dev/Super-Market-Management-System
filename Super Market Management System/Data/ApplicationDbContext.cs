using Microsoft.EntityFrameworkCore;
using Super_Market_Management_System.Models;

namespace Super_Market_Management_System.Data
{
    public class ApplicationDbContext : DbContext
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-2AV6505\\SQLEXPRESS;Initial Catalog=Chinook;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<OrderProduct>().HasKey(op => new {op.orderid, op.ProdId});
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Brand> brands { get; set; }

        public DbSet<Product> products { get; set; }

        public DbSet<Cashier> Cashier { get; set; }
        public DbSet<Supplier> Supplier { get; set; }

        public DbSet<Order> orders { get; set; }

        public DbSet<OrderProduct> orderproduct { get; set; }

    }
}
