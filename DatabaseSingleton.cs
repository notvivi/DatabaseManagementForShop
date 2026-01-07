using Microsoft.Data.SqlClient;
using System.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbProjekt
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
            try
            {
                if (conn == null)
                {
                    SqlConnectionStringBuilder consStringBuilder = new SqlConnectionStringBuilder();
                    consStringBuilder.UserID = ReadSetting("Name");
                    consStringBuilder.Password = ReadSetting("Password");
                    consStringBuilder.InitialCatalog = ReadSetting("Database");
                    consStringBuilder.DataSource = ReadSetting("DataSource");
                    consStringBuilder.ConnectTimeout = 30;
                    consStringBuilder.TrustServerCertificate = true;
                    conn = new SqlConnection(consStringBuilder.ConnectionString);
                    conn.Open();
                    
                }
            }catch (Exception ex)
            {
                Console.WriteLine("Cannot login into database");
                Console.WriteLine("Check config file, if everything is correct.");
                System.Environment.Exit(1);
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
