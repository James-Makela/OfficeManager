using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOTDatabaseTraveller.DataClasses
{
    public class Branch
    {
        public int? ID { get; set; }
        public string? BranchName { get; set; }
        public int? ManagerID { get; set; }
        public DateTime? ManagerStartedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Branch() { }

        public Branch(int? id, string? branchName, int? managerID, DateTime? managerStartedAt, DateTime? createdAt, DateTime? updatedAt)
        {
            ID = id;
            BranchName = branchName;
            ManagerID = managerID;
            ManagerStartedAt = managerStartedAt;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
