using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using MySql.Data.MySqlClient;

namespace DAO
{
    public class StaffPositionDAO
    {

        public static StaffPositionDAO Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new StaffPositionDAO();
                }

                return m_Instance;
            }
        }


        private static StaffPositionDAO m_Instance;

        private StaffPositionDAO()
        {

        }

        public MySqlDataReader InsertStaffPosition(StaffPositionDTO staffPosition)
        {
            try
            {
                // query
                string query = string.Format("insert into STAFFPOSITION values('{0}', '{1}','{2}')",
                                            staffPosition.ID, staffPosition.Name, staffPosition.PayRate);
                // excute query
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteQuery(query);

                return reader;
            }
            finally
            {

            }

        }

        public MySqlDataReader RemoveStaffPosition(string id)
        {
            try
            {
                // query
                string query = string.Format("delete from STAFFPOSITION where PositionID = '{0}'", id);

                // excute query
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteQuery(query);

                return reader;
            }
            finally
            {

            }

        }


        public string GetNewStaffPositionID()
        {
            try
            {
                int indexID = 0;
                string newID = null;

                // excute query 
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteQuery("select Max(cast(Substring(PositionID,3, length(PositionID)-2) as unsigned)) as 'MaxPositionID' from STAFFPOSITION");


                while (reader.Read())
                {
                    if ((reader.IsDBNull(0)))
                        break;

                    indexID = int.Parse(reader.GetString("MaxPositionID"));
                }

                // set newID string
                newID = "PO" + (indexID + 1).ToString("0000");


                return newID;

            }
            finally
            {

            }

        }

    }
}
