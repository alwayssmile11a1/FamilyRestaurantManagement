using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using DAO;
using DTO;
using System.Text.RegularExpressions;

namespace BUS
{
    class CustomerBUS
    {
        public static CustomerBUS Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new CustomerBUS();
                }

                return m_Instance;
            }
        }


        private static CustomerBUS m_Instance;

        private CustomerBUS()
        {

        }


        public CustomerDTO FindCustomerByID(string customerID, bool status)
        {
            try
            {
                return CustomerDAO.Instance.FindCustomerByID(customerID, status);
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }
        }

        public System.Data.DataTable FindCustomers(string customerID, string name, string address, string phoneNumber, string email, decimal soTienNo, string debtAmountCompareType, bool status)
        {
            try
            {
                return CustomerDAO.Instance.FindCustomers(customerID, name, address, phoneNumber, email, soTienNo, debtAmountCompareType, status);
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }
        }

        public List<string> GetAllCustomerIDs()
        {
            try
            {
                return CustomerDAO.Instance.GetAllCustomerIDs();
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }
        }

        public string GetNewCustomerID()
        {
            try
            {
                return CustomerDAO.Instance.GetNewCustomerID();
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);

            }
        }

        public void InsertCustomer(CustomerDTO customer)
        {
            try
            {
                if (IsRightFormat(customer))
                {
                    CustomerDAO.Instance.InsertCustomer(customer);
                }
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }
        }

        public void RemoveCustomer(string maKhachHang)
        {
            try
            {
                CustomerDAO.Instance.RemoveCustomer(maKhachHang);
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }
        }

        public void UpdateCustomer(CustomerDTO customer)
        {
            try
            {
                if (IsRightFormat(customer))
                    CustomerDAO.Instance.UpdateCustomer(customer);
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }
        }

        public void UpdateCustomerDebt(string customerID, decimal amount)
        {
            try
            {
                CustomerDAO.Instance.AddCustomerDebt(customerID, amount);
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }
        }

        public void RetriveRemovedCustomer(string customerID)
        {
            try
            {
                CustomerDAO.Instance.RetriveRemovedCustomer(customerID);
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }
        }

        private bool IsRightFormat(CustomerDTO customer)
        {
            if ((customer.ID.Length > 10))
            {
                throw new BUSException("Mã khách hàng không quá 10 kí tự");
            }

            if ((customer.Name.Length > 100 | customer.Name.Trim().Length <= 0))
            {
                throw new BUSException("Họ tên khách hàng không dưới 0 ký tự và không quá 100 kí tự");

            }

            if ((customer.Address.Length > 100 | customer.Address.Trim().Length <= 0))
            {
                throw new BUSException("Địa chỉ khách hàng không dưới 0 ký tự và không quá 100 kí tự");

            }

            if ((customer.PhoneNumber.Length > 20 | customer.PhoneNumber.Trim().Length <= 0))
            {
                throw new BUSException("Điện thoại không dưới 0 ký tự và không quá 20 kí tự");

            }

            if ((customer.Email.Length > 50))
            {
                throw new BUSException("Email không quá 50 kí tự");

            }

            if ((Regex.IsMatch(customer.Name.Trim(), "^[\\s\\w]*$") == false | Regex.IsMatch(customer.Address.Trim(), "^[\\s\\w]*$") == false | Regex.IsMatch(customer.PhoneNumber.Trim(), "^[\\s\\w]*$") == false))
            {
                throw new BUSException("tên khách hàng, địa chỉ hoặc điện thoại không được chứa kí tự đặc biệt");

            }

            //if ((FormatChecking.CheckValid.IsValidPhoneNumber(customer.DienThoai, FormatChecking.CountryCode.Vietnam) == false))
            //{
            //    exception = "Số điện thoại không hợp lệ";

            //}

            //if ((customer.EMail != "" & FormatChecking.CheckValid.IsValidEmail(customer.EMail) == false))
            //{
            //    exception = "Email không hợp lệ";

            //}

            //if ((customer.SoTienNo > 9999999999L))
            //{
            //    exception = "Đơn giá tối đa 10 kí tự";

            //}

            return true;
        }
    }
}
