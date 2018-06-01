using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class SalesReceiptDetailDTO
    {
        public String ID { get; set; }
        public String SalesReceiptID { get; set; }

        public String DishID { get; set; }

        public int Quantity { get; set; }

        public SalesReceiptDetailDTO()
        {
            Quantity = 0;
        }

        public SalesReceiptDetailDTO(String id, String salesReceiptDetailID, String dishID, int quantity)
        {
            ID = id;
            SalesReceiptID = salesReceiptDetailID;
            DishID = dishID;
            Quantity = quantity;
        }



    }
}
