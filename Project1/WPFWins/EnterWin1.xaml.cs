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
    /// Логика взаимодействия для EnterWin1.xaml
    /// </summary>
    public partial class EnterWin1 : Window
    {
        int idWin;
        Entities db;
        Users user;
        public EnterWin1(int idWin, Entities db, Users user)
        {
            InitializeComponent();
            this.idWin = idWin;
            this.db = db;
            this.user = user;
            switch (idWin)
            {
                case 1:
                    Title = "Введите телефон";
                    txtBlock1.Text = "Введите телефон";
                    break;
                case 2:
                    Title = "Введите email";
                    txtBlock1.Text = "Введите email";
                    break;
                case 3:
                    Title = "Введите СНИЛС";
                    txtBlock1.Text = "Введите СНИЛС";
                    break;
                default: 
                    Close();
                    return;
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(boxInput.Text))
            {
                MessageBox.Show("Введите данные!");
                return;
            }
            var data = boxInput.Text;
            switch (idWin)
            {
                case 1:
                    if (new Check().Phone(data))
                        user.Phone = data;
                    else
                        goto case 4;
                    break;
                case 2:
                    if (new Check().Email(data))
                        user.Email = data;
                    else
                        goto case 4;
                    break;
                case 3:
                    if (new Check().Snils(data))
                        user.Snils = data;
                    else
                        goto case 4;
                    break;
                case 4:
                    MessageBox.Show("Неверно введены данные!");
                    return;
            }
            try
            {
                db.SaveChanges();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.ToString());
                return;
            }
        }
    }
}
