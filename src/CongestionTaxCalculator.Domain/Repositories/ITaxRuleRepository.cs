using CongestionTaxCalculator.Domain.CongestionTaxRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Domain.Repositories;

public interface ITaxRuleRepository
{
    Task<CongestionTaxRule> Get();
}
