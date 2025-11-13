using CongestionTaxCalculator.Domain.Core;

namespace CongestionTaxCalculator.Domain.Common
{
    public class VehicleType:ValueObject
    {
        
        public string Title { get; private set; }

        private VehicleType() { } 

        public VehicleType(string title)
        {
            Title = title;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Title.ToUpper();
        }
    }
}
