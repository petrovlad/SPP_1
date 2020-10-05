using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SPP1
{
    [Serializable]
    class ISBNFormat : IComparable<ISBNFormat>, IEquatable<ISBNFormat>
    {
        public static byte STANDART_LENGTH = 13;

        public int GS1Prefix { get; private set; }
        public int CountryCode { get; private set; }
        public int ManufacterCode { get; private set; }
        public int ProductCode { get; private set; }
        public int CheckDigit { get; private set; }

        public string Value; //?
        // user should enter correct full value
        public ISBNFormat()
        {

        }
        
        public ISBNFormat(string _ISBN)
        {
            if (!IsISBNValid(_ISBN))
            {
                throw new Exception("Invalid ISBN value");
            }
            Value = _ISBN;
            string[] buf = _ISBN.Split(new char[] { '-', ' ' });
            // init fields
            GS1Prefix = int.Parse(buf[0]);
            CountryCode = int.Parse(buf[1]);
            ManufacterCode = int.Parse(buf[2]);
            ProductCode = int.Parse(buf[3]);
            CheckDigit = int.Parse(buf[4]);
        }
        public ISBNFormat(int _GS1Prefix, int _countryCode, int _manufacterCode, int _productCode, byte _checkDigit)
        {
            throw new NotImplementedException();
        }
      
        protected void calculateCheckDigit()
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return $"{GS1Prefix}-{CountryCode}-{ManufacterCode}-{ProductCode}-{CheckDigit}";
        }
        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            if (obj is ISBNFormat)
            {
                return ((ISBNFormat)obj).GS1Prefix.Equals(this.GS1Prefix)
                    && ((ISBNFormat)obj).CountryCode.Equals(this.CountryCode)
                    && ((ISBNFormat)obj).ManufacterCode.Equals(this.ManufacterCode)
                    && ((ISBNFormat)obj).ProductCode.Equals(this.ProductCode)
                    && ((ISBNFormat)obj).CheckDigit.Equals(this.CheckDigit);
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
        public bool Equals(ISBNFormat other)
        {
            if (this == other)
                return true;
            if (other is ISBNFormat)
            {
                return other.GS1Prefix.Equals(this.GS1Prefix)
                    && other.CountryCode.Equals(this.CountryCode)
                    && other.ManufacterCode.Equals(this.ManufacterCode)
                    && other.ProductCode.Equals(this.ProductCode)
                    && other.CheckDigit.Equals(this.CheckDigit);
                //return (this.GetHashCode() == isbn.GetHashCode())
            }
            else
            {
                return false;
            }
        }

        public static bool IsISBNValid(string _ISBN)
        {
            string _ISBNWithoutSeparators = _ISBN.Replace(" ", "-").Replace("-", "");
            if (_ISBNWithoutSeparators.Length != STANDART_LENGTH)
            {
                return false;
            }
            // idk is that working, i hope yes
            // am i really need this?
            /*Regex regex = new Regex(@"ISBN(?:-13)?:?\x20*(?=.{17}$)97(?:8|9)([ -])\d{1,5}\1\d{1,7}\1\d{1,6}\1\d$");
            if (!regex.IsMatch(_ISBN))
            {
                return false;
            }*/
            // there should be 5 fields, separated by " " or "-"
            string[] buf = _ISBN.Split(new char[] { '-', ' ' });
            if (buf.Length != 5)
            {
                return false;
            }
            // try to init
            int _GS1Prefix;
            int _countryCode;
            int _manufacterCode;
            int _productCode;
            int _checkDigit;
            try
            {
                _GS1Prefix = int.Parse(buf[0]);
                _countryCode = int.Parse(buf[1]);
                _manufacterCode = int.Parse(buf[2]);
                _productCode = int.Parse(buf[3]);
                _checkDigit = int.Parse(buf[4]);
            }
            catch
            {
                return false;
            }
            // _GS1Prefix can be: 978, 979. maybe use only 978? 
            if (!((_GS1Prefix == 978) || (_GS1Prefix == 979))) 
            {
                return false;
            }
            // and now calculate checkDigit and check it
            int sum = 0;
            int correctCheckDigit;
            for (int i = 0; i < STANDART_LENGTH - 1; i += 2)
            {
                sum += (int)char.GetNumericValue(_ISBNWithoutSeparators[i]);
            }
            for (int i = 1; i < STANDART_LENGTH - 1; i += 2)
            {
                sum += (int)char.GetNumericValue(_ISBNWithoutSeparators[i]) * 3;
            }
            if ((sum % 10) == 0)
            {
                correctCheckDigit = 0;
            }
            else
            {
                correctCheckDigit = 10 - (sum % 10);
            }

            return (correctCheckDigit == _checkDigit);
        }
        public static bool IsISBNValid(int _GS1Prefix, int _countryCode, int _manufacterCode, int _productCode, byte _checkDigit)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(ISBNFormat other)
        {
            return this.Value.CompareTo(other.Value);
        }

        public static int Compare(ISBNFormat ISBNa, ISBNFormat ISBNb)
        {
            return ISBNa.Value.CompareTo(ISBNb.Value);
        }
    }
}
