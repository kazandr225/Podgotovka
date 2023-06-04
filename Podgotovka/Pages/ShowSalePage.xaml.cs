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
    /// Логика взаимодействия для ShowSalePage.xaml
    /// </summary>
    public partial class ShowSalePage : Page
    {
        List<Product> ProductFilter = new List<Product>(); //лист для работы фильтрации и сортировки

        public ShowSalePage()
        {
            InitializeComponent();

            BaseClass.EM = new EntitiesModel();
            ProductFilter = BaseClass.EM.Product.ToList(); //связываем лист с таблицей БД

            listProduct.ItemsSource = BaseClass.EM.Product.ToList(); //связываем лист с БД для отображения элементов
            List<Kind> BT = BaseClass.EM.Kind.ToList(); //связываем тип товара с листом для заполнения ComboBox

            //программное заполнение выпадающего списка, данными из базы
            cmbProduct.Items.Add("Все продукты");
            for (int i = 0; i<BT.Count; i++)
            {
                cmbProduct.Items.Add(BT[i].Category);
            }
            cmbProduct.SelectedIndex = 0; //значение по умолчанию
        }

        /// <summary>
        /// Метод для поиска, соритровки и фильтрациис
        /// </summary>
        void SortingAndFiltration()
        {
            List<Product> productList = new List<Product>(); //создаем пустой лист, который дальше будем заполнять по мере работы фильтрации, сортировки и поиска

            string category = cmbProduct.SelectedIndex.ToString(); //записываем выбранный пользователем тип товара
            int index = cmbProduct.SelectedIndex;

            //поиск значений, удовлетворяющих условиям фильтра
            if (index != 0)
            {
                productList = BaseClass.EM.Product.Where(x => x.Kind.Category == category).ToList();
            }
            else
            {
                productList = BaseClass.EM.Product.ToList(); //сбрасываем условие по фильтрации
            }

            //поиск по названию продукта
            if (!string.IsNullOrWhiteSpace(tbSearch.Text)) //если строка не пустая
            {
                productList = productList.Where(x => x.Name_Product.ToLower().Contains(tbSearch.Text.ToLower())).ToList(); //бегаем по листу и ищем совпадение
            }

            //соритровка по алфавиту
            switch (cbSort.SelectedIndex)
            {
                case 1:
                    productList.Sort((x, y) => x.Name_Product.CompareTo(y.Name_Product));
                    break;
                case 2:
                    productList.Sort((x, y) => x.Name_Product.CompareTo(y.Name_Product));
                    productList.Reverse();
                    break;
            }

            //тут если сортировка нужна по числовым значенияя
            //if (cbSort.SelectedIndex != -1) //сортировка по баллам
            //{
            //    if (cbSort.SelectedIndex == 0)
            //    {
            //        studentsResulsts = studentsResulsts.OrderBy(x => x.Scores).ToList();
            //    }
            //    else
            //    {
            //        studentsResulsts = studentsResulsts.OrderByDescending(x => x.Scores).ToList();
            //    }
            //}

            listProduct.ItemsSource = productList; //записываем актуальную версию в лист
            if (productList.Count == 0) //если нет таких записей
            {
                MessageBox.Show("Нет данных");
            }

        }

        private void cbmProdoct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortingAndFiltration();
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortingAndFiltration();
        }

        private void cbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            SortingAndFiltration();
        }

        private void Buttondeleate_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.Uid); //обозначаем именно тот объект, который нужно удалить

            //Создаем объект для удаления
            Product product = BaseClass.EM.Product.FirstOrDefault(x => x.id_Product == index);

            BaseClass.EM.Product.Remove(product); //убираем продукт из объекта БД
            BaseClass.EM.SaveChanges();

            FrameClass.MainFrame.Navigate(new ShowSalePage()); //перезагружаем страницу для отображения изменений
        }

        private void btnupdate_Click(object sender, RoutedEventArgs e) 
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.Uid);

            //Создаем объект, который в последствии будет редактироваться
            Product p = BaseClass.EM.Product.FirstOrDefault(x => x.id_Product == index);

            FrameClass.MainFrame.Navigate(new CreateProductPage()); //вставляем для обозначения объекта и заполнения полей
        }

        private void tbRevenue_Loaded(object sender, RoutedEventArgs e) //высчитываем сколько денег будет получено, ну зачем оно надо, верно?
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);

            //ищем в таблице, где хранится информация про количество товара и стоимости продажи
            List<Product> p = BaseClass.EM.Product.Where(x => x.id_Product == index).ToList();

            int sum = 0;

            //вычисляем количество денег
            foreach (Product prod in p)
            {
                sum += Convert.ToInt32(prod.Amount_Product * prod.SellingPrice);
            }

            tb.Text = "Выручка составит: " + sum.ToString() + " руб.";
        }

        private void btnCreateSale_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new CreateProductPage());
        }

        private void btnSavePDF_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(listProduct, "");
            }
        }
    }
}
