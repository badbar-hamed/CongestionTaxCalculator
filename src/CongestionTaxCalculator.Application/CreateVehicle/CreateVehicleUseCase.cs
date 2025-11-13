using CongestionTaxCalculator.Domain.CongestionTaxRules;
using CongestionTaxCalculator.Domain.Repositories;
using CongestionTaxCalculator.Domain.Vehicles;
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
        protected readonly IVehicleTypeRepository _vehicleTypeRepository;
        public CreateVehicleUseCase(IVehicleRepository vehicleRepository, IVehicleTypeRepository vehicleTypeRepository)
        {
            _vehicleRepository = vehicleRepository;
            _vehicleTypeRepository = vehicleTypeRepository;
        }

        public async Task ExecuteAsync(CreateVehicleRequest request)
        {
            var type = await _vehicleTypeRepository.GetByTitleAsync(request.VehicleType);

            if (type == null)
                throw new InvalidOperationException("Invalid vehicle type.");

            // ساخت دامین مدل واقعی
            var vehicle = new Vehicle(request.PlateNumber, type);

            await _vehicleRepository.Add(vehicle);
        }
    }
}
