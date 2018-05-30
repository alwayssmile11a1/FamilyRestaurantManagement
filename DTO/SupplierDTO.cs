using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class SupplierDTO
    {
        public String ID { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }
        public String PhoneNumber { get; set; }
        public String Email { get; set; }


        public SupplierDTO()
        {

        }

        public SupplierDTO(String id, String name, String address, String phoneNumber, String email)
        {
            ID = id;
            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
        }

    }
}
