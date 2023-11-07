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

namespace Demo_var_6
{
    public partial class ProductView : Window
    {
        public ProductView(Product prod)
        {
            InitializeComponent();
            DataContext = new ProductViewModel(prod);
        }
    }
}
