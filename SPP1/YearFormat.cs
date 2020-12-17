using System;
using NLog;

namespace SPP1
{
    [Serializable]
    public class YearFormat : IComparable<YearFormat>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public int Value;
        
        public YearFormat()
        {
            Value = 0;

            logger.Debug($"Created new YearFormat: {this}");
        }
        public YearFormat(int _value)
        {
            Value = _value;

            logger.Debug($"Created new YearFormat: {this}");
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
