using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using DAO;
using DTO;


namespace BUS
{
    public class GoodsReceiptDetailBUS
    {
        public static GoodsReceiptDetailBUS Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new GoodsReceiptDetailBUS();
                }

                return m_Instance;
            }
        }


        private static GoodsReceiptDetailBUS m_Instance;

        private GoodsReceiptDetailBUS()
        {

        }

        public void InsertGoodsReceiptDetail(GoodsReceiptDetailDTO goodsReceiptDetail)
        {
            try
            {
                GoodsReceiptDetailDAO.Instance.InsertGoodsReceiptDetail(goodsReceiptDetail);

            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }

        public string GetNewGoodsReceiptDetailID()
        {
            try
            {
                return GoodsReceiptDetailDAO.Instance.GetNewGoodsReceiptDetailID();

            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }
    }
}
