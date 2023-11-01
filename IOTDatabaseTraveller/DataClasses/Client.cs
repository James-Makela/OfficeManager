using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOTDatabaseTraveller.DataClasses
{
    public class Client
    {
        public int? ID { get; set; }
        public string? ClientName { get; set; }
        public int? BranchID { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Client() { }

        public Client(int? iD, string? clientName, int? branchID, DateTime? createdAt, DateTime? lastUpdatedAt)
        {
            ID = iD;
            ClientName = clientName;
            BranchID = branchID;
            CreatedAt = createdAt;
            UpdatedAt = lastUpdatedAt;
        }
    }
}
