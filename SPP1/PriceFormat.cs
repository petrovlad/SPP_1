using System;
using System.Globalization;
using NLog;

namespace SPP1
{
    [Serializable]
    public class PriceFormat : IComparable<PriceFormat>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public double Value;
        public CultureInfo Culture;

        public PriceFormat()
        {
            Value = 0;
            Culture = new CultureInfo(1);

            logger.Debug($"Created new PriceFormat: {this}");
        }

        public PriceFormat(double _value, CultureInfo _culture)
        {
            Value = _value;
            Culture = _culture;

            logger.Debug($"Created new PriceFormat: {this}");
        }

        public override string ToString()
        {
            return $"{Value} {Culture}";
        }

        public int CompareTo(PriceFormat other)
        {
            int result;
            if ((result = Culture.Name.CompareTo(other.Culture.Name)) == 0)
            {
                result = Value.CompareTo(other.Value);
            }
            return result;
        }
        public static int Compare(PriceFormat priceA, PriceFormat priceB)
        {
            return priceA.CompareTo(priceB);
        }
    }
}
