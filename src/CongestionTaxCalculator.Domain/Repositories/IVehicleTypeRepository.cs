using CongestionTaxCalculator.Domain.Common;

namespace CongestionTaxCalculator.Domain.Repositories
{
    public interface IVehicleTypeRepository
    {
        Task<VehicleType?> GetByIdAsync(int id);
        Task<VehicleType?> GetByTitleAsync(string title);
        Task<IReadOnlyList<VehicleType>> GetAllAsync();
    }

}
