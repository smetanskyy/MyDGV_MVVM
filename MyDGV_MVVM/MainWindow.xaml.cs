using MyDGV_MVVM.Entities;
using MyDGV_MVVM.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using static MyDGV_MVVM.Helpers.Helper;

namespace MyDGV_MVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private EFContext _db;
        private ObservableCollection<PersonVM> _people;
        public MainWindow()
        {
            InitializeComponent();
            _db = new EFContext();
            _db.People.Count();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var model = _db.People.Select(p => new PersonVM
            {
                Id = p.Id,
                Name = p.Name,
                Surname = p.Surname,
                Email = p.Email,
                ImageUrl = p.Photo,
                IsMale = p.Gender == "Male" ? true : false,
                IsFemale = p.Gender == "Female" ? true : false,
                Birthdate = p.Birthdate
            });

            _people = new ObservableCollection<PersonVM>(model);
            dgShow.ItemsSource = _people;
        }

        private void BtnEnterAmount_Click(object sender, RoutedEventArgs e)
        {
            if (tbAmountUsers.Text.Length == 0)
            {
                MessageBox.Show("Enter amount of people", "Error");
                return;
            }
            int amount = 0;

            try
            {
                amount = Convert.ToInt32(tbAmountUsers.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Incorrect amount of people", "Error");
                return;
            }

            List<Entities.Person> randUsers = null;
            randUsers = CreateRandPeople(amount);

            try
            {
                _db.People.AddRange(randUsers);
                _db.SaveChanges();
                MessageBox.Show("Succeed!");
            }
            catch (Exception)
            {
                MessageBox.Show("Something wrong!", "Error");
            }
            Window_Loaded(sender, e);
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Entities.Person person = CreateRandPerson();

            _db.People.Add(person);
            _db.SaveChanges();

            _people.Add(new PersonVM
            {
                Id = person.Id,
                Name = person.Name,
                Surname = person.Surname,
                Email = person.Email,
                ImageUrl = person.Photo,
                IsMale = person.Gender == "Male" ? true : false,
                IsFemale = person.Gender == "Female" ? true : false,
                Birthdate = person.Birthdate
            });
        }

        private void BtnChange_Click(object sender, RoutedEventArgs e)
        {
            if (dgShow.SelectedItem != null)
            {
                PersonVM user = null;
                try
                {
                    user = (PersonVM)dgShow.SelectedItem;
                }
                catch (Exception)
                {
                    return;
                }

                Entities.Person dbPersonEdit = _db.People.SingleOrDefault(u => u.Id == user.Id);
                Entities.Person newUser = CreateRandPerson();

                if (dbPersonEdit != null)
                {
                    dbPersonEdit.Name = newUser.Name;
                    dbPersonEdit.Surname = newUser.Surname;
                    dbPersonEdit.Email = newUser.Email;
                    dbPersonEdit.Photo = newUser.Photo;
                    dbPersonEdit.Gender = newUser.Gender;
                    dbPersonEdit.Birthdate = newUser.Birthdate;

                    _db.SaveChanges();

                    user.Id = dbPersonEdit.Id;
                    user.Name = dbPersonEdit.Name;
                    user.Surname = dbPersonEdit.Surname;
                    user.Email = dbPersonEdit.Email;
                    user.ImageUrl = dbPersonEdit.Photo;
                    user.IsMale = dbPersonEdit.Gender == "Male" ? true : false;
                    user.IsFemale = !user.IsMale;
                    user.Birthdate = dbPersonEdit.Birthdate;
                }
            }
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (dgShow.SelectedItem != null)
            {
                PersonVM user = null;
                try
                {
                    user = (PersonVM)dgShow.SelectedItem;
                }
                catch (Exception)
                {
                    return;
                }

                Entities.Person dbUserRemove = _db.People.SingleOrDefault(u => u.Id == user.Id);

                if (dbUserRemove != null)
                {
                    _db.People.Remove(dbUserRemove);
                    _db.SaveChanges();

                    _people.Remove(user);
                }
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if(dgShow.SelectedItem != null)
            {
                PersonVM user = null;
                try
                {
                    user = (PersonVM)dgShow.SelectedItem;
                }
                catch (Exception)
                {
                    return;
                }
                RadioButton button = (RadioButton)sender;
                if(button.IsChecked == true)
                {
                    Entities.Person personForChange = _db.People.SingleOrDefault(p => p.Id == user.Id);
                    if ((string)button.Content == "male")
                    {
                        personForChange.Gender = "Male";
                        user.IsMale = true;
                        user.IsFemale = false;
                    }
                    else
                    {
                        personForChange.Gender = "Female";
                        user.IsMale = false;
                        user.IsFemale = true;
                    }
                    _db.SaveChanges();
                }
            }
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            PersonVM user = null;
            try
            {
                user = (PersonVM)dgShow.SelectedItem;
            }
            catch (Exception)
            {
                return;
            }

            DatePicker datePicker = (DatePicker)sender;
            Entities.Person personForChange = _db.People.SingleOrDefault(p => p.Id == user.Id);
            personForChange.Birthdate = datePicker.SelectedDate.Value.Date;
            _db.SaveChanges();
        }
    }
}