using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Diagnostics;

namespace GUI
{
    /// <summary>
    /// Interaction logic for WindowPrintReceipt.xaml
    /// </summary>
    public partial class WindowPrintReceipt : Window
    {
        private List<DishInfo> listDishes;
        private int marginBot = 50;
        private string tableNumber;
        public WindowPrintReceipt()
        {
            InitializeComponent();



            datePicker.SelectedDate = DateTime.Now;

        }

        private void buttonInHoaDon_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxTenNhanVien.Text))
            {
                MessageBox.Show("Xin hãy nhập mã nhân viên", "!!!!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string maKhachHang = textBoxMaKhachHang.Text;

            if (string.IsNullOrEmpty(textBoxTenKhachHang.Text))
            {
                if (MessageBox.Show("Bạn chưa nhập hoặc chưa nhập đúng mã khách hàng, bạn có chắc chắn muốn in hóa đơn không?", "!!!!", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.No)
                {
                    maKhachHang = "0000";
                    return;
                }

            }


            try
            {
                //Insert sales receipt
                SalesReceiptDTO salesReceipt = new SalesReceiptDTO(SalesReceiptBUS.Instance.GetNewSalesReceiptID(), datePicker.SelectedDate.Value, maKhachHang, textBoxMaNhanVien.Text);
                SalesReceiptBUS.Instance.InsertSalesReceipt(salesReceipt);

                //Insert sales detail
                ObservableCollection<DishInfo> table = MainWindow.Instance.UCOrder.GetCurrentTable();
                for (int i = 0; i < table.Count; i++)
                {
                    SalesReceiptDetailDTO salesDetail = new SalesReceiptDetailDTO(SalesDetailBUS.Instance.GetNewSalesDetailID(), salesReceipt.ID, table[i].ID, table[i].Quantity);

                    SalesDetailBUS.Instance.InsertSalesDetail(salesDetail);
                }

                //Clear items
                MainWindow.Instance.UCOrder.ClearCurrentTable();
                ClearCustomerInfo();

                MessageBox.Show("In hóa đơn thành công", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);

                // create pdf file.

                try
                {
                    // Create the print dialog object and set options
                    PrintDialog pDialog = new PrintDialog();
                    pDialog.PageRangeSelection = PageRangeSelection.AllPages;
                    pDialog.UserPageRangeEnabled = true;
                    Nullable<bool> dialog = pDialog.ShowDialog();
                    // Display the dialog. This returns true if the user presses the Print button.
                    if (dialog == true)
                    {
                        decimal tongTien = 0;
                        UserControlBill userBill = new UserControlBill();
                        for(int i = 0; i < listDishes.Count(); i++)
                        {
                            tongTien += listDishes.ElementAt(i).Price;
                            TextBlock txbSTT = new TextBlock
                            {
                                Text = (i + 1).ToString(),
                                TextAlignment = TextAlignment.Center,
                                Margin = new Thickness(0, 0, 0, marginBot)
                            };
                            userBill.stackSTT.Children.Add(txbSTT);

                            TextBlock txbTenMonAn = new TextBlock
                            {
                                Text = listDishes.ElementAt<DishInfo>(i).DishName,
                                TextAlignment = TextAlignment.Center,
                                Margin = new Thickness(0, 0, 0, marginBot)
                            };
                            userBill.stackMonAn.Children.Add(txbTenMonAn);

                            TextBlock txbSoLuong = new TextBlock
                            {
                                Text = listDishes.ElementAt<DishInfo>(i).Quantity.ToString(),
                                TextAlignment = TextAlignment.Center,
                                Margin = new Thickness(0, 0, 0, marginBot)
                            };
                            userBill.stackSoLuong.Children.Add(txbSoLuong);

                            TextBlock txbDonGia = new TextBlock
                            {
                                Text = listDishes.ElementAt<DishInfo>(i).UnitPrice.ToString(),
                                TextAlignment = TextAlignment.Center,
                                Margin = new Thickness(0, 0, 0, marginBot)
                            };
                            userBill.stackDonGia.Children.Add(txbDonGia);

                            TextBlock txbThanhTien = new TextBlock
                            {
                                Text = listDishes.ElementAt<DishInfo>(i).Price.ToString(),
                                TextAlignment = TextAlignment.Center,
                                Margin = new Thickness(0, 0, 0, marginBot)
                            };
                            userBill.stackThanhTien.Children.Add(txbThanhTien);
                        }
                        userBill.txbTongTien.Text = tongTien.ToString("C0").Replace("$", "") + " VND";
                        userBill.txbThuNgan.Text = $"Thu ngân: {textBoxTenNhanVien.Text}";
                        userBill.txbKhachHang.Text = $"Khách hàng: {textBoxTenKhachHang.Text}";
                        userBill.txbBan.Text = $"Bàn: {tableNumber}";
                        userBill.txbTime.Text = $"Thời gian: {DateTime.Now.ToLongTimeString()}";
                        pDialog.PrintVisual(userBill.customerBill, "Test print job");
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString(), "!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            catch(BUSException ex)
            {
                MessageBox.Show(ex.ToString(), "!!!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void textBoxMaKhachHang_TextChanged(object sender, TextChangedEventArgs e)
        {


            CustomerDTO customer = CustomerBUS.Instance.FindCustomerByID(textBoxMaKhachHang.Text, true);


            if (customer != null)
            {

                textBoxTenKhachHang.Text = customer.Name;
                textBoxDienThoai.Text = customer.PhoneNumber;
                textBoxDiaChi.Text = customer.Address;
                textBoxSoTienNo.Text = String.Format("{0:#,0.000}", customer.DebtAmount);
            }
            else
            {
                textBoxTenKhachHang.Text = "";
                textBoxDienThoai.Text = "";
                textBoxDiaChi.Text = "";
                textBoxSoTienNo.Text = "0";
            }
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        public void SetData(ItemCollection itemCollection, int table)
        {
            listDishes = new List<DishInfo>();
            foreach(DishInfo dish in itemCollection.SourceCollection.Cast<DishInfo>())
            {
                DishInfo monan = new DishInfo
                {
                    DishName = dish.DishName,
                    Quantity = dish.Quantity,
                    UnitPrice = dish.UnitPrice,
                    Price = dish.Price
                };
                listDishes.Add(monan);
            }
            tableNumber = table.ToString("D2");
        }

        public void SetTongTien(String amount)
        {
            labelTongTien.Content = amount;
        }

        public void ClearCustomerInfo()
        {
            textBoxMaKhachHang.Text = "";
            textBoxTenKhachHang.Text = "";
            textBoxDienThoai.Text = "";
            textBoxDiaChi.Text = "";
            textBoxSoTienNo.Text = "0";
        }

        private void textBoxMaNhanVien_TextChanged(object sender, TextChangedEventArgs e)
        {
            StaffDTO staff = StaffBUS.Instance.FindStaffByID(textBoxMaNhanVien.Text, true);


            if (staff != null)
            {
                textBoxTenNhanVien.Text = staff.Name;

            }
            else
            {
                textBoxTenNhanVien.Text = "";

            }
        }


    }
}
