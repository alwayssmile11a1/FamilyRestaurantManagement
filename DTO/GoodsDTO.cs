using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    class GoodsDTO
    {

        public String ID { get; set; }
        public String Name { get; set; }

        public String SupplierID { get; set; }

        public Decimal UnitPrice { get; set; }

        public GoodsDTO()
        {
            UnitPrice = 0;
        }

        public GoodsDTO(String id, String name,String supplierID ,Decimal unitPrice)
        {
            ID = id;
            Name = name;
            UnitPrice = unitPrice;
        }

    }
}
