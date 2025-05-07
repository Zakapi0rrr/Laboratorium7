using System;
using System.Windows;

namespace Laboratorium7
{
    public partial class SearchWindow : Window
    {
        public string SelectedRadioButton { get; private set; }
        public string SearchText => SearchTextBox.Text;

        public SearchWindow()
        {
            InitializeComponent();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (EmailRadioButton.IsChecked == true)
            {
                SelectedRadioButton = "Email";
            }
            else if (PhoneRadioButton.IsChecked == true)
            {
                SelectedRadioButton = "Phone";
            }
            else if (LastNameRadioButton.IsChecked == true)
            {
                SelectedRadioButton = "LastName";
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SelectedRadioButton) || string.IsNullOrEmpty(SearchText))
            {
                MessageBox.Show("Proszę wybrać kryterium wyszukiwania i wprowadzić tekst.");
                return;
            }

            this.DialogResult = true;
            this.Close();
        }
    }
}
