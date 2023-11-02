using IOTDatabaseTraveller.DataClasses;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IOTDatabaseTraveller.Datamanager
{
    public partial class DataManager
    {
        public List<Employee> employees = new();

        public List<Employee> GetEmployees(string sqlQuery = "SELECT * FROM employees")
        {
            employees.Clear();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sqlQuery, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int? supervisorId;
                    DateTime? lastUpdated;
                    if (reader[9] == DBNull.Value)
                    {
                        lastUpdated = null;
                    }
                    else
                    {
                        lastUpdated = reader.GetDateTime(9);
                    }
                    if (reader[6] == DBNull.Value)
                    {
                        supervisorId = null;
                    }
                    else
                    {
                        supervisorId = reader.GetInt32(6);
                    }

                    Employee employee = new()
                    {
                        ID = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        DateOfBirth = reader.GetDateTime(3),
                        Gender = reader.GetString(4),
                        Salary = reader.GetDecimal(5),
                        SupervisorID = supervisorId,
                        BranchID = reader.GetInt32(7),
                        CreatedAt = reader.GetDateTime(8),
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
            catch (Exception ex)
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
            string dob = ((DateTime)changedEmployee.DateOfBirth).ToString("yyyy-MM-dd");
            sqlQuery = string.Format(sqlQuery,
                                        changedEmployee.FirstName,
                                        changedEmployee.LastName,
                                        dob,
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();
            }
        }

        public List<ComboBoxItem> GetBranchNames(bool includeAll=true)
        {
            string sqlQuery = "SELECT branch_name, id FROM branches";
            List<ComboBoxItem> branchNames = new();

            if (includeAll)
            {
                branchNames.Add(new ComboBoxItem("All", 0));
            }

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sqlQuery, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int.TryParse(reader[1].ToString(), out int branchId);
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
                    int.TryParse(reader[1].ToString(), out int id);
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

        public List<ComboBoxItem> GetEmployeeNames(bool includeAll=true)
        {
            string sqlQuery = @"SELECT CONCAT(given_name, "" "", family_name), id
                                    FROM employees";

            List<ComboBoxItem> employeeNames = new();
            
            if (includeAll)
            {
                employeeNames.Add(new ComboBoxItem("All", 0));
            }
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sqlQuery, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ComboBoxItem comboBoxItem = new(reader.GetString(0), reader.GetInt32(1));
                    employeeNames.Add(comboBoxItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
            return employeeNames;
        }

        public bool ValidEmployeeCheck(Employee employeeToCheck)
        {
            if (!(employeeToCheck.FirstName.Length > 1) || !(employeeToCheck.FirstName.All(Char.IsLetter)))
            {
                return false;
            }
            return true;
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
                searchFirstName = string.Format(@" {0}given_name LIKE ""%{1}%""", andString, searchParams.FirstName);
                andString = "AND ";
            }
            if (searchParams.LastName != null && searchParams.LastName != "")
            {
                searchLastName = string.Format(@" {0}family_name LIKE ""%{1}%""", andString, searchParams.LastName);
                andString = "AND ";
            }
            if (searchParams.Gender != null && searchParams.Gender != "")
            {
                searchGender = string.Format(@" {0}gender_identity LIKE ""{1}""", andString, searchParams.Gender);
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
