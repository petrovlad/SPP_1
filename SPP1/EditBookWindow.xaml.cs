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
        public string NewAuthor { get; set; }
        public string NewTitle { get; set; }
        public string NewPublisher { get; set; }
        public string NewISBN { get; set; }

        internal PriceFormat NewPrice { get; set; }

        internal YearFormat NewYear { get; set; }

        public EditBookWindow()
        {
            InitializeComponent();

            cmbbxCulture.ItemsSource = CultureInfo.GetCultures(CultureTypes.NeutralCultures);
            string cultureName = NewPrice.Culture.Name;
            cmbbxCulture.SelectedItem = CultureInfo.GetCultureInfo(cultureName);

            txtAuthor.Text = NewAuthor;
            txtTitle.Text = NewTitle;
            txtPublisher.Text = NewPublisher;
            txtPrice.Text = NewPrice.Value.ToString();
            txtYear.Text = NewYear.Value.ToString();
            
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            int err = MainWindow.IsInputCorrect(txtYear.Text, txtPrice.Text, cmbbxCulture.SelectedItem.ToString());
            switch (err)
            {
                case 1:
                    MessageBox.Show("Invalid year value!");
                    return;
                case 2:
                    MessageBox.Show("Invalid price value!");
                    return;
                case 3:
                    MessageBox.Show("Invalid culture value!");
                    return;
            }
            NewAuthor = txtAuthor.Text;
            NewTitle = txtTitle.Text;
            NewPublisher = txtPublisher.Text;
            NewYear = new YearFormat(int.Parse(txtYear.Text));
            NewPrice = new PriceFormat(double.Parse(txtPrice.Text), new CultureInfo(cmbbxCulture.SelectedItem.ToString()));
            NewISBN = txtISBN.Text;

            DialogResult = true;
            Close();
        }
    }
}
