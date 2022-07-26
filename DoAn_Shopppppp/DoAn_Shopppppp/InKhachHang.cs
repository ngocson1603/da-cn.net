using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn_Shopppppp
{
    class InKhachHang
    {
        SqlConnection conn = new SqlConnection(Connection.stringSqlConnection);
        DataSet ds_QLSV = new DataSet();
        SqlDataAdapter da;

        public InKhachHang()
        {

        }

        public void LoadDLSinhVienTheoMaLop(string maLop)
        {
            string cmd = "select * from KhachHang where Gmail = '" + maLop + "'";
            da = new SqlDataAdapter(cmd, conn);
            ds_QLSV.Clear();
            da.Fill(ds_QLSV, "KhachHang");

            DataColumn[] pKey = new DataColumn[1];
            pKey[0] = ds_QLSV.Tables["KhachHang"].Columns[0];
            ds_QLSV.Tables["KhachHang"].PrimaryKey = pKey;
        }


        public DataTable GetDLSinhVien()
        {
            return ds_QLSV.Tables["KhachHang"];
        }

    }
}
