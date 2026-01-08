using DbProjekt.src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbProjekt.Tables
{
    /// <summary>
    /// Class for race table
    /// </summary>
    public class Race : IBaseClass
    {
        private int id;
        private string title;

        public int ID { get => id; set => id = value; }

        public string Title { get => title; set => title = value; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        public Race(int id, string title)
        {
            this.ID = id;
            this.title = title;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="title"></param>
        public Race(string title)
        {
            this.ID = 0;
            this.Title = title;
        }
        /// <summary>
        /// To string
        /// </summary>
        /// <returns>string of this object</returns>
        public override string ToString()
        {
            return $" {ID}. {Title}";
        }
    }
}
