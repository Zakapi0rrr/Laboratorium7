﻿using Microsoft.EntityFrameworkCore;
using System;
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
            using (var db = new ContactContext())
            {
                //db.Database.EnsureDeleted(); 
                // db.Database.EnsureCreated();  
            }
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
            using (var context = new ContactContext())
            {
                var selectedPerson = (Person)ContactsListBox.SelectedItem;
                context.Contacts.Remove(selectedPerson);
                context.SaveChanges();
                LoadContacts();
            }



        }

        private void FilterContact_Click(object sender, RoutedEventArgs e)
        {
            var filterWindow = new FilterWindow();
            filterWindow.ShowDialog();
            var filter = filterWindow.SelectedFilter;
            using (var context = new ContactContext())
            {
                var query = context.Contacts.AsQueryable();

                if (filter.isEmail == true)
                {
                    query = query.Where(c => !string.IsNullOrEmpty(c.Email));
                }
                if (filter.isPhone == true)
                {
                    query = query.Where(c => c.Phone != 0);
                }
                if (filter.isLastName == true)
                {
                    query = query.Where(c => !string.IsNullOrEmpty(c.LastName));
                }
                Contacts.Clear();
                foreach (var contact in query.ToList())
                {
                    Contacts.Add(contact);
                }
            }
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
        //sortowanie asc/desc po imieniu
        private bool sortAscending = true;

        private void Sort_Click(object sender, RoutedEventArgs e)
        {
            var sorted = sortAscending
                ? Contacts.OrderBy(c => c.FirstName).ToList()
                : Contacts.OrderByDescending(c => c.FirstName).ToList();

            Contacts.Clear();
            foreach (var c in sorted)
            {
                Contacts.Add(c);
            }
            sortAscending = !sortAscending;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            var searchWindow = new SearchWindow();
            searchWindow.Title = "Wyszukaj kontakty";
            var result = searchWindow.ShowDialog();

            if (result == true)
            {
                string searchText = searchWindow.SearchText;
                string selectedRadioButton = searchWindow.SelectedRadioButton;

                using (var context = new ContactContext())
                {
                    var query = context.Contacts.AsQueryable();

                    if (selectedRadioButton == "Email")
                    {
                        query = query.Where(c => c.Email.Contains(searchText));
                    }
                    else if (selectedRadioButton == "Phone")
                    {
                        //parse na string

                        query = query.Where(c => c.Phone.ToString().Contains(searchText));
                    }
                    else if (selectedRadioButton == "LastName")
                    {
                        query = query.Where(c => c.LastName.Contains(searchText));
                    }

                    Contacts.Clear();
                    foreach (var contact in query.ToList())
                    {
                        Contacts.Add(contact);
                    }
                }
            }
        }
    }
}