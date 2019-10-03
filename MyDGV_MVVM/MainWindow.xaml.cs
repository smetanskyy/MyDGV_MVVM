using MyDGV_MVVM.Entities;
using MyDGV_MVVM.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
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
                Gender = p.Gender,
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
                Gender = person.Gender,
                Birthdate = person.Birthdate
            });
        }

        private void BtnChange_Click(object sender, RoutedEventArgs e)
        {
            if (dgShow.SelectedItem != null)
            {
                PersonVM user = (PersonVM)dgShow.SelectedItem;
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
                    user.Gender = dbPersonEdit.Gender;
                    user.Birthdate = dbPersonEdit.Birthdate;
                }
            }
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (dgShow.SelectedItem != null)
            {
                PersonVM user = (PersonVM)dgShow.SelectedItem;
                Entities.Person dbUserRemove = _db.People.SingleOrDefault(u => u.Id == user.Id);

                if (dbUserRemove != null)
                {
                    _db.People.Remove(dbUserRemove);
                    _db.SaveChanges();

                    _people.Remove(user);
                }
            }
        }
    }
}