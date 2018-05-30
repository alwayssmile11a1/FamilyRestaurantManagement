using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class GoodsReceiptDetailDTO
    {

        public String ID { get; set; }
        public String GoodsReceiptID { get; set; }

        public String GoodsID { get; set; }

        public int Quantity { get; set; }


        public GoodsReceiptDetailDTO()
        {
            Quantity = 0;
        }

        public GoodsReceiptDetailDTO(String id, String goodsReceiptID, String goodsID, int quantity)
        {
            ID = id;
            GoodsReceiptID = goodsReceiptID;
            GoodsID = goodsID;
            Quantity = quantity;
        }


    }
}
