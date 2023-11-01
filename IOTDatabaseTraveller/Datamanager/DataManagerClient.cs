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
        public List<Client> clients = new();

        public List<Client> GetClients(string sqlQuery = "SELECT * FROM clients")
        {
            clients.Clear();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sqlQuery, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    // need to parse branch id, supplier id
                    int.TryParse($"{reader[0]}", out int id);
                    int.TryParse($"{reader[2]}", out int branchId);

                    DateTime? lastUpdated;
                    if (reader[4] == DBNull.Value)
                    {
                        lastUpdated = null;
                    }
                    else
                    {
                        lastUpdated = (DateTime)reader[5];
                    }

                    Client client = new()
                    {
                        ID = id,
                        ClientName = reader[1].ToString(),
                        BranchID = branchId,
                        CreatedAt = (DateTime)reader[3],
                        LastUpdatedAt = lastUpdated
                        
                    };
                    clients.Add(client);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            conn.Close();
            return clients;
        }
    }
}
