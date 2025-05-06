using System.Windows;

namespace Laboratorium7
{
    /// <summary>
    /// Logika interakcji dla klasy FilterWindow.xaml
    /// </summary>
    public partial class FilterWindow : Window
    {
        public FilterWindow()
        {
            InitializeComponent();
        }
        public class Filter
        {
            public bool isEmail { get; set; }
            public bool isPhone { get; set; }
            public bool isLastName { get; set; }

        }

        public Filter SelectedFilter { get; set; }

        private void ApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            bool isEmail = EmailCheckBox.IsChecked == true ? true : (bool)false;
            bool isPhone = PhoneCheckBox.IsChecked == true ? true : (bool)false;

            bool isLastName = LastNameCheckBox.IsChecked == true ? true : (bool)false;

            SelectedFilter = new Filter
            {
                isEmail = isEmail,
                isPhone = isPhone,
                isLastName = isLastName
            };
            DialogResult = true;
        }
    }
}