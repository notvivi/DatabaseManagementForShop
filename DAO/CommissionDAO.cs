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
        public void Cancel(Commission commission)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using SqlCommand cmd = new SqlCommand(
                "UPDATE commission SET status_id = @status WHERE id = @id", conn);

            cmd.Parameters.AddWithValue("@id", commission.ID);
            cmd.Parameters.AddWithValue("@status", (int)OrderStatus.Cancelled);

            cmd.ExecuteNonQuery();
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
                        Commission commission = new Commission(
                            Convert.ToInt32(reader[0].ToString()),
                            reader[1].ToString(),
                            reader[2].ToString()
                        );
                        commission.Status = (OrderStatus)Convert.ToInt32(reader[3].ToString());

                        yield return commission;

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
                        commission.Status = (OrderStatus)Convert.ToInt32(reader[4].ToString());

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
            if (!Enum.IsDefined(typeof(OrderStatus), commission.Status))
            {
                commission.Status = OrderStatus.Created;
            }

            SqlCommand command;

            if (commission.ID < 1)
            {
                Console.WriteLine($"Inserting commission with status = {(int)commission.Status}");

                command = new SqlCommand(
                    "INSERT INTO commission (list_id, customer_id, order_date, status_id) " +
                    "VALUES (@list_id, @customer_id, @order_date, @status_id); SELECT SCOPE_IDENTITY();",
                    conn, tran);

                command.Parameters.AddWithValue("@list_id", commission.List_id);
                command.Parameters.AddWithValue("@customer_id", commission.Customer_id);
                command.Parameters.AddWithValue("@order_date", commission.Order_date);
                command.Parameters.AddWithValue("@status_id", (int)commission.Status);


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
                command.Parameters.AddWithValue("@status_id", (int)commission.Status);

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
