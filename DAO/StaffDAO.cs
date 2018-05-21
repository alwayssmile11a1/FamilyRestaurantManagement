using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using MySql.Data.MySqlClient;

namespace DAO
{
    public class StaffDAO
    {
        public static StaffDAO Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new StaffDAO();
                }

                return m_Instance;
            }
        }


        private static StaffDAO m_Instance;

        private StaffDAO()
        {

        }


        public MySqlDataReader InsertStaff(StaffDTO staff)
        {
            try
            {
                //ExcuteQuery
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteProcedure("InsertStaff",
                                               new MySqlParameter("@_ID", staff.ID),
                                               new MySqlParameter("@_Name", staff.Name),
                                               new MySqlParameter("@_Address", staff.Address),
                                               new MySqlParameter("@_Phone", staff.PhoneNumber),
                                               new MySqlParameter("@_Email", staff.Email),
                                               new MySqlParameter("@_PositionID", staff.PositionID),
                                               new MySqlParameter("@_Salary", staff.Salary),
                                               new MySqlParameter("@_Status", true));



                return reader;
            }
            finally
            {

            }

        }


        public MySqlDataReader RemoveStaff(string staffID)
        {
            try
            {



                //ExcuteQuery
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteProcedure("UpdateStaffStatus",
                                                                                       new MySqlParameter("@_ID", staffID),
                                                                                        new MySqlParameter("@_Status", false));


                return reader;
            }
            finally
            {

            }

        }

        public MySqlDataReader UpdateStaff(StaffDTO staff)
        {
            try
            {
                //ExcuteQuery
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteProcedure("UpdateStaff", new MySqlParameter("@_ID", staff.ID),
                                                    new MySqlParameter("@_Name", staff.Name),
                                                    new MySqlParameter("@_Address", staff.Address),
                                                    new MySqlParameter("@_Phone", staff.PhoneNumber),
                                                    new MySqlParameter("@_Email", staff.Email),
                                                    new MySqlParameter("@_PositionID", staff.PositionID));

                return reader;
            }
            finally
            {

            }

        }


        public MySqlDataReader AddStaffSalary(string staffID, decimal amount)
        {


            try
            {

                //ExcuteQuery
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteProcedure("AddStaffSalary",
                                                                                     new MySqlParameter("@_ID", staffID),
                                                                                     new MySqlParameter("@_Amount", amount));

                return reader;

            }
            finally
            {

            }

        }

        public MySqlDataReader RetriveRemovedStaff(string staffID)
        {

            try
            {

                //ExcuteQuery
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteProcedure("UpdateStaffStatus",
                                                                                    new MySqlParameter("@_ID", staffID),
                                                                                    new MySqlParameter("@_Status", true));

                return reader;
            }
            finally
            {

            }

        }


        public StaffDTO FindStaffByID(string staffID, bool status = true)
        {

            try
            {
                //A variable to store customer's information
                StaffDTO staff = null;

                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteProcedure("FindStaff", new MySqlParameter("@_ID", staffID),
                                                                                                     new MySqlParameter("@_Status", status));


                //get customer's information
                while (reader.Read())
                {


                    staff = new StaffDTO(reader.GetString("StaffID"), reader.GetString("StaffName"), reader.GetString("StaffAddress"),
                                             reader.GetString("Phone"), reader.GetString("Email"), reader.GetString("PositionID"), decimal.Parse(reader.GetString("Salary")));
                }

                return staff;

            }
            finally
            {

            }

        }


        public System.Data.DataTable FindStaffs(string staffID, string name, string address, string phoneNumber,
                                           string email, string positionID, decimal salary, string salaryCompareType,
                                            bool status)
        {
            try
            {

                System.Data.DataTable dataTable = MySqlConnectionDAO.Instance.GetDataTableByProcedure("FindStaffs", new MySqlParameter("@_ID", staffID),
                                                                            new MySqlParameter("@_Name", name), new MySqlParameter("@_Address", address),
                                                                            new MySqlParameter("@_Phone", phoneNumber), new MySqlParameter("@_Email", email),
                                                                            new MySqlParameter("@_PositionID", positionID),
                                                                            new MySqlParameter("@_Salary", salary),
                                                                            new MySqlParameter("@_SalaryCompareType", salaryCompareType),
                                                                            new MySqlParameter("@_Status", status));


                return dataTable;
            }
            finally
            {

            }
        }

        public List<string> GetAllStaffIDs()
        {
            try
            {
                List<string> staffIDs = new List<string>();

                string query = string.Format("select StaffID from Staff where StaffStatus = true");

                //Excute query in MySQL
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteQuery(query);


                while (reader.Read())
                {
                    staffIDs.Add(reader.GetString("StaffID"));
                }


                return staffIDs;
            }
            finally
            {

            }
        }


        public string GetNewStaffID()
        {
            try
            {

                //the index of ID
                int indexID = 0;
                //new ID string
                string newID;
                //excute query 
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteQuery("select Max(cast(Substring(StaffID,3, length(StaffID)-2) as unsigned)) as 'MaxStaffID' from STAFF");

                //get the biggest StaffID in database
                while (reader.Read())
                {

                    if (reader.IsDBNull(0))
                    {
                        break;
                    }

                    indexID = int.Parse(reader.GetString("MaxStaffID"));

                }

                //get the next ID
                newID = "ST" + (indexID + 1).ToString("0000");

                return newID;

            }
            finally
            {

            }

        }

    }


}
