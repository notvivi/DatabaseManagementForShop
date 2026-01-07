using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbProjekt.Tables
{
    /// <summary>
    /// Class for customer table
    /// </summary>
    public class Customer : IBaseClass
    {
        private int id;
        private string nickname;
        private int race_id;
        private string race;

        public int ID { get => id; set => id = value; }

        public string Nickname { get => nickname; set => nickname = value; }
        public int Race_id { get => race_id; set => race_id = value; }
        public string Race { get => race; set => race = value; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nickname"></param>
        /// <param name="race_id"></param>
        public Customer(int id, string nickname, int race_id)
        {
            this.ID = id;
            this.Nickname = nickname;
            this.Race_id = race_id;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nickname"></param>
        /// <param name="race_id"></param>
        public Customer(string nickname, int race_id)
        {
            this.ID = 0;
            this.Nickname = nickname;
            this.Race_id = race_id;
        }
        /// <summary>
        /// Constructor for data without FK IDs
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nickname"></param>
        /// <param name="race"></param>
        public Customer(int id, string nickname, string race)
        {
            this.ID = id;
            this.Nickname = nickname;
            this.Race = race;
        }
        /// <summary>
        /// To string
        /// </summary>
        /// <returns>string of this object</returns>
        public override string ToString()
        {
            return $"{ID}. {Nickname} {Race}";
        }
    }
}
