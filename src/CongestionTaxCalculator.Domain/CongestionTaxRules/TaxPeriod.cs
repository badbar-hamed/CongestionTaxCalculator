using CongestionTaxCalculator.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Domain.CongestionTaxRules
{
    
    public class TaxPeriod : ValueObject
    {
        public TimeOnly Start { get; }
        public TimeOnly End { get; }
        public decimal Fee { get; }
        private TaxPeriod() { }
        
        public TaxPeriod(TimeOnly start, TimeOnly end, decimal fee)
        {
            Start = start;
            End = end;
            Fee = fee;
        }

        public bool Includes(TimeOnly time)
            => time >= Start && time <= End;

        public bool Equals(TaxPeriod? other)
        {
            if (other is null) return false;
            return Start == other.Start && End == other.End && Fee == other.Fee;
        }

        public override bool Equals(object? obj) => Equals(obj as TaxPeriod);
        public override int GetHashCode() => HashCode.Combine(Start, End, Fee);

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Start;
            yield return End;
            yield return Fee;
        }
    }
}
