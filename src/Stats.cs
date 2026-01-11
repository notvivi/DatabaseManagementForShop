using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbProjekt.src
{
    /// <summary>
    /// Class for defining user statistics
    /// </summary>
    public class Stats
    {
        public int TheAmountSpent { get; set; }
        public decimal MostExpensive { get; set; }
        public decimal LeastExpensive { get; set; }
        public int NumberOfOrders { get; set; }

        /// <summary>
        /// To string
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return
                $"The amount spent: {TheAmountSpent}\n" +
                $"The most expensive artifact: {MostExpensive}\n" +
                $"The least expensive artifact: {LeastExpensive}\n" +
                $"Number of orders: {NumberOfOrders}";
        }
    }
}
