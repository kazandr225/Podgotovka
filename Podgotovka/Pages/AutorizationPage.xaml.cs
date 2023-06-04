using Podgotovka.Classes;
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

namespace Podgotovka.Pages
{
    /// <summary>
    /// Логика взаимодействия для AutorizationPage.xaml
    /// </summary>
    public partial class AutorizationPage : Page
    {
        public AutorizationPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Событие на кнопку авторизации, проверка введенных пользователем данных
        /// </summary>
        private void btnAutorization_Click(object sender, RoutedEventArgs e)
        {
            //if (tboxLogin.Text == "" || tboxLogin.Text == " " && pbPasword.Text == "" || pbPassword.Text == " ") //если не надо будет хэшировать пароль, а так не забыть пассворд бокс поставить
            //{ 
            //MessageBox.Show("Ошибка ввода!", "Проверьте введенные данные!");
            //}

            //int p = pbPasword.Password.GetHashCode(); //хэшируем введенный пароль и записываем его в переменную

            //Users autoUser = BaseClass.EM.Users.FirstOrDefault(x => x.Login == tboxLogin.Text && x.Password == p); //сверяем введенный логин и пароль с логином и ъэшкодом в базе

            //if (autoUser == null) //если пользователя в базе нет
            //{
            //    MessageBox.Show("Данный пользователь не найден!", "Проверьте введенные данные!");
            //}

            //else 
            //{
            //    switch (autoUser.id_User) //проверяем роль пользователя и отправляем его на соответствующую страницу 
            //    {
            //        case 1: //администратор
            //            GlobalValues.id = 1;
            //            MessageBox.Show("Здравствуйте, администратор!");
            //            FrameClass.MainFrame.Navigate(new AdminPage());
            //            break;
            //        case 2: //обычный пользователь
            //            GlobalValues.id = 2;
            //            MessageBox.Show("Здравствуйте, пользователь!");
            //            FrameClass.MainFrame.Navigate(new ShowSalePage());
            //            break;
            //        default:
            //            MessageBox.Show("У вас нет роли, обратитесь к администратору");
            //            break;
            //    }
            //}

            int p = pbPasword.Password.GetHashCode();

            Users autoUser = BaseClass.EM.Users.FirstOrDefault(x => x.Login == tboxLogin.Text || x.Password == p);

            if (autoUser == null)
            {
                MessageBox.Show("Пользователь не найден");
            }

            switch (autoUser.id_Role)
            {
                case 1:
                    GlobalValues.id = 1;
                    MessageBox.Show("Добра пожаловать, администратор!");
                    FrameClass.MainFrame.Navigate(new AdminPage());
                    break;
                case 2:
                    GlobalValues.id = 2;
                    MessageBox.Show("Добро пожаловать, пользователь!");
                    FrameClass.MainFrame.Navigate(new ShowSalePage());
                    break;
                default:
                    MessageBox.Show("Получите роль у администратора");
                    break;
            }

        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new RegistrationPage());
        }
    }
}
