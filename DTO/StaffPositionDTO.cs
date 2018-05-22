using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class StaffPositionDTO
    {

        public String ID { get; set; }
        public String Name { get; set; }

        public float PayRate { get; set; }

        public StaffPositionDTO()
        {
            
        }

        public StaffPositionDTO(String id, String name, float payRate)
        {
            ID = id;
            Name = name;
            PayRate = payRate;
        }

    }
}
