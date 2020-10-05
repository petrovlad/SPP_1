using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SPP1
{
    /// <summary>
    /// Логика взаимодействия для EditBookWindow.xaml
    /// </summary>
    public partial class EditBookWindow : Window
    {
        private BooksList booksList;
        private int oldIndex;

        public string NewAuthor;
        public string NewTitle;
        public string NewPublisher;

        internal ISBNFormat NewISBN;
        internal PriceFormat NewPrice;
        internal YearFormat NewYear;

        public EditBookWindow()
        {
            InitializeComponent();

            cmbbxCulture.ItemsSource = CultureInfo.GetCultures(CultureTypes.NeutralCultures);
        }

        internal void FillOldValues(BooksList _booksList, int _oldIndex)
        {
            booksList = _booksList;
            oldIndex = _oldIndex;

            txtAuthor.Text = NewAuthor;
            txtTitle.Text = NewTitle;
            txtPublisher.Text = NewPublisher;
            txtPrice.Text = NewPrice.Value.ToString();
            cmbbxCulture.SelectedItem = CultureInfo.GetCultureInfo(NewPrice.Culture.ToString());
            txtYear.Text = NewYear.Value.ToString();
            txtISBN.Text = NewISBN.Value;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int err = MainWindow.IsInputCorrect(txtYear.Text, txtPrice.Text, cmbbxCulture.SelectedItem.ToString(), txtISBN.Text);
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
                NewAuthor = txtAuthor.Text;
                NewTitle = txtTitle.Text;
                NewPublisher = txtPublisher.Text;
                NewYear = new YearFormat(int.Parse(txtYear.Text));
                NewPrice = new PriceFormat(double.Parse(txtPrice.Text), new CultureInfo(cmbbxCulture.SelectedItem.ToString()));
                NewISBN = new ISBNFormat(txtISBN.Text);

                Book newBook = new Book(NewAuthor, NewTitle, NewPublisher, NewYear, NewPrice, NewISBN);
                // check if this book already exists
                if ((booksList.IndexOf(newBook) != -1) && (booksList.IndexOf(newBook) != oldIndex))
                {
                    throw new Exception("Such book already exists!");
                }

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
