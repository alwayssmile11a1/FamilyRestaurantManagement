using System.Windows;
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
                MainWindow MainWindow = new MainWindow();
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
