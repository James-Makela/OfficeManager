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
        List<Branch> branches = new();
        

        public List<Branch> GetBranches(string sqlQuery = "SELECT * FROM branches")
        {
            branches.Clear();

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sqlQuery, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DateTime? lastUpdated;
                    if (reader[5] == DBNull.Value)
                    {
                        lastUpdated = null;
                    }
                    else
                    {
                        lastUpdated = reader.GetDateTime(5);
                    }

                    Branch branch = new()
                    {
                        ID = reader.GetInt32(0),
                        BranchName = reader.GetString(1),
                        ManagerID = reader.GetInt32(2),
                        ManagerStartedAt = reader.GetDateTime(3),
                        CreatedAt = reader.GetDateTime(4),
                        UpdatedAt = lastUpdated,
                    };
                    branches.Add(branch);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            conn.Close();
            return branches;
        }

        public void DeleteBranch(Branch oldBranch)
        {
            string sqlNonQuery = @"DELETE FROM branches WHERE id={0}";
            sqlNonQuery = string.Format(sqlNonQuery, oldBranch.ID);
            SqlNonQuery(sqlNonQuery);
        }

        public void AddBranch(Branch newBranch)
        {
            string sqlNonQuery = @"INSERT INTO branches (
                                            branch_name,
                                            manager_id,
                                            manager_started_at,
                                            created_at)
                                        VALUES
                                            ('{0}', {1}, ""{2}"", ""{3}"")";

            string managerStartedAt = ((DateTime)newBranch.ManagerStartedAt).ToString("yyyy-MM-dd");
            string createdAt = ((DateTime)newBranch.CreatedAt).ToString("yyyy-MM-dd");
            sqlNonQuery = String.Format(sqlNonQuery, newBranch.BranchName, newBranch.ManagerID, managerStartedAt, createdAt);

            SqlNonQuery(sqlNonQuery);
        }

        public bool CheckBranchIsValid(Branch branchToCheck)
        {
            if (branchToCheck == null) return false;
            if ((branchToCheck.BranchName == null) || (branchToCheck.BranchName.Length < 1))
            {
                return false;
            }
            return true;
        }

        public void EditBranch(Branch changedBranch)
        {
            string sqlNonQuery = @"UPDATE branches
                                    SET
                                        branch_name='{0}',
                                        manager_id={1},
                                        manager_started_at=""{2}""
                                    WHERE
                                        id={3}";

            string managerStartedAt = ((DateTime)changedBranch.ManagerStartedAt).ToString("yyyy-MM-dd");
            sqlNonQuery = string.Format(sqlNonQuery,
                                        changedBranch.BranchName,
                                        changedBranch.ManagerID,
                                        managerStartedAt,
                                        changedBranch.ID);

            SqlNonQuery(sqlNonQuery);
        }

        public List<Branch> SearchBranches(Branch searchParams)
        {
            string searchQuery = @"SELECT * FROM branches
                                    WHERE";

            string searchBranchID = "";
            string searchSupervisorId = "";
            string andString = "";

            if (searchParams.ID != 0)
            {
                searchBranchID = string.Format(" id={0}", searchParams.ID);
                andString = "AND ";
            }
            if (searchParams.ManagerID != 0)
            {
                searchSupervisorId = string.Format(" {0}manager_id={1}", andString, searchParams.ManagerID);
            }

            string fullSearch = searchQuery + searchBranchID + searchSupervisorId;
            List<Branch> result = GetBranches(fullSearch);
            return result;
        }
    }
}
