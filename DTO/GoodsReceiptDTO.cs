using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class GoodsReceiptDTO
    {
        public String ID { get; set; }
        public DateTime ReceivedDate { get; set; }

        public String StaffID { get; set; }


        public GoodsReceiptDTO()
        {

        }

        public GoodsReceiptDTO(String id, DateTime date, String staffID)
        {
            ID = id;
            ReceivedDate = date;
            StaffID = staffID;
        }

    }
}
