using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Data;
using BUS;
using DTO;
using System.Text.RegularExpressions;
using System.IO;

namespace GUI
{
    /// <summary>
    /// Interaction logic for UserControlDishes.xaml
    /// </summary>
    public partial class UserControlDishes : UserControl
    {
        private DataTable dataDishes = new DataTable();
        public UserControlDishes()
        {
            InitializeComponent();
            DisplayAllDishes();
        }

        private void UpdateGridByDataTable(DataTable dataTable)
        {
            dataGridDishes.Items.Clear();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];



                DishDTO dish = new DishDTO(row.ItemArray[0].ToString(), row.ItemArray[1].ToString(), 
                                            Decimal.Parse(row.ItemArray[2].ToString().Remove(row.ItemArray[2].ToString().Length - 1).Trim().Replace(",", "")),
                                            row.ItemArray[3].ToString());
                dish.StringUnitPrice = row.ItemArray[2].ToString();
                dataGridDishes.Items.Add(dish);

            }
        }

        private void DisplayAllDishes()
        {
            //Display all dishes by default
            try
            {
                DataTable dataTable = DishBUS.Instance.FindDishes("", "", 0, ">=");


                UpdateGridByDataTable(dataTable);

            }
            catch (BUSException ex)
            {
                MessageBox.Show(ex.ToString(), "!!!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSellectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog imagePicker = new OpenFileDialog();
            imagePicker.Title = "Select a picture";
            imagePicker.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";

            if (imagePicker.ShowDialog() == true)
            {
                dishImage.Source = new BitmapImage(new Uri(imagePicker.FileName));
                txtboxDishImage.Text = imagePicker.FileName;
                dishImage.Visibility = Visibility.Visible;
                gridImage.Visibility = Visibility.Hidden;
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtboxDishID.Text = DishBUS.Instance.GetNewDishID();
            txtboxDishName.Text = "";
            txtboxDishPrice.Text = "";
            txtboxDishImage.Text = "";
            dishImage.Visibility = Visibility.Hidden;
            gridImage.Visibility = Visibility.Visible;
            btnAction.Visibility = Visibility.Hidden;
            DisplayAllDishes();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            btnAction.Content = "Đồng ý thêm";
            txtboxDishID.Text = DishBUS.Instance.GetNewDishID();
            txtboxDishID.IsReadOnly = true;
            btnAction.Visibility = Visibility.Visible;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            btnAction.Content = "Đồng ý sửa";
            txtboxDishID.IsReadOnly = false;
            btnAction.Visibility = Visibility.Visible;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            btnAction.Content = "Đồng ý tìm kiếm";
            txtboxDishID.IsReadOnly = false;
            btnAction.Visibility = Visibility.Visible;
        }

        private void AddDish()
        {
            if (string.IsNullOrEmpty(txtboxDishID.Text) || string.IsNullOrEmpty(txtboxDishName.Text) || string.IsNullOrEmpty(txtboxDishPrice.Text))
            {
                MessageBox.Show("Xin hãy nhập những trường thông tin cần thiết", "!!!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn thêm không?", "!!!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {

                try
                {
                    DishDTO dishDTO = new DishDTO(txtboxDishID.Text, txtboxDishName.Text, decimal.Parse(txtboxDishPrice.Text), txtboxDishImage.Text);

                    DishBUS.Instance.InsertDish(dishDTO);

                    MessageBox.Show("Thêm thực đơn thành công", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);

                    txtboxDishID.Text = StaffBUS.Instance.GetNewStaffID();

                    DisplayAllDishes();

                }
                catch (BUSException ex)
                {
                    MessageBox.Show(ex.ToString(), "!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                }


            }
        }

        private void UpdateDish()
        {
            if (string.IsNullOrEmpty(txtboxDishID.Text) || string.IsNullOrEmpty(txtboxDishName.Text) || string.IsNullOrEmpty(txtboxDishPrice.Text))
            {
                MessageBox.Show("Xin hãy nhập những trường thông tin cần thiết", "!!!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn cập nhật không?", "!!!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {

                try
                {
                    DishDTO dishDTO = new DishDTO(txtboxDishID.Text, txtboxDishName.Text, decimal.Parse(txtboxDishPrice.Text), txtboxDishImage.Text);

                    DishBUS.Instance.UpdateDish(dishDTO);

                    MessageBox.Show("Cập nhật thực đơn thành công", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);

                    txtboxDishID.Text = StaffBUS.Instance.GetNewStaffID();

                    DisplayAllDishes();

                }
                catch (BUSException ex)
                {
                    MessageBox.Show(ex.ToString(), "!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "!!!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {

                    DishDTO dish = (DishDTO)dataGridDishes.SelectedItem;

                    DishBUS.Instance.RemoveDish(dish.ID);

                    DisplayAllDishes();

                    MessageBox.Show("Xóa món ăn thành công", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                catch (BUSException ex)
                {
                    MessageBox.Show(ex.ToString(), "!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void SearchDish()
        {
            try
            {

                DataTable dataTable = DishBUS.Instance.FindDishes(txtboxDishID.Text, txtboxDishName.Text, string.IsNullOrEmpty(txtboxDishPrice.Text) ? 0 : decimal.Parse(txtboxDishPrice.Text), ">=");


                UpdateGridByDataTable(dataTable);

            }
            catch (BUSException ex)
            {
                MessageBox.Show(ex.ToString(), "!!!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool IsNumber(string text)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            return regex.IsMatch(text);
        }

        private void txtboxDishPrice_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = IsNumber(e.Text);
        }

        private void btnAction_Click(object sender, RoutedEventArgs e)
        {
            switch(btnAction.Content.ToString())
            {
                case "Đồng ý thêm":
                    {
                        AddDish();
                        break;
                    }
                case "Đồng ý sửa":
                    {
                        UpdateDish();
                        break;
                    }
                case "Đồng ý tìm kiếm":
                    {
                        SearchDish();
                        break;
                    }
            }
        }

        private void txtDishID_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (btnAction.Content.ToString() == "Đồng ý sửa")
            {
                DishDTO dishDTO = DishBUS.Instance.FindDishByID(txtboxDishID.Text);

                if (dishDTO != null)
                {
                    txtboxDishName.Text = dishDTO.Name;
                    txtboxDishImage.Text = dishDTO.ImagePath;
                    txtboxDishPrice.Text = dishDTO.UnitPrice.ToString();
                    if (File.Exists(dishDTO.ImagePath))
                        dishImage.Source = new BitmapImage(new Uri(dishDTO.ImagePath, UriKind.RelativeOrAbsolute));
                    dishImage.Visibility = Visibility.Visible;
                    gridImage.Visibility = Visibility.Hidden;
                }
                else
                {
                    txtboxDishName.Text = "";
                    txtboxDishImage.Text = "";
                    txtboxDishPrice.Text = "";
                    dishImage.Visibility = Visibility.Hidden;
                    gridImage.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
