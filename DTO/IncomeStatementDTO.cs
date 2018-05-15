using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class IncomeStatementDTO
    {
        public String ID { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public IncomeStatementDTO()
        {
            Month = 1;
            Year = 2018;
        }

        public IncomeStatementDTO(String id, int month, int year)
        {
            Month = month;
            Year = year;
        }

    }
}
