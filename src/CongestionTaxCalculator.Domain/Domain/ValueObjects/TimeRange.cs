using System;

namespace CongestionTaxCalculator.Domain.ValueObjects;

public sealed record TimeRange
{
    public TimeOnly Start { get; }

    public TimeOnly End { get; }

    public TimeRange(TimeOnly start, TimeOnly end)
    {
        if (end < start)
        {
            throw new ArgumentException("End time must be greater than or equal to start time.", nameof(end));
        }

        Start = start;
        End = end;
    }

    public bool Contains(TimeOnly time) => time >= Start && time <= End;
}
