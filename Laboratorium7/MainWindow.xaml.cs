using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows;

namespace Laboratorium7
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Person> Contacts { get; set; } = new ObservableCollection<Person>();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            EnsureDbCreated();
            LoadContacts();
        }
        private void EnsureDbCreated()
        {
            using (var context = new ContactContext())
            {
                context.Database.EnsureCreated();
            }

        }
        public class ContactContext : DbContext
        {
            
            public DbSet<Person> Contacts { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlite("Data Source=contacts.db");
            }
        }

        private void AddContact_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddWindow();
            addWindow.Title = "Dodawanie Kontaktu";

            // Wywołanie ShowDialog tylko raz
            if (addWindow.ShowDialog() == true)
            {
                // (Dodano teraz) Dodanie nowego kontaktu tylko, jeśli użytkownik kliknął OK w oknie dodawania
                var newPerson = new Person
                {
                    FirstName = addWindow.FirstName,
                    LastName = addWindow.LastName,
                    Email = addWindow.Email,
                    Phone = addWindow.Phone,
                    Address = addWindow.Address
                };
                using (var context = new ContactContext())
                {
                    context.Contacts.Add(newPerson);
                    context.SaveChanges();
                }
                Contacts.Add(newPerson);
                LoadContacts();

            }
        }

        private void EditContact_Click(object sender, RoutedEventArgs e)
        {
            if (ContactsListBox.SelectedItem == null)
            {
                MessageBox.Show("Proszę wybrać kontakt do edytowania.");
                return;
            }

            var selectedPerson = (Person)ContactsListBox.SelectedItem;

            var editWindow = new AddWindow
            {
                Title = "Edytowanie kontaktu",
                FirstName = selectedPerson.FirstName,
                LastName = selectedPerson.LastName,
                Email = selectedPerson.Email,
                Phone = selectedPerson.Phone,
                Address = selectedPerson.Address
            };

            if (editWindow.ShowDialog() == true)
            {
                // (Dodano teraz) Zaktualizowanie wybranego kontaktu w kolekcji
                selectedPerson.FirstName = editWindow.FirstName;
                selectedPerson.LastName = editWindow.LastName;
                selectedPerson.Email = editWindow.Email;
                selectedPerson.Phone = editWindow.Phone;
                selectedPerson.Address = editWindow.Address;
            }
            using (var context = new ContactContext())
            {
                context.Contacts.Update(selectedPerson);
                context.SaveChanges();
                LoadContacts();

            }
        }

        private void DeleteContact_Click(object sender, RoutedEventArgs e)
        {
            if (ContactsListBox.SelectedItem == null)
            {
                MessageBox.Show("Proszę wybrać kontakt do usunięcia.");
                return;
            }

            var selectedPerson = (Person)ContactsListBox.SelectedItem;
            // (Dodano teraz) Usuwanie wybranego kontaktu z kolekcji
            Contacts.Remove(selectedPerson);
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

        private void LoadContacts()
        {
            Contacts.Clear();
            using (var context = new ContactContext())
            {
                var contacts = context.Contacts.ToList();
                foreach (var contact in contacts)
                {
                    Contacts.Add(contact);
                }
            }
        }

        private void Aogus_Click(object sender, RoutedEventArgs e)
        {
            LoadContacts();
        }
    }
}
