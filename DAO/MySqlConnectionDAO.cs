using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

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




        public MySqlConnection ConnectToDatabase(String server, String user, String password, String database,out String exception)
        {
            exception = null;

            try
            {
                if (m_MySql != null)
                {
                    DisconnectFromDatabase(out exception);
                }

                //Get a new mysql connection
                m_MySql = new MySqlConnection();

                m_Adapter = new MySqlDataAdapter();

                //Get connection string
                m_MySql.ConnectionString = String.Format("server={0};user={1};password={2};database={3}", server, user, password, database);

                //open 
                m_MySql.Open();
            }
            catch(MySqlException ex)
            {
                exception = ex.Message;
            }
                   
            return m_MySql;
        }





        /// <summary>
        /// Disconnect from database and dispose resources
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public bool DisconnectFromDatabase(out String exception)
        {
            exception = null;

            try
            {
                if (m_MySql != null)
                {
                    m_MySql.Close();                    
                }
            }
            catch(MySqlException ex)
            {
                exception = ex.Message;

                return false;
            }                         
            finally
            {
                if(m_Reader!=null)
                {
                    m_Reader.Dispose();
                }

                if(m_Adapter!=null)
                {
                    m_Adapter.Dispose();
                }

                if (m_MySql != null)
                {
                    m_MySql.Dispose();
                }
            }

            return true;
        }
           

        /// <summary>
        ///  Excute a query
        /// </summary>
        /// <param name="query"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public MySqlDataReader ExcuteQuery(String query,out String exception)
        {
            exception = null;

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
            catch (MySqlException ex)
            {
                exception = ex.Message;
            }
            finally
            {
                command.Dispose();
            }

            return m_Reader;
        }



        /// <summary>
        /// Get datatable
        /// </summary>
        /// <param name="query"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public DataTable GetDataTableByQuery(String query, out String exception)
        {
            exception = null;

            //We have to close previous reader first to be able to excute new reader
            if(m_Reader!=null)
            {
                m_Reader.Close();
            }

            //DataTable
            DataTable dataTable = new DataTable(); ;

            //Get command
            MySqlCommand command = new MySqlCommand(query, m_MySql);

            try
            {
                m_Adapter.SelectCommand = command;

                m_Adapter.Fill(dataTable);
         
            }
            catch(MySqlException ex)
            {
                exception = ex.Message;
            }
            finally
            {
                command.Dispose();
            }

            return dataTable;
        }


        /// <summary>
        /// Excute procedure
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="String"></param>
        /// <param name="exception"></param>
        /// <param name="String"></param>
        /// <param name="ParamArray"></param>
        /// <param name=""></param>
        /// <param name="As"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public MySqlDataReader ExcuteProcedure(String procedureName, out String exception, params MySqlParameter[] values)
        {
            exception = null;

            //we have to close previous reader first to be able to excute new reader
            if (m_Reader != null)
            {
                m_Reader.Close();
            }

            MySqlCommand command = new MySqlCommand(procedureName, m_MySql);

            //Excute reader
            try
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddRange(values);

                m_Reader = command.ExecuteReader();

               
            }
            catch (MySqlException ex)
            {
                exception = ex.Message;
            }
            finally
            {
                command.Dispose();
            }

            return m_Reader;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="exception"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public DataTable GetDataTableByProcedure(String procedureName, out String exception, params MySqlParameter[] values)
        {
            exception = null;

            //we have to close previous reader first to be able to excute new reader
            if (m_Reader != null)
            {
                m_Reader.Close();
            }

            //Get a new datable
            DataTable dataTable = new DataTable();

            //get command
            MySqlCommand command = new MySqlCommand(procedureName, m_MySql);

            try
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(values);
                //select command
                m_Adapter.SelectCommand = command;
                //fill in datatable
                m_Adapter.Fill(dataTable);
            }
            catch (MySqlException ex)
            {
                exception = ex.Message;
            }
            finally
            {
                command.Dispose();
            }


            return dataTable;
        }
            

    }
}
