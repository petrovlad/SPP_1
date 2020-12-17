using NUnit.Framework;
using NLog;
using SPP1;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SPP1.Tests
{
	[TestFixture]
	public class BookTests
	{
		[TestCase("Author1", "Title1", "Publisher1", 2001, 99.99, "byn", "978-3-16-148410-0")]
		[TestCase("Author2", "Title2", "Publisher2", 2002, 199.99, "byn", "978-1-4028-9462-6")]
		public void CreateValidBookTest(string author, string title, string publisher, int year, double price, string cultureName, string ISBN)
		{
			Book book = new Book(author, title, publisher, year, price, cultureName, ISBN);

			Assert.IsTrue(book.Author.Equals(author) &&
							book.Title.Equals(title) &&
							book.Publisher.Equals(publisher) &&
							book.Year.Value.Equals(year) &&
							book.Price.Value.Equals(price) &&
							book.ISBN.Value.Equals(ISBN));
		}

		[TestCase("Author", "Title", "Publisher", 2001, 99.99, "byn", "invalid ISBN")]
		[TestCase("Author", "Title", "Publisher", 2001, 99.99, "byn", "978-3-16-148410-1")]
		public void CreateInvalidISBNBookTest(string author, string title, string publisher, int year, double price, string cultureName, string ISBN)
		{
			Assert.Throws<ArgumentException>(() => new Book(author, title, publisher, year, price, cultureName, ISBN), "Invalid ISBN value!");

		}

		[TestCase("Author", "", "Publisher", 2001, 99.99, "byn", "978-3-16-148410-0")]
		public void CreateEmptyTitleBookTest(string author, string title, string publisher, int year, double price, string cultureName, string ISBN)
		{
			Book book = new Book(author, title, publisher, year, price, cultureName, ISBN);

			Assert.IsTrue(book.Author.Equals(author) &&
							book.Title.Equals("UNTITLED") &&
							book.Publisher.Equals(publisher) &&
							book.Year.Value.Equals(year) &&
							book.Price.Value.Equals(price) &&
							book.ISBN.Value.Equals(ISBN));
		}

		[TestCase("", "Title", "Publisher", 2001, 99.99, "byn", "978-3-16-148410-0")]
		public void CreateEmptyAuthorBookTest(string author, string title, string publisher, int year, double price, string cultureName, string ISBN)
		{
			Book book = new Book(author, title, publisher, year, price, cultureName, ISBN);

			Assert.IsTrue(book.Author.Equals("WITHOUT AUTHOR") &&
							book.Title.Equals(title) &&
							book.Publisher.Equals(publisher) &&
							book.Year.Value.Equals(year) &&
							book.Price.Value.Equals(price) &&
							book.ISBN.Value.Equals(ISBN));
		}
	}

	[TestFixture]
	public class BooksListTests
	{
		BooksList booksList;

		[SetUp]
		public void SetUp() 
		{
			booksList = new BooksList();

			booksList.Add(new Book("Author3", "Title1", "Publisher2", 2001, 99.99, "byn", "978-3-16-148410-0"));
			booksList.Add(new Book("Author2", "Title2", "Publisher3", 2002, 199.99, "byn", "978-1-56619-909-4"));
			booksList.Add(new Book("Author1", "Title3", "Publisher1", 2000, 9.99, "byn", "978-1-4028-9462-6"));
		}

		[Test]
		public void SortByAuthorTest()
		{
			List<Book> expectedResult = new List<Book>(booksList.Books.OrderBy(x => x.Author));
			booksList.SortByAuthor();
			CollectionAssert.AreEqual(expectedResult, booksList.Books);
		}

		[Test]
		public void SortByTitleTest()
		{
			List<Book> expectedResult = new List<Book>(booksList.Books.OrderBy(x => x.Title));
			booksList.SortByTitle();
			CollectionAssert.AreEqual(expectedResult, booksList.Books);
		}

		[Test]
		public void SortByISBNTest()
		{
			List<Book> expectedResult = new List<Book>(booksList.Books.OrderBy(x => x.ISBN));
			booksList.SortByISBN();
			CollectionAssert.AreEqual(expectedResult, booksList.Books);
		}

	}

	[TestFixture]
	public class ISBNFormatTests
	{
		[TestCase("978-3-16-148410-0")]
		[TestCase("978-1-56619-909-4")]
		[TestCase("978-1-4028-9462-6")]
		[TestCase("978 1 4028 9462 6")]
		public void CreateValidISBNTest(string value)
		{
			ISBNFormat isbn = new ISBNFormat(value);
			Assert.AreEqual(value, isbn.Value);
		}

		[TestCase("Some random string")]
		[TestCase("978-1-4028-9462-0")] // wrong check digit
		[TestCase("777-1-4028-9462-0")] // wrong gs1 prefix
		[TestCase("9781402894626")]
		public void CreateInvalidISBNTest(string value)
		{
			Assert.Throws<ArgumentException>(() => new ISBNFormat(value), "Wrong ISBN value");
		}
	}

	[TestFixture]
	public class YearFormatTests
	{
		[TestCase(2001, ExpectedResult = "2001 AC")]
		[TestCase(0, ExpectedResult = "0 AC")]
		[TestCase(-2001, ExpectedResult = "2001 BC")]
		public string CreateYearTest(int value)
		{
			YearFormat year = new YearFormat(value);
			return year.ToString();
		}
	}
}