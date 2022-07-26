using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace DoAn_Shopppppp
{
    public class Connection
    {
        public static string stringSqlConnection = @"Data Source=ADMIN\SQLEXPRESS;Initial Catalog=QL_SHOP;User ID=sa ; Password=123";
        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(stringSqlConnection);
        }

    }
}
