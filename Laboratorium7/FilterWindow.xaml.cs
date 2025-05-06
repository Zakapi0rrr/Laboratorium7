using System;
using System.Collections.Generic;
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