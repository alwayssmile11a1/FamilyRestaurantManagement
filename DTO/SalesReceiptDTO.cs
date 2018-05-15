using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class SalesReceiptDTO
    {

        public String ID { get; set; }
        public DateTime DateCosted { get; set; }

        public String CustomerID { get; set; }

        public String StaffID { get; set; }


        public SalesReceiptDTO()
        {

        }

        public SalesReceiptDTO(String id, DateTime dateCosted, String customerID , String staffID)
        {
            ID = id;
            DateCosted = dateCosted;
            CustomerID = customerID;
            StaffID = staffID;
        }

    }
}
