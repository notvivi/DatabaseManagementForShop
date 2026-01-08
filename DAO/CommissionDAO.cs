using DbProjekt.src;
using DbProjekt.Tables;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbProjekt.DAO
{
    /// <summary>
    /// DAO for class commission
    /// </summary>
    public class CommissionDAO : IDAO<Commission>
    {
        /// <summary>
        /// Method for deleting an entry in database
        /// </summary>
        /// <param name="commission"></param>
        public void Delete(Commission commission)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM commission WHERE id = @id", conn))
            {
                command.Parameters.Add(new SqlParameter("@id", commission.ID));
                command.ExecuteNonQuery();
                commission.ID = 0;
            }
        }
        /// <summary>
        /// Method for getting every entry in database
        /// </summary>
        /// <returns>every entry</returns>
        public IEnumerable<Commission> GetAll()
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("select * from get_commissions", conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Commission commision = new Commission(
                            Convert.ToInt32(reader[0].ToString()),
                            reader[1].ToString(),
                            reader[2].ToString()
                        );
                        yield return commision;
                    }
                }
            }
        }
        /// <summary>
        ///  Method for getting one entry in database by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>one entry</returns>
        public Commission? GetByID(int id)
        {
            Commission? commission = null;
            SqlConnection connection = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT * FROM commission WHERE id = @Id", connection))
            {

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@Id";
                param.Value = id;

                command.Parameters.Add(param);
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        commission = new Commission(
                            Convert.ToInt32(reader[0].ToString()),
                            Convert.ToInt32(reader[1].ToString()),
                            Convert.ToInt32(reader[2].ToString()),
                            Convert.ToDateTime(reader[3].ToString())
                            );
                    }
                }
            }
            return commission;

        }
        /// <summary>
        /// Method for saving entry to database
        /// </summary>
        /// <param name="commission"></param>
        public void Save(Commission commission, SqlConnection conn, SqlTransaction tran)
        {
            SqlCommand command;

            if (commission.ID < 1)
            {
                command = new SqlCommand(
                    "INSERT INTO commission (list_id, customer_id, order_date) " +
                    "VALUES (@list_id, @customer_id, @order_date); SELECT SCOPE_IDENTITY();",
                    conn, tran);

                command.Parameters.AddWithValue("@list_id", commission.List_id);
                command.Parameters.AddWithValue("@customer_id", commission.Customer_id);
                command.Parameters.AddWithValue("@order_date", commission.Order_date);

                commission.ID = Convert.ToInt32(command.ExecuteScalar());
            }
            else
            {
                command = new SqlCommand(
                    "UPDATE commission SET list_id = @list_id, customer_id = @customer_id WHERE id = @id",
                    conn, tran);

                command.Parameters.AddWithValue("@id", commission.ID);
                command.Parameters.AddWithValue("@list_id", commission.List_id);
                command.Parameters.AddWithValue("@customer_id", commission.Customer_id);

                command.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Method for removing every entry in table
        /// </summary>
        public void RemoveAll()
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM commission", conn))
            {
                command.ExecuteNonQuery();
            }
        }

        public void Save(Commission element)
        {
            throw new NotImplementedException();
        }
    }
}
