using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class CoronaOperations
    {
        public List<Datum> GetTheRecords(string sqlQuery)
        {
            SqlConnectionStringBuilder connString = new SqlConnectionStringBuilder();
            connString.UserID = "sa";
            connString.Password = "Technology3";
            connString.DataSource = "l2.kaje.ucnit20.eu";
            connString.IntegratedSecurity = false; // if true then windows authentication
            connString.InitialCatalog = "Corona";
            List<Datum> theReply = new List<Datum>();
            using (SqlConnection connDB = new SqlConnection(connString.ConnectionString))
            {
                try
                {
                    connDB.Open();
                    var sqlCmd = new SqlCommand(sqlQuery, connDB);
                    var reader = sqlCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int index = 0;
                        Datum newData = new Datum();
                        newData.countrycode = reader.GetString(index++);
                        newData.date = reader.GetDateTime(index++).ToString();
                        newData.cases = reader.GetInt32(index++).ToString();
                        newData.deaths = reader.GetInt32(index++).ToString();
                        newData.recovered = reader.GetInt32(index++).ToString();
                        theReply.Add(newData);
                    }
                    reader.Close();
                    connDB.Close();
                }
                catch (SqlException ex)
                {
                    return (theReply);
                }

            }
            return (theReply);
        }

        public Boolean InsertNewRecord(Datum newRecord)
        {
            SqlConnectionStringBuilder connString = new SqlConnectionStringBuilder();
            connString.UserID = "sa";
            connString.Password = "Technology3";
            connString.DataSource = "l2.1081496.ucnit20.eu";
            connString.IntegratedSecurity = false; // if true then windows authentication
            connString.InitialCatalog = "Corona";

            using (SqlConnection connDB = new SqlConnection(connString.ConnectionString))
            {
                connDB.Open();
                int rowsAffected = -1;
                try
                {
                    string pQuery = "INSERT INTO theStats (countrycode, date, cases, deaths, recovered)";
                    pQuery += " VALUES (@countrycode, @date, @cases, @deaths, @recovered)";
                    SqlCommand myCommand = new SqlCommand(pQuery, connDB);
                    myCommand.Parameters.AddWithValue("@countrycode", newRecord.countrycode);
                    myCommand.Parameters.AddWithValue("@date", newRecord.date);
                    myCommand.Parameters.AddWithValue("@cases", newRecord.cases);
                    myCommand.Parameters.AddWithValue("@deaths", newRecord.deaths);
                    myCommand.Parameters.AddWithValue("@recovered", newRecord.recovered);
                    rowsAffected = myCommand.ExecuteNonQuery();           
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                return rowsAffected == 1 ? true : false;
            }
        }
    }
}