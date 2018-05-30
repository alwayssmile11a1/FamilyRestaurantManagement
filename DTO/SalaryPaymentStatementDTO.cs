using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class SalaryPaymentStatementDTO
    {

        public String ID { get; set; }
        public int Month { get; set; }

        public int Year { get; set; }

        public SalaryPaymentStatementDTO()
        {
            Month = 1;
            Year = 2018;
        }

        public SalaryPaymentStatementDTO(String id, int month, int year)
        {
            ID = id;
            Month = month;
            Year = year;    
        }


    }
}
