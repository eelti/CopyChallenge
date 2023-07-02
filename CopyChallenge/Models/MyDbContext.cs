using Microsoft.EntityFrameworkCore;

namespace CopyChallenge.Models;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public DbSet<SampleOrder> SampleOrders { get; set; }
    public DbSet<Player> Players { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SampleOrder>(e => { e.HasKey(k => k.ID); });
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Player>(e =>
        {
            e.HasKey(k => k.ID);
            e.Property(p => p.FirstName).HasMaxLength(50);
            e.Property(p => p.LastName).HasMaxLength(50);
            e.Property(p => p.Week1).HasPrecision(6, 2);
            e.Property(p => p.Week2).HasPrecision(6, 2);
            e.Property(p => p.Week3).HasPrecision(6, 2);
            e.Property(p => p.Week4).HasPrecision(6, 2);
            e.Property(p => p.Week5).HasPrecision(6, 2);
        });
    }
}