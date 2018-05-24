using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using DAO;
using DTO;

namespace BUS
{
    public class SalaryPaymentStatementBUS
    {
        public static SalaryPaymentStatementBUS Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new SalaryPaymentStatementBUS();
                }

                return m_Instance;
            }
        }


        private static SalaryPaymentStatementBUS m_Instance;

        private SalaryPaymentStatementBUS()
        {

        }

        public void InsertSalaryPaymentStatement(SalaryPaymentStatementDTO salaryPaymentStatement)
        {
            try
            {
                SalaryPaymentStatementDAO.Instance.InsertSalaryPaymentStatement(salaryPaymentStatement);
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }


        public string GetNewSalaryPaymentStatementID()
        {
            try
            {
                return SalaryPaymentStatementDAO.Instance.GetNewSalaryPaymentStatementID();
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }
    }
}
