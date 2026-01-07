using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbProjekt.Tables
{
    /// <summary>
    /// Class for artifact table
    /// </summary>
    public class Artifact : IBaseClass
    {
        private int id;
        private string title;
        private string usage;
        private bool dangerous;

        public int ID { get => id; set => id = value; }

        public string Title { get => title; set => title = value; }
        public string Usage { get => usage; set => usage = value; }
        public bool Dangerous { get => dangerous; set => dangerous = value; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="usage"></param>
        /// <param name="dangerous"></param>
        public Artifact(int id, string title, string usage, bool dangerous)
        {
            this.ID = id;
            this.Title = title;
            this.Usage = usage;
            this.Dangerous = dangerous;

        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="title"></param>
        /// <param name="usage"></param>
        /// <param name="dangerous"></param>
        public Artifact(string title, string usage, bool dangerous)
        {
            this.ID = 0;
            this.Title = title;
            this.Usage = usage;
            this.Dangerous = dangerous;
        }
        /// <summary>
        /// To string
        /// </summary>
        /// <returns>string of this object</returns>
        public override string ToString()
        {
            return $"{ID}. {Title} {Usage} {Dangerous}";
        }
    }
}
