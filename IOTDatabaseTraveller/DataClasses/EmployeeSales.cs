using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOTDatabaseTraveller.DataClasses
{
    public class EmployeeSale
    {
        public int? ID { get; set; }
        public string? Name { get; set; }
        public string? ClientName { get; set; }
        public decimal? Sales { get; set; }

        public EmployeeSale() { }

        public EmployeeSale(int? id, string? name, string? clientName, decimal? sales)
        {
            ID = id;
            Name = name;
            ClientName = clientName;
            Sales = sales;
        }
    }
}