using Microsoft.EntityFrameworkCore;

namespace SpeedVsEfficiencyApi.Models;

public class BikeSpeedDataContext : DbContext
{
    public BikeSpeedDataContext(DbContextOptions<BikeSpeedDataContext> options)
        : base(options)
    {
    }

    public DbSet<BikeSpeedDataItem> BikeSpeedDataItems { get; set; } = null!;
}