using Microsoft.EntityFrameworkCore;
using TestApi.Application.Models;

namespace TestApi.Persistence.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("dbo");

        modelBuilder.Entity<Customer>()
            .HasKey(e => e.customerId);  

        modelBuilder.Entity<Transaction>()
            .HasKey(e => e.transactionId);   
             
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
}