using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class DishDTO
    {
        public String ID { get; set; }
        public String Name { get; set; }
        public Decimal UnitPrice { get; set; }

        //Used for displaying data only
        public String StringUnitPrice { get; set; }

        public String ImagePath { get; set; }

        public DishDTO()
        {
            UnitPrice = 0;
        }

        public DishDTO(String id, String name, Decimal unitPrice, String imagePath)
        {
            ID = id;
            Name = name;
            UnitPrice = unitPrice;
            ImagePath = imagePath;
        }

    }



}
