using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CongestionTaxCalculator.Domain.CongestionTaxRules;
using CongestionTaxCalculator.Infrastructure.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CongestionTaxCalculator.Infrastructure.Configurations;



public class RulePeriodDbModelConfiguration : IEntityTypeConfiguration<RulePeriodDbModel>
{
    public void Configure(EntityTypeBuilder<RulePeriodDbModel> builder)
    {
        builder.ToTable("RulePeriods");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Start).IsRequired();
        builder.Property(e => e.End).IsRequired();
        builder.Property(e => e.Fee)
               .HasColumnType("decimal(18,2)")
               .IsRequired();

        // Index on RuleId (as specified)
        builder.HasIndex(e => e.RuleId);    
    }
}
