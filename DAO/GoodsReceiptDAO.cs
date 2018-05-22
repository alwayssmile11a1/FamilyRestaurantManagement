using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using MySql.Data.MySqlClient;

namespace DAO
{
    public class GoodsReceiptDAO
    {

        public static GoodsReceiptDAO Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new GoodsReceiptDAO();
                }

                return m_Instance;
            }
        }


        private static GoodsReceiptDAO m_Instance;

        private GoodsReceiptDAO()
        {

        }

        public MySqlDataReader InsertGoodsReceipt(GoodsReceiptDTO goodsReceipt)
        {
            try
            {
                // query
                string query = string.Format("insert into GOODSRECEIPT values('{0}', '{1}', (select StaffID from Staff where StaffID='{2}'))",
                                            goodsReceipt.ID, goodsReceipt.ReceivedDate.ToString(), goodsReceipt.StaffID);
                // excute query
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteQuery(query);

                return reader;
            }
            finally
            {

            }

        }


        public string GetNewGoodsReceiptID()
        {
            try
            {
                int indexID = 0;
                string newID = null;

                // excute query 
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteQuery("select Max(cast(Substring(GoodsReceiptID,3, length(GoodsReceiptID)-2) as unsigned)) as 'MaxReceiptID' from GOODSRECEIPT");


                while (reader.Read())
                {
                    if ((reader.IsDBNull(0)))
                        break;

                    indexID = int.Parse(reader.GetString("MaxReceiptID"));
                }

                // set newID string
                newID = "GR" + (indexID + 1).ToString("00000000");


                return newID;

            }
            finally
            {

            }

        }

    }
}
