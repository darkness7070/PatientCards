using Project1.Database;
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

namespace Project1.WPFWins
{
    /// <summary>
    /// Логика взаимодействия для EnterWin2.xaml
    /// </summary>
    public partial class EnterWin2 : Window
    {
        Entities db;
        Users user;
        public EnterWin2(Entities db, Users user)
        {
            InitializeComponent();
            this.db = db;
            this.user = user;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(boxDateEnd.Text) ||
               String.IsNullOrWhiteSpace(boxDateStart.Text) ||
               String.IsNullOrWhiteSpace(boxNumber.Text))
            { 
                MessageBox.Show("Заполните все данные!");
                return;
            }
            DateTime dateStart, dateEnd;
            try
            {
                dateStart = Convert.ToDateTime(boxDateStart.Text);
                dateEnd = Convert.ToDateTime(boxDateEnd.Text);
            }
            catch
            {
                MessageBox.Show("Неверный формат для даты!");
                return;
            }
            if (!new Check().DatePolicy(dateStart, dateEnd))       //TODO: Функция ПроверитьДатыПолиса(ДатаВх, ДатаК)
            {
                MessageBox.Show("Дата начала больше, чем дата окончания!");
                return;
            }
            Policies policy = new Policies()
            {
                DateStart = dateStart,
                DateEnd = dateEnd,
                Number = boxNumber.Text,
            };
            db.Policies.Add(policy);
            user.Policies = policy;
            try
            {
                db.SaveChanges();
                Close();
            }
            catch(Exception ex) 
            {
                MessageBox.Show("Ошибка! "+ ex);
                return;
            }
        }
    }
}
