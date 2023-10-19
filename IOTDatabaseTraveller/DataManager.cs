using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IOTDatabaseTraveller
{
    internal class DataManager
    {
        List<Employee> employees = new();

        string dbName = "jim_ictprg431";
        string dbUser = "JIM-ictprg431_james";
        string dbPassword = "password1";
        int dbPort = 3306;
        string dbServer = "localhost";

        string DbConnectionString;
        MySqlConnection conn;

        public DataManager()
        {
            DbConnectionString = $"server={dbServer}; user={dbUser}; database={dbName}; port={dbPort}; password={dbPassword};";
            conn = new MySqlConnection(DbConnectionString);
        }

        public List<Employee> GetEmployees()
        {
            employees.Clear();
            string sqlQuery = "SELECT * FROM employees";

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sqlQuery, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int.TryParse($"{reader[0]}", out int id);
                    decimal.TryParse($"{reader[5]}", out decimal salary);
                    int.TryParse($"{reader[6]}", out int supervisorID);
                    int.TryParse($"{reader[7]}", out int branchID);

                    DateTime? lastUpdated;
                    if (reader[9] == DBNull.Value)
                    {
                        lastUpdated = null;
                    }
                    else
                    {
                        lastUpdated = (DateTime)reader[9];
                    }

                    Employee employee = new Employee()
                    {
                        ID = id,
                        FirstName = reader[1].ToString(),
                        LastName = reader[2].ToString(),
                        DateOfBirth = (DateTime)reader[3],
                        Gender = (string)reader[4],
                        Salary = salary,
                        SupervisorID = supervisorID,
                        BranchID = branchID,
                        CreatedAt = (DateTime)reader[8],
                        LastUpdatedAt = lastUpdated
                    };
                    employees.Add(employee);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            conn.Close();
            return employees;
        }


        public void AddEmployee(Employee newEmployee)
        {
            string sqlQuery = @"INSERT INTO employees VALUES
                                ({0}, '{1}', '{2}', ""{3}"", '{4}', {5}, {6}, {7}, ""{8}"", null)";
            sqlQuery = string.Format(sqlQuery, newEmployee.ID,
                    newEmployee.FirstName,
                    newEmployee.LastName,
                    newEmployee.DateOfBirth.ToString("yyyy-MM-dd"),
                    newEmployee.Gender,
                    newEmployee.Salary,
                    newEmployee.SupervisorID,
                    newEmployee.BranchID,
                    newEmployee.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")
                    );
            
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sqlQuery, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();
            }
        }

        public void RemoveEmployee(Employee oldEmployee)
        {
            string sqlQuery = "DELETE FROM employees WHERE id={0}";
            sqlQuery = string.Format(sqlQuery, oldEmployee.ID);
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sqlQuery, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();
            }
        }

    }
}
