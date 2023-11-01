using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOTDatabaseTraveller.DataClasses;
using System.Windows;
using MySql.Data.MySqlClient;

namespace IOTDatabaseTraveller.Datamanager
{
    public partial class DataManager
    {
        List<WorksWith> workingwithdata = new();

        public List<WorksWith> GetWorksWiths(string sqlQuery = "SELECT * FROM working_with")
        {
            workingwithdata.Clear();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sqlQuery, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DateTime? lastUpdated;
                    if (reader[4] == DBNull.Value)
                    {
                        lastUpdated = null;
                    }
                    else
                    {
                        lastUpdated = reader.GetDateTime(4);
                    }

                    WorksWith worksWith = new()
                    {
                        EmployeeID = reader.GetInt32(0),
                        ClientID = reader.GetInt32(1),
                        TotalSales = reader.GetDecimal(2),
                        CreatedAt = reader.GetDateTime(3),
                        UpdatedAt = lastUpdated
                    };
                    workingwithdata.Add(worksWith);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            conn.Close();
            return workingwithdata;
        }
    }
}
