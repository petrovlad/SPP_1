using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPP1
{
    [Serializable]
    public class PriceFormat : IComparable<PriceFormat>
    {
        public double Value;
        public CultureInfo Culture;

        public PriceFormat()
        {
            Value = 0;
            Culture = new CultureInfo(1); 
        }

        public PriceFormat(double _value, CultureInfo _culture)
        {
            Value = _value;
            Culture = _culture;
        }

        public override string ToString()
        {
            return $"{Value} {Culture}";
        }

        public int CompareTo(PriceFormat other)
        {
            int result;
            if ((result = this.Culture.Name.CompareTo(other.Culture.Name)) == 0)
            {
                result = this.Value.CompareTo(other.Value);
            }
            return result;
        }
        public static int Compare(PriceFormat priceA, PriceFormat priceB)
        {
            return priceA.CompareTo(priceB);
        }
    }
}
