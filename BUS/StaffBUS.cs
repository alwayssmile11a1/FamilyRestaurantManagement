using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using DAO;
using DTO;

namespace BUS
{
    public class StaffBUS
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


        public void InsertStaff(StaffDTO staff)
        {
            try
            {
                 StaffDAO.Instance.InsertStaff(staff);
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }


        public void RemoveStaff(string staffID)
        {
            try
            {
                 StaffDAO.Instance.RemoveStaff(staffID);
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }

        public void UpdateStaff(StaffDTO staff)
        {
            try
            {
                 StaffDAO.Instance.UpdateStaff(staff);
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }


        public void AddStaffSalary(string staffID, decimal amount)
        {
            try
            {
                StaffDAO.Instance.AddStaffSalary(staffID, amount);
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }

        public void RetriveRemovedStaff(string staffID)
        {

            try
            {
                StaffDAO.Instance.RetriveRemovedStaff(staffID);
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
