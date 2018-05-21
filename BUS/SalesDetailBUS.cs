using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using DAO;
using DTO;

namespace BUS
{
    class SalesDetailBUS
    {

        public static SalesDetailBUS Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new SalesDetailBUS();
                }

                return m_Instance;
            }
        }


        private static SalesDetailBUS m_Instance;

        private SalesDetailBUS()
        {

        }

        public void InsertSalesDetail(SalesReceiptDetailDTO salesDetail)
        {
            try
            {
                SalesDetailDAO.Instance.InsertSalesDetail(salesDetail);
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }

        public string GetNewSalesDetailID()
        {
            try
            {
                return SalesDetailDAO.Instance.GetNewSalesDetailID();
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }
    }
}
