using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace DoAn_Shopppppp
{
    public class Modify
    {
     
        public Modify()
            {

            }
        SqlDataReader dar;
        SqlCommand cmd;
        public List<TaiKhoan1> taiKhoan1s(string query)
        {
            List<TaiKhoan1> taiKhoan1s = new List<TaiKhoan1>();
            using (SqlConnection sqlConnection = Connection.GetSqlConnection())
            {
                sqlConnection.Open();
                cmd = new SqlCommand(query, sqlConnection);
                dar = cmd.ExecuteReader();
                while(dar.Read())
                {
                    taiKhoan1s.Add(new TaiKhoan1(dar.GetString(0), dar.GetString(1)));

                }    




                sqlConnection.Close();


            }


            return taiKhoan1s;
        }
    }
}
