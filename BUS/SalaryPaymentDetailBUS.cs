using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using DAO;
using DTO;

namespace BUS
{
    class SalaryPaymentDetailBUS
    {
        public static SalaryPaymentDetailBUS Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new SalaryPaymentDetailBUS();
                }

                return m_Instance;
            }
        }


        private static SalaryPaymentDetailBUS m_Instance;

        private SalaryPaymentDetailBUS()
        {

        }

        public void InsertSalaryPaymentDetail(SalaryPaymentDetailDTO salaryPaymentDetail)
        {
            try
            {
                SalaryPaymentDetailDAO.Instance.InsertSalaryPaymentDetail(salaryPaymentDetail);
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }

        public string GetNewSalaryPaymentDetailID()
        {
            try
            {
               return SalaryPaymentDetailDAO.Instance.GetNewSalaryPaymentDetailID();
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }
    }
}
