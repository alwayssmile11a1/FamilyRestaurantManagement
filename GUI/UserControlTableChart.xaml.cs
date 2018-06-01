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
                    tableButtons.Add((Button)(((Grid)(rowGrid.Children[j])).Children[0]));
                }

            }

            for (int i = 0; i < stackPanelTang1.Children.Count; i++)
            {
                Grid rowGrid = (Grid)stackPanelTang1.Children[i];

                for (int j = 0; j < rowGrid.Children.Count; j++)
                {
                    tableButtons.Add((Button)(((Grid)(rowGrid.Children[j])).Children[0]));
                }

            }

            for(int i = 0; i < stackPanelTang2.Children.Count; i++)
            {
                Grid rowGrid = (Grid)stackPanelTang2.Children[i];

                for (int j = 0; j < rowGrid.Children.Count; j++)
                {
                    tableButtons.Add((Button)(((Grid)(rowGrid.Children[j])).Children[0]));
                }

            }


        }

        public void SetTableStatus(int tableNumber, TableStatus status)
        {

            TextBlock statusButton = ((TextBlock)((Grid)(tableButtons[tableNumber - 1].Content)).Children[0]);

            switch (status)
            {
                case TableStatus.Occupied:
                    {
                        statusButton.Text = "Đang dùng";
                        statusButton.Background = buttonDangDung.Background;

                        break;
                    }
                case TableStatus.Unoccupied:
                    {
                        statusButton.Text = "Đang trống";
                        statusButton.Background = buttonDangTrong.Background;
                        break;
                    }
                case TableStatus.Reserved:
                    {
                        statusButton.Text = "Đặt trước";
                        statusButton.Background = buttonDatTruoc.Background;
                        break;
                    }
            }


        }


    }
}
