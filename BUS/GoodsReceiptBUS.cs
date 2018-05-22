using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using DAO;
using DTO;

namespace BUS
{
    public class GoodsReceiptBUS
    {
        public static GoodsReceiptBUS Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new GoodsReceiptBUS();
                }

                return m_Instance;
            }
        }


        private static GoodsReceiptBUS m_Instance;

        private GoodsReceiptBUS()
        {

        }

        public void InsertGoodsReceipt(GoodsReceiptDTO goodsReceipt)
        {
            try
            {
                GoodsReceiptDAO.Instance.InsertGoodsReceipt(goodsReceipt);

            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }


        public string GetNewGoodsReceiptID()
        {
            try
            {
                return GoodsReceiptDAO.Instance.GetNewGoodsReceiptID();

            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }
    }
}
