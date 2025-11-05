using CongestionTaxCalculator.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Domain.CongestionTaxRules
{
    public class CongestionTaxRule: AggregateRoot
    {
        public decimal DailyCap { get; }
        public List<TaxPeriod> TaxPeriods { get; }
        public List<string> TollFreeVehicles { get; }

        private readonly IPublicHolidayProvider _publicHolidayProvider;

        public CongestionTaxRule(
            decimal dailyCap,
            List<TaxPeriod> taxPeriods,
            List<string> tollFreeVehicles,
            IPublicHolidayProvider publicHolidayProvider)
        {
            DailyCap = dailyCap;
            TaxPeriods = taxPeriods;
            TollFreeVehicles = tollFreeVehicles;
            _publicHolidayProvider = publicHolidayProvider;
        }

        public bool IsTollFreeVehicle(Vehicle vehicle)
            => TollFreeVehicles.Contains(vehicle.Type);

        public bool IsTollFreeDate(DateTime date)
            => IsWeekend(date)
            || IsInJuly(date)
            || _publicHolidayProvider.IsPublicHoliday(date)
            || _publicHolidayProvider.IsDayBeforeHoliday(date);

        private bool IsWeekend(DateTime date)
            => date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;

        private bool IsInJuly(DateTime date) => date.Month == 7;
    }
}
