using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using MySql.Data.MySqlClient;

namespace DAO
{
    public class GoodsDAO
    {
        public static GoodsDAO Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new GoodsDAO();
                }

                return m_Instance;
            }
        }


        private static GoodsDAO m_Instance;

        private GoodsDAO()
        {

        }


        public MySqlDataReader InsertGoods(GoodsDTO goods)
        {
            try
            {
                //ExcuteQuery
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteProcedure("InsertGoods",
                                               new MySqlParameter("@_ID", goods.ID),
                                               new MySqlParameter("@_Name", goods.Name),
                                               new MySqlParameter("@_SupplierID", goods.SupplierID),
                                               new MySqlParameter("@_UnitPrice", goods.UnitPrice),
                                               new MySqlParameter("@_Stock", goods.Stock),
                                               new MySqlParameter("@_Status", true));



                return reader;
            }
            finally
            {

            }

        }


        public MySqlDataReader RemoveGoods(string goodsID)
        {
            try
            {



                //ExcuteQuery
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteProcedure("UpdateGoodsStatus",
                                                                                       new MySqlParameter("@_ID", goodsID),
                                                                                        new MySqlParameter("@_Status", false));


                return reader;
            }
            finally
            {

            }

        }

        public MySqlDataReader UpdateGoods(GoodsDTO goods)
        {
            try
            {
                //ExcuteQuery
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteProcedure("UpdateGoods", new MySqlParameter("@_ID", goods.ID),
                                                    new MySqlParameter("@_Name", goods.Name),
                                                    new MySqlParameter("@_SupplierID", goods.SupplierID),
                                                    new MySqlParameter("@_UnitPrice", goods.UnitPrice));

                return reader;
            }
            finally
            {

            }

        }

        public MySqlDataReader RetriveRemovedGoods(string goodsID)
        {

            try
            {

                //ExcuteQuery
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteProcedure("UpdateGoodsStatus",
                                                                                    new MySqlParameter("@_ID", goodsID),
                                                                                    new MySqlParameter("@_Status", true));

                return reader;
            }
            finally
            {

            }

        }


        public GoodsDTO FindGoodsByID(string goodsID, bool status = true)
        {

            try
            {
                //A variable to store goods's information
                GoodsDTO goods = null;

                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteProcedure("FindGoods", new MySqlParameter("@_ID", goodsID),
                                                                                                     new MySqlParameter("@_Status", status));


                //get goods's information
                while (reader.Read())
                {


                    goods = new GoodsDTO(reader.GetString("ID"), reader.GetString("GoodsName"), reader.GetString("SupplierID"), decimal.Parse(reader.GetString("UnitPrice")), int.Parse(reader.GetString("Stock")));
                }

                return goods;

            }
            finally
            {

            }

        }


        public System.Data.DataTable FindGoods(string goodsID, string name, string supplierID, decimal unitPrice, string unitPriceCompareType, int stock, string stockCompareType, bool status)
        {
            try
            {

                System.Data.DataTable dataTable = MySqlConnectionDAO.Instance.GetDataTableByProcedure("FindGoodss", new MySqlParameter("@_ID", goodsID),
                                                                            new MySqlParameter("@_Name", name),
                                                                            new MySqlParameter("@_SupplierID", supplierID),
                                                                            new MySqlParameter("@_UnitPrice", unitPrice),
                                                                            new MySqlParameter("@_UnitPriceCompareType", unitPriceCompareType),
                                                                            new MySqlParameter("@_Stock", unitPrice),
                                                                            new MySqlParameter("@_StockCompareType", unitPriceCompareType),
                                                                            new MySqlParameter("@_Status", status));


                return dataTable;
            }
            finally
            {

            }
        }

        public List<string> GetAllGoodsIDs()
        {
            try
            {
                List<string> goodsIDs = new List<string>();

                string query = string.Format("select GoodsID from Goods where GoodsStatus = true");

                //Excute query in MySQL
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteQuery(query);


                while (reader.Read())
                {
                    goodsIDs.Add(reader.GetString("GoodsID"));
                }


                return goodsIDs;
            }
            finally
            {

            }
        }


        public string GetNewGoodsID()
        {
            try
            {

                //the index of ID
                int indexID = 0;
                //new ID string
                string newID;
                //excute query 
                MySqlDataReader reader = MySqlConnectionDAO.Instance.ExcuteQuery("select Max(cast(Substring(GoodsID,3, length(GoodsID)-2) as unsigned)) as 'MaxGoodsID' from GOODS");

                //get the biggest CustomerID in database
                while (reader.Read())
                {

                    if (reader.IsDBNull(0))
                    {
                        break;
                    }

                    indexID = int.Parse(reader.GetString("MaxGoodsID"));

                }

                //get the next ID
                newID = "GO" + (indexID + 1).ToString("0000");

                return newID;

            }
            finally
            {

            }

        }


    }
}
