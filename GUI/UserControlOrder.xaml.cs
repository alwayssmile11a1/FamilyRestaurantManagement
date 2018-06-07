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
using DTO;
using System.Diagnostics;
using System.Data;
using System.Collections.ObjectModel;
using System.IO;

namespace GUI
{
    /// <summary>
    /// Interaction logic for UserControlOrder.xaml
    /// </summary>
    public partial class UserControlOrder : UserControl
    {

        private Dictionary<string, Button> dishButtons = new Dictionary<string, Button>();


        private Dictionary<string, ObservableCollection<DishInfo>> tables = new Dictionary<string, ObservableCollection<DishInfo>>();

        private WindowPrintReceipt windowPrintReceipt = null;


        public UserControlOrder()
        {
            InitializeComponent();

            ComboBoxTableNumber.Items.Add("Mang đi");
            for (int i = 1; i <= 20; i++)
                ComboBoxTableNumber.Items.Add(i);

            ComboBoxTableNumber.SelectedIndex = 1;

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

                CreateNewDishElement(dataTable.Rows[i].ItemArray[0].ToString(), dataTable.Rows[i].ItemArray[1].ToString(), dataTable.Rows[i].ItemArray[2].ToString(), dataTable.Rows[i].ItemArray[3].ToString());

            }

        }

        public ObservableCollection<DishInfo> GetCurrentTable()
        {
            if (tables.ContainsKey(ComboBoxTableNumber.SelectedItem.ToString()))
                return tables[ComboBoxTableNumber.SelectedItem.ToString()];

            return null;
        }

        public void ClearCurrentTable()
        {
            //Clear items
            dataGrid.Items.Clear();
            dataGrid.Items.Refresh();
            tables.Remove(ComboBoxTableNumber.SelectedItem.ToString());
            UpdatePrice();
            ThaoHocGioi.Instance.UCTableChart.SetTableStatus(int.Parse(ComboBoxTableNumber.SelectedItem.ToString()), UserControlTableChart.TableStatus.Unoccupied);
        }

        public void GoToTable(int tableNumber)
        {
            ComboBoxTableNumber.SelectedIndex = tableNumber;
        }

        public bool IsTableEmpty(int tableNumber)
        {

            if (tables.ContainsKey(tableNumber.ToString()))
            {
                return tables[tableNumber.ToString()].Count == 0;
            }

            return true;

        }

        private void CreateNewDishElement(string dishID, string dishName, string unitPrice, string imagePath)
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
                dishButton = CreateNewDishButton(dishID, dishName, unitPrice, imagePath);

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

        private Button CreateNewDishButton(string dishID, string dishName, string unitPrice, string imagePath)
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
            dishNameTextBlock.Height = 30;
            dishNameTextBlock.TextAlignment = TextAlignment.Center;
            dishNameTextBlock.FontSize = 11;
            dishNameTextBlock.Background = new SolidColorBrush(Color.FromRgb(255, 0, 102)); 
            dishNameTextBlock.FontWeight = FontWeights.Bold;
            dishNameTextBlock.TextWrapping = TextWrapping.Wrap;


            //Price text block
            TextBlock priceTextBlock = new TextBlock();
            priceTextBlock.HorizontalAlignment = HorizontalAlignment.Right;
            priceTextBlock.VerticalAlignment = VerticalAlignment.Top;
            priceTextBlock.FontSize = 11;
            priceTextBlock.Width = 70;
            priceTextBlock.Height = 15;
            priceTextBlock.Background = new SolidColorBrush(Color.FromRgb(255, 0, 102));
            priceTextBlock.TextAlignment = TextAlignment.Center;
            priceTextBlock.FontWeight = FontWeights.Bold;
            priceTextBlock.Text = unitPrice;

            //Stack Panel
            StackPanel dishStackPanel = new StackPanel();
            dishStackPanel.HorizontalAlignment = HorizontalAlignment.Center;
            dishStackPanel.VerticalAlignment = VerticalAlignment.Top;
            dishStackPanel.Width = 100;
            dishStackPanel.Height = 95;



            if (File.Exists(imagePath))
            {
                BitmapImage bitimg = new BitmapImage();
                bitimg.BeginInit();
                bitimg.UriSource = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                bitimg.EndInit();
                dishStackPanel.Background = new ImageBrush(bitimg);
            }
            else
            {
                Debug.WriteLine(imagePath);
            }
            dishStackPanel.Children.Add(priceTextBlock);


