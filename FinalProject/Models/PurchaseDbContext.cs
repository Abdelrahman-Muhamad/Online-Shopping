using FinalProject.Models;
using Microsoft.EntityFrameworkCore;
 

namespace FinalProject.Models
{
    public class PurchaseDbContext : DbContext
    {
        public PurchaseDbContext(DbContextOptions<PurchaseDbContext> options) : base(options) { }

        //public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }
    
    }
}
