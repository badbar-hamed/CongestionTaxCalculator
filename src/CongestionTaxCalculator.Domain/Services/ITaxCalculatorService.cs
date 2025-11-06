namespace CongestionTaxCalculator.Domain.Services
{
    public interface ITaxCalculatorService
    {
        Task<decimal> CalculateTax(string vehiclePlate, DateOnly date);
    }
}
