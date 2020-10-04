using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPP1
{
    class PriceFormat : IComparable<PriceFormat>
    {
        public double Value { get; set; }
        public CultureInfo Culture { get; set; }

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
            return this.Value.CompareTo(other.Value);
        }

        public static int Compare(PriceFormat yearA, PriceFormat yearB)
        {
            return yearA.Value.CompareTo(yearB.Value);
        }
    }
}
