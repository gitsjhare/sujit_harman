using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebClient.Models;

namespace WebClient.GetFornameDto
{
    public class FetchFornameDto
    {
        private readonly string ConnectionString = ConfigurationManager.AppSettings["connectionString"];

        public IList<FornameModel> FetchForname()
        {
            IList<FornameModel> _fornamemodel = new List<FornameModel>();
            if (ConnectionString != null)
            {
                using (var sqlConnection = new SqlConnection(ConnectionString))
                {
                    using (var sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.CommandText = "FetchForname";
                        try
                        {
                            sqlConnection.Open();
                            using (var reader = sqlCommand.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    _fornamemodel.Add(new FornameModel
                                    {
                                        FORNAME_id = Convert.ToInt32(reader["FORNAME_id"]),
                                        FORNAME = Convert.ToString(reader["FORNAME"])
                                    });
                                }
                            }
                            sqlConnection.Close();
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }

            }

            return _fornamemodel;
        }
    }
}