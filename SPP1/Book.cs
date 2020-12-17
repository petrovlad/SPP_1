using System;
using System.Globalization;
using NLog;

namespace SPP1 
{
    [Serializable]
    public class Book : IComparable<Book>, IEquatable<Book>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static string DEFAULT_AUTHOR = "WITHOUT AUTHOR";
        private static string DEFAULT_TITLE = "UNTITLED";
        private static string DEFAULT_PUBLISHER = "WITHOUT PUBLISHER";
        private static int DEFAULT_YEAR = 0;
        private static double DEFAULT_PRICE = 0.0;
        private static CultureInfo DEFAULT_CULTURE = new CultureInfo("byn");

        private string author;
        private string title;
        private string publisher;
        private YearFormat year;
        private PriceFormat price;
        private ISBNFormat isbn;

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
                    logger.Info("Author parameter was null, so set default Author.");

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
                    logger.Info("Title parameter was null, so set default Title.");

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
                    logger.Info("Publisher parameter was null, so set default Publisher.");

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
                    logger.Info("Year parameter was null, so created default YearFormat.");

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
                    logger.Info("Price parameter was null, so created default PriceFormat.");

                    price = new PriceFormat(DEFAULT_PRICE, DEFAULT_CULTURE);
                }
                else
                {
                    price = value;
                }
            }
        }

        public ISBNFormat ISBN
        {
            get
            {
                return isbn;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value.Value))
                {
                    logger.Error("ISBN parameter was null.");

                    throw new ArgumentException("ISBN can't be null!");
                }
                else
                {
                    isbn = value;
                }
            }
        }

        public Book()
        {
            logger.Debug($"Created new book: {this}.");
        }

        public Book(string _author, string _title, string _publisher, YearFormat _year, PriceFormat _price, ISBNFormat _ISBN)
        {
            Author = _author;
            Title = _title;
            Publisher = _publisher;
            Year = _year;
            Price = _price;
            ISBN = _ISBN;

            logger.Debug($"Created new book: {this}.");
        }

        public Book(string _author, string _title, string _publisher, int _year, double _price, string _cultureName, string _ISBN)
        {
            Author = _author;
            Title = _title;
            Publisher = _publisher;
            Year = new YearFormat(_year);
            Price = new PriceFormat(_price, new CultureInfo(_cultureName));
            ISBN = new ISBNFormat(_ISBN);

            logger.Debug($"Created new book: {this}.");
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
            return ISBNFormat.Compare(bookA.ISBN, bookB.ISBN);
        }

        public bool Equals(Book other)
        {
            if (other == null)
            {
                return false;
            }
            return this.ISBN.Equals(other.ISBN);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Book;
            if (other == null)
            {
                return false;
            }

            return this.ISBN.Equals(other.ISBN);
        }

        public override int GetHashCode()
        {
            return (this.ISBN.GetHashCode() + Author.GetHashCode() * 5);
        }
    }

}
