using Podgotovka.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Podgotovka.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();

            cbRole.ItemsSource = BaseClass.EM.Role.ToList(); //по сути бесполезный блок назначения роли, если можно записать сазу единицу, как обычному пользователю
            cbRole.SelectedValuePath = "idRole";
            cbRole.DisplayMemberPath = "Role";
            cbRole.SelectedIndex = 1; //ну вот про это я и говорил

        }

        /// <summary>
        /// Событие на кнопку регистрации, добавление введенных пользователем данных в БД
        /// </summary>
        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //делаю в несколько строк изза требования того, чтобы в одной строке была только ОДНА команда
                if (rbMen.IsChecked == true) //выбран мужской пол
                {
                    GlobalValues.gerder = 1; //можно просто в переменную записать, ведь не особо пол то хранить надо, верно?
                }

                if (rbWomen.IsChecked == true) //выбран женский пол
                {
                    GlobalValues.gerder = 2;
                }

                //проверка на логин
                Users autoUser = BaseClass.EM.Users.FirstOrDefault(x => x.Login == tboxLogin.Text);
                if (autoUser != null) //если пользователя в базе нет
                {
                    MessageBox.Show("Данный логин занят!", "Пожалуйста, придумайте другой логин!");
                }

                //проверка пароля
                if (!PasswordVerification(pbPassword))
                {
                    return;
                }

                //объект для записей в БД
                Users userTable = new Users() //после скобочки ничего не стоит, понятно?
                {
                    Surname_User = tboxSurname.Text,
                    Name_User = tboxName.Text,
                    Patronymic_User = tboxPatronymic.Text,
                    Birthday_User = Convert.ToDateTime(dpBirthday.SelectedDate),
                    Login = tboxLogin.Text,
                    Password = pbPassword.Password.GetHashCode(), //опять записываем хэшкод, и наоборот, если он не нцжен тут, ну или то же самое, но без перевода, тогда и проверкаа на пароль будет актуальной
                    id_Gender = GlobalValues.gerder,
                    id_User = GlobalValues.id
                }; //вот тут надо ставить

                BaseClass.EM.Users.Add(userTable); //добавляем в таблицу Uers данные из созданного объекта
                BaseClass.EM.SaveChanges();
                MessageBox.Show("Регистрация прошла успешно!");
                FrameClass.MainFrame.Navigate(new AutorizationPage());
            }
            catch
            {
                MessageBox.Show("Ошибка регистрации!", "Проверьте введенные данные!");
            }

        }

        /// <summary>
        /// Метод для проверки придуманного пользователем пароля на соответствие установленным требованиям
        /// </summary>
        private bool PasswordVerification(PasswordBox password)
        {
            string pass = password.Password;

            Regex CapitalSymb = new Regex("(?=.*[A-Z])"); //заглавный латинский символ
            Regex SmallSymbs = new Regex("(?=.*[a-z])"); //три строчных латинских символа
            Regex Numbers = new Regex("(?=.*[0-9])"); //цифры
            Regex SpecSymb = new Regex("(?=.*[!?@#$%^&*-=+()])"); //Специальные симфолы

            if (CapitalSymb.IsMatch(pass))
            {
                MessageBox.Show("В пароле должен содержатсья как минимум 1 заглавный латинский символ");
                return false;
            }

            if (SmallSymbs.IsMatch(pass))
            {
                MessageBox.Show("В пароле должно быть как минимум 3 строчных латинских символа");
                return false;
            }

            if (Numbers.IsMatch(pass))
            {
                MessageBox.Show("Пароль должен содержать как минимум 2 цифры");
                return false;
            }

            if (SpecSymb.IsMatch(pass))

            {
                MessageBox.Show("Пароль должен содержать как минимум 1 специальный символ");
                return false;
            }

            if (pass.Length < 8)
            {
                MessageBox.Show("Пароль должен содержать не менее 8 символов");
                return false;
            }

            else
            {
                return true;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new AutorizationPage());
        }
    }
}
