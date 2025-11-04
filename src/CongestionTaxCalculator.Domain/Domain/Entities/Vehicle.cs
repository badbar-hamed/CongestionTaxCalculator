using System;
using CongestionTaxCalculator.Domain.Enums;

namespace CongestionTaxCalculator.Domain.Entities;

public sealed class Vehicle
{
    public Vehicle(string registrationNumber, VehicleType type)
    {
        if (string.IsNullOrWhiteSpace(registrationNumber))
        {
            throw new ArgumentException("Registration number must be provided.", nameof(registrationNumber));
        }

        RegistrationNumber = registrationNumber.Trim().ToUpperInvariant();
        Type = type;
    }

    public string RegistrationNumber { get; }

    public VehicleType Type { get; }

    public bool IsTollFree => IsTollFreeType(Type);

    public static bool IsTollFreeType(VehicleType type) => type switch
    {
        VehicleType.Motorbike => true,
        VehicleType.Tractor => true,
        VehicleType.Emergency => true,
        VehicleType.Diplomat => true,
        VehicleType.Foreign => true,
        VehicleType.Military => true,
        VehicleType.Bus => true,
        _ => false
    };

    public override string ToString() => $"{RegistrationNumber} ({Type})";
}
