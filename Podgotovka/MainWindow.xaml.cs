using Podgotovka.Classes;
using Podgotovka.Pages;
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

namespace Podgotovka
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //Данные администратора 
        //Логин: Admin
        //Пароль: Abgh123$

        public MainWindow()
        {
            InitializeComponent();

            BaseClass.EM = new EntitiesModel(); //подключаем модель
            FrameClass.MainFrame = fMain; //привязываем фрейм
            FrameClass.MainFrame.Navigate(new AutorizationPage()); //грузим на фрейм страницу
        }
    }
}
