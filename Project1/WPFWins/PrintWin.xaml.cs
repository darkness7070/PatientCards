using Microsoft.SqlServer.Server;
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
    /// Логика взаимодействия для PrintWin.xaml
    /// </summary>
    public partial class PrintWin : Window
    {
        public PrintWin(Users user)
        {
            InitializeComponent();
            boxFullname.Text = new Print().Fullname(user.Surname, user.Name, user.Patronymic);
            boxDateBirth.Text = user.DateBirth is null ? "Дата рождения: " : $"Дата рождения: {user.DateOfBirth} ({new Print().AgeStr(new Print().Age(user.DateBirth.Value))})" + user.DateOfBirth;
            boxSnils.Text = "СНИЛС: 119-274-116 53" + user.Snils;
            boxPolicy.Text = user.Policies is null ? "" : "Полис: " + user.Policies.Number;
            boxPassport.Text = user.Passports is null ? "" : new Print().Passport
            (
                user.Passports.Series,
                user.Passports.Number,
                user.Passports.DateIssue,
                user.Passports.IssuedBy
            );
            boxPhone.Text = "Телефон: " + user.Phone;
            boxEmail.Text = "Email: " + user.Email;
            boxSignature.Text = "_____________ / " + user.Surname + $"{user.Name[0]}. + ${user.Patronymic[0]}.";
        }
    }
}
