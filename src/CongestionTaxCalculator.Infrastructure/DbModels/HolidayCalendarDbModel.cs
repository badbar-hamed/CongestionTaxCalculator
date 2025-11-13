using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Infrastructure.DbModels
{
    public class HolidayCalendarDbModel
    {
        public int Id { get; set; }

        public DateOnly Date { get; set; }
    }
}
