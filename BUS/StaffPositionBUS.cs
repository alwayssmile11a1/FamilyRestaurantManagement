using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using DAO;
using DTO;

namespace BUS
{
    public class StaffPositionBUS
    {
        public static StaffPositionBUS Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new StaffPositionBUS();
                }

                return m_Instance;
            }
        }


        private static StaffPositionBUS m_Instance;

        private StaffPositionBUS()
        {

        }

        public void InsertStaffPosition(StaffPositionDTO staffPosition)
        {
            try
            {
                StaffPositionDAO.Instance.InsertStaffPosition(staffPosition);
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }


        }

        public void RemoveStaffPosition(string id)
        {
            try
            {
                StaffPositionDAO.Instance.RemoveStaffPosition(id);
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }

        public System.Data.DataTable GetAllStaffPosition()
        {
            try
            {
                return StaffPositionDAO.Instance.GetAllStaffPosition();

            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }


        public string GetNewStaffPositionID()
        {
            try
            {
                return StaffPositionDAO.Instance.GetNewStaffPositionID();
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }



    }
}
