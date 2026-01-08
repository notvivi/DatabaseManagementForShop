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
    /// DAO for class list
    /// </summary>
    public class ListDAO : IDAO<List>
    {
        /// <summary>
        /// Method for deleting an entry in database
        /// </summary>
        /// <param name="list"></param>
        public void Delete(List list)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM list WHERE id = @id", conn))
            {
                command.Parameters.Add(new SqlParameter("@id", list.ID));
                command.ExecuteNonQuery();
                list.ID = 0;
            }
        }
        /// <summary>
        /// Method for getting every entry in database
        /// </summary>
        /// <returns>every entry</returns>
        public IEnumerable<List> GetAll()
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

         
                using (SqlCommand command = new SqlCommand("select * from get_pricelist", conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            List list = new List(
                                Convert.ToInt32(reader[0].ToString()),
                                reader[1].ToString(),
                                reader[2].ToString(),
                                Convert.ToBoolean(reader[3].ToString()),
                                Convert.ToSingle(reader[4].ToString()),
                                Convert.ToInt32(reader[5].ToString())
                            );
                            yield return list;
                        }
                    }
                }
        }
        /// <summary>
        ///  Method for getting one entry in database by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>one entry</returns>
        public List? GetByID(int id)
        {
            List? list = null;
            SqlConnection connection = DatabaseSingleton.GetInstance();
            // 1. declare command object with parameter
            using (SqlCommand command = new SqlCommand("select artifact.title, artifact.usage, artifact.dangerous, quality, price from list inner join artifact on list.artifact_id = artifact.id" +
                "WHERE list.id = @Id", connection))
            {

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@Id";
                param.Value = id;

                command.Parameters.Add(param);
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        list = new List(
                            Convert.ToInt32(reader[0].ToString()),
                            reader[1].ToString(),
                            reader[2].ToString(),
                            Convert.ToBoolean(reader[3].ToString()),
                            Convert.ToSingle(reader[4].ToString()),
                            Convert.ToInt32(reader[5].ToString())
                            );
                    }
                }
            }
            return list;

        }
        /// <summary>
        /// Method for saving entry to database
        /// </summary>
        /// <param name="list"></param>
        public void Save(List list)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            SqlCommand command = null;
            try
            {
                if (list.ID < 1)
                {

                    using (command = new SqlCommand("INSERT INTO list (artifact_id, quality, price) VALUES (@artifact_id, @quality, @price)", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@artifact_id", list.Artifact_id));
                        command.Parameters.Add(new SqlParameter("@quality", list.Quality));
                        command.Parameters.Add(new SqlParameter("@price", list.Price));
                        command.ExecuteNonQuery();
                        command.CommandText = "Select @@Identity";
                        list.ID = Convert.ToInt32(command.ExecuteScalar());

                    }
                }
                else
                {
                    using (command = new SqlCommand("UPDATE list SET artifact_id = @artifact_id, quality = @quality, price = @price " +
                        "WHERE id = @id", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@artifact_id", list.Artifact_id));
                        command.Parameters.Add(new SqlParameter("@quality", list.Quality));
                        command.Parameters.Add(new SqlParameter("@price", list.Price));
                        command.ExecuteNonQuery();
                    }
                }
            }catch
            {
                Console.WriteLine("Foreign key of artifact wasnt found");
            }
            
                
            
           
           
        }

        /// <summary>
        /// Method for removing every entry in table
        /// </summary>
        public void RemoveAll()
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM list", conn))
            {
                command.ExecuteNonQuery();
            }
        }

    }
}
