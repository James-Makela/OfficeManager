using IOTDatabaseTraveller.DataClasses;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IOTDatabaseTraveller.Datamanager
{
    public partial class DataManager
    {
        public List<BranchSupplier> branchSuppliers = new();

        public List<BranchSupplier> GetBranchSuppliers(string sqlQuery = "SELECT * FROM branch_supplier")
        {
            branchSuppliers.Clear();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sqlQuery, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DateTime? lastUpdated;
                    if (reader[5] == DBNull.Value)
                    {
                        lastUpdated = null;
                    }
                    else
                    {
                        lastUpdated = reader.GetDateTime(5);
                    }

                    BranchSupplier branchSupplier = new()
                    {
                        BranchID = reader.GetInt32(0),
                        SupplierID = reader.GetInt32(1),
                        SupplierName = reader.GetString(2),
                        ProductSupplied = reader.GetString(3),
                        CreatedAt = reader.GetDateTime(4),
                        UpdatedAt = lastUpdated
                    };
                    branchSuppliers.Add(branchSupplier);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            conn.Close();
            return branchSuppliers;
        }

        public void AddBranchSupplier(BranchSupplier newBranchSupplier)
        {
            string sqlNonQuery = @"INSERT INTO branch_supplier (
                                            branch_id,
                                            supplier_id,
                                            supplier_name,
                                            product_supplied)
                                        VALUES ({0}, {1}, ""{2}"", ""{3}"")";

            sqlNonQuery = string.Format(sqlNonQuery,
                                            newBranchSupplier.BranchID,
                                            newBranchSupplier.SupplierID,
                                            newBranchSupplier.SupplierName,
                                            newBranchSupplier.ProductSupplied);

            SqlNonQuery(sqlNonQuery);
        }

        public void RemoveBranchSupplier(BranchSupplier oldBranchSupplier)
        {
            string sqlNonQuery = "DELETE FROM branch_supplier WHERE supplier_id={0}";
            sqlNonQuery = string.Format(sqlNonQuery, oldBranchSupplier.SupplierID);
            SqlNonQuery(sqlNonQuery);
        }

        public void EditBranchSupplier(BranchSupplier changedBranchSupplier)
        {
            string sqlNonQuery = @"UPDATE branch_supplier
                                    SET 
                                        branch_id={0},
                                        supplier_name='{1}',
                                        product_supplied='{2}'
                                    WHERE
                                        supplier_id={3}";
            sqlNonQuery = string.Format(sqlNonQuery,
                                            changedBranchSupplier.BranchID,
                                            changedBranchSupplier.SupplierName,
                                            changedBranchSupplier.ProductSupplied,
                                            changedBranchSupplier.SupplierID);
            SqlNonQuery(sqlNonQuery);
        }

        public List<BranchSupplier> SearchBranchSuppliers(BranchSupplier searchParams)
        {
            string searchQuery = "SELECT * FROM branch_supplier WHERE";

            string searchSupplierName = "";
            string searchSupplierBranch = "";
            string searchSupplerProduct = "";
            string andString = "";

            if (searchParams.SupplierName != null && searchParams.SupplierName != "")
            {
                searchSupplierName = string.Format(@" supplier_name LIKE ""%{0}%""", searchParams.SupplierName);
                andString = "AND ";
            }
            if (searchParams.BranchID != 0 && searchParams.BranchID != null)
            {
                searchSupplierBranch = string.Format(@" {0}branch_id={1}", andString, searchParams.BranchID);
                andString = "AND ";
            }
            if (searchParams.ProductSupplied != null && searchParams.ProductSupplied != "")
            {
                searchSupplerProduct = string.Format(@" {0}product_supplied LIKE ""%{1}%""", andString, searchParams.ProductSupplied);
            }

            string fullSearch = searchQuery + searchSupplierName + searchSupplierBranch + searchSupplerProduct;
            List<BranchSupplier> branchSuppliers = GetBranchSuppliers(fullSearch);
            return branchSuppliers;

        }

        public bool CheckBranchSupplierIsValid(BranchSupplier supplier)
        {
            if (supplier.SupplierID != 0 && supplier.SupplierName.Length > 2 && supplier.BranchID != 0 && supplier.ProductSupplied.Length > 2)
            {
                return true;
            }
            return false;
        }

    }
}
