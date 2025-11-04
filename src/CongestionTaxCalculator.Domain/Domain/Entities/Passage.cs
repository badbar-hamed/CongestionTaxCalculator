using System;

namespace CongestionTaxCalculator.Domain.Entities;

public sealed class Passage
{
    public Passage(DateTime timestamp)
    {
        if (timestamp == default)
        {
            throw new ArgumentException("Timestamp must be specified.", nameof(timestamp));
        }

        Timestamp = timestamp;
    }

    public DateTime Timestamp { get; }

    public DateOnly Date => DateOnly.FromDateTime(Timestamp);

    public TimeOnly Time => TimeOnly.FromDateTime(Timestamp);

    public override string ToString() => $"{Timestamp:G}";
}
