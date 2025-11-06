using CongestionTaxCalculator.Domain.CongestionTaxRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Domain.Repositories
{
    public interface IVehicleRepository
    {
        Task Add(Vehicle vehicle);
        Task<IEnumerable<Vehicle>> GetAll();

        Task<Vehicle> Get(Guid id);

        Task<Vehicle> GetByLicensePlateAsync(string licensePlate);
    }
}
