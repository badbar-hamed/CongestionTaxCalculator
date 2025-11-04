using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CongestionTaxCalculator.Domain.ValueObjects;

public sealed record TaxRule
{
    private readonly IReadOnlyList<TaxRate> _rates;

    public Money DailyCap { get; }

    public IReadOnlyList<TaxRate> Rates => _rates;

    public TaxRule(IEnumerable<TaxRate> rates, Money dailyCap)
    {
        if (rates is null)
        {
            throw new ArgumentNullException(nameof(rates));
        }

        DailyCap = dailyCap ?? throw new ArgumentNullException(nameof(dailyCap));
        _rates = new ReadOnlyCollection<TaxRate>(rates.OrderBy(rate => rate.TimeRange.Start).ToList());
    }

    public Money GetFee(TimeOnly time)
    {
        foreach (var rate in _rates)
        {
            if (rate.AppliesTo(time))
            {
                return rate.Fee;
            }
        }

        return Money.Zero;
    }
}
