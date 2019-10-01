using MyDGV_MVVM.Entities;
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
        public MainWindow()
        {
            InitializeComponent();
            _db = new EFContext();
            _db.People.Count();
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
        }
    }
}