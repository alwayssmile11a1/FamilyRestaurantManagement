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
                                               new MySqlParameter("@_Phone", customer.PhoneNumber),
                                               new MySqlParameter("@_Email", customer.Email),
                                               new MySqlParameter("@_DebtAmount", customer.DebtAmount),
                                               new MySqlParameter("@_Status", true));



                return reader;
            }
            finally
            {

            }

        }


        public MySqlDataReader RemoveCustomer(string customerID)
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
                                                    new MySqlParameter("@_Phone", customer.PhoneNumber),
                                                    new MySqlParameter("@_Email", customer.Email));

                return reader;
            }
            finally
            {

            }

        }


        public MySqlDataReader AddCustomerDebt(string customerID, decimal amount)
        {


            try
            {

                //ExcuteQuery
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteProcedure("AddCustomerDebt",
                                                                                     new MySqlParameter("@_ID", customerID),
                                                                                     new MySqlParameter("@_Amount", amount));

                return reader;

            }
            finally
            {

            }

        }

        public MySqlDataReader RetriveRemovedCustomer(string customerID)
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


        public CustomerDTO FindCustomerByID(string customerID, bool status = true)
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


                    customer = new CustomerDTO(reader.GetString("CustomerID"), reader.GetString("CustomerName"), reader.GetString("CustomerAddress"),
                                             reader.GetString("Phone"), reader.GetString("Email"), decimal.Parse(reader.GetString("DebtAmount")));
                }

                return customer;

            }
            finally
            {

            }

        }


        public System.Data.DataTable FindCustomers(string customerID, string name, string address, string phoneNumber,
                                           string email, decimal debtAmount, string debtAmountCompareType,
                                            bool status)
        {
            try
            {

                System.Data.DataTable dataTable = MySqlConnectionDAO.Instance.GetDataTableByProcedure("FindCustomers", new MySqlParameter("@_ID", customerID),
                                                                            new MySqlParameter("@_Name", name), new MySqlParameter("@_Address", address),
                                                                            new MySqlParameter("@_Phone", phoneNumber), new MySqlParameter("@_Email", email),
                                                                            new MySqlParameter("@_DebtAmount", debtAmount),
                                                                            new MySqlParameter("@_DebtAmountCompareType", debtAmountCompareType),
                                                                            new MySqlParameter("@_Status", status));


                return dataTable;
            }
            finally
            {

            }
        }

        public List<string> GetAllCustomerIDs()
        {
            try
            {
                List<string> customerIDs = new List<string>();

                string query = string.Format("select CustomerID from Customer where CustomerStatus = true");

                //Excute query in MySQL
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteQuery(query);


                while (reader.Read())
                {
                    customerIDs.Add(reader.GetString("CustomerID"));
                }


                return customerIDs;
            }
            finally
            {

            }
        }


        public string GetNewCustomerID()
        {
            try
            {

                //the index of ID
                int indexID = 0;
                //new ID string
                string newID;
                //excute query 
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteQuery("select Max(cast(Substring(CustomerID,3, length(CustomerID)-2) as unsigned)) as 'MaxCustomerID' from Customer");

                //get the biggest CustomerID in database
                while (reader.Read())
                {

                    if (reader.IsDBNull(0))
                    {
                        break;
                    }

                    indexID = int.Parse(reader.GetString("MaxCustomerID"));

                }

                //get the next ID
                newID = "CU" + (indexID + 1).ToString("0000");

                return newID;

            }
            finally
            {

            }

        }

    }


}
