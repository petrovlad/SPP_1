﻿using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SPP1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private BooksList booksList;
        public MainWindow()
        {
            InitializeComponent();

            cmbbxCulture.ItemsSource = CultureInfo.GetCultures(CultureTypes.NeutralCultures);
            cmbbxCulture.SelectedItem = CultureInfo.GetCultureInfo("byn");
            
            booksList = new BooksList();
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAddBook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int err = IsInputCorrect(txtYear.Text, txtPrice.Text, cmbbxCulture.SelectedItem.ToString(), txtISBN.Text);
                switch (err)
                {
                    case 1:
                        throw new Exception("Invalid year value!");
                    case 2:
                        throw new Exception("Invalid price value!");
                    case 3:
                        throw new Exception("Invalid culture value!");
                    case 4:
                        throw new Exception("Invalid ISBN value!");
                }
                // create and initialize book
                Book book = new Book();
                book.Author = txtAuthor.Text;
                book.Title = txtTitle.Text;
                book.Publisher = txtPublisher.Text;
                book.Year = new YearFormat(int.Parse(txtYear.Text));
                book.Price = new PriceFormat(double.Parse(txtPrice.Text), new CultureInfo(cmbbxCulture.SelectedItem.ToString()));
                book.ISBN = new ISBNFormat(txtISBN.Text);

                if (booksList.IndexOf(book) != -1)
                {
                    throw new Exception("Such book already exists!");
                }
                
                booksList.Add(book);
                RedrawGridBooks();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MenuItemSortByAuthor_Click(object sender, RoutedEventArgs e)
        {
            booksList.SortByAuthor();
            RedrawGridBooks();

        }
        private void MenuItemSortByTitle_Click(object sender, RoutedEventArgs e)
        {
            booksList.SortByTitle();
            RedrawGridBooks();
        }
        private void MenuItemSortByPublisher_Click(object sender, RoutedEventArgs e)
        {
            booksList.SortByPublisher();
            RedrawGridBooks();
        }
        private void MenuItemSortByYear_Click(object sender, RoutedEventArgs e)
        {
            booksList.SortByYear();
            RedrawGridBooks();
        }
        private void MenuItemSortByPrice_Click(object sender, RoutedEventArgs e)
        {
            booksList.SortByPrice();
            RedrawGridBooks();
        }
        private void MenuItemSortByISBN_Click(object sender, RoutedEventArgs e)
        {
            booksList.SortByISBN();
            RedrawGridBooks();
        }

        private void RedrawGridBooks()
        {
            gridBooks.Items.Clear(); //????
            for (int i = 0; i < booksList.Count; i++)
            {
                gridBooks.Items.Add(booksList.ElementAt(i));
            }
        }

        private void btnEditBook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (gridBooks.SelectedItem != null)
                {
                    EditBookWindow editBookWindow = new EditBookWindow();
                    // create new window znd init it
                    Book editedBook = booksList.ElementAt(gridBooks.SelectedIndex);
                    editBookWindow.Owner = this;
                    editBookWindow.NewAuthor = editedBook.Author;
                    editBookWindow.NewTitle = editedBook.Title;
                    editBookWindow.NewPublisher = editedBook.Publisher;
                    editBookWindow.NewYear = editedBook.Year;
                    editBookWindow.NewPrice = editedBook.Price;
                    editBookWindow.NewISBN = editedBook.ISBN;
                    editBookWindow.FillOldValues(booksList, gridBooks.SelectedIndex);

                    if ((bool)editBookWindow.ShowDialog())
                    {
                        editedBook.Author = editBookWindow.NewAuthor;
                        editedBook.Title = editBookWindow.NewTitle;
                        editedBook.Publisher = editBookWindow.NewPublisher;
                        editedBook.Year = editBookWindow.NewYear;
                        editedBook.Price = editBookWindow.NewPrice;
                        editedBook.ISBN = editBookWindow.NewISBN;

                        RedrawGridBooks();
                    }
                
                }
                else
                {
                    throw new Exception("No one book is selected!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // check strings for null/spaces is unnecessary, because ibook can be untitled/unauthorized
        // this function checks price, year, culture and isbn
        // returns 0, if input is correct, otherwise returns number of invalid parameter
        public static int IsInputCorrect(string _year, string _price, string _culture, string _ISBN)
        {            
            // check year
            int year = 0;
            try
            {
                year = int.Parse(_year);
            }
            catch
            {
                return 1;
            }
            // check price
            double price = 0;
            try
            {
                price = double.Parse(_price);
            }
            catch
            {
                return 2;
            }
            // check culture
            CultureInfo culture = null;
            try
            {
                culture = new CultureInfo(_culture);
            }
            catch
            {
                return 3;
            }
            if (!ISBNFormat.IsISBNValid(_ISBN))
            {
                return 4;
            }
            return 0;
        }

        private void btnDeleteBook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (gridBooks.SelectedItem != null)
                {
                    booksList.RemoveAt(gridBooks.SelectedIndex);
                    RedrawGridBooks();
                }
                else
                {
                    throw new Exception("No one book is selected!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void MenuItemSave_Click(object sender, RoutedEventArgs e)
        {
            // is    as
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Binary files (*.bin)|*.bin|JSON files (*.json)|*.json";
            if ((bool)saveFileDialog.ShowDialog())
            {
                if (saveFileDialog.FileName.EndsWith(".bin"))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    using (FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate)) 
                    {  
                        formatter.Serialize(fileStream, booksList);
                    }
                } 
                else
                {
                    string json = JsonConvert.SerializeObject(booksList);
                    byte[] arr = Encoding.Default.GetBytes(json);
                    using (FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate))
                    {
                        fileStream.Write(arr, 0, arr.Length);
                    }
                }
            }
        }

        private void MenuItemOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Binary files (*.bin)|*.bin|JSON files (*.json)|*.json";
            if ((bool)openFileDialog.ShowDialog())
            {
                if (openFileDialog.FileName.EndsWith(".bin"))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    booksList.Clear();
                    using (FileStream fileStream = new FileStream(openFileDialog.FileName, FileMode.OpenOrCreate))
                    {
                        booksList = (BooksList)formatter.Deserialize(fileStream);
                    }
                    RedrawGridBooks();
                }
                else
                {
                    booksList.Clear();
                    using (FileStream fileStream = new FileStream(openFileDialog.FileName, FileMode.OpenOrCreate))
                    {
                        byte[] arr = new byte[fileStream.Length];
                        fileStream.Read(arr, 0, (int)fileStream.Length);
                        string json = Encoding.Default.GetString(arr);
                        booksList = JsonConvert.DeserializeObject<BooksList>(json);
                    }
                    RedrawGridBooks();
                }
             
            }
        }
    }
}
