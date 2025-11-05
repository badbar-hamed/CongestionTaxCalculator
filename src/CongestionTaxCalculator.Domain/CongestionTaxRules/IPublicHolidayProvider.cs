using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Domain.CongestionTaxRules
{
    public interface IPublicHolidayProvider
    {
        bool IsPublicHoliday(DateTime date);
        bool IsDayBeforeHoliday(DateTime date);
    }
}
