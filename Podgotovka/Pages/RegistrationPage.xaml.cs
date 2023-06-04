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
        }

        /// <summary>
        /// Событие на кнопку регистрации, добавление введенных пользователем данных в БД
        /// </summary>
        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int g = 0;
                //делаю в несколько строк изза требования того, чтобы в одной строке была только ОДНА команда
                if (rbMen.IsChecked == true) //выбран мужской пол
                {
                    g = 1; //можно просто в переменную записать, ведь не особо пол то хранить надо, верно?
                }

                if (rbWomen.IsChecked == true) //выбран женский пол
                {
                    g = 2;
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
                    id_Gender = g,
                    id_Role = 2
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

            Regex CapitalSymb = new Regex("(?=.*[A-Z])");
            Regex SmallSymb = new Regex("(?=.*[a-z])");
            Regex Numbers = new Regex("(?=.*[0-9])");
            Regex SpecSymb = new Regex("(?=.*[!?@#$%^&*()-=+])");

            if (!CapitalSymb.IsMatch(pass))
            {
                MessageBox.Show("Пароль должен содержать как минимум один заглавный символ");
                return false;
            }

            if (!SmallSymb.IsMatch(pass))
            {
                MessageBox.Show("роль должен содержать не менее 3-х маленьких латинских символа");
                return false;
            }

            if (!Numbers.IsMatch(pass))
            {
                MessageBox.Show("Пароль должен содержать не менее 1 цифры");
                return false;
            }

            if (!SpecSymb.IsMatch(pass))
            {
                MessageBox.Show("Пароль должен содержать не менее 1 специального символа");
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
