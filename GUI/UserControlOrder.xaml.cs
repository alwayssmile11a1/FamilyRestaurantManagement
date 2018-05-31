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
using System.Data;
using System.Collections.ObjectModel;

namespace GUI
{
    /// <summary>
    /// Interaction logic for UserControlOrder.xaml
    /// </summary>
    public partial class UserControlOrder : UserControl
    {

        private Dictionary<string, Button> dishButtons = new Dictionary<string, Button>();
        private Dictionary<string, DishInfo> dishInfos = new Dictionary<string, DishInfo>();

        public ObservableCollection<DishInfo> DishInfoCollection
        {
            get;set;
        }

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

            DishInfoCollection = new ObservableCollection<DishInfo>();
        }


        private void CreateNewDishElement(string dishID, string dishName, string unitPrice)
        {
            Grid rowGrid;

            if (stackPanel.Children.Count == 0 || ((Grid)stackPanel.Children[stackPanel.Children.Count - 1]).Children.Count == 4)
            {
                //Create new Grid
                rowGrid = new Grid();

                rowGrid.Margin = new Thickness(10, 10, 25, 0);
                for (int i = 0; i < 4; i++)
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

            

            //Create or get dish button 
            Button dishButton;
            if (!dishButtons.TryGetValue(dishID, out dishButton))
            {
                dishButton = CreateNewDishButton(dishID, dishName, unitPrice);

                //Add to Dictionary
                dishButtons[dishID] = dishButton;
            }

            //Set button to propriate column
            Grid.SetColumn(dishButton, rowGrid.Children.Count);

            //Add to grid
            if (dishButton.Parent != null) //de-parent first 
            {
                ((Grid)dishButton.Parent).Children.Clear();
            }
            rowGrid.Children.Add(dishButton);



        }

        private Button CreateNewDishButton(string dishID, string dishName, string unitPrice)
        {
            //Create button
            Button dishButton = new Button();
            dishButton.Width = 120;
            dishButton.Height = 130;
            dishButton.BorderThickness = new Thickness(1);
            dishButton.Uid = dishID;

            //Dish name text block
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


            //Price text block
            TextBlock priceTextBlock = new TextBlock();
            priceTextBlock.HorizontalAlignment = HorizontalAlignment.Right;
            priceTextBlock.VerticalAlignment = VerticalAlignment.Top;
            priceTextBlock.FontSize = 11;
            priceTextBlock.Width = 70;
            priceTextBlock.Height = 50;
            priceTextBlock.TextAlignment = TextAlignment.Center;
            priceTextBlock.FontWeight = FontWeights.Bold;
            priceTextBlock.Text = unitPrice;

            //Stack Panel
            StackPanel dishStackPanel = new StackPanel();
            dishStackPanel.HorizontalAlignment = HorizontalAlignment.Center;
            dishStackPanel.VerticalAlignment = VerticalAlignment.Top;
            dishStackPanel.Width = 100;
            dishStackPanel.Height = 95;
            //stackPanel.Background = new ImageBrush()
            dishStackPanel.Children.Add(priceTextBlock);


            //Dish grid
            Grid dishGrid = new Grid();
            dishGrid.Width = 100;
            dishGrid.Height = 112;
            dishGrid.HorizontalAlignment = HorizontalAlignment.Center;
            dishGrid.VerticalAlignment = VerticalAlignment.Center;
            dishGrid.Children.Add(dishNameTextBlock);
            dishGrid.Children.Add(dishStackPanel);

            dishButton.Content = dishGrid;
           

            dishButton.Click += dishButton_Click;

            return dishButton;
        }

        private void buttonSearchFood_Click(object sender, RoutedEventArgs e)
        {
            //Clear 
            stackPanel.Children.Clear();

            //Find dishes by name or by id
            System.Data.DataTable dataTable = DishBUS.Instance.FindDishes("", textBoxSearchFood.Text, 0, ">=");
            if (dataTable.Rows.Count == 0)
            {
                dataTable = DishBUS.Instance.FindDishes(textBoxSearchFood.Text, "", 0, ">=");
            }
            
            //Add to stackPanel
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                CreateNewDishElement(dataTable.Rows[i].ItemArray[0].ToString(), dataTable.Rows[i].ItemArray[1].ToString(), dataTable.Rows[i].ItemArray[2].ToString());
            }

        }

        private void dishButton_Click(object sender, RoutedEventArgs e)
        {
            //Get info
            Button dishButton = (Button)sender;
            Grid grid = (Grid)(dishButton.Content);
            TextBlock dishNameTextBlock = (TextBlock)grid.Children[0];
            TextBlock priceTextBlock = (TextBlock)(((StackPanel)grid.Children[1]).Children[0]);

            

            DishInfo dishInfo;
            if (dishInfos.TryGetValue(dishButton.Uid, out dishInfo))
            {
                dishInfo.Quantity++;
                dishInfo.Price = dishInfo.Quantity * dishInfo.UnitPrice;
                dataGrid.Items.Refresh();
            }
            else
            {
                //Add info

                Decimal unitPrice = Decimal.Parse(priceTextBlock.Text.Remove(priceTextBlock.Text.Length-1).Trim());

                dishInfo = new DishInfo(dishNameTextBlock.Text, 1, unitPrice, unitPrice);
                dataGrid.Items.Add(dishInfo);
                dishInfos[dishButton.Uid] = dishInfo;
            }
            
        }

    }

    public class DishInfo
    {
        public string DishName { get; set; }
        public int Quantity { get; set; }
        public Decimal UnitPrice { get; set; }
        public Decimal Price { get; set; }

        public DishInfo()
        {

        }

        public DishInfo(string dishName, int quantity, Decimal unitPrice, Decimal price)
        {
            DishName = dishName;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Price = price;
        }

    }
}
