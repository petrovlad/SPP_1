using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPP1
{
    [Serializable]
    public class YearFormat : IComparable<YearFormat>
    {
        public int Value;
        
        public YearFormat()
        {
            Value = 0;
        }
        public YearFormat(int _value)
        {
            Value = _value;
        }
        public override string ToString()
        {
            return $"{Math.Abs(Value)} {(Value < 0 ? "BC" : "AC")}";
        }

        public int CompareTo(YearFormat other)
        {
            return this.Value.CompareTo(other.Value);
        }

        public static int Compare(YearFormat yearA, YearFormat yearB)
        {
            return yearA.Value.CompareTo(yearB.Value);
        }
    }
}
