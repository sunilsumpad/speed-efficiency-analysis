using Microsoft.EntityFrameworkCore;

namespace DataGeneratorApi.Models;

public class BikeSpeedDataContext : DbContext
{
    public BikeSpeedDataContext(DbContextOptions<BikeSpeedDataContext> options)
        : base(options)
    {
    }

    public DbSet<BikeSpeedDataItem> BikeSpeedDataItems { get; set; } = null!;
}