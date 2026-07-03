using Microsoft.EntityFrameworkCore;
using SafeWatchAI.API.Models;

namespace SafeWatchAI.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<VehicleEvent> VehicleEvents { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VehicleEvent>().Ignore(e => e.IndiceRiesgo);
    }
}
