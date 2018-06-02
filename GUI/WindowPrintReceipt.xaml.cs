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
        public WindowPrintReceipt()
        {
            InitializeComponent();



            datePicker.SelectedDate = DateTime.Now;

        }

        private void buttonInHoaDon_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxTenNhanVien.Text == "")
            {
                MessageBox.Show("Xin hãy nhập mã nhân viên", "!!!!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string maKhachHang = textBoxMaKhachHang.Text;

            if (textBoxTenKhachHang.Text == "")
            {
                if (MessageBox.Show("Bạn chưa nhập hoặc chưa nhập đúng mã khách hàng, bạn có chắc chắn muốn in hóa đơn không?", "!!!!", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.No)
                {
                    maKhachHang = "0000";
                    return;
                }

            }

            //Insert sales receipt
            SalesReceiptDTO salesReceipt = new SalesReceiptDTO(SalesReceiptBUS.Instance.GetNewSalesReceiptID(), datePicker.SelectedDate.Value, maKhachHang, textBoxMaNhanVien.Text);
            SalesReceiptBUS.Instance.InsertSalesReceipt(salesReceipt);

            //Insert sales detail
            ObservableCollection<DishInfo> table = ThaoHocGioi.Instance.UCOrder.GetCurrentTable();
            for (int i = 0; i < table.Count; i++)
            {
                SalesReceiptDetailDTO salesDetail = new SalesReceiptDetailDTO(SalesDetailBUS.Instance.GetNewSalesDetailID(), salesReceipt.ID, table[i].ID, table[i].Quantity);

                SalesDetailBUS.Instance.InsertSalesDetail(salesDetail);
            }

            //Clear items
            ThaoHocGioi.Instance.UCOrder.ClearCurrentTable();

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
