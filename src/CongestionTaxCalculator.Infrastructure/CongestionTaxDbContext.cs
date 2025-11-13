using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CongestionTaxCalculator.Domain.Common;
using CongestionTaxCalculator.Domain.CongestionTaxRules;
using CongestionTaxCalculator.Domain.Vehicles;
using CongestionTaxCalculator.Infrastructure.DbModels;
using Microsoft.EntityFrameworkCore;


namespace CongestionTaxCalculator.Infrastructure;

public class CongestionTaxDbContext : DbContext
{
    public CongestionTaxDbContext(DbContextOptions<CongestionTaxDbContext> options)
        : base(options)
    {
    }

    // --- DbSets (Tables) ---
    public DbSet<VehicleTypeDbModel> VehicleTypes { get; set; }
    public DbSet<VehicleDbModel> Vehicles { get; set; }
    public DbSet<PassDbModel> Passes { get; set; }

    public DbSet<RuleDbModel> Rules { get; set; }
    public DbSet<RulePeriodDbModel> RulePeriods { get; set; }
    public DbSet<ExemptVehicleTypeDbModel> ExemptVehicleTypes { get; set; }

    public DbSet<HolidayCalendarDbModel> HolidayCalendar { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // --- Apply All Configuration Classes Automatically ---
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CongestionTaxDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}


