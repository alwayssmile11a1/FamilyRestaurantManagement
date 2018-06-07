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

        public String IdentityNumber { get; set; }

        public String PositionID { get; set; }

        public decimal Salary { get; set; }

        //Use for display data only
        public String StringSalary { get; set; }

        public StaffDTO()
        {

        }

        public StaffDTO(String id, String name, String address, String phoneNumber, String email, string identityNumber, String positionID, decimal salary)
        {
            ID = id;
            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
            IdentityNumber = identityNumber;
            PositionID = positionID;
            Salary = salary;
        }

    }
}
