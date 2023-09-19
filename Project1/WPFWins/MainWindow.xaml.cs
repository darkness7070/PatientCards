using Project1.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
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

namespace Project1.WPFWins
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Entities db = new Entities();
        public MainWindow()
        {
            InitializeComponent();
            UpdateGrid();
        }
        private void UpdateGrid()
        {
            db = new Entities();
            dgUsers.ItemsSource = db.Users.ToList();
        }
        private void Snils_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedValue is null)
            {
                MessageBox.Show("Выберите пользователя!");
                return;
            }
            try
            {
                Users user = (Users)dgUsers.SelectedValue;
                EnterWin1 newWin = new EnterWin1(3, db, user);
                newWin.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка!\n"+ ex);
            }
            UpdateGrid();
        }
        private void Phone_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedValue is null)
            {
                MessageBox.Show("Выберите пользователя!");
                return;
            }
            try
            {
                Users user = (Users)dgUsers.SelectedValue;
                EnterWin1 newWin = new EnterWin1(1, db, user);
                newWin.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка!\n" + ex);
            }
            UpdateGrid();
        }
        private void Email_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedValue is null)
            {
                MessageBox.Show("Выберите пользователя!");
                return;
            }
            try
            {
                Users user = (Users)dgUsers.SelectedValue;
                EnterWin1 newWin = new EnterWin1(2, db, user);
                newWin.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка!\n" + ex);
            }
            UpdateGrid();
        }
        private void Policy_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedValue is null)
            {
                MessageBox.Show("Выберите пользователя!");
                return;
            }
            EnterWin2 newWin = new EnterWin2(db, (Users)dgUsers.SelectedValue);
            newWin.ShowDialog();
            UpdateGrid();
        }
        private void Passport_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedValue is null)
            {
                MessageBox.Show("Выберите пользователя!");
                return;
            }
            EnterWin3 newWin = new EnterWin3(db, (Users)dgUsers.SelectedValue);
            newWin.ShowDialog();
            UpdateGrid();
        }
        private void Print_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedValue is null)
            {
                MessageBox.Show("Выберите пользователя!");
                return;
            }
            PrintWin newWin = new PrintWin((Users)dgUsers.SelectedValue);
            newWin.ShowDialog();
            UpdateGrid();
        }
    }
}
