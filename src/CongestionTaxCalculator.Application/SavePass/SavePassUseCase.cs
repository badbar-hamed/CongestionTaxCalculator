using CongestionTaxCalculator.Application.CalculateTax;
using CongestionTaxCalculator.Domain.CongestionTaxRules;
using CongestionTaxCalculator.Domain.Repositories;
using CongestionTaxCalculator.Domain.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Application.SavePass
{
    public class SavePassUseCase
    {
        protected readonly ITollStationPassRepository _tollStationPassRepository;
        protected readonly IVehicleRepository _vehicleRepository;
        public SavePassUseCase(ITollStationPassRepository tollStationPassRepository, IVehicleRepository vehicleRepository)
        {
            _tollStationPassRepository = tollStationPassRepository;
            _vehicleRepository = vehicleRepository;
        }

        public async Task ExecuteAsync(SavePassRequest request)
        {
            var vehicle = await _vehicleRepository.GetByLicensePlateAsync(request.VehiclePlate);
            if (vehicle == null)
                throw new InvalidOperationException($"Vehicle with plate {request.VehiclePlate} not found");


            vehicle.RegisterPass(request.VehiclePassDate);            
        }
    }
}
