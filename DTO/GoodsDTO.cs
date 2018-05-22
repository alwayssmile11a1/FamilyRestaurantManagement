using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class GoodsDTO
    {

        public String ID { get; set; }
        public String Name { get; set; }

        public String SupplierID { get; set; }

        public Decimal UnitPrice { get; set; }

        public int Stock { get; set; }

        public GoodsDTO()
        {
            UnitPrice = 0;
        }

        public GoodsDTO(String id, String name,String supplierID ,Decimal unitPrice, int stock)
        {
            ID = id;
            Name = name;
            UnitPrice = unitPrice;
            SupplierID = supplierID;
            Stock = stock;
        }

    }
}
