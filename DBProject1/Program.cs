using Spectre.Console;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DBProject1
{
    internal class Program
    {
        static Entities db = new Entities();
        static string[] fullnames = File.ReadAllLines(Environment.CurrentDirectory + @"\data\fullname.txt");
        static string[] emails = File.ReadAllLines(Environment.CurrentDirectory + @"\data\emails.txt");
        static string[] phones = File.ReadAllLines(Environment.CurrentDirectory + @"\data\phones.txt");
        static void Main()
        {
            MainThread().Wait();
        }
        static async Task MainThread()
        {
            int count = AnsiConsole.Ask<int>("[darkorange3_1]Сколько человек нужно добавить?[/]");
            var optionSelection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("[darkorange3_1]Выберите тип заполнения БД[/]")
                .PageSize(3)
                .HighlightStyle(new Style(Color.DarkOrange3_1))
                .AddChoices("[lightcyan1]Только ФИО[/]", "[lightcyan1]Все данные[/]", "[lightcyan1]ФИО и дата рождения[/]"));
            AnsiConsole.Clear();
            await AnsiConsole.Progress()
            .StartAsync(async ctx =>
            {
                var task1 = ctx.AddTask("[darkorange3_1]Заполняем базу данных[/]");
                while (!ctx.IsFinished)
                {
                    await Update(optionSelection, task1, count);
                }

            });
            AnsiConsole.Markup("[darkorange3_1]Можно выходить[/]\n");
            Console.ReadKey();
        }

        static async Task Update(string method, ProgressTask progress, int count)
        {
            Random random = new Random();
            #region Удаляем 0-30%
            try
            {
                db.Users.RemoveRange(db.Users.ToList());
                progress.Increment(9);
                db.Policies.RemoveRange(db.Policies.ToList());
                progress.Increment(9);
                db.Passports.RemoveRange(db.Passports.ToList());
                progress.Increment(9);
                db.SaveChanges();
                progress.Increment(3);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n" + ex);
            }
            #endregion
            #region Загружаем 30-90%
            for (int i = 0; i< count; i++)
            {
                if (method.Contains("Все данные"))
                {
                    Policies policy = new Policies()
                    {
                        DateStart = new DateTime(
                        random.Next(1970, 2000),
                        random.Next(1, 13),
                        random.Next(1, 28)
                    ),
                        DateEnd = new DateTime(
                        random.Next(2020, 2040),
                        random.Next(1, 13),
                        random.Next(1, 28)
                        ),
                        Number = GenerateRandomDigitString(16, random)
                    };
                    db.Policies.Add(policy);
                    Passports passport = new Passports()
                    {
                        DateIssue = new DateTime(random.Next(1970, 2020)),
                        IssuedBy = "ГУ МВД РОССИИ ПО ЧЕЛЯБИНСКОЙ ОБЛАСТИ",
                        Number = GenerateRandomDigitString(6, random),
                        Series = GenerateRandomDigitString(4, random)
                    };
                    db.Passports.Add(passport);

                    var fullname = fullnames[random.Next(0, 100)].Split(' ');
                    Users user = new Users()
                    {
                        Surname     = fullname[0],
                        Name        = fullname[1],
                        Patronymic  = fullname[2],
                        DateBirth = new DateTime(
                            random.Next(1970, 2001),
                            random.Next(1, 13),
                            random.Next(1, 28)
                        ),
                        Email       = emails[random.Next(0, 99)],
                        Phone       = phones[random.Next(0, 100)],
                        Snils       = GenerateRandomDigitString(11, random),
                        Passports   = passport,
                        Policies    = policy
                    };
                    db.Users.Add(user);
                }
                else if(method.Contains("Только ФИО"))
                {
                    var fullname = fullnames[random.Next(0, 100)].Split(' ');
                    Users user = new Users()
                    {
                        Surname     = fullname[0],
                        Name        = fullname[1],
                        Patronymic  = fullname[2]
                    };
                    db.Users.Add(user);
                }
                else if(method.Contains("ФИО и дата рождения"))
                {
                    var fullname = fullnames[random.Next(0, 100)].Split(' ');
                    Users user = new Users()
                    {
                        Surname     = fullname[0],
                        Name        = fullname[1],
                        Patronymic  = fullname[2],
                        DateBirth   = new DateTime(
                            random.Next(1970, 2001),
                            random.Next(1, 13),
                            random.Next(1, 28)
                        )
                    };
                    db.Users.Add(user);
                }
                progress.Increment(count / 60);
            }
            #endregion
            #region Сохраняем 90-95%
            try
            {
                db.SaveChanges();
                progress.Increment(9);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n" + ex);
            }
            #endregion
            #region Выводим 95-100%
            #endregion
        }
        static string GenerateRandomDigitString(int count, Random random)
        {
            char[] digits = new char[count];
            for (int i = 0; i < count; i++)
            {
                digits[i] = (char)(random.Next(0, 10) + '0');
            }
            return new string(digits);
        }
    }
}
