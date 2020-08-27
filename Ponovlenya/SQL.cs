using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Ponovlenya
{
    class SQL
    {
        private static readonly SQL Instance = new SQL();
        private readonly SqlConnection Connection;
        private SQL()
        {
            string ConnectionString = @"Data Source=.\SQLSERVER;Initial Catalog=Mail;Integrated Security=True;";
            Connection = new SqlConnection(ConnectionString);
            Connection.Open();
        }
        public static SqlConnection GetInstance()
        {
            return Instance.Connection;
        }

    }
}
