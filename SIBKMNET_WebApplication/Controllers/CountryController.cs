using Microsoft.AspNetCore.Mvc;
using SIBKMNET_WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SIBKMNET_WebApplication.Controllers
{
    public class CountryController : Controller
    {

        SqlConnection sqlConnection;

        string connectionString = "Data Source=LAPTOP-7UAI3VT6\\SQLSERVER;Initial Catalog=db_SIBKMNET;User ID=sibkmnet;Password=1234567890;Connect Timeout=30";

        //GETALL
        public IActionResult Index()
        {

            string query = "SELECT * FROM Country";

            sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            List<Country> Countries = new List<Country>();
            try
            {
                sqlConnection.Open();
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            Country country = new Country();
                            country.Id = Convert.ToInt32(sqlDataReader[0]);
                            country.Name = sqlDataReader[1].ToString();
                            Countries.Add(country);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Rows");
                    }
                    sqlDataReader.Close();
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }

            return View(Countries);
        }

        //GETBY ID
        
        public IActionResult Details(int? id)
        {

                     
            string query = "SELECT * FROM Country WHERE Id=Id";

            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@id";
            sqlParameter.Value = id;

            sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.Add(sqlParameter);
            List<Country> Countries = new List<Country>();
            try
            {
                sqlConnection.Open();
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {


                            Country country = new Country();
                            country.Id = Convert.ToInt32(sqlDataReader[0]);
                            country.Name = sqlDataReader[1].ToString();
                            Countries.Add(country);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Rows");
                    }
                    sqlDataReader.Close();
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }

            return View();
        }
        

       

        //create
        //get

        [HttpGet]
        public IActionResult create(int id)
        {
            
            return View();
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Country country)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                SqlParameter sqlParameter = new SqlParameter();
                sqlParameter.ParameterName = "@name";
                sqlParameter.Value = country.Name;

                sqlCommand.Parameters.Add(sqlParameter);

                try
                {
                    sqlCommand.CommandText = "INSERT INTO Country " + "(Name) VALUES (@name)";
                    //sqlCommand.CommandText = "UPDATE Country SET Name=@name WHERE Id = @id";

                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
            }
            return View();
        }

        //delete
        //get

        //post
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(Country country)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                //string query = "SELECT * FROM Country";

                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                //SqlParameter sqlParameter = new SqlParameter();
                SqlParameter delete = new SqlParameter();


                delete.ParameterName = "@id";
                delete.Value = country.Id;
                //sqlParameter.ParameterName = "@name";
                //sqlParameter.Value = country.Name;

                sqlCommand.Parameters.Add(delete);


                try
                {
                    sqlCommand.CommandText = "DELETE FROM Country WHERE Id = @id";
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
            }
            return View();
        }

        //update
        //get
        //[HttpGet]

        //public IActionResult Update()
        //{
            
        //    return View();
        //}

        //post
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Update(Country country)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                //string query = "SELECT * FROM Country";

                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                SqlParameter sqlParameter = new SqlParameter();
                SqlParameter up = new SqlParameter();


                up.ParameterName = "@id";
                up.Value = country.Id;
                sqlParameter.ParameterName = "@name";
                sqlParameter.Value = country.Name;

                sqlCommand.Parameters.Add(sqlParameter);
                sqlCommand.Parameters.Add(up);


                try
                {
                    sqlCommand.CommandText = "UPDATE Country SET Name=@name WHERE Id = @id";
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
            }
            return View();
        }

       




    }
}
