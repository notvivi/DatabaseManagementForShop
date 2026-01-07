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
    /// DAO for class race
    /// </summary>
    public class RaceDAO : IDAO<Race>
    {
        /// <summary>
        /// Method for deleting an entry in database
        /// </summary>
        /// <param name="race"></param>
        public void Delete(Race race)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM race WHERE id = @id", conn))
            {
                command.Parameters.Add(new SqlParameter("@id", race.ID));
                command.ExecuteNonQuery();
                race.ID = 0;
            }
        }
        /// <summary>
        /// Method for getting every entry in database
        /// </summary>
        /// <returns>every entry</returns>
        public IEnumerable<Race> GetAll()
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT * FROM race", conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Race race = new Race(
                            Convert.ToInt32(reader[0].ToString()),
                            reader[1].ToString()
                        );
                        yield return race;
                    }
                }
            }
        }
        /// <summary>
        /// Method for getting one entry in database by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>one entry</returns>
        public Race? GetByID(int id)
        {
            Race? race = null;
            SqlConnection connection = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT * FROM race WHERE id = @Id", connection))
            {
               
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@Id";
                param.Value = id;

                command.Parameters.Add(param);
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        race = new Race(
                            Convert.ToInt32(reader[0].ToString()),
                            reader[1].ToString()
                        );
                    }
                }
            }
            return race;

        }
        /// <summary>
        /// Method for saving entry to database
        /// </summary>
        /// <param name="race"></param>
        public void Save(Race race)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            SqlCommand command = null;

            if (race.ID < 1)
            {
                using (command = new SqlCommand("INSERT INTO race (title) VALUES (@title)", conn))
                {
                    command.Parameters.Add(new SqlParameter("@title", race.Title));
                    command.ExecuteNonQuery();

                    command.CommandText = "Select @@Identity";
                    race.ID = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            else
            {
                using (command = new SqlCommand("UPDATE race SET title = @title " +
                    "WHERE id = @id", conn))
                {
                    command.Parameters.Add(new SqlParameter("@title", race.Title));
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Method for removing every entry in table
        /// </summary>
        public void RemoveAll()
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM race", conn))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
