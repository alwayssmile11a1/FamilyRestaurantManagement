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
    /// Interaction logic for UserControlEmployees.xaml
    /// </summary>
    public partial class UserControlEmployees : UserControl
    {
        public UserControlEmployees()
        {
            InitializeComponent();
            var data = new Test { Test1 = "Test1", Test2 = "Test2" };

            dtgEmployees.Items.Add(data);
        }

        private void btnAddNewEmployee_Click(object sender, RoutedEventArgs e)
        {
            flyoutAddEmployee.IsOpen = true;
        }

        private void btnFindEmployee_Click(object sender, RoutedEventArgs e)
        {
            flyoutFindEmployee.IsOpen = true;
        }
    }
    public class Test
    {
        public string Test1 { get; set; }
        public string Test2 { get; set; }
    }
}
