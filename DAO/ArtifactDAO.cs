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
    /// DAO for class artifact
    /// </summary>
    public class ArtifactDAO : IDAO<Artifact>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="artifact"></param>
        public void Delete(Artifact artifact)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM artifact WHERE id = @id", conn))
            {
                command.Parameters.Add(new SqlParameter("@id", artifact.ID));
                command.ExecuteNonQuery();
                artifact.ID = 0;
            }
        }
        /// <summary>
        /// Method for getting every entry in database
        /// </summary>
        /// <returns>every entry</returns>
        public IEnumerable<Artifact> GetAll()
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT * FROM artifact", conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Artifact artifact = new Artifact(
                            Convert.ToInt32(reader[0].ToString()),
                            reader[1].ToString(),
                            reader[2].ToString(),
                            Convert.ToBoolean(reader[3].ToString())
                        );
                        yield return artifact;
                    }
                }
            }
        }
        /// <summary>
        /// Method for getting one entry in database by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>one entry</returns>
        public Artifact? GetByID(int id)
        {
            Artifact? artifact = null;
            SqlConnection connection = DatabaseSingleton.GetInstance();
            // 1. declare command object with parameter
            using (SqlCommand command = new SqlCommand("SELECT * FROM artifact WHERE id = @Id", connection))
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
                        artifact = new Artifact(
                            Convert.ToInt32(reader[0].ToString()),
                            reader[1].ToString(),
                            reader[2].ToString(),
                            Convert.ToBoolean(reader[3].ToString())
                            );
                    }
                }
            }
            return artifact;

        }
        /// <summary>
        /// Method for saving entry to database
        /// </summary>
        /// <param name="artifact"></param>
        public void Save(Artifact artifact)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            SqlCommand command = null;

            if (artifact.ID < 1)
            {
                using (command = new SqlCommand("INSERT INTO artifact (title, usage, dangerous) VALUES (@title, @usage, @dangerous)", conn))
                {
                    command.Parameters.Add(new SqlParameter("@title", artifact.Title));
                    command.Parameters.Add(new SqlParameter("@usage", artifact.Usage));
                    command.Parameters.Add(new SqlParameter("@dangerous", artifact.Dangerous));
                    command.ExecuteNonQuery();

                    command.CommandText = "Select @@Identity";
                    artifact.ID = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            else
            {
                using (command = new SqlCommand("UPDATE artifact SET title = @title, usage = @usage, dangerous =  @dangerous " +
                    "WHERE id = @id", conn))
                {
                    command.Parameters.Add(new SqlParameter("@id", artifact.ID));
                    command.Parameters.Add(new SqlParameter("@title", artifact.Title));
                    command.Parameters.Add(new SqlParameter("@usage", artifact.Usage));
                    command.Parameters.Add(new SqlParameter("@dangerous", artifact.Dangerous));
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

            using (SqlCommand command = new SqlCommand("DELETE FROM artifact", conn))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}

