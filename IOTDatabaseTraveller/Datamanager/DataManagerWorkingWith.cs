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

        public List<WorksWith> GetWorksWiths(string sqlQuery="")
        {
            workingwithdata.Clear();

            if (sqlQuery == "")
            {
                sqlQuery = @"SELECT 
                                working_with.employee_id,
                                working_with.client_id,
	                            CONCAT(given_name, ' ', family_name) as name,
	                            client_name,
                                total_sales,
	                            working_with.created_at,
	                            working_with.updated_at
                            FROM working_with
                            JOIN clients ON clients.id=working_with.client_id
                            JOIN employees ON employee_id=employees.id
                             ORDER BY name";
            }

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sqlQuery, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DateTime? lastUpdated;
                    if (reader[6] == DBNull.Value)
                    {
                        lastUpdated = null;
                    }
                    else
                    {
                        lastUpdated = reader.GetDateTime(6);
                    }

                    WorksWith worksWith = new()
                    {
                        EmployeeID = reader.GetInt32(0),
                        ClientID = reader.GetInt32(1),
                        EmployeeName = reader.GetString(2),
                        ClientName = reader.GetString(3),
                        TotalSales = reader.GetDecimal(4),
                        CreatedAt = reader.GetDateTime(5),
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


        public List<WorksWith> SearchWorksWith(WorksWith searchParams)
        {
            string searchQuery = @"SELECT * FROM working_with
                                    WHERE";

            string searchClient = "";
            string searchEmployee = "";
            string andString = "";

            if (searchParams.ClientID != 0)
            {
                searchClient = string.Format(" client_id={0}", searchParams.ClientID);
                andString = "AND ";
            }
            if (searchParams.EmployeeID != 0)
            {
                searchEmployee = string.Format(" {0}employee_id={1}", andString, searchParams.EmployeeID);
            }

            string fullSearch = searchQuery + searchClient + searchEmployee;
            List<WorksWith> result = GetWorksWiths(fullSearch);
            return result;
        }

        public bool CheckWorksWithIsValid(WorksWith worksWithToCheck)
        {
            if ((worksWithToCheck.EmployeeID) != 0 && (worksWithToCheck.ClientID) != 0 && (worksWithToCheck.TotalSales > 1))
            {
                return true;
            }
            return false;
        }

        public void AddWorksWith(WorksWith newWorksWith)
        {
            string sqlNonQuery = @"INSERT INTO working_with (
                                            employee_id,
                                            client_id,
                                            total_sales)
                                        VALUES
                                            ({0}, {1}, {2})";

            sqlNonQuery = string.Format(sqlNonQuery, newWorksWith.EmployeeID, newWorksWith.ClientID, newWorksWith.TotalSales);
            SqlNonQuery(sqlNonQuery);
        }

        public void DeleteWorksWith(WorksWith oldWorksWith)
        {
            string sqlNonQuery = @"DELETE FROM working_with WHERE employee_id={0} AND client_id={1} AND total_sales={2}";
            sqlNonQuery = string.Format(sqlNonQuery, oldWorksWith.EmployeeID, oldWorksWith.ClientID, oldWorksWith.TotalSales);
            SqlNonQuery(sqlNonQuery);
        }

        public void EditWorksWith(WorksWith changedWorksWith)
        {
            string sqlNonQuery = @"UPDATE working_with
                                        SET total_sales={0}
                                        WHERE employee_id={1} AND client_id={2}";
            sqlNonQuery = string.Format(sqlNonQuery, changedWorksWith.TotalSales, changedWorksWith.EmployeeID, changedWorksWith.ClientID);
            SqlNonQuery(sqlNonQuery);
        }


    }
}
