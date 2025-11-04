using System;

namespace CongestionTaxCalculator.Domain.ValueObjects;

public sealed record Money
{
    public decimal Amount { get; }

    public string Currency { get; }

    public static Money Zero { get; } = new(0m, DefaultCurrency);

    private const string DefaultCurrency = "SEK";

    private Money(decimal amount, string currency)
    {
        if (string.IsNullOrWhiteSpace(currency))
        {
            throw new ArgumentException("Currency must be provided.", nameof(currency));
        }

        Amount = decimal.Round(amount, 2, MidpointRounding.AwayFromZero);
        Currency = currency.Trim().ToUpperInvariant();
    }

    public static Money FromAmount(decimal amount, string currency = DefaultCurrency) => new(amount, currency);

    public bool IsZero => Amount == 0m;

    public static Money Min(Money left, Money right)
    {
        EnsureSameCurrency(left, right);
        return left.Amount <= right.Amount ? left : right;
    }

    public static Money Max(Money left, Money right)
    {
        EnsureSameCurrency(left, right);
        return left.Amount >= right.Amount ? left : right;
    }

    public static Money operator +(Money left, Money right)
    {
        EnsureSameCurrency(left, right);
        return new Money(left.Amount + right.Amount, left.Currency);
    }

    public static Money operator -(Money left, Money right)
    {
        EnsureSameCurrency(left, right);
        return new Money(left.Amount - right.Amount, left.Currency);
    }

    public static bool operator >(Money left, Money right)
    {
        EnsureSameCurrency(left, right);
        return left.Amount > right.Amount;
    }

    public static bool operator <(Money left, Money right)
    {
        EnsureSameCurrency(left, right);
        return left.Amount < right.Amount;
    }

    public static bool operator >=(Money left, Money right)
    {
        EnsureSameCurrency(left, right);
        return left.Amount >= right.Amount;
    }

    public static bool operator <=(Money left, Money right)
    {
        EnsureSameCurrency(left, right);
        return left.Amount <= right.Amount;
    }

    public override string ToString() => $"{Amount:0.##} {Currency}";

    private static void EnsureSameCurrency(Money left, Money right)
    {
        if (!string.Equals(left.Currency, right.Currency, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException($"Cannot operate on different currencies ({left.Currency} vs {right.Currency}).");
        }
    }
}
