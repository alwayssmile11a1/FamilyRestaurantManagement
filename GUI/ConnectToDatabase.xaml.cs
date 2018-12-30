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
using BUS;

namespace GUI
{
    /// <summary>
    /// Interaction logic for ConnectToDatabase.xaml
    /// </summary>
    public partial class ConnectToDatabase : Window
    {
        public ConnectToDatabase()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlConnectionBUS.ConnectToDatabase(txtboxServer.Text, txtboxUser.Text, txtboxPassword.Password, txtboxDatabase.Text);
                MessageBox.Show("Kết nối thành công!", "Thành công", MessageBoxButton.OK);
                ThaoHocGioi MainWindow = new ThaoHocGioi();
                MainWindow.Show();
                this.Close();
            }
            catch (BUSException ex)
            {
                MessageBox.Show(ex.ToString(), "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
