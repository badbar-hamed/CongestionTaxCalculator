using CongestionTaxCalculator.Domain.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Domain.Repositories
{
    public interface ITollStationPassRepository
    {
        Task<IEnumerable<VehiclePass>> GetVehicleAllPassOnDay(int vehicleId,DateOnly date);


        Task AddRange(IEnumerable<VehiclePass> tollStationPasses);
    }
}
