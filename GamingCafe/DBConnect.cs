using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace GamingCafe
{
    internal class DBConnect
    {
        private static string strCon = @"Server=localhost;Database=QuanLyCafe;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";
        public static SqlConnection getConnection()
        {
            return new SqlConnection(strCon);
        }
    }
}