using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbProjekt.Tables
{
    /// <summary>
    /// Class for list table
    /// </summary>
    public class List : IBaseClass
    {
        private int id;
        private int artifact_id;
        private int quality;
        private int price;
        private string artifact_title;
        private string artifact_description;
        private bool artifact_dangerous;

        public int ID { get => id; set => id = value; }

        public int Artifact_id { get => artifact_id; set => artifact_id = value; }
        public int Quality
        {
            get => quality;
            set 
            {
                if (value > 0 && value < 11)
                {
                    quality = value;
                }

            }
        }
        public string Artifact_title { get => artifact_title; set => artifact_title = value; }
        public string Artifact_description { get => artifact_description; set => artifact_description = value; }
        public bool Artifact_dangerous { get => artifact_dangerous; set => artifact_dangerous = value; }
        public int Price
        {
            get => price;
            set
            {
                if (value > 0 )
                {
                    price = value;
                }

            }
        }
        /// <summary>
        /// Constuctor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="artifact_id"></param>
        /// <param name="quality"></param>
        /// <param name="price"></param>
        public List(int id, int artifact_id, int quality, int price)
        {
            this.ID = id;
            this.Artifact_id = artifact_id;
            this.Quality = quality;
            this.Price = price;
          
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="artifact_id"></param>
        /// <param name="quality"></param>
        /// <param name="price"></param>
        public List(int artifact_id, int quality, int price)
        {
            this.ID = 0;
            this.Artifact_id = artifact_id;
            this.Quality = quality;
            this.Price = price;
        }
        /// <summary>
        /// Constructor for data without FK IDs
        /// </summary>
        /// <param name="id"></param>
        /// <param name="artifact_title"></param>
        /// <param name="artifact_description"></param>
        /// <param name="artifact_dangerous"></param>
        /// <param name="quality"></param>
        /// <param name="price"></param>
        public List(int id, string artifact_title, string artifact_description, bool artifact_dangerous, int quality, int price)
        {
            this.ID= id;
            this.Artifact_title = artifact_title;
            this.Artifact_description = artifact_description;
            this.Artifact_dangerous = artifact_dangerous;
            this.Quality = quality;
            this.Price = price;

            
        }
        /// <summary>
        /// To string
        /// </summary>
        /// <returns>string of this object</returns>
        public override string ToString()
        {
            return $"{ID}. Name:{Artifact_title} Usage:{Artifact_description} Dangerous:{Artifact_dangerous} Quality:{Quality} Price:{Price}";
        }

    }
}
