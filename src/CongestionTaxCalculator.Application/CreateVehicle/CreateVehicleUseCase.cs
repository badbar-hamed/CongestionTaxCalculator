using CongestionTaxCalculator.Domain.CongestionTaxRules;
using CongestionTaxCalculator.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Application.CreateVehicle
{
    public class CreateVehicleUseCase
    {
        protected readonly IVehicleRepository _vehicleRepository;
        public CreateVehicleUseCase(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task ExecuteAsync(CreateVehicleRequest request)
        {
            var vehicle = new Vehicle(request.PlateNumber, request.VehicleType);


            await _vehicleRepository.Add(vehicle);
        }
    }
}
