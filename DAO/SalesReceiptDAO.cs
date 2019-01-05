using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using MySql.Data.MySqlClient;

namespace DAO
{
    public class SalesReceiptDAO
    {

        public static SalesReceiptDAO Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new SalesReceiptDAO();
                }

                return m_Instance;
            }
        }


        private static SalesReceiptDAO m_Instance;

        private SalesReceiptDAO()
        {

        }

        public MySqlDataReader InsertSalesReceipt(SalesReceiptDTO salesReceipt)
        {
            try
            {
                // query
                string query = string.Format("insert into SALESRECEIPT values('{0}', (select CustomerID from CUSTOMER where CustomerID='{1}'),'{2}',(select StaffID from Staff where StaffID='{3}'))",
                                            salesReceipt.ID, salesReceipt.CustomerID, string.Format("{0}-{1}-{2}", salesReceipt.DateCosted.Year, salesReceipt.DateCosted.Month,salesReceipt.DateCosted.Day), salesReceipt.StaffID);
                // excute query
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteQuery(query);

                return reader;
            }
            finally
            {

            }

        }

        public List<SalesReceiptDTO> GetSalesReceipt(int month, int year)
        {
            try
            {
                DateTime startDate = new DateTime(year, month, 1);
                DateTime endDate = new DateTime(year, month, DateTime.DaysInMonth(year,month));

                List<SalesReceiptDTO> list = new List<SalesReceiptDTO>();

                // query
                string query = string.Format("select* from SALESRECEIPT where DateCosted >= {0} and DateCosted <= {1}", startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"));

                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteQuery(query);


                //get salesreceipt's information
                while (reader.Read())
                {
                    list.Add(new SalesReceiptDTO(reader.GetString("ReceiptID"), DateTime.Parse(reader.GetString("DateCosted")), reader.GetString("CustomerID"), reader.GetString("StaffID")));
                }

                return list;

            }
            finally
            {

            }
        }

        public string GetNewSalesReceiptID()
        {
            try
            {
                int indexID = 0;
                string newID = null;

                // excute query 
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteQuery("select Max(cast(Substring(ReceiptID,3, length(ReceiptID)-2) as unsigned)) as 'MaxReceiptID' from SALESRECEIPT");


                while (reader.Read())
                {
                    if ((reader.IsDBNull(0)))
                        break;

                    indexID = int.Parse(reader.GetString("MaxReceiptID"));
                }

                // set newID string
                newID = "SR" + (indexID + 1).ToString("00000000");


                return newID;

            }
            finally
            {

            }

        }
    }


}
