using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using DAO;
using DTO;

namespace BUS
{
    class SalesReceiptBUS
    {
        public static SalesReceiptBUS Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new SalesReceiptBUS();
                }

                return m_Instance;
            }
        }


        private static SalesReceiptBUS m_Instance;

        private SalesReceiptBUS()
        {

        }

        public void InsertSalesReceipt(SalesReceiptDTO salesReceipt)
        {
            try
            {
                SalesReceiptDAO.Instance.InsertSalesReceipt(salesReceipt);
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }
        }


        public string GetNewSalesReceiptID()
        {
            try
            {
               return SalesReceiptDAO.Instance.GetNewSalesReceiptID();
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }
    }
}
