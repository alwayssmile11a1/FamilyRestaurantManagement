using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    class CustomerDTO
    {
        public String ID { get; set; }
        public String Name { get; set; }
        public String Adress { get; set; }
        public String PhoneNumber { get; set; }
        public String Email { get; set; }

        public Decimal DebtAmount { get; private set; }




        public CustomerDTO()
        {
            DebtAmount = 0;
        }



        public CustomerDTO(String maKhachHang, String hoTenKhachHang, String diaChi, String dienThoai, String email, Decimal soTienNo)
        {
            ID = maKhachHang;
            Name = hoTenKhachHang;
            Adress = diaChi;
            PhoneNumber = dienThoai;
            Email = email;
            DebtAmount = soTienNo;
        }


        public void AddDebt(Decimal amount)
        {
            DebtAmount += amount;
        }

    }
}
