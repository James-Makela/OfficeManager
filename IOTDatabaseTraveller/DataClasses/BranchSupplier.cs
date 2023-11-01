using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOTDatabaseTraveller.DataClasses
{
    internal class BranchSupplier
    {
        public int? BranchID { get; set; }
        public int? SupplierID { get; set; }
        public string? SupplierName { get; set; }
        public string? ProductSupplied { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public BranchSupplier() { }

        public BranchSupplier(int? branchID, int? supplierID, string? supplierName, string? productSupplied, DateTime? createdAt,  DateTime updatedAt)
        {
            BranchID = branchID;
            SupplierID = supplierID;
            SupplierName = supplierName;
            ProductSupplied = productSupplied;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

    }
}
