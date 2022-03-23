using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using XQLiteMgm.Models.UnitOfWork.Interface;

namespace XQLiteMgm.Models.UnitOfWork
{
    public class UnitOfWorkFactory
    {
        public static IUnitOfWork Create()
        {
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            var connection = new SqlConnection(connString);

            connection.Open();

            return new SQLUnitOfWork(connection, true);
        }
    }
}