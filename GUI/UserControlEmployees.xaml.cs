using System;
using System.Collections.Generic;
using System.Data;
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

namespace GUI
{
    /// <summary>
    /// Interaction logic for UserControlEmployees.xaml
    /// </summary>
    public partial class UserControlEmployees : UserControl
    {

        private Dictionary<string, string> staffPositionsNameToID = new Dictionary<string, string>();
        private Dictionary<string, string> staffPositionsIDToName = new Dictionary<string, string>();

        public UserControlEmployees()
        {
            InitializeComponent();

            
            //Setup combobox
            comboBoxKieuSoSanh.Items.Add(">");
            comboBoxKieuSoSanh.Items.Add(">=");
            comboBoxKieuSoSanh.Items.Add("=");
            comboBoxKieuSoSanh.Items.Add("<");
            comboBoxKieuSoSanh.Items.Add("<=");
            comboBoxKieuSoSanh.SelectedIndex = 1;

            //Setup combobox
            comboBoxTimViTri.Items.Add("");
            try
            {
                DataTable dataTable = StaffPositionBUS.Instance.GetAllStaffPosition();

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    string staffPositionID = dataTable.Rows[i].ItemArray[0].ToString();
                    string staffPositionName = dataTable.Rows[i].ItemArray[1].ToString();
                    comboBoxThemViTri.Items.Add(staffPositionName);
                    comboBoxTimViTri.Items.Add(staffPositionName);
                    staffPositionsNameToID[staffPositionName] = staffPositionID;
                    staffPositionsIDToName[staffPositionID] = staffPositionName;
                }

            }
            catch (BUSException ex)
            {
                MessageBox.Show(ex.ToString(), "!!!", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            DisplayAllStaffs();

        }

        private void UpdateGridByDataTable(DataTable dataTable)
        {
            dataGridEmployees.Items.Clear();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];

                StaffDTO staff = new StaffDTO(row.ItemArray[0].ToString(), row.ItemArray[1].ToString(), row.ItemArray[2].ToString(), 
                                                row.ItemArray[3].ToString(), row.ItemArray[4].ToString(), row.ItemArray[5].ToString(),
                                                staffPositionsIDToName[row.ItemArray[6].ToString()], 
                                                Decimal.Parse(row.ItemArray[7].ToString().Remove(row.ItemArray[7].ToString().Length - 1).Trim()));


                dataGridEmployees.Items.Add(staff);

            }
        }

        private void DisplayAllStaffs()
        {
            //Display all staffs by default
            try
            {
                DataTable dataTable = StaffBUS.Instance.FindStaffs("", "", "", "", "", "", "", 0, ">=");


                UpdateGridByDataTable(dataTable);

            }
            catch (BUSException ex)
            {
                MessageBox.Show(ex.ToString(), "!!!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAddNewEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                textBoxThemMaNhanVien.Text = StaffBUS.Instance.GetNewStaffID();
            }
            catch (BUSException ex)
            {
                MessageBox.Show(ex.ToString(), "!!!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            flyoutAddEmployee.IsOpen = true;
        }

        private void btnFindEmployee_Click(object sender, RoutedEventArgs e)
        {
            flyoutFindEmployee.IsOpen = true;
        }

        private void buttonThem_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(textBoxThemHoTen.Text) || string.IsNullOrEmpty(comboBoxThemViTri.Text) || string.IsNullOrEmpty(textBoxThemMucLuong.Text))
            {
                MessageBox.Show("Xin hãy nhập những trường thông tin cần thiết", "!!!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn thêm không?", "!!!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {

                StaffDTO staffDTO = new StaffDTO(textBoxThemMaNhanVien.Text, textBoxThemHoTen.Text, textBoxThemDiaChi.Text, textBoxThemDienThoai.Text,
                                    textBoxThemEmail.Text, textBoxThemCMND.Text, staffPositionsNameToID[comboBoxThemViTri.Text], decimal.Parse(textBoxThemMucLuong.Text));


                try
                {
                    StaffBUS.Instance.InsertStaff(staffDTO);

                    MessageBox.Show("Thêm nhân viên thành công", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);

                    textBoxThemMaNhanVien.Text = StaffBUS.Instance.GetNewStaffID();

                    DisplayAllStaffs();

                }
                catch (BUSException ex)
                {
                    MessageBox.Show(ex.ToString(), "!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                }


            }



        }

        private void buttonTim_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                DataTable dataTable = StaffBUS.Instance.FindStaffs(textBoxTimMaNhanVien.Text, textBoxTimHoTen.Text, textBoxTimDiaChi.Text,
                                                                textBoxTimDienThoai.Text, textBoxTimEmail.Text, textBoxTimCMND.Text,
                                                                string.IsNullOrEmpty(comboBoxTimViTri.Text) ? "" : staffPositionsNameToID[comboBoxTimViTri.Text],
                                                                string.IsNullOrEmpty(textBoxTimMucLuong.Text)?0:decimal.Parse(textBoxTimMucLuong.Text), comboBoxKieuSoSanh.Text);


                UpdateGridByDataTable(dataTable);


                flyoutFindEmployee.IsOpen = false;
            }
            catch (BUSException ex)
            {
                MessageBox.Show(ex.ToString(), "!!!", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void buttonXoa_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "!!!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {

                    StaffDTO staff = (StaffDTO)dataGridEmployees.SelectedItem;

                    StaffBUS.Instance.RemoveStaff(staff.ID);

                    DisplayAllStaffs();

                    MessageBox.Show("Xóa nhân viên thành công", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                catch (BUSException ex)
                {
                    MessageBox.Show(ex.ToString(), "!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            DisplayAllStaffs();
        }
    }
}
