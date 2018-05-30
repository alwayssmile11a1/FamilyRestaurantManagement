using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using MySql.Data.MySqlClient;

namespace DAO
{
    public class GoodsReceiptDetailDAO
    {
        public static GoodsReceiptDetailDAO Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new GoodsReceiptDetailDAO();
                }

                return m_Instance;
            }
        }


        private static GoodsReceiptDetailDAO m_Instance;

        private GoodsReceiptDetailDAO()
        {

        }

        public MySqlDataReader InsertGoodsReceiptDetail(GoodsReceiptDetailDTO goodsReceiptDetail)
        {
            try
            {
                // query
                string query = string.Format(@"insert into GOODSRECEIPTDETAIL values('{0}', (select GoodsReceiptID from GOODSRECEIPT where GoodsReceiptID='{1}'),
                                                (select GoodsID from GOODS where GoodsID='{2}'),'{3}')", goodsReceiptDetail.ID, goodsReceiptDetail.GoodsReceiptID, 
                                                                                            goodsReceiptDetail.GoodsID, goodsReceiptDetail.Quantity);
                // excute query
                return MySqlConnectionDAO.Instance.ExcuteQuery(query);

            }
            finally
            {

            }

        }

        public string GetNewGoodsReceiptDetailID()
        {
            try
            {
                int indexID = 0;
                string newID = null;

                // excute query 
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteQuery("select Max(cast(Substring(GoodsReceiptDetailID,3, length(GoodsReceiptDetailID)-2) as unsigned)) as 'MaxGoodsReceiptDetailID' from GOODSRECEIPTDETAIL");


                while (reader.Read())
                {
                    if ((reader.IsDBNull(0)))
                        break;

                    indexID = int.Parse(reader.GetString("MaxGoodsReceiptDetailID"));
                }

                // set newID string
                newID = "GD" + (indexID + 1).ToString("00000000");


                return newID;
            }
            finally
            {

            }

        }
    }
}
