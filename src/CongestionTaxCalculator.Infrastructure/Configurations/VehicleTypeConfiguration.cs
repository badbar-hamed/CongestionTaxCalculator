using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CongestionTaxCalculator.Domain.Common;
using CongestionTaxCalculator.Infrastructure.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CongestionTaxCalculator.Infrastructure.Configurations;


public class VehicleTypeDbModelConfiguration : IEntityTypeConfiguration<VehicleTypeDbModel>
{
    public void Configure(EntityTypeBuilder<VehicleTypeDbModel> builder)
    {
        builder.ToTable("VehicleTypes");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
               .IsRequired()
               .HasMaxLength(64);
       
        builder.HasIndex(e => e.Title).IsUnique();
    }
}

