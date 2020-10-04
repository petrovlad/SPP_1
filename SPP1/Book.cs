using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SPP1 
{
    class Book : IComparable<Book>
    {
        private const string DEFAULT_AUTHOR = "WITHOUT AUTHOR";
        private const string DEFAULT_TITLE = "UNTITLED";
        private const string DEFAULT_PUBLISHER = "WITHOUT PUBLISHER";
        private const int DEFAULT_YEAR = 0;
        private const double DEFAULT_PRICE = 0.0;
        private const string DEFAULT_ISBN = "000000000";
        private static CultureInfo DEFAULT_CULTURE = new CultureInfo("byn");

        private string author;
        private string title;
        private string publisher;
        private YearFormat year;
        private PriceFormat price;
        private string isbn;

        public string Author
        {
            get
            {
                return author;
            }

            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    author = value;
                } 
                else
                {
                    author = DEFAULT_AUTHOR;
                }
            }
        }

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    title = value;
                }
                else
                {
                    title = DEFAULT_TITLE;
                }
            }
        }

        public string Publisher
        {
            get
            {
                return publisher;
            }

            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    publisher = value;
                }
                else
                {
                    publisher = DEFAULT_PUBLISHER;
                }
            }
        }

        public YearFormat Year
        {
            get
            {
                return year;
            }

            set
            {
                if (value == null)
                {
                    year = new YearFormat(DEFAULT_YEAR);
                }
                else
                {
                    year = value;
                }
            }
        }

        public PriceFormat Price
        {
            get
            {
                return price;
            }

            set
            {
                if (value == null)
                {
                    price = new PriceFormat(DEFAULT_PRICE, DEFAULT_CULTURE);
                }
                else
                {
                    price = value;
                }
            }
        }

        public string ISBN
        {
            get
            {
                return isbn;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    isbn = DEFAULT_ISBN;
                }
                else
                {
                    isbn = value;
                }
            }
        }

        public Book()
        {

        }

        public Book(string _author, string _title, string _publisher, YearFormat _year, PriceFormat _price, string _ISBN)
        {
            Author = _author;
            Title = _title;
            Publisher = _publisher;
            Year = _year;
            Price = _price;
            ISBN = _ISBN;  
        }

        public Book(string _author, string _title, string _publisher, int _year, double _price, string _cultureName, string _ISBN)
        {
            Author = _author;
            Title = _title;
            Publisher = _publisher;
            Year = new YearFormat(_year);
            Price = new PriceFormat(_price, new CultureInfo(_cultureName));
            ISBN = _ISBN;
        }

        public override string ToString()
        {
            return $"{Author} {Title} {Publisher} {Year} {Price} {ISBN}";
        }

        public int CompareTo(Book other)
        {
            int result;
            if ((result = CompareByAuthor(this, other)) == 0)
            {
                if ((result = CompareByTitle(this, other)) == 0)
                {
                    if ((result = CompareByPublisher(this, other)) == 0)
                    {
                        if ((result = CompareByYear(this, other)) == 0)
                        {
                            if ((result = CompareByPrice(this, other)) == 0)
                            {
                                result = CompareByISBN(this, other);
                            }
                        }
                    }
                }
            }
            return result;
        }

        public static int CompareByAuthor(Book bookA, Book bookB)
        {
            return string.Compare(bookA.Author, bookB.Author);
        }
        public static int CompareByTitle(Book bookA, Book bookB)
        {
            return string.Compare(bookA.Title, bookB.Title);
        }
        public static int CompareByPublisher(Book bookA, Book bookB)
        {
            return string.Compare(bookA.Publisher, bookB.Publisher);
        }
        public static int CompareByYear(Book bookA, Book bookB)
        {
            return YearFormat.Compare(bookA.Year, bookB.Year);
        }
        public static int CompareByPrice(Book bookA, Book bookB)
        {
            return PriceFormat.Compare(bookA.Price, bookB.Price);
        }
        public static int CompareByISBN(Book bookA, Book bookB)
        {
            return string.Compare(bookA.ISBN, bookB.ISBN);
        }

    }

}
