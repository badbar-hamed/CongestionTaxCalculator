using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CongestionTaxCalculator.Infrastructure.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CongestionTaxCalculator.Infrastructure.Configurations;

public class HolidayCalendarDbModelConfiguration : IEntityTypeConfiguration<HolidayCalendarDbModel>
{
    public void Configure(EntityTypeBuilder<HolidayCalendarDbModel> builder)
    {
        builder.ToTable("HolidayCalendar");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Date)
            .HasConversion(
        v => v.ToDateTime(TimeOnly.MinValue),
        v => DateOnly.FromDateTime(v))
              .HasColumnType("Date")
               .IsRequired();

       
        builder.HasIndex(e => e.Date).IsUnique();
    }
}