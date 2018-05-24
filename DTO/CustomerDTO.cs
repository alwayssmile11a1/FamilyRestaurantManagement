using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class CustomerDTO
    {
        public String ID { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }
        public String PhoneNumber { get; set; }
        public String Email { get; set; }

        public Decimal DebtAmount { get; set; }




        public CustomerDTO()
        {
            DebtAmount = 0;
        }



        public CustomerDTO(String id, String name, String address, String phoneNumber, String email, Decimal debtAmount)
        {
            ID = id;
            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
            DebtAmount = debtAmount;
        }


    }
}
