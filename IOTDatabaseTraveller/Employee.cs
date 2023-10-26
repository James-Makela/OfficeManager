using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOTDatabaseTraveller
{
    public class Employee
    {
        public int? ID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public decimal? Salary { get; set; }
        public int? SupervisorID { get; set; }
        public int? BranchID { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set;}

        public Employee() { }

        public Employee(string firstName, string lastName, DateTime? dateOfBirth, string gender, decimal salary, int supervisorID, int branchID, DateTime createdAt, DateTime? lastUpdatedAt, int? iD = null)
        {
            ID = iD;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Salary = salary;
            SupervisorID = supervisorID;
            BranchID = branchID;
            CreatedAt = createdAt;
            LastUpdatedAt = lastUpdatedAt;
        }
    }
}
