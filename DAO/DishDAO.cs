using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using MySql.Data.MySqlClient;

namespace DAO
{
    public class DishDAO
    {
        public static DishDAO Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new DishDAO();
                }

                return m_Instance;
            }
        }


        private static DishDAO m_Instance;

        private DishDAO()
        {

        }


        public MySqlDataReader InsertDish(DishDTO dish)
        {
            try
            {
                //ExcuteQuery
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteProcedure("InsertDish",
                                               new MySqlParameter("@_ID", dish.ID),
                                               new MySqlParameter("@_DishName", dish.Name),
                                               new MySqlParameter("@_UnitPrice", dish.UnitPrice),
                                               new MySqlParameter("@_Status", true));



                return reader;
            }
            finally
            {

            }

        }


        public MySqlDataReader RemoveDish(string dishID)
        {
            try
            {



                //ExcuteQuery
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteProcedure("UpdateDishStatus",
                                                                                       new MySqlParameter("@_ID", dishID),
                                                                                        new MySqlParameter("@_Status", false));


                return reader;
            }
            finally
            {

            }

        }

        public MySqlDataReader UpdateDish(DishDTO dish)
        {
            try
            {
                //ExcuteQuery
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteProcedure("UpdateDish", new MySqlParameter("@_ID", dish.ID),
                                                    new MySqlParameter("@_Name", dish.Name),
                                                    new MySqlParameter("@_DishName", dish.Name),
                                                    new MySqlParameter("@_UnitPrice", dish.UnitPrice));

                return reader;
            }
            finally
            {

            }

        }

        public MySqlDataReader RetriveRemovedDish(string dishID)
        {

            try
            {

                //ExcuteQuery
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteProcedure("UpdateDishStatus",
                                                                                    new MySqlParameter("@_ID", dishID),
                                                                                    new MySqlParameter("@_Status", true));

                return reader;
            }
            finally
            {

            }

        }


        public DishDTO FindDishByID(string dishID, bool status = true)
        {

            try
            {
                //A variable to store dish's information
                DishDTO dish = null;

                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteProcedure("FindDish", new MySqlParameter("@_ID", dishID),
                                                                                                     new MySqlParameter("@_Status", status));


                //get dish's information
                while (reader.Read())
                {


                    dish = new DishDTO(reader.GetString("ID"), reader.GetString("DishName"), decimal.Parse(reader.GetString("UnitPrice")));
                }

                return dish;

            }
            finally
            {

            }

        }


        public System.Data.DataTable FindDishes(string dishID, string name, decimal unitPrice, string unitPriceCompareType, bool status)
        {
            try
            {

                System.Data.DataTable dataTable = MySqlConnectionDAO.Instance.GetDataTableByProcedure("FindDishes", new MySqlParameter("@_ID", dishID),
                                                                            new MySqlParameter("@_DishName", name),
                                                                            new MySqlParameter("@_UnitPrice", unitPrice),
                                                                            new MySqlParameter("@_UnitPriceCompareType", unitPriceCompareType),
                                                                            new MySqlParameter("@_Status", status));


                return dataTable;
            }
            finally
            {

            }
        }

        public List<string> GetAllDishIDs()
        {
            try
            {
                List<string> dishIDs = new List<string>();

                string query = string.Format("select DishID from Dish where DishStatus = true");

                //Excute query in MySQL
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteQuery(query);


                while (reader.Read())
                {
                    dishIDs.Add(reader.GetString("DishID"));
                }


                return dishIDs;
            }
            finally
            {

            }
        }


        public string GetNewDishID()
        {
            try
            {

                //the index of ID
                int indexID = 0;
                //new ID string
                string newID;
                //excute query 
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteQuery("select Max(cast(Substring(DishID,3, length(DishID)-2) as unsigned)) as 'MaxDishID' from DISH");

                //get the biggest CustomerID in database
                while (reader.Read())
                {

                    if (reader.IsDBNull(0))
                    {
                        break;
                    }

                    indexID = int.Parse(reader.GetString("MaxDishID"));

                }

                //get the next ID
                newID = "DI" + (indexID + 1).ToString("0000");

                return newID;

            }
            finally
            {

            }

        }
    }
}
