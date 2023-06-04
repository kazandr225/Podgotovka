using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Podgotovka //тут стереть папку классов
{
    public partial class Product //тут публичный частичный класс и название совпадает с таблицей продуктов
    {

        public SolidColorBrush KindColor //изменение цвета грида в зависимости от вида продукта
        {
            get
            {
                switch (id_Kind)
                {
                    case 1:
                        return Brushes.Ivory; //не спутать Brushe и Breshes
                    case 2:
                        return Brushes.LightCoral;
                    case 3:
                        return Brushes.LightGreen;
                    case 4:
                        return Brushes.Aqua;
                    default:
                        return Brushes.White;
                }
            }
        }

        public string Name //у казываем название в бинде, тут же совмещаем название продукта и его вид
        {
            get 
            {
                return Name_Product + "/" + Kind.Category;
            }
        }

        public string ProdAmount //ну там можно было и TextBox вставить для такого случая
        {
            get 
            {
                return "Количество товара: " + Amount_Product;
            }
        }



    }
}
