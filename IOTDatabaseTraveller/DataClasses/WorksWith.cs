using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOTDatabaseTraveller.DataClasses
{
    public class WorksWith
    {
        public int? EmployeeID { get; set; }
        public int? ClientID { get; set; }
        public decimal? TotalSales { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public WorksWith() { }

        public WorksWith(int? employeeID, int? clientID, decimal? totalSales, DateTime? createdAt, DateTime? updatedAt)
        {
            EmployeeID = employeeID;
            ClientID = clientID;
            TotalSales = totalSales;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
