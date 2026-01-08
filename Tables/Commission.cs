using DbProjekt.src;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbProjekt.Tables
{
    /// <summary>
    /// Class for commission table
    /// </summary>
    public class Commission : IBaseClass
    {
        private int id;
        private int list_id;
        private int customer_id;
        private DateTime order_date;

        private string customer_nick;
        private string artifact_title;
        

        public int ID { get => id; set => id = value; }
        public int List_id { get => list_id; set => list_id = value; }
        public int Customer_id { get => customer_id; set => customer_id = value; }
        public DateTime Order_date { get => order_date; set => order_date = value; }

        public string Customer_nick { get => customer_nick; set => customer_nick = value; }
        public string Artifact_title { get => artifact_title; set => artifact_title = value; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="list_id"></param>
        /// <param name="customer_id"></param>
        /// <param name="finished_date"></param>
        public Commission(int id, int list_id, int customer_id, DateTime finished_date)
        {
            this.ID = id;
            this.List_id = list_id;
            this.Customer_id = customer_id;
            this.Order_date = finished_date;

        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="list_id"></param>
        /// <param name="customer_id"></param>
        /// <param name="finished_date"></param>
        public Commission(int list_id, int customer_id, DateTime finished_date)
        {
            this.ID = 0;
            this.List_id = list_id;
            this.Customer_id = customer_id;
            this.Order_date = finished_date;
        }
        /// <summary>
        /// Constructor for data without FK IDs
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customer_nick"></param>
        /// <param name="artifact_title"></param>
        public Commission(int id, string customer_nick, string artifact_title)
        {
            this.ID = id;
            this.Customer_nick = customer_nick;
            this.Artifact_title = artifact_title;
        }

        /// <summary>
        /// To string
        /// </summary>
        /// <returns>string of this object</returns>
        public override string ToString()
        {
            return $"{ID}. {Customer_nick} {Artifact_title}";
        }
    }
}
