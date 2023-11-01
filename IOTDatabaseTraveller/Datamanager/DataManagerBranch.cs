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
        List<Branch> branches = new();
        
        public List<Branch> GetBranches(string sqlQuery = "SELECT * FROM branches")
        {
            branches.Clear();

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

                    Branch branch = new()
                    {
                        ID = reader.GetInt32(0),
                        BranchName = reader.GetString(1),
                        ManagerID = reader.GetInt32(2),
                        ManagerStartedAt = reader.GetDateTime(3),
                        CreatedAt = reader.GetDateTime(4),
                        UpdatedAt = lastUpdated,
                    };
                    branches.Add(branch);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            conn.Close();
            return branches;
        }
    }
}
