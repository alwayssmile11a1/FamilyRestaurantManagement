using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Data;


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
                dishImage.Visibility = Visibility.Visible;
                gridImage.Visibility = Visibility.Hidden;
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
