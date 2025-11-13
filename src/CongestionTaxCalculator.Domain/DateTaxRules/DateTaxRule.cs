using CongestionTaxCalculator.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Domain.DateTaxRules
{
    public class DateTaxRule : AggregateRoot<int>
    {
        public IReadOnlySet<DateOnly> Holidays { get; }

        public DateTaxRule(IEnumerable<DateOnly> holidays)
        {
            Holidays = holidays.ToHashSet();
        }       


        public bool IsTollFreeDate(DateOnly date)
        => IsWeekend(date)
        || IsInJuly(date)
        || IsPublicHoliday(date)
        || IsDayBeforeHoliday(date);

        private bool IsInJuly(DateOnly date) => date.Month == 7;

        private bool IsWeekend(DateOnly date)
            => date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;

        private bool IsPublicHoliday(DateOnly date)
            => Holidays.Contains(date);

        private bool IsDayBeforeHoliday(DateOnly date)
            => Holidays.Contains(date.AddDays(1));

    }
}
