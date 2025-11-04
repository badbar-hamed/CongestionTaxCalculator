using System;

namespace CongestionTaxCalculator.Domain.ValueObjects;

public sealed record TaxRate
{
    public TimeRange TimeRange { get; }

    public Money Fee { get; }

    public TaxRate(TimeRange timeRange, Money fee)
    {
        TimeRange = timeRange ?? throw new ArgumentNullException(nameof(timeRange));
        Fee = fee ?? throw new ArgumentNullException(nameof(fee));
    }

    public bool AppliesTo(TimeOnly time) => TimeRange.Contains(time);
}
