using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using MySql.Data.MySqlClient;

namespace DAO
{
    public class SupplierDAO
    {

        public static SupplierDAO Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new SupplierDAO();
                }

                return m_Instance;
            }
        }


        private static SupplierDAO m_Instance;

        private SupplierDAO()
        {

        }


        public MySqlDataReader InsertSupplier(SupplierDTO supplier)
        {
            try
            {
                //ExcuteQuery
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteProcedure("InsertSupplier",
                                               new MySqlParameter("@_ID", supplier.ID),
                                               new MySqlParameter("@_Name", supplier.Name),
                                               new MySqlParameter("@_Address", supplier.Address),
                                               new MySqlParameter("@_Phone", supplier.PhoneNumber),
                                               new MySqlParameter("@_Email", supplier.Email),
                                               new MySqlParameter("@_Status", true));



                return reader;
            }
            finally
            {

            }

        }


        public MySqlDataReader RemoveSupplier(string supplierID)
        {
            try
            {



                //ExcuteQuery
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteProcedure("UpdateSupplierStatus",
                                                                                       new MySqlParameter("@_ID", supplierID),
                                                                                        new MySqlParameter("@_Status", false));


                return reader;
            }
            finally
            {

            }

        }

        public MySqlDataReader UpdateSupplier(SupplierDTO supplier)
        {
            try
            {
                //ExcuteQuery
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteProcedure("UpdateSupplier", new MySqlParameter("@_ID", supplier.ID),
                                                    new MySqlParameter("@_Name", supplier.Name),
                                                    new MySqlParameter("@_Address", supplier.Address),
                                                    new MySqlParameter("@_Phone", supplier.PhoneNumber),
                                                    new MySqlParameter("@_Email", supplier.Email));

                return reader;
            }
            finally
            {

            }

        }

        public MySqlDataReader RetriveRemovedSupplier(string supplierID)
        {

            try
            {

                //ExcuteQuery
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteProcedure("UpdateSupplierStatus",
                                                                                    new MySqlParameter("@_ID", supplierID),
                                                                                    new MySqlParameter("@_Status", true));

                return reader;
            }
            finally
            {

            }

        }


        public SupplierDTO FindSupplierByID(string supplierID, bool status = true)
        {

            try
            {
                //A variable to store supplier's information
                SupplierDTO supplier = null;

                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteProcedure("FindSupplier", new MySqlParameter("@_ID", supplierID),
                                                                                                     new MySqlParameter("@_Status", status));


                //get supplier's information
                while (reader.Read())
                {


                    supplier = new SupplierDTO(reader.GetString("SupplierID"), reader.GetString("SupplierName"), reader.GetString("SupplierAddress"),
                                             reader.GetString("Phone"), reader.GetString("Email"));
                }

                return supplier;

            }
            finally
            {

            }

        }


        public System.Data.DataTable FindSuppliers(string supplierID, string name, string address, string phoneNumber,
                                           string email, bool status)
        {
            try
            {

                System.Data.DataTable dataTable = MySqlConnectionDAO.Instance.GetDataTableByProcedure("FindSuppliers", new MySqlParameter("@_ID", supplierID),
                                                                            new MySqlParameter("@_Name", name), new MySqlParameter("@_Address", address),
                                                                            new MySqlParameter("@_Phone", phoneNumber), new MySqlParameter("@_Email", email),
                                                                            new MySqlParameter("@_Status", status));


                return dataTable;
            }
            finally
            {

            }
        }

        public List<string> GetAllSupplierIDs()
        {
            try
            {
                List<string> supplierIDs = new List<string>();

                string query = string.Format("select SupplierID from Supplier where SupplierStatus = true");

                //Excute query in MySQL
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteQuery(query);


                while (reader.Read())
                {
                    supplierIDs.Add(reader.GetString("SupplierID"));
                }


                return supplierIDs;
            }
            finally
            {

            }
        }


        public string GetNewSupplierID()
        {
            try
            {

                //the index of ID
                int indexID = 0;
                //new ID string
                string newID;
                //excute query 
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteQuery("select Max(cast(Substring(SupplierID,3, length(SupplierID)-2) as unsigned)) as 'MaxSupplierID' from Supplier");

                //get the biggest SupplierID in database
                while (reader.Read())
                {

                    if (reader.IsDBNull(0))
                    {
                        break;
                    }

                    indexID = int.Parse(reader.GetString("MaxSupplierID"));

                }

                //get the next ID
                newID = "SU" + (indexID + 1).ToString("0000");

                return newID;

            }
            finally
            {

            }

        }

    }
}
