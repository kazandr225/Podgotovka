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
    /// Логика взаимодействия для CreateProductPage.xaml
    /// </summary>
    public partial class CreateProductPage : Page
    {
        Product PRODUCT;
        bool flagUpdate = false;
        string path;

        public CreateProductPage()
        {
            InitializeComponent();
            UploadFields();
        }

        public CreateProductPage(Product product)
        {
            InitializeComponent();
            UploadFields();

            flagUpdate = true;
            PRODUCT = product; //приравниваем к экземпляру объекта

            tbName_Product.Text = product.Name_Product; //выводим название продукта
            tbAmount_Product.Text = Convert.ToString(product.ProdAmount);
            tbPurchaseCost.Text = Convert.ToString(product.PurchaseCost);
            tbSellingPrice.Text = Convert.ToString(product.SellingPrice);
            cmbKind.SelectedIndex = (int)(product.id_Kind - 1);
            cmbContractor.SelectedIndex = product.Supply.id_Contractor - 1;

            //вывод картинки
            if (product.Photo != null)
            {
                BitmapImage img = new BitmapImage(new Uri(product.Photo, UriKind.RelativeOrAbsolute));
                photoProduct.Source = img;
            }
        }

        /// <summary>
        /// Заполнение ComboBox элементами
        /// </summary>
        public void UploadFields()
        {
            cmbContractor.ItemsSource = BaseClass.EM.Contractor.ToList(); //откуда брать значения
            cmbContractor.SelectedValuePath = "id_Contractor"; //путь по которому искать
            cmbContractor.DisplayMemberPath = "Name_Contractor";//что выбираем по выбранному пути

            cmbKind.ItemsSource = BaseClass.EM.Kind.ToList();
            cmbKind.SelectedValuePath = "id_Kind";
            cmbKind.DisplayMemberPath = "Category";
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //если флаг раввен false, то создаем объект для добавления продукта
                if (flagUpdate == false)
                {
                    PRODUCT = new Product();
                }

                //заполняем поля
                PRODUCT.Name_Product = tbName_Product.Text;
                PRODUCT.id_Kind = cmbKind.SelectedIndex + 1;
                PRODUCT.PurchaseCost = Convert.ToInt32(tbPurchaseCost);
                PRODUCT.SellingPrice = Convert.ToInt32(tbSellingPrice);
                PRODUCT.Amount_Product = Convert.ToInt32(tbAmount_Product);
                PRODUCT.Photo = path;

                //если объект новый, то добавляем его в базу
                if (flagUpdate == false)
                {
                    BaseClass.EM.Product.Add(PRODUCT);
                }
                BaseClass.EM.SaveChanges();
                MessageBox.Show("Информация добавлена");
            }

            catch
            {
                MessageBox.Show("Что-то пошло не так");
            }
        }

        private void btnPhoto_Click(object sender, RoutedEventArgs e)
        {

        }

       
    }
}
