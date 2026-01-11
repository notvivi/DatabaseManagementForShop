using DbProjekt.src;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbProjekt.DAO
{
    public class StatsDAO
    {
        /// <summary>
        /// Method for selecting stats for a customer from database
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>Data from database</returns>
        /// <exception cref="Exception"></exception>
        public Stats GetStats(int customerId)
        {

            SqlConnection conn = DatabaseSingleton.GetInstance();

            string sql = "SELECT * FROM get_stats_per_order_customer WHERE customer_id = @cid";
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@cid", customerId);

            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Stats
                {
                    NumberOfArtifacts = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                    MostExpensive = reader.IsDBNull(2) ? 0 : Convert.ToDecimal(reader[2]),
                    LeastExpensive = reader.IsDBNull(3) ? 0 : Convert.ToDecimal(reader[3]),
                    NumberOfOrders = reader.IsDBNull(1) ? 0 : reader.GetInt32(1)
                };
            }

            throw new Exception("No stats found for customer_id = " + customerId);
        }
    }
    
}
