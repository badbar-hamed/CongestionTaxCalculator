using CongestionTaxCalculator.Domain.Core;
using System.Data;



namespace CongestionTaxCalculator.Domain.CongestionTaxRules;



public class CongestionTaxRule : AggregateRoot
{
    public decimal DailyCap { get; }
    public int SingleChargePeriodMinutes { get;  }
    public IReadOnlyCollection<TaxPeriod> TaxPeriods { get; }
    public IReadOnlyCollection<VehicleType> TollExemptVehicles { get; }    

    public CongestionTaxRule(
        decimal dailyCap,
        int singleChargePeriodMinutes,
        IEnumerable<TaxPeriod> taxPeriods,
        IEnumerable<VehicleType> tollExemptVehicles)
    {
        DailyCap = dailyCap;
        SingleChargePeriodMinutes = singleChargePeriodMinutes;
        TaxPeriods = taxPeriods.ToList().AsReadOnly();
        TollExemptVehicles = tollExemptVehicles.ToList().AsReadOnly();        
    }

    /// <summary>
    /// Calculates the total congestion tax for a given vehicle and its daily passes.
    /// Applies toll exemptions, free dates, and the 60-minute single-charge rule.
    /// </summary>
    public decimal CalculateTax(Vehicle vehicle, IEnumerable<TollStationPass> passesForDay)
    {
        if (!passesForDay.Any()) return 0m;

        if (TollExemptVehicles.Contains(vehicle.Type)) return 0m;

        if (!AreAllPassesFromSameDay(passesForDay))
            throw new InvalidDataException("All passes must be within the same day to calculate daily tax.");        
        

        return CalculateDailyTax(passesForDay);
    }


    

    public bool IsExemptVehicle(VehicleType vehicleType)
    {
        return TollExemptVehicles.Contains(vehicleType);
    }


    /// <summary>
    /// Applies the 60-minute single-charge rule and daily cap.
    /// </summary>
    private decimal CalculateDailyTax(IEnumerable<TollStationPass> passes)
    {
        var ordered = passes.OrderBy(p => p.Timestamp).ToList();
        decimal total = 0;
        decimal maxFee = 0;
        DateTime windowStart = ordered.First().Timestamp;

        foreach (var pass in ordered)
        {
            var currentFee = GetTollFee(TimeOnly.FromDateTime(pass.Timestamp));
            var minutesSinceWindowStart = (pass.Timestamp - windowStart).TotalMinutes;

            if (minutesSinceWindowStart <= SingleChargePeriodMinutes)
            {
                maxFee = Math.Max(maxFee, currentFee);
            }
            else
            {
                total += maxFee;
                windowStart = pass.Timestamp;
                maxFee = currentFee;
            }
        }

        total += maxFee;
        return Math.Min(total, DailyCap);
    }

    /// <summary>
    /// Returns the applicable fee for a given time, or zero if outside charge periods.
    /// </summary>
    private decimal GetTollFee(TimeOnly time)
    {
        var period = TaxPeriods.FirstOrDefault(p => p.Includes(time));
        return period?.Fee ?? 0;
    }

    

    private bool AreAllPassesFromSameDay(IEnumerable<TollStationPass> passes)
    {
        var firstDate = DateOnly.FromDateTime(passes.First().Timestamp);
        return passes.All(p => DateOnly.FromDateTime(p.Timestamp) == firstDate);
    }
}