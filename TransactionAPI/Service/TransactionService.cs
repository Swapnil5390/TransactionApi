using System;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using TransactionAPI.Model;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Http;

using TransactionAPI.Service;
using System.Collections.Generic;
using System.Threading;
using System.Linq;


namespace TransactionAPI.Service
{
    public class TransactionService
    {

        string connectionString = "Data Source=SWAPNILLAPTOP;Initial Catalog=transactionDataBase;Integrated Security=True;TrustServerCertificate=True;";

        public int addtransactions(AddTransaction addTransaction)
        {
            int rowAffected = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {


                SqlCommand cmd = new SqlCommand("INSERT INTO TRecords (Price, Category, Item, IsActive) VALUES (@Price, @Category, @Item, @IsActive)", con);
                //Add parameters to command
                cmd.Parameters.AddWithValue("@Price", addTransaction.Price);
                cmd.Parameters.AddWithValue("@Category", addTransaction.Category);
                cmd.Parameters.AddWithValue("@Item", addTransaction.Item);
                cmd.Parameters.AddWithValue("@IsActive", addTransaction.IsActive);
                con.Open();
                rowAffected = cmd.ExecuteNonQuery();
                con.Close();

            }
            return rowAffected;
        }


        public List<TransactionRequest> GetAllTransaction(string sortproperty)
        {
            List<TransactionRequest> records = new List<TransactionRequest>();


            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("  SELECT Id,Price, Category, Item, IsActive FROM TRecords WHERE IsActive =1;", con);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var record = new TransactionRequest
                        {
                            Id = (int)reader["Id"],
                            Price = (int)reader["Price"],
                            Category = reader["Category"].ToString(),
                            Item = reader["Item"].ToString(),
                            IsActive = (int)reader["IsActive"]
                        };
                        records.Add(record);
                    }
                }
                con.Close();
            }
            if (sortproperty == null)
            {

                return records;
            }

            List<TransactionRequest> sortedResponces = new List<TransactionRequest>();
            if (sortproperty.ToLower() == "category")
            {
                sortedResponces = records.OrderBy(response => response.Category).ToList();

            }
            if (sortproperty.ToLower() == "price")
            {
                sortedResponces = records.OrderBy(response => response.Price).ToList();

            }
            return (sortedResponces);

        }


            public int UpdateSingleRecordById(UpdateTransactionRequest updateTransactionRequest)
        {
            int rowAffected = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Prepare the SQL command with parameters
                SqlCommand cmd = new SqlCommand("UPDATE TRecords SET Price = @Price, Category = @Category, Item = @Item WHERE ID = @Id", con);

                // Add parameters to the command
                cmd.Parameters.AddWithValue("@Price", updateTransactionRequest.Price);
                cmd.Parameters.AddWithValue("@Category", updateTransactionRequest.Category);
                cmd.Parameters.AddWithValue("@Item", updateTransactionRequest.Item);
                cmd.Parameters.AddWithValue("@Id", updateTransactionRequest.Id);
                con.Open();
                rowAffected = cmd.ExecuteNonQuery();
                con.Close();

            }
            return rowAffected;
        }

        public Boolean deleteRecords(DeleteTransaction deleteTransaction)
        {
            int rowAffected = 0;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Prepare the SQL command with parameters
                SqlCommand cmd = new SqlCommand("UPDATE TRecords SET IsActive=0 WHERE ID = @Id;", con);

                // Add parameters to the command

                cmd.Parameters.AddWithValue("@Id", deleteTransaction.Id);
                con.Open();
                rowAffected = cmd.ExecuteNonQuery();
                con.Close();
            }
            return rowAffected > 0;
        }


        public Boolean purgeRecords(DeleteTransaction deleteTransaction)
        {
            int rowAffected = 0;
            
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    // Prepare the SQL command with parameters
                    SqlCommand cmd = new SqlCommand("Delete From TRecords where ID=@Id;", con);

                    // Add parameters to the command

                    cmd.Parameters.AddWithValue("@Id", deleteTransaction.Id);
                    con.Open();
                    rowAffected = cmd.ExecuteNonQuery();
                    con.Close();
                }
            return rowAffected > 0;
        }
    }
}