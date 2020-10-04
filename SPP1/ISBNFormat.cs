using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SPP1
{
    class ISBNFormat : IEquatable<ISBNFormat>
    {
        const byte STANDART_LENGTH = 13;

        private int GS1Prefix; // can be: 977, 978, 979. maybe use only 978? 
        private int countryCode;
        private int manufacterCode;
        private int productCode;
        private byte checkDigit;

        public string value;
        // user should enter correct full value, i dont give a shit
        public ISBNFormat()
        {

        }
        /*
        public ISBNFormat(string _ISBN)
        {
            if (_ISBN.Length != STANDART_LENGTH)
            {

            }
            // regexp for new standart: "ISBN(?:-13)?:?\x20*(?=.{17}$)97(?:8|9)([ -])\d{1,5}\1\d{1,7}\1\d{1,6}\1\d$"
            // check digits count??? is it necessary?
            Regex regex = new Regex("^[0-9]{1,5}[- ](?:[0-9]+[- ]){2}[0-9]$");
            if (!regex.IsMatch(_ISBN))
            {
                throw new Exception("Wrong ISBN!");
            }

            string[] buf = _ISBN.Split(new char[] { '-', ' ' });
            // init fields
            GS1Prefix = buf[0];
            manufacterCode = buf[1];
            productCode = buf[2];
            string checkD = buf[3];

            // find checkDigit
            calculateCheckDigit();
            if (!checkD.Equals(checkDigit))
            {
                throw new Exception("Wrong check digit!");
            }
        }
        public ISBNFormat(string _GS1Prefix, string _manufacterCode, string _productCode)
        {
            // init fields
            GS1Prefix = _GS1Prefix;
            manufacterCode = _manufacterCode;
            productCode = _productCode;

            // find checkDigit
            calculateCheckDigit();
        }
        public ISBNFormat(string _GS1Prefix, string _manufacterCode, string _productCode, string _checkDigit)
        {
            // init fields
            GS1Prefix = _GS1Prefix;
            manufacterCode = _manufacterCode;
            productCode = _productCode;
            // find checkDigit
            calculateCheckDigit();
            if (!_checkDigit.Equals(checkDigit))
            {
                throw new Exception("Wrong check digit!");
            }
        }
        protected void calculateCheckDigit()
        {
            int i = 1, sum = 0, j, num;
            for (j = 0; j < GS1Prefix.Length; j++)
            {
                num = (int)char.GetNumericValue(GS1Prefix[j]);
                sum += num * i;
                i++;
            }
            for (j = 0; j < manufacterCode.Length; j++)
            {
                num = (int)char.GetNumericValue(manufacterCode[j]);
                sum += num * i;
                i++;
            }
            for (j = 0; j < productCode.Length; j++)
            {
                num = (int)char.GetNumericValue(productCode[j]);
                sum += num * i;
                i++;
            }
            sum %= 11;
            checkDigit = sum.ToString();
        }*/
        public override string ToString()
        {
            return $"{GS1Prefix}-{manufacterCode}-{productCode}-{checkDigit}";
        }
        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            if (obj is ISBNFormat)
            {
                return ((ISBNFormat)obj).GS1Prefix.Equals(this.GS1Prefix)
                    && ((ISBNFormat)obj).manufacterCode.Equals(this.manufacterCode)
                    && ((ISBNFormat)obj).productCode.Equals(this.productCode)
                    && ((ISBNFormat)obj).checkDigit.Equals(this.checkDigit);
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return GS1Prefix.GetHashCode()
                + manufacterCode.GetHashCode()
                + productCode.GetHashCode()
                + checkDigit.GetHashCode();
        }
        public bool Equals(ISBNFormat isbn)
        {
            if (this == isbn)
                return true;
            if (isbn is ISBNFormat)
            {
                return isbn.GS1Prefix.Equals(this.GS1Prefix)
                    && isbn.manufacterCode.Equals(this.manufacterCode)
                    && isbn.productCode.Equals(this.productCode)
                    && isbn.checkDigit.Equals(this.checkDigit);
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
    }
}
