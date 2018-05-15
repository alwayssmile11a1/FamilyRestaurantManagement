﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class DishDTO
    {
        public String ID { get; set; }
        public String Name { get; set; }
        public Decimal UnitPrice { get; set; }

        public DishDTO()
        {
            UnitPrice = 0;
        }

        public DishDTO(String id, String name, Decimal unitPrice)
        {
            ID = id;
            Name = name;
            UnitPrice = unitPrice;
        }

    }



}
