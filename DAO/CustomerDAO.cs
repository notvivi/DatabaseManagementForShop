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
    /// DAO for class customer
    /// </summary>
    public class CustomerDAO : IDAO<Customer>
    {
        /// <summary>
        /// Method for deleting an entry in database
        /// </summary>
        /// <param name="customer"></param>
        public void Delete(Customer customer)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM commission WHERE id = @id", conn))
            {
                command.Parameters.Add(new SqlParameter("@id", customer.ID));
                command.ExecuteNonQuery();
                customer.ID = 0;
            }
        }
        /// <summary>
        /// Method for getting every entry in database
        /// </summary>
        /// <returns>every entry</returns>
        public IEnumerable<Customer> GetAll()
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT customer.id, customer.nickname, race.title as race FROM customer inner join race on customer.race_id = race.id ", conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Customer customer = new Customer(
                            Convert.ToInt32(reader[0].ToString()),
                            reader[1].ToString(),
                            reader[2].ToString()
                        );
                        yield return customer;
                    }
                }
            }
        }
        /// <summary>
        /// Method for getting one entry in database by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>one entry</returns>
        public Customer? GetByID(int id)
        {
            Customer? customer = null;
            SqlConnection connection = DatabaseSingleton.GetInstance();
            // 1. declare command object with parameter
            using (SqlCommand command = new SqlCommand("SELECT * FROM customer WHERE id = @Id", connection))
            {
                // 2. define parameters used in command 
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@Id";
                param.Value = id;

                // 3. add new parameter to command object
                command.Parameters.Add(param);
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        customer = new Customer(
                            Convert.ToInt32(reader[0].ToString()),
                            reader[1].ToString(),
                            reader[2].ToString()
                            );
                    }
                }
            }
            return customer;

        }
        /// <summary>
        /// Method for saving entry to database
        /// </summary>
        /// <param name="customer"></param>
        public void Save(Customer customer)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            SqlCommand command = null;
            try
            {
                if (customer.ID < 1)
                {
                    using (command = new SqlCommand("INSERT INTO customer (nickname, race_id) VALUES (@nickname, @race_id)", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@nickname", customer.Nickname));
                        command.Parameters.Add(new SqlParameter("@race_id", customer.Race_id));
                        command.ExecuteNonQuery();

                        command.CommandText = "Select @@Identity";
                        customer.ID = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                else
                {
                    using (command = new SqlCommand("UPDATE customer SET nickname = @nickname, race_id = @race_id " +
                        "WHERE id = @id", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@id", customer.ID));
                        command.Parameters.Add(new SqlParameter("@nickname", customer.Nickname));
                        command.Parameters.Add(new SqlParameter("@race_id", customer.Race_id));
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                Console.WriteLine("Foreign key of race wasnt found");
            }
         
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="conn"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public bool Exists(int id, SqlConnection conn, SqlTransaction tran)
        {
            SqlCommand cmd = new SqlCommand(
                "SELECT COUNT(*) FROM customer WHERE id = @id",
                conn, tran);

            cmd.Parameters.AddWithValue("@id", id);
            return (int)cmd.ExecuteScalar() > 0;
        }

        /// <summary>
        /// Method for removing every entry in table
        /// </summary>
        public void RemoveAll()
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM customer", conn))
            {
                command.ExecuteNonQuery();
            }
        }
    
    }
}
