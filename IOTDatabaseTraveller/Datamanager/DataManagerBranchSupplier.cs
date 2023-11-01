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
                    int.TryParse(reader[0].ToString(), out int branchID);
                    int.TryParse(reader[1].ToString(), out int supplierID);

                    DateTime? lastUpdated;
                    if (reader[5] == DBNull.Value)
                    {
                        lastUpdated = null;
                    }
                    else
                    {
                        lastUpdated = (DateTime)reader[5];
                    }

                    BranchSupplier branchSupplier = new()
                    {
                        BranchID = branchID,
                        SupplierID = supplierID,
                        SupplierName = reader[2].ToString(),
                        ProductSupplied = reader[3].ToString(),
                        CreatedAt = (DateTime)reader[4],
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
    }
}
