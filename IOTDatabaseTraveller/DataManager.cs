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
    public class DataManager
    {
        public List<Employee> employees = new();

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

        public List<Employee> GetEmployees(string sqlQuery="SELECT * FROM employees")
        {
            employees.Clear();

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
            string sqlQuery = @"INSERT INTO employees (
                                            given_name,
                                            family_name,
                                            date_of_birth,
                                            gender_identity,
                                            gross_salary,
                                            supervisor_id,
                                            branch_id,
                                            created_at) 
                                        VALUES
                                            ('{0}', '{1}', ""{2}"", '{3}', {4}, {5}, {6}, ""{7}"")";
            string dob = ((DateTime)newEmployee.DateOfBirth).ToString("yyyy-MM-dd");
            string createdAt = ((DateTime)newEmployee.CreatedAt).ToString("yyyy-MM-dd HH:mm:ss");
            sqlQuery = string.Format(sqlQuery,
                                            newEmployee.FirstName,
                                            newEmployee.LastName,
                                            dob,
                                            newEmployee.Gender,
                                            newEmployee.Salary,
                                            newEmployee.SupervisorID,
                                            newEmployee.BranchID,
                                            createdAt
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

        public void EditEmployee(Employee changedEmployee)
        {
            string sqlQuery = @"UPDATE employees
                                SET 
                                    given_name='{0}',
                                    family_name='{1}',
                                    date_of_birth=""{2}"",
                                    gender_identity='{3}',
                                    gross_salary={4},
                                    supervisor_id={5},
                                    branch_id={6},
                                    updated_at=""{7}""
                                WHERE 
                                    id={8}";
            sqlQuery = string.Format(sqlQuery, 
                                        changedEmployee.FirstName,
                                        changedEmployee.LastName,
                                        changedEmployee.DateOfBirth,
                                        changedEmployee.Gender,
                                        changedEmployee.Salary,
                                        changedEmployee.SupervisorID,
                                        changedEmployee.BranchID,
                                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                        changedEmployee.ID
                                        );
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

        public List<ComboBoxItem> GetBranchNames()
        {
            string sqlQuery = "SELECT branch_name, id FROM branches";
            List<ComboBoxItem> branchNames = new()
            {
                new ComboBoxItem("All", 0)
            };
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sqlQuery, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int.TryParse($"{reader[1]}", out int branchId);
                    string branchName = reader[0].ToString();
                    ComboBoxItem branchname = new ComboBoxItem(branchName, branchId);
                    branchNames.Add(branchname);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            conn.Close();
            return branchNames;
        }

        public List<ComboBoxItem> GetSupervisorNames()
        {
            string sqlQuery = @"SELECT CONCAT(given_name, "" "", family_name) 
                                    AS name, id FROM employees 
                                    WHERE id IN (SELECT supervisor_id FROM employees);";
            List<ComboBoxItem> supervisorNames = new()
            {
                new ComboBoxItem("All", 0)
            };
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sqlQuery, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int.TryParse($"{reader[1]}", out int id);
                    string name = reader[0].ToString();
                    ComboBoxItem comboBoxItem = new ComboBoxItem(name, id);

                    supervisorNames.Add(comboBoxItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
            return supervisorNames;
        }

        public List<Employee> SearchEmployees(Employee searchParams, decimal low, decimal high)
        {   
            string searchQuery = @"SELECT * FROM employees
                                    WHERE";
            
            string searchID = "";
            string searchFirstName = "";
            string searchLastName = "";
            string searchGender = "";
            string searchLow = "";
            string searchHigh = "";
            string searchSupervisor = "";
            string searchBranch = "";
            string andString = "";

            if (searchParams.ID != 0)
            {
                searchID = string.Format(" id={0}", searchParams.ID);
                andString = "AND ";
            }
            if (searchParams.FirstName != null && searchParams.FirstName != "")
            {
                searchFirstName = string.Format(@" {0}given_name LIKE ""%{1}%""",andString, searchParams.FirstName);
                andString = "AND ";
            }
            if (searchParams.LastName != null && searchParams.LastName != "")
            {
                searchLastName = string.Format(@" {0}family_name LIKE ""%{1}%""",andString, searchParams.LastName);
                andString = "AND ";
            }
            if (searchParams.Gender != null && searchParams.Gender != "")
            {
                searchGender = string.Format(@" {0}gender_identity LIKE ""{1}""",andString, searchParams.Gender);
                andString = "AND ";
            }
            if (low != 0)
            {
                searchLow = string.Format(@" {0}gross_salary >= {1}", andString, low);
                andString = "AND ";
            }
            if (high != 0)
            {
                searchHigh = string.Format(@" {0}gross_salary <= {1}", andString, high);
                andString = "AND ";
            }
            if (searchParams.SupervisorID != null && searchParams.SupervisorID != 0)
            {
                searchSupervisor = string.Format(@" {0}supervisor_id={1}", andString, searchParams.SupervisorID);
                andString = "AND ";
            }
            if (searchParams.BranchID != null && searchParams.BranchID != 0)
            {
                searchBranch = string.Format(@" {0}branch_id={1}", andString, searchParams.BranchID);
            }



            string fullSearch = searchQuery + searchID + searchFirstName + searchLastName + searchGender +
                searchLow + searchHigh + searchSupervisor + searchBranch;


            List<Employee> employees = GetEmployees(fullSearch);
            return employees;
        }

    }
}
