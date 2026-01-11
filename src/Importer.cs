using DbProjekt.DAO;
using DbProjekt.Tables;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbProjekt.src
{
    /// <summary>
    /// Class for importing from file
    /// </summary>
    public class Importer
    {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="artifactFile"></param>
        /// <param name="raceFile"></param>
        /// 

        public Importer()
        {
        }
        /// <summary>
        /// Method for importing from file
        /// Source: ChatGPT helped with some lines
        /// </summary> 
        /// <param name="artifactFile"></param>
        /// <param name="raceFile"></param>
        public void ImportCsv(string artifactFile, string raceFile) 
        {
            ArtifactDAO aDao = new ArtifactDAO();
            RaceDAO rDao = new RaceDAO();

            try
            {

                var raceRead = File.ReadLines(raceFile);

                var artifactRead = File.ReadLines(artifactFile);

                foreach (var line in artifactRead.Skip(1))
                {
                    var columns = line.Split(",");

                    if (columns.Length == 3)
                    {
                        string title = columns[0];
                        string usage = columns[1];
                        bool dangerous = bool.Parse(columns[2]);

                        Artifact artifact = new Artifact(title, usage, dangerous);
                        aDao.Save(artifact);
                    }
                }
                foreach (var line in raceRead.Skip(1)) 
                {
                    var columns = line.Split(",");

                    if (columns.Length == 1)  
                    {
                        string title = columns[0];

                        Race race = new Race(title);
                        rDao.Save(race);
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Data couldn´t have been imported, error.");
                Console.WriteLine("Please turn off program and try again.");
            }
            finally
            {
                try
                {
                    DatabaseSingleton.CloseConnection();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
         

        }
    }

}
