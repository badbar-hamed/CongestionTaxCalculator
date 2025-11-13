using CongestionTaxCalculator.Domain.CongestionTaxRules;
using CongestionTaxCalculator.Infrastructure.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Infrastructure.Configurations;


public class RuleDbModelConfiguration : IEntityTypeConfiguration<RuleDbModel>
{
    public void Configure(EntityTypeBuilder<RuleDbModel> builder)
    {
        builder.ToTable("Rules");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.DailyCap)
               .HasColumnType("decimal(18,2)")
               .IsRequired()
               .HasDefaultValue(0m);

        builder.Property(e => e.SingleChargePeriodMinutes)
               .IsRequired();             

        // Relationship: One-to-Many with Periods
        builder.HasMany(e => e.Periods)
               .WithOne()
               .HasForeignKey(p => p.RuleId)
               .OnDelete(DeleteBehavior.Cascade);

        // Relationship: One-to-Many with ExemptVehicles
        builder.HasMany(e => e.ExemptVehicles)
               .WithOne() 
               .HasForeignKey(e => e.RuleId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}

