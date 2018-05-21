using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using DAO;
using DTO;

namespace BUS
{
    class StaffBUS
    {
        public static StaffBUS Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new StaffBUS();
                }

                return m_Instance;
            }
        }


        private static StaffBUS m_Instance;

        private StaffBUS()
        {

        }


        public MySqlDataReader InsertStaff(StaffDTO staff)
        {
            try
            {
                return StaffDAO.Instance.InsertStaff(staff);
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }


        public MySqlDataReader RemoveStaff(string staffID)
        {
            try
            {
                return StaffDAO.Instance.RemoveStaff(staffID);
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }

        public MySqlDataReader UpdateStaff(StaffDTO staff)
        {
            try
            {
                return StaffDAO.Instance.UpdateStaff(staff);
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }


        public MySqlDataReader AddStaffSalary(string staffID, decimal amount)
        {


            try
            {
                return StaffDAO.Instance.AddStaffSalary(staffID, amount);
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }

        public MySqlDataReader RetriveRemovedStaff(string staffID)
        {

            try
            {
                return StaffDAO.Instance.RetriveRemovedStaff(staffID);
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }


        public StaffDTO FindStaffByID(string staffID, bool status = true)
        {

            try
            {
                return StaffDAO.Instance.FindStaffByID(staffID);
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }


        public System.Data.DataTable FindStaffs(string staffID, string name, string address, string phoneNumber,
                                           string email, string positionID, decimal salary, string salaryCompareType,
                                            bool status = true)
        {
            try
            {
                return StaffDAO.Instance.FindStaffs(staffID, name,address,phoneNumber,email,positionID,salary,salaryCompareType,status);
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }
        }

        public List<string> GetAllStaffIDs()
        {
            try
            {
                return StaffDAO.Instance.GetAllStaffIDs();
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }
        }


        public string GetNewStaffID()
        {
            try
            {
                return StaffDAO.Instance.GetNewStaffID();
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }
    }
}
