using CongestionTaxCalculator.Domain.CongestionTaxRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Domain.Repositories
{
    public interface ITollStationPassRepository
    {
        Task<IEnumerable<TollStationPass>> GetVehicleAllPassOnDay(Guid vehicleId,DateOnly date);


        Task AddRange(IEnumerable<TollStationPass> tollStationPasses);
    }
}
