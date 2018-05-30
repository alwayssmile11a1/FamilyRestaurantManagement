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

namespace GUI
{
    /// <summary>
    /// Interaction logic for UserControlOrder.xaml
    /// </summary>
    public partial class UserControlOrder : UserControl
    {
        public UserControlOrder()
        {
            InitializeComponent();

            ComboBoxTableNumber.Items.Add("Mang đi");
            for (int i = 1; i <= 10; i++)
                ComboBoxTableNumber.Items.Add(i);

            for (int i = 0; i <= 9; i++)
            {
                ComboBoxGiamGia.Items.Add(i * 10 + "%");
                ComboBoxGiamGia.Items.Add(i * 10 + 5 + "%");
            }

            for (int i = 0; i <= 9; i++)
            {
                ComboBoxVAT.Items.Add(i * 10 + "%");
                ComboBoxVAT.Items.Add(i * 10 + 5 + "%");
            }

        }

        
    }
}
