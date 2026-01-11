using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DbProjekt.src
{
    /// <summary>
    /// Class for joining into database
    /// </summary>
    public class DatabaseSingleton 
    {


        private static SqlConnection? conn = null;

        /// <summary>
        /// Constructor
        /// </summary>
        private DatabaseSingleton()
        {
        }
        /// <summary>
        /// Method for joining into database
        /// </summary>
        /// <returns>Sql connection</returns>
        public static SqlConnection GetInstance()
        {

            AppConfig config = null;
            try
            {
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
                string connectionString = File.ReadAllText(path);
                config = JsonSerializer.Deserialize<AppConfig>(connectionString);
                conn = new SqlConnection(config.ConnectionString);
                conn.Open();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot login into database");
                Console.WriteLine("Check json file, if everything is correct.");
                Console.WriteLine(ex.Message);
                //Environment.Exit(1);
            }
    
            return conn;

        }
        /// <summary>
        /// Method for closing sql connection
        /// </summary>
        public static void CloseConnection()
        {
            if (conn != null)
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }
        /// <summary>
        /// Method for reading setting from config file
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Key from appsettings</returns>
        private static string ReadSetting(string key)
        {
            //nutno doinstalovat, VS nabídne doinstalaci samo
            var appSettings = ConfigurationManager.AppSettings;
            string result = appSettings[key] ?? "Not Found";
            return result;
        }
    }
}

public class AppConfig
{
    public string ConnectionString { get; set; }
}