            //Dish grid
            Grid dishGrid = new Grid();
            dishGrid.Width = 100;
            dishGrid.Height = 112;
            dishGrid.HorizontalAlignment = HorizontalAlignment.Center;
            dishGrid.VerticalAlignment = VerticalAlignment.Center;
            dishGrid.Children.Add(dishStackPanel);
            dishGrid.Children.Add(dishNameTextBlock);

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
                CreateNewDishElement(dataTable.Rows[i].ItemArray[0].ToString(), dataTable.Rows[i].ItemArray[1].ToString(), dataTable.Rows[i].ItemArray[2].ToString(), dataTable.Rows[i].ItemArray[3].ToString());
            }

        }

        private void dishButton_Click(object sender, RoutedEventArgs e)
        {
            //Get info
            Button dishButton = (Button)sender;
            Grid grid = (Grid)(dishButton.Content);
            TextBlock dishNameTextBlock = (TextBlock)grid.Children[1];
            TextBlock priceTextBlock = (TextBlock)(((StackPanel)grid.Children[0]).Children[0]);

            DishInfo dishInfo = null;
            if (tables.ContainsKey(ComboBoxTableNumber.SelectedItem.ToString()))
            {
                dishInfo = GetDishInfo(tables[ComboBoxTableNumber.SelectedItem.ToString()], dishButton.Uid);
            }
            //if existed
            if (dishInfo != null)
            {
                dishInfo.Quantity++;
                dishInfo.Price = dishInfo.Quantity * dishInfo.UnitPrice;
                dataGrid.Items.Refresh();
            }
            else //new 
            {

                Decimal unitPrice = Decimal.Parse(priceTextBlock.Text.Remove(priceTextBlock.Text.Length - 1).Trim());

                dishInfo = new DishInfo(dishButton.Uid, dishNameTextBlock.Text, 1, unitPrice, unitPrice);
                dataGrid.Items.Add(dishInfo);

                if (!tables.ContainsKey(ComboBoxTableNumber.SelectedItem.ToString()))
                {
                    tables[ComboBoxTableNumber.SelectedItem.ToString()] = new ObservableCollection<DishInfo>();
                }
                tables[ComboBoxTableNumber.SelectedItem.ToString()].Add(dishInfo);

            }


            UpdatePrice();

            if (ComboBoxTableNumber.SelectedIndex != 0)
            {
                ThaoHocGioi.Instance.UCTableChart.SetTableStatus(int.Parse(ComboBoxTableNumber.SelectedItem.ToString()), UserControlTableChart.TableStatus.Occupied);
            }
        }

        private DishInfo GetDishInfo(ObservableCollection<DishInfo> collection, string id)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                if (collection[i].ID == id)
                {
                    return collection[i];
                }
            }

            return null;
        }

        private void UpdatePrice()
        {
            decimal sum = 0;

            if (tables.ContainsKey(ComboBoxTableNumber.SelectedItem.ToString()))
            {
                ObservableCollection<DishInfo> table = tables[ComboBoxTableNumber.SelectedItem.ToString()];
                for (int i = 0; i < table.Count; i++)
                {
                    sum += table[i].Price;
                }
            }


            labelRawTongCong.Content = String.Format("{0:#,0.000}", sum) + " đ";
            labelTongTien.Content = String.Format("{0:#,0.000}", sum) + " đ";

        }


        private void plusButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedIndex >= 0)
            {
                DishInfo dishInfo = (DishInfo)dataGrid.Items[dataGrid.SelectedIndex];
                dishInfo.Quantity++;
                dishInfo.Price = dishInfo.Quantity * dishInfo.UnitPrice;

                dataGrid.Items.Refresh();
            }


            UpdatePrice();
        }

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedIndex >= 0)
            {
                DishInfo dishInfo = (DishInfo)dataGrid.Items[dataGrid.SelectedIndex];

                dishInfo.Quantity--;

                if (dishInfo.Quantity == 0)
                {
                    dataGrid.Items.Remove(dishInfo);

                    tables[ComboBoxTableNumber.SelectedItem.ToString()].Remove(dishInfo);

                }
                else
                {
                    dishInfo.Price = dishInfo.Quantity * dishInfo.UnitPrice;
                }
                dataGrid.Items.Refresh();
            }


            UpdatePrice();
        }

        private void ComboBoxTableNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tables.ContainsKey(ComboBoxTableNumber.SelectedItem.ToString()))
            {
                dataGrid.Items.Clear();

                ObservableCollection<DishInfo> table = tables[ComboBoxTableNumber.SelectedItem.ToString()];

                for (int i = 0; i < table.Count; i++)
                {
                    dataGrid.Items.Add(table[i]);
                }

                dataGrid.Items.Refresh();

            }
            else
            {
                dataGrid.Items.Clear();
                dataGrid.Items.Refresh();
            }

            UpdatePrice();
        }

        private void buttonInHoaDon_Click(object sender, RoutedEventArgs e)
        {

            if (dataGrid.Items.Count == 0) return;

            //Create new window and show user control
            if (windowPrintReceipt == null)
            {
                windowPrintReceipt = new WindowPrintReceipt();
            }

            windowPrintReceipt.SetTongTien(labelTongTien.Content.ToString());
            windowPrintReceipt.ClearCustomerInfo();
            windowPrintReceipt.Show();

        }

        private void btnLamMoi_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa bàn hiện tại không?", "!!!!", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                ClearCurrentTable();
            }
        }
    }

    public class DishInfo
    {
        public String ID { get; set; }
        public string DishName { get; set; }
        public int Quantity { get; set; }
        public Decimal UnitPrice { get; set; }
        public Decimal Price { get; set; }

        public DishInfo()
        {

        }

        public DishInfo(string id, string dishName, int quantity, Decimal unitPrice, Decimal price)
        {
            ID = id;
            DishName = dishName;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Price = price;
        }

    }

}
