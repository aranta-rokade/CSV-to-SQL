using System;
using CSVReader;
using System.Data.SqlClient;
using System.Data;

namespace SQLOperations
{
    public class CollectionToSQL
    {
        public static void WriteCollectionToSQL()
        {
            ReadCSV.Read();

            using (SqlConnection con = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                con.Open();
                SqlTransaction trans = con.BeginTransaction();
                SqlCommand cmd = null;
                try
                {
                    string query = "";
                    foreach (Customer item in ReadCSV.list)
                    {
                        query += "INSERT INTO Customer VALUES(" + item.CID + ",'" + item.CName + "'," + item.BillNo + ",'" + item.Email + "');";   
                    }

                    cmd = new SqlCommand(query, con, trans);
                    cmd.ExecuteNonQuery();
                    trans.Commit();
                    Console.WriteLine("Done ..");
                }
                catch (SqlException ex)
                {
                    trans.Rollback();
                    Console.WriteLine("Not Done ...");
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public static DataTable Select()
        {
            using (SqlConnection con = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                con.Open();
                SqlCommand cmd = null;
               
                string query = "SELECT * FROM Customer";
                cmd = new SqlCommand(query, con);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    return dt;
                }
            }
        }

        public static int Insert(int id, string name, int billNo, string email)
        {
            using (SqlConnection con = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                con.Open();
                SqlCommand cmd = null;

                string query = "INSERT INTO Customer VALUES (" + id + ",'" + name + "', "+billNo+",'"+email +"');";
                cmd = new SqlCommand(query, con);
                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected;
            }
        }

        public static int Update(int id, string name, int billNo, string email)
        {
            using (SqlConnection con = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                con.Open();
                SqlCommand cmd = null;

                string query = "UPDATE Customer SET name = '" + name + "', billNo = " + billNo + ", email = '" + email + "' WHERE id=" + id + ";";
                cmd = new SqlCommand(query, con);
                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected;
            }
        }

        public static int Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                con.Open();
                SqlCommand cmd = null;

                string query = "DELETE FROM CUSTOMER WHERE id = " + id + " ; ";
                cmd = new SqlCommand(query, con);
                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected;
            }
        }
    }
}
