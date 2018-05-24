using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using MySql.Data.MySqlClient;

namespace DAO
{
    public class SalaryPaymentDetailDAO
    {
        public static SalaryPaymentDetailDAO Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new SalaryPaymentDetailDAO();
                }

                return m_Instance;
            }
        }


        private static SalaryPaymentDetailDAO m_Instance;

        private SalaryPaymentDetailDAO()
        {

        }

        public MySqlDataReader InsertSalaryPaymentDetail(SalaryPaymentDetailDTO salaryPaymentDetail)
        {
            try
            {
                // query
                string query = string.Format(@"insert into SALARYPAYMENTDETAIL values('{0}', (select SalaryPaymentStatementID from SALARYPAYMENTSTATEMENT where SalaryPaymentStatementID='{1}'),
                                                (select StaffID from STAFF where StaffID='{2}'),'{3}')", salaryPaymentDetail.ID, salaryPaymentDetail.SalaryPaymentStatementID,
                                                                                            salaryPaymentDetail.StaffID, salaryPaymentDetail.PaidAmount);
                // excute query
                return MySqlConnectionDAO.Instance.ExcuteQuery(query);

            }
            finally
            {

            }

        }

        public string GetNewSalaryPaymentDetailID()
        {
            try
            {
                int indexID = 0;
                string newID = null;

                // excute query 
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteQuery("select Max(cast(Substring(SalaryPaymentDetailID,3, length(SalaryPaymentDetailID)-2) as unsigned)) as 'MaxSalaryPaymentDetailID' from SALARYPAYMENTDETAIL");


                while (reader.Read())
                {
                    if ((reader.IsDBNull(0)))
                        break;

                    indexID = int.Parse(reader.GetString("MaxSalaryPaymentDetailID"));
                }

                // set newID string
                newID = "PD" + (indexID + 1).ToString("00000000");


                return newID;
            }
            finally
            {

            }

        }
    }
}
