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
using System.Diagnostics;

namespace GUI
{
    /// <summary>
    /// Interaction logic for ThaoHocGioi.xaml
    /// </summary>
    public partial class ThaoHocGioi : Window
    {

        public static ThaoHocGioi Instance { get; private set; }

        public UserControlOrder UCOrder { get; private set; }
        public UserControlTableChart UCTableChart { get; private set; }
        public UserControlEmployees UCEmployees { get; private set; }


        public ThaoHocGioi()
        {
            Instance = this;

            InitializeComponent();
            GridPrincipal.Children.Add(new UserControlIntroduce());

            try
            {
                MySqlConnectionBUS.ConnectToDatabase("localhost", "root", "root", "familyrestaurant");
                Debug.WriteLine("Connected");
            }
            catch
            {
                Debug.WriteLine("Can't connect");
            }
            

        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //Làm form di chuyển theo khi bấm kéo chuột
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            MoveCursorMenu(index);

            //Thu gọn ListView
            GridMenu.Width = 60;            

            switch (index)
            {
                case 0:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new UserControlHome());
                    break;
                case 1:                    
                    GridPrincipal.Children.Clear();

                    if (UCOrder == null)
                    {
                        UCOrder = new UserControlOrder();
                        UCOrder.Uid = "1";
                        UCTableChart = new UserControlTableChart();
                        UCTableChart.Uid = "2";
                    }

                    GridPrincipal.Children.Add(UCOrder);

                    break;
                case 2:
                    GridPrincipal.Children.Clear();

                    if (UCTableChart == null)
                    {
                        UCTableChart = new UserControlTableChart();
                        UCTableChart.Uid = "2";
                        UCOrder = new UserControlOrder();
                        UCOrder.Uid = "1";
                    }

                    GridPrincipal.Children.Add(UCTableChart);
                    break;
                case 3:
                    GridPrincipal.Children.Clear();

                    if(UCEmployees==null)
                    {
                        UCEmployees = new UserControlEmployees();
                    }

                    GridPrincipal.Children.Add(UCEmployees);
                    break;
                case 4:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new UserControlReport());
                    break;
                case 5:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new UserControlBuyingGoods());
                    break;
                case 6:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new UserControlStore());
                    break;
                default:                    
                    break;
            }
        }

        public void MoveToMenu(UserControl menu)
        {
            GridPrincipal.Children.Clear();
            GridPrincipal.Children.Add(menu);

            ListViewMenu.SelectedIndex = int.Parse(menu.Uid);
            MoveCursorMenu(int.Parse(menu.Uid));
        }

        //Hiệu ứng có viền trái màu hồng khi chọn ListViewItem
        private void MoveCursorMenu(int index)
        {
            TransitioningContentSlide.OnApplyTemplate();
            GridCursor.Margin = new Thickness(0, (80 + 60 * index), 0, 0);
        }

        //Hiệu ứng thu gọn ListView
        private void ButtonShorten_Click(object sender, RoutedEventArgs e)
        {           
            if (GridMenu.Width == 250)
            {
                GridMenu.Width = 60;
            }          
            else
            {
                GridMenu.Width = 250;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                MySqlConnectionBUS.DisConnectFromDatabase();
                Debug.WriteLine("Disconneted");
            }
            catch
            {
                Debug.WriteLine("Something went wrong");
            }
        }
    }
}
