using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CongestionTaxCalculator.Domain.Vehicles;
using CongestionTaxCalculator.Infrastructure.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CongestionTaxCalculator.Infrastructure.Configurations;



public class VehicleDbModelConfiguration : IEntityTypeConfiguration<VehicleDbModel>
{
    public void Configure(EntityTypeBuilder<VehicleDbModel> builder)
    {
        builder.ToTable("Vehicles");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.LicensePlate)
               .IsRequired()
               .HasMaxLength(10);

        builder.HasIndex(x => x.LicensePlate)
        .IsUnique();


        
        builder.HasOne(e => e.Type)
               .WithMany() 
               .HasForeignKey(e => e.TypeId)
               .OnDelete(DeleteBehavior.Restrict);

      
        builder.HasMany(e => e.Passes)
               .WithOne()
               .HasForeignKey(p => p.VehicleId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}

