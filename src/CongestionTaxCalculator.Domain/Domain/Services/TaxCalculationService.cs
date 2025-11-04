using System;
using CongestionTaxCalculator.Domain.Aggregates;
using CongestionTaxCalculator.Domain.ValueObjects;

namespace CongestionTaxCalculator.Domain.Services;

public sealed class TaxCalculationService
{
    private static readonly TimeSpan SingleChargeInterval = TimeSpan.FromMinutes(60);

    private readonly TaxRulesProvider _taxRulesProvider;

    public TaxCalculationService(TaxRulesProvider taxRulesProvider)
    {
        _taxRulesProvider = taxRulesProvider ?? throw new ArgumentNullException(nameof(taxRulesProvider));
    }

    public Money CalculateDailyTax(CongestionTax congestionTax)
    {
        if (congestionTax is null)
        {
            throw new ArgumentNullException(nameof(congestionTax));
        }

        if (_taxRulesProvider.IsTollFreeVehicle(congestionTax.Vehicle))
        {
            return Money.Zero;
        }

        if (_taxRulesProvider.IsTollFreeDate(congestionTax.Date))
        {
            return Money.Zero;
        }

        var taxRule = _taxRulesProvider.GetTaxRule(congestionTax.Date);
        if (taxRule is null)
        {
            return Money.Zero;
        }

        var passages = congestionTax.GetPassagesOrdered();
        if (passages.Count == 0)
        {
            return Money.Zero;
        }

        Money total = Money.Zero;
        DateTime? windowStart = null;
        Money windowMax = Money.Zero;

        foreach (var passage in passages)
        {
            var fee = taxRule.GetFee(passage.Time);
            if (fee.IsZero)
            {
                continue;
            }

            if (windowStart is null)
            {
                windowStart = passage.Timestamp;
                windowMax = fee;
                continue;
            }

            var elapsed = passage.Timestamp - windowStart.Value;
            if (elapsed <= SingleChargeInterval)
            {
                if (fee > windowMax)
                {
                    windowMax = fee;
                }
            }
            else
            {
                total += windowMax;
                windowStart = passage.Timestamp;
                windowMax = fee;
            }
        }

        if (windowStart is not null && !windowMax.IsZero)
        {
            total += windowMax;
        }

        return Money.Min(total, taxRule.DailyCap);
    }
}
