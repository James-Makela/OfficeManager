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
        List<EmployeeSale> employeeSales = new();

        public List<EmployeeSale> GetEmployeeSales(string whereQuery="")
        {
            string sqlQuery = @"SELECT 
                                employees.id,
                                CONCAT(given_name, "" "", family_name),
                                client_name,
                                total_sales
                        FROM employees
                        JOIN working_with ON employees.id=employee_id
                        JOIN clients ON clients.id=working_with.client_id
                        {0}
                        ORDER BY employees.id";

            decimal? totalEmployeeSales = 0;
            sqlQuery = string.Format(sqlQuery, whereQuery);
            employeeSales.Clear();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sqlQuery, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    EmployeeSale employeeSale = new()
                    {
                        ID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        ClientName = reader.GetString(2),
                        Sales = reader.GetInt32(3),
                    };
                    totalEmployeeSales += employeeSale.Sales;
                    employeeSales.Add(employeeSale);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (whereQuery != "")
            {
                EmployeeSale total = new()
                {
                    ID = null,
                    Name = "Total",
                    ClientName = null,
                    Sales = totalEmployeeSales,
                };
                employeeSales.Add(total);
            }

            conn.Close();
            return employeeSales;
        }

        // TODO: Add function to delete employeesale

        // TODO: Add function to add employeesale

        // TODO: Add function to edit employeesale

        // TODO: Add function to Check employeesale is valid

        // TODO: Add function to search employeesales
    }
}
