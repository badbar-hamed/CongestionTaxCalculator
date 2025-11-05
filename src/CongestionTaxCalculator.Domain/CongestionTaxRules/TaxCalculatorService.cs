using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Domain.CongestionTaxRules
{
    public class TaxCalculatorService
    {
        private readonly CongestionTaxRule _rule;

        public TaxCalculatorService(CongestionTaxRule rule)
        {
            _rule = rule;
        }

        public decimal GetTax(Vehicle vehicle, IEnumerable<TollStationPass> passes)
        {
            if (_rule.IsTollFreeVehicle(vehicle))
                return 0;

            var orderedPasses = passes.OrderBy(p => p.Timestamp).ToList();
            if (!orderedPasses.Any())
                return 0;

            decimal total = 0;
            DateTime intervalStart = orderedPasses.First().Timestamp;
            decimal maxFee = 0;

            foreach (var pass in orderedPasses)
            {
                if (_rule.IsTollFreeDate(pass.Timestamp))
                    continue;

                var fee = GetTollFee(pass.Timestamp.TimeOfDay);

                var diff = (pass.Timestamp - intervalStart).TotalMinutes;

                if (diff <= 60)
                {
                    if (fee > maxFee)
                        maxFee = fee;
                }
                else
                {
                    total += maxFee;
                    intervalStart = pass.Timestamp;
                    maxFee = fee;
                }
            }

            total += maxFee;
            return Math.Min(total, _rule.DailyCap);
        }

        private decimal GetTollFee(TimeSpan time)
        {
            var period = _rule.TaxPeriods.FirstOrDefault(p => p.Includes(time));
            return period?.Fee ?? 0;
        }
    }
}
