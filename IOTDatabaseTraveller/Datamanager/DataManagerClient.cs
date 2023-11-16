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
                    DateTime? lastUpdated;
                    if (reader[4] == DBNull.Value)
                    {
                        lastUpdated = null;
                    }
                    else
                    {
                        lastUpdated = (DateTime)reader[4];
                    }

                    Client client = new()
                    {
                        ID = reader.GetInt32(0),
                        ClientName = reader.GetString(1),
                        BranchID = reader.GetInt32(2),
                        CreatedAt = reader.GetDateTime(3),
                        UpdatedAt = lastUpdated
                        
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

        public List<ComboBoxStringIdItem> GetClientNames(bool includeAll=true)
        {
            string sqlQuery = "SELECT client_name, id FROM clients";
            List<ComboBoxStringIdItem> clientNames = new();
            
            if(includeAll)
            {
                clientNames.Add(new ComboBoxStringIdItem("All", 0));
            }

            return GetNameAndIDForCombo(clientNames, sqlQuery);
        }

        public bool CheckClientIsValid(Client clientToCheck)
        {
            if (clientToCheck.ClientName.Length < 1 || clientToCheck.BranchID == 0)
            {
                return false;
            }
            return true;
        }

        public void AddClient(Client newClient)
        {
            string sqlNonQuery = @"INSERT INTO clients (
                                            client_name,
                                            branch_id)
                                        VALUES
                                            ('{0}', {1})";

            sqlNonQuery = string.Format(sqlNonQuery, newClient.ClientName, newClient.BranchID);

            SqlNonQuery(sqlNonQuery);
        }

        public void DeleteClient(Client oldClient)
        {
            string sqlNonQuery = @"DELETE FROM clients WHERE id={0}";
            sqlNonQuery = string.Format(sqlNonQuery, oldClient.ID);
            SqlNonQuery(sqlNonQuery);
        }

        public void EditClient(Client changedClient)
        {
            string sqlNonQuery = @"UPDATE clients
                                        SET 
                                            client_name='{0}',
                                            branch_id={1}
                                        WHERE id={2}";
            sqlNonQuery = string.Format(sqlNonQuery, changedClient.ClientName, changedClient.BranchID, changedClient.ID);
            SqlNonQuery(sqlNonQuery);
        }

        public List<Client> SearchClients(Client searchParams)
        {
            string searchQuery = @"SELECT * FROM clients
                                        WHERE";
            string searchClientName = "";
            string searchBranch = "";
            string andString = "";

            if (searchParams.ClientName != null && searchParams.ClientName != "")
            {
                searchClientName = string.Format(@" client_name LIKE ""%{0}%""", searchParams.ClientName);
                andString = "AND ";
            }
            if (searchParams.BranchID != null && searchParams.BranchID != 0)
            {
                searchBranch = string.Format(@" {0}branch_id={1}", andString, searchParams.BranchID);
            }

            string fullSearch = searchQuery + searchClientName + searchBranch;

            List<Client> clients = GetClients(fullSearch);
            return clients;
        }
    }
}
