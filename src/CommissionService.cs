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
    /// Class for creating a commision
    /// </summary>
    public class CommissionService
    {
        private readonly CommissionDAO commissionDAO = new CommissionDAO();
        private readonly CustomerDAO customerDAO = new CustomerDAO();
        private readonly ListDAO listDAO = new ListDAO();

        public void CreateCommission(int listId, int customerId)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();
            SqlTransaction tran = conn.BeginTransaction();

            try
            {
                if (!customerDAO.Exists(customerId, conn, tran))
                    throw new Exception("Customer does not exist");

                if (!listDAO.Exists(listId, conn, tran))
                    throw new Exception("List item does not exist");

                Commission c = new Commission(listId, customerId, DateTime.Now);
                commissionDAO.Save(c, conn, tran);

                tran.Commit();
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }
    }
}
