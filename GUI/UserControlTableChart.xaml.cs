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
    /// Interaction logic for UserControlTableChart.xaml
    /// </summary>
    public partial class UserControlTableChart : UserControl
    {
        public enum TableStatus { Unoccupied, Occupied, Reserved}


        private List<Button> tableButtons = new List<Button>();

        public UserControlTableChart()
        {
            InitializeComponent();

                
            for (int i = 0; i < stackPanelTangTret.Children.Count; i++)
            {
                Grid rowGrid = (Grid)stackPanelTangTret.Children[i];

                for (int j = 0; j < rowGrid.Children.Count; j++)
                {
                    Button tableButton = (Button)(((Grid)(rowGrid.Children[j])).Children[0]);                    
                    tableButton.PreviewMouseDown += buttonTable_Click;
                    tableButtons.Add(tableButton);
                }

            }

            for (int i = 0; i < stackPanelTang1.Children.Count; i++)
            {
                Grid rowGrid = (Grid)stackPanelTang1.Children[i];

                for (int j = 0; j < rowGrid.Children.Count; j++)
                {
                    Button tableButton = (Button)(((Grid)(rowGrid.Children[j])).Children[0]);
                    tableButton.PreviewMouseDown += buttonTable_Click;
                    tableButtons.Add(tableButton);
                }

            }

            for(int i = 0; i < stackPanelTang2.Children.Count; i++)
            {
                Grid rowGrid = (Grid)stackPanelTang2.Children[i];

                for (int j = 0; j < rowGrid.Children.Count; j++)
                {
                    Button tableButton = (Button)(((Grid)(rowGrid.Children[j])).Children[0]);
                    tableButton.PreviewMouseDown += buttonTable_Click;
                    tableButtons.Add(tableButton);
                }

            }


        }

        public void SetTableStatus(int tableNumber, TableStatus status)
        {

            TextBlock statusTextBlock = ((TextBlock)((Grid)(tableButtons[tableNumber - 1].Content)).Children[0]);

            switch (status)
            {
                case TableStatus.Occupied:
                    {
                        statusTextBlock.Text = "Đang dùng";
                        statusTextBlock.Background = buttonDangDung.Background;

                        break;
                    }
                case TableStatus.Unoccupied:
                    {
                        statusTextBlock.Text = "Đang trống";
                        statusTextBlock.Background = buttonDangTrong.Background;
                        break;
                    }
                case TableStatus.Reserved:
                    {
                        statusTextBlock.Text = "Đặt trước";
                        statusTextBlock.Background = buttonDatTruoc.Background;
                        break;
                    }
            }


        }

        
        private void buttonTable_Click(object sender, MouseButtonEventArgs e)
        {
            Button tableButton = (Button)sender;

            TextBlock tableNumberTextBlock = ((TextBlock)((Grid)(tableButton.Content)).Children[1]);
            TextBlock statusTextBlock = ((TextBlock)((Grid)(tableButton.Content)).Children[0]);

            if (e.ChangedButton == MouseButton.Right)
            {
                switch (statusTextBlock.Text)
                {
                    case "Đang dùng":
                        {
                            if(ThaoHocGioi.Instance.UCOrder.IsTableEmpty(int.Parse(tableNumberTextBlock.Text)))
                            {
                                statusTextBlock.Text = "Đặt trước";
                                statusTextBlock.Background = buttonDatTruoc.Background;
                            }
                            else
                            {
                                MessageBox.Show("Bàn chưa thanh toán", "!!!", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }

                            break;
                        }
                    case "Đang trống":
                        {
                            statusTextBlock.Text = "Đang dùng";
                            statusTextBlock.Background = buttonDangDung.Background;
                            break;
                        }
                    case "Đặt trước":
                        {
                            statusTextBlock.Text = "Đang trống";
                            statusTextBlock.Background = buttonDangTrong.Background;
                            break;
                        }
                }
            }
            else
            {
                ThaoHocGioi.Instance.UCOrder.GoToTable(int.Parse(tableNumberTextBlock.Text));

                ThaoHocGioi.Instance.MoveToMenu(ThaoHocGioi.Instance.UCOrder);

            }

        }

    }
}
