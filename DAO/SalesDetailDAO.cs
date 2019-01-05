using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using MySql.Data.MySqlClient;

namespace DAO
{
    public class SalesDetailDAO
    {

        public static SalesDetailDAO Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new SalesDetailDAO();
                }

                return m_Instance;
            }
        }


        private static SalesDetailDAO m_Instance;

        private SalesDetailDAO()
        {

        }

        public MySqlDataReader InsertSalesDetail(SalesReceiptDetailDTO salesDetail)
        {
            try
            {
                // query
                string query = string.Format(@"insert into SALESRECEIPTDETAIL values('{0}', (select ReceiptID from SALESRECEIPT where ReceiptID='{1}'),
                                                (select DishID from DISH where DishID='{2}'),'{3}')", salesDetail.ID, salesDetail.SalesReceiptID, salesDetail.DishID, salesDetail.Quantity);
                // excute query
                return MySqlConnectionDAO.Instance.ExcuteQuery(query);

            }
            finally
            {

            }

        }

        public List<SalesReceiptDetailDTO> GetSalesDetail(string salesReceiptID)
        {
            try
            {
                List<SalesReceiptDetailDTO> list = new List<SalesReceiptDetailDTO>();

                // query
                string query = string.Format("select* from SALESRECEIPTDETAIL where ReceiptID = {0]", salesReceiptID);

                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteQuery(query);


                //get salesreceiptdetail's information
                while (reader.Read())
                {
                    list.Add(new SalesReceiptDetailDTO(reader.GetString("ReceiptDetailID"), reader.GetString("ReceiptID"), reader.GetString("DishID"), reader.GetInt32("SalesQuantity")));
                }

                return list;

            }
            finally
            {

            }
        }

        public string GetNewSalesDetailID()
        {
            try
            {
                int indexID = 0;
                string newID = null;

                // excute query 
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteQuery("select Max(cast(Substring(ReceiptDetailID,3, length(ReceiptDetailID)-2) as unsigned)) as 'MaxSalesDetailID' from SALESRECEIPTDETAIL");


                while (reader.Read())
                {
                    if ((reader.IsDBNull(0)))
                        break;

                    indexID = int.Parse(reader.GetString("MaxSalesDetailID"));
                }

                // set newID string
                newID = "SD" + (indexID + 1).ToString("00000000");


                return newID;
            }
            finally
            {

            }

        }
    }

}
