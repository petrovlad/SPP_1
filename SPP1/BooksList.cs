using System;
using System.Collections.Generic;
using System.Linq;
using NLog;

namespace SPP1
{
    [Serializable]
    public class BooksList
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public List<Book> Books { get; private set; }
        public BooksList()
        {
            Books = new List<Book>();

            logger.Debug("Created new BooksList.");
        }

        public void Add(Book book)
        {
            Books.Add(book);

            logger.Debug($"Added book `{book}` to list.");
        }

        public void RemoveAt(int index)
        {
            Book book = Books.ElementAt(index);
            Books.RemoveAt(index);

            logger.Debug($"Removed book `{book}` from list.");
        }
        public Book ElementAt(int index)
        {
            return Books.ElementAt(index);
        }
        // -1 if not found
        public int IndexOf(Book book)
        {
            return Books.IndexOf(book);
        }
        public void Clear()
        {
            Books.Clear();

            logger.Debug("BooksList was cleared.");
        }

        public void Sort()
        {
            Books.Sort();

            logger.Debug("BooksList was sorted.");
        }

        public void SortByAuthor()
        {
            Books.Sort(Book.CompareByAuthor);
            
            logger.Debug("BooksList was sorted by author.");
        }

        public void SortByTitle()
        {
            Books.Sort(Book.CompareByTitle);

            logger.Debug("BooksList was sorted by title.");
        }
        public void SortByPublisher()
        {
            Books.Sort(Book.CompareByPublisher);

            logger.Debug("BooksList was sorted by publisher.");
        }
        public void SortByYear()
        {
            Books.Sort(Book.CompareByYear);

            logger.Debug("BooksList was sorted by year.");
        }
        public void SortByPrice()
        {
            Books.Sort(Book.CompareByPrice);

            logger.Debug("BooksList was sorted by price.");
        }
        public void SortByISBN()
        {
            Books.Sort(Book.CompareByISBN);

            logger.Debug("BooksList was sorted by ISBN.");
        }
        public int Count
        {
            get
            {
                return Books.Count;
            }
        }
    }
}
