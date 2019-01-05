using System;
using System.Collections.Generic;
using System.Diagnostics;
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


            buttonDangTrongTangTret.Click += buttonSearchByStatus_Click;
            buttonDangTrongTang1.Click += buttonSearchByStatus_Click;
            buttonDangTrongTang2.Click += buttonSearchByStatus_Click;

            buttonDangDungTangTret.Click += buttonSearchByStatus_Click;
            buttonDangDungTang1.Click += buttonSearchByStatus_Click;
            buttonDangDungTang2.Click += buttonSearchByStatus_Click;

            buttonDatTruocTangTret.Click += buttonSearchByStatus_Click;
            buttonDatTruocTang1.Click += buttonSearchByStatus_Click;
            buttonDatTruocTang2.Click += buttonSearchByStatus_Click;


            buttonTatCaTangTret.Click += buttonSearchByStatus_Click;
            buttonTatCaTang1.Click += buttonSearchByStatus_Click;
            buttonTatCaTang2.Click += buttonSearchByStatus_Click;


            ButtonSearchFloor0.Click += buttonSearchByNumber_Click;
            ButtonSearchFloor1.Click += buttonSearchByNumber_Click;
            ButtonSearchFloor2.Click += buttonSearchByNumber_Click;
        }

        public void SetTableStatus(int tableNumber, TableStatus status)
        {

            TextBlock statusTextBlock = ((TextBlock)((Grid)(tableButtons[tableNumber - 1].Content)).Children[0]);

            switch (status)
            {
                case TableStatus.Occupied:
                    {
                        statusTextBlock.Text = "Đang dùng";
                        statusTextBlock.Background = buttonDangDungTang1.Background;

                        break;
                    }
                case TableStatus.Unoccupied:
                    {
                        statusTextBlock.Text = "Đang trống";
                        statusTextBlock.Background = buttonDangTrongTang1.Background;
                        break;
                    }
                case TableStatus.Reserved:
                    {
                        statusTextBlock.Text = "Đặt trước";
                        statusTextBlock.Background = buttonDatTruocTang1.Background;
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
                            if(MainWindow.Instance.UCOrder.IsTableEmpty(int.Parse(tableNumberTextBlock.Text)))
                            {
                                statusTextBlock.Text = "Đặt trước";
                                statusTextBlock.Background = buttonDatTruocTang1.Background;
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
                            statusTextBlock.Background = buttonDangDungTang1.Background;
                            break;
                        }
                    case "Đặt trước":
                        {
                            statusTextBlock.Text = "Đang trống";
                            statusTextBlock.Background = buttonDangTrongTang1.Background;
                            break;
                        }
                }
            }
            else
            {
                MainWindow.Instance.UCOrder.GoToTable(int.Parse(tableNumberTextBlock.Text));

                MainWindow.Instance.MoveToMenu(MainWindow.Instance.UCOrder);

            }

        }


        private void buttonSearchByStatus_Click(object sender, RoutedEventArgs e)
        {

            StackPanel stackPanel = null;
            int minIndex = 0;
            int maxIndex = 0;
            
            switch (((Grid)((Button)sender).Parent).Uid)
            {
                case "0":
                    {
                        stackPanel = stackPanelTangTret;
                        minIndex = 1;
                        maxIndex = 6;
                        break;
                    }
                case "1":
                    {
                        stackPanel = stackPanelTang1;
                        minIndex = 7;
                        maxIndex = 14;
                        break;
                    }
                case "2":
                    {
                        stackPanel = stackPanelTang2;
                        minIndex = 15;
                        maxIndex = 20;
                        break;
                    }
            }


            UpdateStackPanel(stackPanel, minIndex, maxIndex, ((Button)(sender)).Content.ToString());

        }

        private void buttonSearchByNumber_Click(object sender, RoutedEventArgs e)
        {
            StackPanel stackPanel = null;
            int minIndex = 30;
            int maxIndex = 0;

            switch (((Grid)((Button)sender).Parent).Uid)
            {
                case "0":
                    {
                        stackPanel = stackPanelTangTret;
                        int.TryParse(TextBoxSearchFloor0.Text, out minIndex);
                        maxIndex = 6;
                        break;
                    }
                case "1":
                    {
                        stackPanel = stackPanelTang1;
                        int.TryParse(TextBoxSearchFloor1.Text, out minIndex);
                        maxIndex = 14;
                        break;
                    }
                case "2":
                    {
                        stackPanel = stackPanelTang2;
                        int.TryParse(TextBoxSearchFloor2.Text, out minIndex);
                        maxIndex = 20;
                        break;
                    }
            }

            if (minIndex <= maxIndex && minIndex > 0)
            {
                UpdateStackPanel(stackPanel, minIndex, minIndex, "Tất cả");

            }
            else
            {
                UpdateStackPanel(stackPanel, 1, 0, "");
            }
        }



        private void UpdateStackPanel(StackPanel stackPanel, int minIndex, int maxIndex, String status)
        {
            if (stackPanel == null) return;

            stackPanel.Children.Clear();

            for (int i = minIndex; i <= maxIndex; i++)
            {
                if (i-1 < 0) return;

                Button tableButton = tableButtons[i-1];
                TextBlock statusTextBlock = ((TextBlock)((Grid)(tableButton.Content)).Children[0]);
                Debug.WriteLine(statusTextBlock.Text);

                if (statusTextBlock.Text.Equals(status) || status.Equals("Tất cả"))
                {
                    //Create rơw
                    Grid rowGrid;
                    if (stackPanel.Children.Count == 0 || ((Grid)stackPanel.Children[stackPanel.Children.Count - 1]).Children.Count == 4)
                    {
                        //Create new Grid
                        rowGrid = new Grid();

                        rowGrid.Margin = new Thickness(30, 20, 45, 0);
                        for (int j = 0; j < 4; j++)
                        {
                            ColumnDefinition columnDefinition = new ColumnDefinition();
                            rowGrid.ColumnDefinitions.Add(columnDefinition);
                        }


                        //add grid to panel
                        stackPanel.Children.Add(rowGrid);

                    }
                    else
                    {
                        rowGrid = ((Grid)stackPanel.Children[stackPanel.Children.Count - 1]);
                    }

                    //Set button to propriate column
                    Grid.SetColumn(tableButton, rowGrid.Children.Count);

                    //Add to grid
                    if (tableButton.Parent != null) //de-parent first 
                    {
                        ((Grid)tableButton.Parent).Children.Clear();
                    }
                    rowGrid.Children.Add(tableButton);
                }
            }
        }



        private void buttonSearchTable_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
