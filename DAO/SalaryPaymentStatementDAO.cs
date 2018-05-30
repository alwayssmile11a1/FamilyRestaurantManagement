using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using MySql.Data.MySqlClient;

namespace DAO
{
    public class SalaryPaymentStatementDAO
    {
        public static SalaryPaymentStatementDAO Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new SalaryPaymentStatementDAO();
                }

                return m_Instance;
            }
        }


        private static SalaryPaymentStatementDAO m_Instance;

        private SalaryPaymentStatementDAO()
        {

        }

        public MySqlDataReader InsertSalaryPaymentStatement(SalaryPaymentStatementDTO salaryPaymentStatement)
        {
            try
            {
                // query
                string query = string.Format("insert into SALARYPAYMENTSTATEMENT values('{0}', '{1}', '{2}')",
                                            salaryPaymentStatement.ID, salaryPaymentStatement.Month, salaryPaymentStatement.Year);
                // excute query
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteQuery(query);

                return reader;
            }
            finally
            {

            }

        }


        public string GetNewSalaryPaymentStatementID()
        {
            try
            {
                int indexID = 0;
                string newID = null;

                // excute query 
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteQuery("select Max(cast(Substring(SalaryPaymentStatementID,3, length(SalaryPaymentStatementID)-2) as unsigned)) as 'MaxStatementID' from SALARYPAYMENTSTATEMENT");


                while (reader.Read())
                {
                    if ((reader.IsDBNull(0)))
                        break;

                    indexID = int.Parse(reader.GetString("MaxStatementID"));
                }

                // set newID string
                newID = "SP" + (indexID + 1).ToString("00000000");


                return newID;

            }
            finally
            {

            }

        }

    }


}

