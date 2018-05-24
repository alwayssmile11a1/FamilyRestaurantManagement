using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class SalaryPaymentDetailDTO
    {
        public String ID { get; set; }
        public String SalaryPaymentStatementID { get; set; }

        public String StaffID { get; set; }

        public Decimal PaidAmount { get; set; }

        public SalaryPaymentDetailDTO()
        {
            PaidAmount = 0;
        }

        public SalaryPaymentDetailDTO(String id, String salaryPaymentStatementID, String staffID, Decimal paidAmount)
        {
            ID = id;
            SalaryPaymentStatementID = salaryPaymentStatementID;
            StaffID = staffID;
            PaidAmount = paidAmount;
        }

    }
}
