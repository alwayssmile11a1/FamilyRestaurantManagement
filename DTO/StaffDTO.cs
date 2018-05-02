using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    class StaffDTO
    {
        public String ID { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }
        public String PhoneNumber { get; set; }
        public String Email { get; set; }

        public String Position { get; set; }

        public Decimal Salary { get; private set; }





        public StaffDTO()
        {
            Salary = 0;
        }



        public StaffDTO(String id, String name, String address, String phoneNumber, String email, String position,Decimal salary)
        {
            ID = id;
            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
            Salary = salary;
            Position = position;
        }


        public void AddSalary(Decimal amount)
        {
            Salary += amount;
        }
    }
}
