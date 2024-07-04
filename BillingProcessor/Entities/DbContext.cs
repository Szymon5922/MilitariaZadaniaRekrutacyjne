using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BillingProcessor.Entities
{
    public class ApplicationDbContext : DbContext
    {
        private string _connectionString;
        public ApplicationDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        public DbSet<Order> OrderTable { get; set; }
        public DbSet<Billing> BillingsTable { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Order>()
                .HasIndex(o => new { o.storeId, o.orderId })
                .IsUnique();

            modelBuilder.Entity<Billing>()
                .HasIndex(b => new { b.typeId, b.orderId })
                .IsUnique();
        }
    }
}
