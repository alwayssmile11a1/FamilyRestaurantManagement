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
    /// Interaction logic for UserControlReport.xaml
    /// </summary>
    public partial class UserControlReport : UserControl
    {
        public UserControlReport()
        {
            InitializeComponent();

            for (int i = 0; i < 12; i++)
            {
                comboBoxMonth.Items.Add(i + 1);
            }

            for (int i = 2016; i < 2050; i++)
            {
                comboBoxYear.Items.Add(i + 1);
            }
            comboBoxMonth.SelectedItem = DateTime.Today.Month;
            comboBoxYear.SelectedItem = DateTime.Today.Year;
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnExportExcel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
