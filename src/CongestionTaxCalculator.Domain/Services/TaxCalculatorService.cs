using CongestionTaxCalculator.Domain.DateTaxRules;
using CongestionTaxCalculator.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Domain.Services
{

    public class TaxCalculatorService: ITaxCalculatorService
    {
        protected readonly ICalendarRepository _calendarRepository;
        protected readonly ITaxRuleRepository _taxRuleRepository;
        protected readonly IVehicleRepository _vehicleRepository;
        protected readonly ITollStationPassRepository _tollStationPassRepository;

        public TaxCalculatorService(ICalendarRepository calendarRepository)
        {
            _calendarRepository = calendarRepository;
        }


        public async Task<decimal> CalculateTax(string vehiclePlate,DateOnly date)
        {
            var holidays = await _calendarRepository.GetHolidays();

            var dateRule = new DateTaxRule(holidays);


            if (dateRule.IsTollFreeDate(date))
                return 0m;

            var congestionRule = await _taxRuleRepository.Get();

            if (congestionRule == null)
                throw new InvalidOperationException("No tax rule defined");

            var vehicle = await _vehicleRepository.GetByLicensePlateAsync(vehiclePlate);

            if (vehicle == null)
                throw new InvalidOperationException($"Vehicle with plate {vehiclePlate} not found");

            if (congestionRule.IsExemptVehicle(vehicle.Type))
                return 0m;

            var passes = await _tollStationPassRepository.GetVehicleAllPassOnDay(vehicle.Id, date);

            return congestionRule.CalculateTax(vehicle, passes);
        }
    }
}
