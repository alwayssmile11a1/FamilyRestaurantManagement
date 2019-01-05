using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using DAO;
using DTO;

namespace BUS
{
    public class DishBUS
    {
        public static DishBUS Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new DishBUS();
                }

                return m_Instance;
            }
        }


        private static DishBUS m_Instance;

        private DishBUS()
        {

        }


        public void InsertDish(DishDTO dish)
        {
            try
            {
                DishDAO.Instance.InsertDish(dish);
               
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }


        public void RemoveDish(string dishID)
        {
            try
            {
                DishDAO.Instance.RemoveDish(dishID);
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }

        public void UpdateDish(DishDTO dish)
        {
            try
            {
                 DishDAO.Instance.UpdateDish(dish);
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }

        public void RetriveRemovedDish(string dishID)
        {

            try
            {
                DishDAO.Instance.RetriveRemovedDish(dishID);
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }


        public DishDTO FindDishByID(string dishID, bool status = true)
        {

            try
            {
                return DishDAO.Instance.FindDishByID(dishID, status);
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }


        public System.Data.DataTable FindDishes(string dishID, string name, decimal unitPrice, string unitPriceCompareType, bool status = true)
        {
            try
            {
                return DishDAO.Instance.FindDishes(dishID, name,unitPrice, unitPriceCompareType, status);
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }
        }

        public List<DishDTO> GetAllDishes()
        {
            try
            {
                return DishDAO.Instance.GetAllDishes();
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }
        }


        public string GetNewDishID()
        {
            try
            {
                return DishDAO.Instance.GetNewDishID();
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }
    }
}
