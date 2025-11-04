using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CongestionTaxCalculator.Domain.Entities;
using CongestionTaxCalculator.Domain.ValueObjects;

namespace CongestionTaxCalculator.Domain.Services;

public abstract class TaxRulesProvider
{
    public abstract TaxRule GetTaxRule(DateOnly date);

    public virtual bool IsTollFreeDate(DateOnly date)
    {
        if (IsWeekend(date))
        {
            return true;
        }

        if (date.Month == 7)
        {
            return true;
        }

        if (IsPublicHoliday(date))
        {
            return true;
        }

        if (IsDayBeforePublicHoliday(date))
        {
            return true;
        }

        return false;
    }

    public virtual bool IsTollFreeVehicle(Vehicle vehicle) => vehicle?.IsTollFree ?? true;

    protected virtual bool IsWeekend(DateOnly date) => date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;

    protected virtual bool IsDayBeforePublicHoliday(DateOnly date) => IsPublicHoliday(date.AddDays(1));

    protected abstract bool IsPublicHoliday(DateOnly date);
}

public sealed class GothenburgTaxRulesProvider : TaxRulesProvider
{
    private static readonly IReadOnlyList<TaxRate> DefaultRates = new ReadOnlyCollection<TaxRate>(
        new List<TaxRate>
        {
            new(new TimeRange(new TimeOnly(6, 0), new TimeOnly(6, 29)), Money.FromAmount(8m)),
            new(new TimeRange(new TimeOnly(6, 30), new TimeOnly(6, 59)), Money.FromAmount(13m)),
            new(new TimeRange(new TimeOnly(7, 0), new TimeOnly(7, 59)), Money.FromAmount(18m)),
            new(new TimeRange(new TimeOnly(8, 0), new TimeOnly(8, 29)), Money.FromAmount(13m)),
            new(new TimeRange(new TimeOnly(8, 30), new TimeOnly(14, 59)), Money.FromAmount(8m)),
            new(new TimeRange(new TimeOnly(15, 0), new TimeOnly(15, 29)), Money.FromAmount(13m)),
            new(new TimeRange(new TimeOnly(15, 30), new TimeOnly(16, 59)), Money.FromAmount(18m)),
            new(new TimeRange(new TimeOnly(17, 0), new TimeOnly(17, 59)), Money.FromAmount(13m)),
            new(new TimeRange(new TimeOnly(18, 0), new TimeOnly(18, 29)), Money.FromAmount(8m))
        });

    private static readonly Money DailyCap = Money.FromAmount(60m);

    private readonly Dictionary<int, HashSet<DateOnly>> _publicHolidays = new();

    public override TaxRule GetTaxRule(DateOnly date) => new TaxRule(DefaultRates, DailyCap);

    protected override bool IsPublicHoliday(DateOnly date) => GetPublicHolidays(date.Year).Contains(date);

    private HashSet<DateOnly> GetPublicHolidays(int year)
    {
        if (_publicHolidays.TryGetValue(year, out var holidays))
        {
            return holidays;
        }

        holidays = CreatePublicHolidays(year);
        _publicHolidays[year] = holidays;
        return holidays;
    }

    private static HashSet<DateOnly> CreatePublicHolidays(int year)
    {
        var holidays = new HashSet<DateOnly>();

        void Add(DateOnly date) => holidays.Add(date);

        Add(new DateOnly(year, 1, 1)); // New Year's Day
        Add(new DateOnly(year, 1, 6)); // Epiphany

        var easterSunday = CalculateEasterSunday(year);
        Add(easterSunday.AddDays(-2)); // Good Friday
        Add(easterSunday.AddDays(1)); // Easter Monday
        Add(easterSunday.AddDays(39)); // Ascension Day

        Add(new DateOnly(year, 5, 1)); // Labour Day
        Add(new DateOnly(year, 6, 6)); // National Day

        var midsummerEve = GetMidsummerEve(year);
        Add(midsummerEve);
        Add(midsummerEve.AddDays(1)); // Midsummer Day

        var allSaintsDay = GetAllSaintsDay(year);
        Add(allSaintsDay);

        Add(new DateOnly(year, 12, 24)); // Christmas Eve
        Add(new DateOnly(year, 12, 25)); // Christmas Day
        Add(new DateOnly(year, 12, 26)); // Boxing Day
        Add(new DateOnly(year, 12, 31)); // New Year's Eve

        return holidays;
    }

    private static DateOnly CalculateEasterSunday(int year)
    {
        var a = year % 19;
        var b = year / 100;
        var c = year % 100;
        var d = b / 4;
        var e = b % 4;
        var f = (b + 8) / 25;
        var g = (b - f + 1) / 3;
        var h = (19 * a + b - d - g + 15) % 30;
        var i = c / 4;
        var k = c % 4;
        var l = (32 + 2 * e + 2 * i - h - k) % 7;
        var m = (a + 11 * h + 22 * l) / 451;
        var month = (h + l - 7 * m + 114) / 31;
        var day = ((h + l - 7 * m + 114) % 31) + 1;
        return new DateOnly(year, month, day);
    }

    private static DateOnly GetMidsummerEve(int year)
    {
        var date = new DateOnly(year, 6, 19);
        while (date.DayOfWeek != DayOfWeek.Friday)
        {
            date = date.AddDays(1);
        }

        return date;
    }

    private static DateOnly GetAllSaintsDay(int year)
    {
        var date = new DateOnly(year, 10, 31);
        while (date.DayOfWeek != DayOfWeek.Saturday)
        {
            date = date.AddDays(1);
        }

        return date;
    }
}
