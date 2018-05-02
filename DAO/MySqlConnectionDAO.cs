using System;
using MySql.Data.MySqlClient;

namespace DAO
{
    public class MySqlConnectionDAO
    {

        public static MySqlConnectionDAO Instance
        {
            get
            {
                if(m_Instance==null)
                {
                    m_Instance = new MySqlConnectionDAO();
                }

                return m_Instance;
            }
        }


        private static MySqlConnectionDAO m_Instance;

        private MySqlConnection m_MySql;
        private MySqlDataReader m_Reader;
        private MySqlDataAdapter m_Adapter;


        private MySqlConnectionDAO()
        {
            m_MySql = null;
            m_Reader = null;
            m_Adapter = new MySqlDataAdapter();
        }




        public MySqlConnection ConnectToDatabase(String server, String user, String password, String database)
        {

            try
            {
                if (m_MySql != null)
                {
                    DisconnectFromDatabase();
                }

                //Get a new mysql connection
                m_MySql = new MySqlConnection();

                m_Adapter = new MySqlDataAdapter();

                //Get connection string
                m_MySql.ConnectionString = String.Format("server={0};user={1};password={2};database={3}", server, user, password, database);

                //open 
                m_MySql.Open();
            }
            finally
            {

            }
                   
            return m_MySql;
        }





        /// <summary>
        /// Disconnect from database and dispose resources
        /// </summary>
        public void DisconnectFromDatabase()
        {
            try
            {
                if (m_MySql != null)
                {
                    m_MySql.Close();                    
                }
            }       
            finally
            {
                if(m_Reader!=null)
                {
                    m_Reader.Dispose();
                    m_Reader = null;
                }

                if(m_Adapter!=null)
                {
                    m_Adapter.Dispose();
                    m_Adapter = null;
                }

                if (m_MySql != null)
                {
                    m_MySql.Dispose();
                    m_MySql = null;
                }
            }
        }
           

        public MySqlDataReader ExcuteQuery(String query)
        {

            //we have to close previous reader first to be able to excute new reader
            if(m_Reader !=null)
            {
                m_Reader.Close();
            }

            MySqlCommand command = new MySqlCommand(query, m_MySql);

            //Excute reader
            try
            {
                m_Reader = command.ExecuteReader();
            }
            finally
            {
                command.Dispose();
            }

            return m_Reader;
        }



        public System.Data.DataTable GetDataTableByQuery(String query)
        {

            //We have to close previous reader first to be able to excute new reader
            if(m_Reader!=null)
            {
                m_Reader.Close();
            }

            //DataTable
            System.Data.DataTable dataTable = new System.Data.DataTable();

            //Get command
            MySqlCommand command = new MySqlCommand(query, m_MySql);

            try
            {
                m_Adapter.SelectCommand = command;

                m_Adapter.Fill(dataTable);
         
            }
            finally
            {
                command.Dispose();
            }

            return dataTable;
        }



        public MySqlDataReader ExcuteProcedure(String procedureName, params MySqlParameter[] values)
        {
            //we have to close previous reader first to be able to excute new reader
            if (m_Reader != null)
            {
                m_Reader.Close();
            }

            MySqlCommand command = new MySqlCommand(procedureName, m_MySql);

            //Excute reader
            try
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddRange(values);

                m_Reader = command.ExecuteReader();

               
            }
            finally
            {
                command.Dispose();
            }

            return m_Reader;
        }


        public System.Data.DataTable GetDataTableByProcedure(String procedureName, params MySqlParameter[] values)
        {
            //we have to close previous reader first to be able to excute new reader
            if (m_Reader != null)
            {
                m_Reader.Close();
            }

            //Get a new datable
            System.Data.DataTable dataTable = new System.Data.DataTable();

            //get command
            MySqlCommand command = new MySqlCommand(procedureName, m_MySql);

            try
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddRange(values);
                //select command
                m_Adapter.SelectCommand = command;
                //fill in datatable
                m_Adapter.Fill(dataTable);
            }
            finally
            {
                command.Dispose();
            }


            return dataTable;
        }
            

    }
}
