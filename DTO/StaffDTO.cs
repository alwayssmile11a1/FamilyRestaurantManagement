using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class StaffDTO
    {
        public String ID { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }
        public String PhoneNumber { get; set; }
        public String Email { get; set; }

        public String PositionID { get; set; }

        public Decimal Salary { get; set; }

        public StaffDTO()
        {

        }

        public StaffDTO(String id, String name, String address, String phoneNumber, String email, String positionID, Decimal salary)
        {
            ID = id;
            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
            PositionID = positionID;
            Salary = salary;
        }

    }
}
