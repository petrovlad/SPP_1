using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace SPP1
{
    [Serializable]
    class BooksList
    {
        private List<Book> books; // как правильно
        public BooksList()
        {
            books = new List<Book>();
        }

        public void Add(Book book)
        {
            books.Add(book);
        }

        public void RemoveAt(int index)
        {
            books.RemoveAt(index);
        }
        public Book ElementAt(int index)
        {
            return books.ElementAt(index);
        }

        public void Clear()
        {
            books.Clear();
        }

        public void Sort()
        {
            books.Sort();
        }

        public void SortByAuthor()
        {
            books.Sort(Book.CompareByAuthor);
        }

        public void SortByTitle()
        {
            books.Sort(Book.CompareByTitle);
        }
        public void SortByPublisher()
        {
            books.Sort(Book.CompareByPublisher);
        }
        public void SortByYear()
        {
            books.Sort(Book.CompareByYear);
        }
        public void SortByPrice()
        {
            books.Sort(Book.CompareByPrice);
        }
        public void SortByISBN()
        {
            books.Sort(Book.CompareByISBN);
        }
        public int Count
        {
            get
            {
                return books.Count;
            }
        }
    }
}
