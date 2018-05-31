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
using BUS;
using System.Diagnostics;

namespace GUI
{
    /// <summary>
    /// Interaction logic for UserControlOrder.xaml
    /// </summary>
    public partial class UserControlOrder : UserControl
    {

        private Dictionary<string, Button> dishButtons = new Dictionary<string, Button>();


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


            //Create dish element
            System.Data.DataTable dataTable = DishBUS.Instance.FindDishes("", "", 0, ">=");

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {

                CreateNewDishElement(dataTable.Rows[i].ItemArray[0].ToString(), dataTable.Rows[i].ItemArray[1].ToString(), dataTable.Rows[i].ItemArray[2].ToString());

            }

        }


        private void CreateNewDishElement(string dishID, string dishName, string unitPrice)
        {

            if (stackPanel.Children.Count == 0 || ((Grid)stackPanel.Children[stackPanel.Children.Count - 1]).Children.Count == 4)
            {
                //Create new Grid
                Grid grid = new Grid();

                grid.Margin = new Thickness(10, 10, 25, 0);
                for (int i = 0; i < 4; i++)
                {
                    ColumnDefinition columnDefinition = new ColumnDefinition();
                    grid.ColumnDefinitions.Add(columnDefinition);
                }

                stackPanel.Children.Add(grid);


            }

            //Add new dish element to grid
            Grid rowGrid = ((Grid)stackPanel.Children[stackPanel.Children.Count - 1]);


            Button dishButton;

            if (!dishButtons.TryGetValue(dishID, out dishButton))
            {
                dishButton = CreateNewDishButton(dishName, unitPrice);

                //Add to Dictionary
                dishButtons[dishID] = dishButton;
            }

            Grid.SetColumn(dishButton, rowGrid.Children.Count);

            //Add to grid
            if (dishButton.Parent != null)
            {
                ((Grid)dishButton.Parent).Children.Clear();
            }

            rowGrid.Children.Add(dishButton);


        }

        private Button CreateNewDishButton(string dishName, string unitPrice)
        {
            //Create button
            Button dishButton = new Button();
            dishButton.Width = 120;
            dishButton.Height = 130;
            dishButton.BorderThickness = new Thickness(1);

            Grid grid = new Grid();
            grid.Width = 100;
            grid.Height = 112;
            grid.HorizontalAlignment = HorizontalAlignment.Center;
            grid.VerticalAlignment = VerticalAlignment.Center;

            TextBlock dishNameTextBlock = new TextBlock();
            dishNameTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
            dishNameTextBlock.VerticalAlignment = VerticalAlignment.Bottom;
            dishNameTextBlock.Text = dishName;
            dishNameTextBlock.Width = 100;
            dishNameTextBlock.Height = 35;
            dishNameTextBlock.TextAlignment = TextAlignment.Center;
            dishNameTextBlock.FontSize = 11;
            dishNameTextBlock.FontWeight = FontWeights.Bold;
            dishNameTextBlock.TextWrapping = TextWrapping.Wrap;

            StackPanel stackPanel = new StackPanel();
            stackPanel.HorizontalAlignment = HorizontalAlignment.Center;
            stackPanel.VerticalAlignment = VerticalAlignment.Top;
            stackPanel.Width = 100;
            stackPanel.Height = 95;

            TextBlock priceTextBlock = new TextBlock();
            priceTextBlock.HorizontalAlignment = HorizontalAlignment.Right;
            priceTextBlock.VerticalAlignment = VerticalAlignment.Top;
            priceTextBlock.FontSize = 11;
            priceTextBlock.Width = 70;
            priceTextBlock.Height = 50;
            priceTextBlock.TextAlignment = TextAlignment.Center;
            priceTextBlock.FontWeight = FontWeights.Bold;
            priceTextBlock.Text = unitPrice;


            //stackPanel.Background = new ImageBrush()

            stackPanel.Children.Add(priceTextBlock);

            grid.Children.Add(dishNameTextBlock);
            grid.Children.Add(stackPanel);

            dishButton.Content = grid;


            return dishButton;
        }

        private void buttonSearchFood_Click(object sender, RoutedEventArgs e)
        {
            stackPanel.Children.Clear();

            System.Data.DataTable dataTable = DishBUS.Instance.FindDishes("", textBoxSearchFood.Text, 0, ">=");

            if (dataTable.Rows.Count == 0)
            {
                dataTable = DishBUS.Instance.FindDishes(textBoxSearchFood.Text, "", 0, ">=");
            }

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                CreateNewDishElement(dataTable.Rows[i].ItemArray[0].ToString(), dataTable.Rows[i].ItemArray[1].ToString(), dataTable.Rows[i].ItemArray[2].ToString());
            }

        }

    }

}
