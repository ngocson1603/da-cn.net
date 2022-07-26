using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn_Shopppppp
{
    class ChiTietHoaDon
    {
        SqlConnection conn = new SqlConnection(Connection.stringSqlConnection);
        DataSet ds_Shop = new DataSet();
        SqlDataAdapter da;
        SqlCommandBuilder b = new SqlCommandBuilder();
        public ChiTietHoaDon()
        {
            loadTK();
        }
        public void loadTK()
        {
            string cmd = "select * from ChiTietHoaDon";
            da = new SqlDataAdapter(cmd, conn);
            da.Fill(ds_Shop, "ChiTietHoaDon");
            DataColumn[] key = new DataColumn[1];
            key[0] = ds_Shop.Tables["ChiTietHoaDon"].Columns[0];
            ds_Shop.Tables["ChiTietHoaDon"].PrimaryKey = key;
        }

        public void loadlop(DataGridView gv, string tablename)
        {
            gv.DataSource = ds_Shop.Tables[tablename];
        }
        public void loadbyconlop(DataGridView gv, string con)
        {
            string slkl = "select * from ChiTietHoaDon where Gmail = '" + con + "'";
            SqlDataAdapter da = new SqlDataAdapter(slkl, conn);
            da.Fill(ds_Shop, "bycondition");
            loadlop(gv, "bycondition");
        }

        public DataTable loadDLTK()
        {
            return ds_Shop.Tables["ChiTietHoaDon"];
        }

        public void updatetable(SqlCommandBuilder scb, SqlDataAdapter da, string tablename)
        {
            scb = new SqlCommandBuilder(da);
            da.Update(ds_Shop, tablename);
        }

        public bool KiemTraKhoaNgoai(string ma)
        {
            string cmd = "select * from KhachHang where Gmail = '" + ma + "'";
            da = new SqlDataAdapter(cmd, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public void add(string masp, string gmail, int sl, string ns, Form f)
        {
            string insert = "SET DATEFORMAT MDY SET IDENTITY_INSERT [ChiTietHoaDon] OFF INSERT [dbo].[ChiTietHoaDon] ([MaSanPham],[Gmail], [SoLuong], [NgayLapHoaDon]) VALUES('" + masp + "','" + gmail + "'," + sl + ",'" + ns + "')";
            if (KiemTraKhoaNgoai(gmail) == false)
            {
                MessageBox.Show("Tài khoản không tồn tại");
                loadTK();
            }
            else
            {
                try
                {
                    conn.Close();
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(insert, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("thêm thành công");
                    loadTK();
                }
                catch
                {
                    MessageBox.Show("thêm thất bại");
                }
            }
        }

        public void del(int manv, Form f)
        {
            object[] keys = new object[1];
            keys[0] = manv;

            DataRow fdtr = ds_Shop.Tables["ChiTietHoaDon"].Rows.Find(keys);
            if (fdtr == null)
            {
                MessageBox.Show("hóa đơn không tồn tại");
                loadTK();
            }
            else
            {
                fdtr.Delete();
                updatetable(b, da, "ChiTietHoaDon");
                MessageBox.Show("hủy thành công");
                loadTK();
            }
        }

        public bool fix(int mahd, string masp, string gmail, int sl, string ns)
        {
            try
            {
                // 1. Tìm dòng dữ liệu cần xóa (Find chỉ có tác dụng khi có khóa chính)
                object[] keys = new object[1];
                keys[0] = mahd;

                DataRow fdtr = ds_Shop.Tables["ChiTietHoaDon"].Rows.Find(keys);
                // 2. Xóa dòng khỏi bảng KHOA trên dataset 
                fdtr["MaSanPham"] = masp;
                fdtr["Gmail"] = gmail;
                fdtr["SoLuong"] = sl;
                fdtr["NgayLapHoaDon"] = ns;
                // 4. Update vào database 
                SqlCommandBuilder buil = new SqlCommandBuilder(da);
                // 5. 
                da.Update(ds_Shop, "ChiTietHoaDon");
                loadTK();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
