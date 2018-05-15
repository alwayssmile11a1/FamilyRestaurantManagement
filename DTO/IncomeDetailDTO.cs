using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class IncomeDetailDTO
    {

        public String ID { get; set; }
        public String IncomeStatementID { get; set; }

        public String CustomerID { get; set; }

        public Decimal IncomeAmount { get; set; }

        public IncomeDetailDTO()
        {
            IncomeAmount = 0;
        }

        public IncomeDetailDTO(String id, String incomeStatementID, String customerID, Decimal incomeAmount)
        {
            ID = id;
            IncomeStatementID = incomeStatementID;
            CustomerID = customerID;
            IncomeAmount = incomeAmount;
        }


    }
}
