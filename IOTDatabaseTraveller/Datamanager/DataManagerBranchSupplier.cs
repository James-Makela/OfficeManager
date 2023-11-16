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

    }
}
