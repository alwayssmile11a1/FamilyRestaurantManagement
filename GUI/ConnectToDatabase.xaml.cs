using System.Windows;
using System.Windows.Input;
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
            Connect();
        }

        private void Connect()
        {
            try
            {
                MySqlConnectionBUS.ConnectToDatabase(txtboxServer.Text, txtboxUser.Text, txtboxPassword.Password, txtboxDatabase.Text);
                MessageBox.Show("Kết nối thành công!", "Thành công", MessageBoxButton.OK);
                MainWindow MainWindow = new MainWindow();
                MainWindow.Show();
                this.Close();
            }
            catch (BUSException ex)
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void txtboxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Connect();
            }
        }
    }
}
