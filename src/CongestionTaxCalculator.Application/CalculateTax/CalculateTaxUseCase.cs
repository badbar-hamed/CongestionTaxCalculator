using CongestionTaxCalculator.Domain.CongestionTaxRules;
using CongestionTaxCalculator.Domain.DateTaxRules;
using CongestionTaxCalculator.Domain.Repositories;
using CongestionTaxCalculator.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Application.CalculateTax
{
    public class CalculateTaxUseCase
    {
        protected readonly ITaxCalculatorService _taxCalculatorService;


        public CalculateTaxUseCase(ITaxCalculatorService taxCalculatorService)
        {
            _taxCalculatorService = taxCalculatorService;
        }

        public async Task<CalculateTaxResponse> ExecuteAsync(CalculateTaxRequest request)
        {
            var price = await _taxCalculatorService.CalculateTax(request.VehiclePlate, request.Date);


            return new CalculateTaxResponse
            {
                TaxPrice = price
            };

        }
    }
}
