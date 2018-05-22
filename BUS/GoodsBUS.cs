using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using DAO;
using DTO;

namespace BUS
{
    public class GoodsBUS
    {
        public static GoodsBUS Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new GoodsBUS();
                }

                return m_Instance;
            }
        }


        private static GoodsBUS m_Instance;

        private GoodsBUS()
        {

        }


        public void InsertGoods(GoodsDTO goods)
        {
            try
            {
                GoodsDAO.Instance.InsertGoods(goods);

            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }


        public void RemoveGoods(string goodsID)
        {
            try
            {
                GoodsDAO.Instance.RemoveGoods(goodsID);

            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }

        public void UpdateGoods(GoodsDTO goods)
        {
            try
            {
                GoodsDAO.Instance.UpdateGoods(goods);

            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }

        public void RetriveRemovedGoods(string goodsID)
        {

            try
            {
                GoodsDAO.Instance.RetriveRemovedGoods(goodsID);

            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }


        public GoodsDTO FindGoodsByID(string goodsID, bool status = true)
        {

            try
            {
                return GoodsDAO.Instance.FindGoodsByID(goodsID);

            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }


        public System.Data.DataTable FindGoods(string goodsID, string name, string supplierID, decimal unitPrice, string unitPriceCompareType, int stock, string stockCompareType, bool status = true)
        {
            try
            {
                return GoodsDAO.Instance.FindGoods(goodsID, name,supplierID,unitPrice,unitPriceCompareType, stock,stockCompareType, status);

            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }
        }

        public List<string> GetAllGoodsIDs()
        {
            try
            {
             return   GoodsDAO.Instance.GetAllGoodsIDs();

            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }
        }


        public string GetNewGoodsID()
        {
            try
            {
               return GoodsDAO.Instance.GetNewGoodsID();

            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }

    }
}
