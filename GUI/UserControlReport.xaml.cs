using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
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
using Microsoft;
using Microsoft.Win32;


namespace GUI
{
    /// <summary>
    /// Interaction logic for UserControlReport.xaml
    /// </summary>
    public partial class UserControlReport : UserControl
    {
        public UserControlReport()
        {
            InitializeComponent();

            for (int i = 0; i < 12; i++)
            {
                comboBoxMonth.Items.Add(i + 1);
            }

            for (int i = 2016; i < 2050; i++)
            {
                comboBoxYear.Items.Add(i + 1);
            }
            comboBoxMonth.SelectedItem = DateTime.Today.Month;
            comboBoxYear.SelectedItem = DateTime.Today.Year;
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {

            //map id to dishes
            Dictionary<string, ReportDetailInfo> mapIDToDish = new Dictionary<string, ReportDetailInfo>();
            List < DishDTO > dishes = DishBUS.Instance.GetAllDishes();
            int i = 1;
            foreach (var dish in dishes)
            {
                mapIDToDish[dish.ID] = new ReportDetailInfo() {STT = i, DishID=dish.ID, DishName = dish.Name, Quantity=0, UnitPrice = dish.UnitPrice};
                i++;
            }
            

            //get all sales receipt of the specified month
            List<SalesReceiptDTO> salesReceiptList = SalesReceiptBUS.Instance.GetSalesReceipt(int.Parse(comboBoxMonth.SelectedItem.ToString()),int.Parse(comboBoxYear.SelectedItem.ToString()));

            //loop through all sales receipt of the specified month
            foreach (SalesReceiptDTO salesReceipt in salesReceiptList)
            {
                List<SalesReceiptDetailDTO> salesDetailList = SalesDetailBUS.Instance.GetSalesDetail(salesReceipt.ID);

                //loop through all sales detail of the specified month
                foreach (SalesReceiptDetailDTO salesDetail in salesDetailList)
                {
                    //add quantity
                    mapIDToDish[salesDetail.DishID].Quantity += salesDetail.Quantity;
                }

            }


            //Try to display 
            dataGridReport.Items.Clear();
            Decimal totalMoney = 0;
            foreach (var item in mapIDToDish)
            {
                dataGridReport.Items.Add(item.Value);
                totalMoney += item.Value.TotalPrice;
            }

            txtboxTotalMoney.Text = totalMoney.ToString();

        }


        private void btnExportExcel_Click(object sender, RoutedEventArgs e)
        {
            //Set Filter being displayed 
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Title = "Export";
            saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";

            //If user presses OK
            if (saveDialog.ShowDialog() == true)
            {
                //get excel application
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

                //Add workbook to excel
                Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(Type.Missing);

                //Store worksheet
                Microsoft.Office.Interop.Excel.Worksheet worksheet = null;

                try
                {
                    //Get active worksheet
                    worksheet = (Microsoft.Office.Interop.Excel.Worksheet)(workbook.ActiveSheet);

                    worksheet.Cells[1, 1] = "STT";
                    worksheet.Cells[1, 2] = "Mã món ăn";
                    worksheet.Cells[1, 3] = "Tên món ăn";
                    worksheet.Cells[1, 4] = "Số lượng";
                    worksheet.Cells[1, 5] = "Đơn giá";
                    worksheet.Cells[1, 6] = "Thành tiền";

                    //Set the value for remaining row of worksheet by the value being presented in GridView 
                    for (int iRow = 0; iRow < dataGridReport.Items.Count; iRow++)
                    {
                        //get current row
                        ReportDetailInfo reportDetailInfo = (ReportDetailInfo)dataGridReport.Items[iRow];

                        worksheet.Cells[iRow + 2, 1] = reportDetailInfo.STT;
                        worksheet.Cells[iRow + 2, 2] = reportDetailInfo.DishID;
                        worksheet.Cells[iRow + 2, 3] = reportDetailInfo.DishName;
                        worksheet.Cells[iRow + 2, 4] = reportDetailInfo.Quantity;
                        worksheet.Cells[iRow + 2, 5] = reportDetailInfo.UnitPrice;
                        worksheet.Cells[iRow + 2, 6] = reportDetailInfo.TotalPrice;

                    }

                    //Don't allow displaying alerts
                    //this is just a setup to prevent some kind of disturb things
                    excel.DisplayAlerts = false;

                    //Save Workbook
                    workbook.SaveAs(saveDialog.FileName);

                    excel.DisplayAlerts = true;

                    //Display successful dialog
                    MessageBox.Show("Xuất excel thành công", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    workbook.Close();
                    excel.Quit();
                    workbook = null;
                    excel = null;
                    GC.Collect();
                }
            }
        }

    }

    class ReportDetailInfo
    {
        public int STT { get; set; }
        public string DishID { get; set; }
        public string DishName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get { return Quantity * UnitPrice; } }


    }
  

}
