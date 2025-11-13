using CongestionTaxCalculator.Domain.Common;
using CongestionTaxCalculator.Infrastructure.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Infrastructure.Configurations;


public class ExemptVehicleTypeDbModelConfiguration : IEntityTypeConfiguration<ExemptVehicleTypeDbModel>
{
    public void Configure(EntityTypeBuilder<ExemptVehicleTypeDbModel> builder)
    {
        builder.ToTable("ExemptVehicleTypes");

        builder.HasKey(e => e.Id);

        // Index on RuleId (as specified)
        builder.HasIndex(e => e.RuleId);

  
        builder.HasIndex(e => new { e.VehicleTypeId, e.RuleId }).IsUnique();
    }
}