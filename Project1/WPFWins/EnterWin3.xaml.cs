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

    public partial class EnterWin3 : Window
    {
        Entities db;
        Users user;
        public EnterWin3(Entities db, Users user)
        {
            InitializeComponent();
            this.db = db;
            this.user = user;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if(String.IsNullOrWhiteSpace(boxDateIssue.Text) ||
               String.IsNullOrWhiteSpace(boxIssuedBy.Text)  ||
               String.IsNullOrWhiteSpace(boxNumber.Text)    ||
               String.IsNullOrWhiteSpace(boxSeries.Text))
            {
                MessageBox.Show("Заполните все данные!");
                return;
            }
            DateTime dateIssue;
            try
            {
                dateIssue = Convert.ToDateTime(boxDateIssue.Text);
                if (!new Check().DateIssue(dateIssue))              //TODO: Функция ПроверитьДатуВыдачиПаспорта(дата ДатаВыдачиПаспорта)
                    throw new Exception();
            }
            catch
            {
                MessageBox.Show("Неверный формат даты выдачи!");
                return;
            }
            var passport = new Passports()
            {
                Number = boxNumber.Text,
                Series = boxSeries.Text,
                DateIssue = dateIssue,
                IssuedBy = boxIssuedBy.Text
            };
            db.Passports.Add(passport);
            user.Passports = passport;
            try
            {
                db.SaveChanges();
                Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка! " + ex);
                return;
            }
        }
    }
}
