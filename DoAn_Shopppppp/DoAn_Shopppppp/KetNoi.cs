using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DoAn_Shopppppp
{
    class KetNoi
    {
        SqlConnection conn = new SqlConnection(Connection.stringSqlConnection);

        public KetNoi()
        {
            SqlConnection conn = new SqlConnection(Connection.stringSqlConnection); ;
        }

        public SqlConnection connection()
        {
            SqlConnection conn = new SqlConnection(Connection.stringSqlConnection);
            return conn;
        }

        public SqlConnection getConnection
        {
            get
            {
                return conn;
            }
        }

        public void Mo()
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
        }
        public void Dong()
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }

        }
        public object ExcuteScalar(string sql)
        {
            SqlCommand comm = new SqlCommand(sql, conn);
            Mo();
            object ketqua = comm.ExecuteScalar();
            Dong();
            return ketqua;

        }

        public void exitForm()
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn thoát?", "Exit", MessageBoxButtons.YesNo);
            if (dr == System.Windows.Forms.DialogResult.Yes)
                Application.Exit();
        }

    }
}
