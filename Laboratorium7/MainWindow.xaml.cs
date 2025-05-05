using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Laboratorium7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection <Person> Contacts = new ObservableCollection<Person>();

        public MainWindow()
        {
            InitializeComponent();
             
        }

        private void AddContact_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddWindow();
            addWindow.ShowDialog();
            addWindow.Title = "Dodawanie Kontaktu";
        }

        private void EditContact_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new AddWindow();
            editWindow.Title = "Edytowanie kontaktu";
            editWindow.ShowDialog();
        }

        private void DeleteContact_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FilterContact_Click(object sender, RoutedEventArgs e)
        {
            var filterWindow = new FilterWindow();
            filterWindow.Title = "Filtruj kontakty";
            filterWindow.ShowDialog();
        }

        private void SortContact_Click(object sender, RoutedEventArgs e)
        {
            var sortWindow = new SortWindow();
            sortWindow.Title = "Sortuj kontakty";
            sortWindow.ShowDialog();
        }
    }
}