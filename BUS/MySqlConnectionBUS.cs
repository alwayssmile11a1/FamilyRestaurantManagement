using System;
using System.Collections.Generic;
using System.Text;
using DAO;

namespace BUS
{
    class MySqlConnectionBUS
    {
        public static MySql.Data.MySqlClient.MySqlConnection ConnectToDatabase(string server, string user, string password, string database)
        {
            try
            {
                return MySqlConnectionDAO.Instance.ConnectToDatabase(server, user, password, database);


            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }

        public void DisConnectFromDatabase()
        {
            try
            {
                MySqlConnectionDAO.Instance.DisconnectFromDatabase();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }
    }
}
