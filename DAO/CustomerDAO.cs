using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using MySql.Data.MySqlClient;

namespace DAO
{
    public class CustomerDAO
    {
        public static CustomerDAO Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new CustomerDAO();
                }

                return m_Instance;
            }
        }


        private static CustomerDAO m_Instance;

        private CustomerDAO()
        {

        }


        public MySqlDataReader InsertCustomer(CustomerDTO customer)
        {
            try
            {
                //ExcuteQuery
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteProcedure("InsertCustomer",
                                               new MySqlParameter("@_ID", customer.ID),
                                               new MySqlParameter("@_Name", customer.Name),
                                               new MySqlParameter("@_Address", customer.Address),
                                               new MySqlParameter("@_PhoneNumber", customer.PhoneNumber),
                                               new MySqlParameter("@_Email", customer.Email),
                                               new MySqlParameter("@_DebtAmount", customer.DebtAmount),
                                               new MySqlParameter("@_Status", true));



                return reader;
            }
            finally
            {

            }

        }


        public MySqlDataReader RemoveCustomer(String customerID)
        {
            try
            {



                //ExcuteQuery
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteProcedure("UpdateCustomerStatus",
                                                                                       new MySqlParameter("@_ID", customerID),
                                                                                        new MySqlParameter("@_Status", false));


                return reader;
            }
            finally
            {

            }

        }

        public MySqlDataReader UpdateCustomer(CustomerDTO customer)
        {
            try
            {
                //ExcuteQuery
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteProcedure("UpdateCustomer", new MySqlParameter("@_ID", customer.ID),
                                                    new MySqlParameter("@_Name", customer.Name),
                                                    new MySqlParameter("@_Address", customer.Address),
                                                    new MySqlParameter("@_PhoneNumber", customer.PhoneNumber),
                                                    new MySqlParameter("@_Email", customer.Email));

                return reader;
            }
            finally
            {

            }

        }


        public MySqlDataReader AddCustomerDebt(String customerID, Decimal amount)
        {


            try {

                //ExcuteQuery
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteProcedure("AddCustomerDebt",
                                                                                     new MySqlParameter("@_ID", customerID),
                                                                                     new MySqlParameter("@_DebtAmount", amount));

                return reader;

            }
            finally
            {

            }

        }

        public MySqlDataReader RetriveRemovedCustomer(String customerID)
        {

            try
            {

                //ExcuteQuery
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteProcedure("UpdateCustomerStatus",
                                                                                    new MySqlParameter("@_ID", customerID),
                                                                                    new MySqlParameter("@_Status", true));

                return reader;
            }
            finally
            {

            }

        }


        public CustomerDTO FindCustomerByID(String customerID, bool status = true)
        {

            try
            {
                //A variable to store customer's information
                CustomerDTO customer = null;

                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteProcedure("FindCustomer", new MySqlParameter("@_ID", customerID),
                                                                                                     new MySqlParameter("@_Status", status));


                //get customer's information
                while (reader.Read())
                {


                    customer = new CustomerDTO(reader.GetString("ID"), reader.GetString("Name"), reader.GetString("Address"),
                                             reader.GetString("PhoneNumber"), reader.GetString("Email"), Decimal.Parse(reader.GetString("DebtAmount")));
                }

                return customer;

            }
            finally
            {

            }

        }


        public System.Data.DataTable FindCustomers(String customerID, String name, String address, String phoneNumber,
                                           String email, Decimal debtAmount, String debtAmountCompareType,
                                            bool status)
        {
            try
            {

                System.Data.DataTable dataTable = MySqlConnectionDAO.Instance.GetDataTableByProcedure("FindCustomers", new MySqlParameter("@_ID", customerID),
                                                                            new MySqlParameter("@_Name", name), new MySqlParameter("@_Address", address),
                                                                            new MySqlParameter("@_PhoneNumber", phoneNumber), new MySqlParameter("@_Email", email),
                                                                            new MySqlParameter("@_DebtAmount", debtAmount),
                                                                            new MySqlParameter("@_DebtAmountCompareType", debtAmountCompareType),
                                                                            new MySqlParameter("@_Status", status));


                return dataTable;
            }
            finally
            {

            }
        }

        public List<String> GetAllCustomerIDs()
        {
            try
            {
                List<String> customerIDs = new List<string>();

                String query = String.Format("select MaKhachHang from KHACHHANG where TinhTrang = true");

                //Excute query in MySQL
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteQuery(query);


                while (reader.Read())
                {
                    customerIDs.Add(reader.GetString("MaKhachHang"));
                }


                return customerIDs;
            }
            finally
            {

            }
        }


        public String GetNewCustomerID()
        {
            try
            {

                //the index of ID
                int indexID = 0;
                //new ID string
                String newID;
                //excute query 
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteQuery("select Max(cast(Substring(MaKhachHang,3, length(MaKhachHang)-2) as unsigned)) as 'MaxMaKhachHang' from KHACHHANG");

                //get the biggest MaKhachHang in database
                while (reader.Read())
                {

                    if (reader.IsDBNull(0))
                    {
                        break;
                    }

                    indexID = int.Parse(reader.GetString("MaxMaKhachHang"));

                }

                //get the next ID
                newID = "KH" + (indexID + 1).ToString("0000");

                return newID;

            }
            finally
            {

            }

        }

    }


}
