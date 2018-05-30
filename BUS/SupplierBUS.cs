using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using DAO;
using DTO;

namespace BUS
{
    public class SupplierBUS
    {
        public static SupplierBUS Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new SupplierBUS();
                }

                return m_Instance;
            }
        }


        private static SupplierBUS m_Instance;

        private SupplierBUS()
        {

        }


        public void InsertSupplier(SupplierDTO supplier)
        {
            try
            {
                SupplierDAO.Instance.InsertSupplier(supplier);
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }
        }


        public void RemoveSupplier(string supplierID)
        {
            try
            {
                SupplierDAO.Instance.RemoveSupplier(supplierID);
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }

        public void UpdateSupplier(SupplierDTO supplier)
        {
            try
            {
                SupplierDAO.Instance.UpdateSupplier(supplier);
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }

        public void RetriveRemovedSupplier(string supplierID)
        {

            try
            {
                SupplierDAO.Instance.RetriveRemovedSupplier(supplierID);
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }


        public SupplierDTO FindSupplierByID(string supplierID, bool status = true)
        {

            try
            {
                return SupplierDAO.Instance.FindSupplierByID(supplierID, status);
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }


        public System.Data.DataTable FindSuppliers(string supplierID, string name, string address, string phoneNumber,
                                           string email, bool status = true)
        {
            try
            {
                return SupplierDAO.Instance.FindSuppliers(supplierID, name, address, phoneNumber, email,status);
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }
        }

        public List<string> GetAllSupplierIDs()
        {
            try
            {
                return SupplierDAO.Instance.GetAllSupplierIDs();
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }
        }


        public string GetNewSupplierID()
        {
            try
            {
                return SupplierDAO.Instance.GetNewSupplierID();
            }
            catch (MySqlException ex)
            {
                throw new BUSException(ex.Message);
            }

        }
    }
}
