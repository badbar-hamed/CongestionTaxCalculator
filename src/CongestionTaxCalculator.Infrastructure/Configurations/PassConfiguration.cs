using CongestionTaxCalculator.Domain.Vehicles;
using CongestionTaxCalculator.Infrastructure.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Infrastructure.Configurations;

public class PassDbModelConfiguration : IEntityTypeConfiguration<PassDbModel>
{
    public void Configure(EntityTypeBuilder<PassDbModel> builder)
    {
        builder.ToTable("Passes");

        builder.HasKey(x => new { x.VehicleId, x.Moment });

        builder.Property(e => e.Moment)
            .HasColumnType("datetime2(0)")
            .IsRequired();

        
        builder.HasIndex(e => e.VehicleId);        
    }
}
