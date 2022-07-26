using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace DoAn_Shopppppp
{
    class TaiKhoan
    {
        SqlConnection conn = new SqlConnection(Connection.stringSqlConnection);
        DataSet ds_Shop = new DataSet();
        SqlDataAdapter da;
        SqlCommandBuilder b = new SqlCommandBuilder();
        public TaiKhoan()
        {
            loadTK();
        }
        public void loadTK()
        {
            string cmd = "select * from Users";
            da = new SqlDataAdapter(cmd, conn);
            da.Fill(ds_Shop, "Users");
            DataColumn[] key = new DataColumn[1];
            key[0] = ds_Shop.Tables["Users"].Columns[0];
            ds_Shop.Tables["Users"].PrimaryKey = key;
        }

        public DataTable loadDLTK()
        {
            return ds_Shop.Tables["Users"];
        }

        public void updatetable(SqlCommandBuilder scb, SqlDataAdapter da, string tablename)
        {
            scb = new SqlCommandBuilder(da);
            da.Update(ds_Shop, tablename);
        }
        public bool KiemTraKhoaNgoai(string ma)
        {
            string cmd = "select * from Users where MaNhanVien = '" + ma + "'";
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
        public void add(string tendn, string pass, string Manv, string quen, Form f)
        {
            object[] keys = new object[1];
            keys[0] = tendn;

            DataRow fdtr = ds_Shop.Tables["Users"].Rows.Find(keys);
            DataRow nr = ds_Shop.Tables["Users"].NewRow();
            if (fdtr != null)
            {
                MessageBox.Show("trùng tài khoản");
                loadTK();
            }
            else
            {
                nr["TenDangNhap"] = tendn;
                nr["Password"] = pass;
                nr["MaNhanVien"] = Manv;
                nr["Quyen"] = quen;
                ds_Shop.Tables["Users"].Rows.Add(nr);
                updatetable(b, da, "Users");
                MessageBox.Show("thêm thành công");
                loadTK();
            }
        }

        public void del(string manv, Form f)
        {
            object[] keys = new object[1];
            keys[0] = manv;

            DataRow fdtr = ds_Shop.Tables["Users"].Rows.Find(keys);
            if (fdtr == null)
            {
                MessageBox.Show("tài khoản không tồn tại");
                loadTK();
            }
            else
            {
                fdtr.Delete();
                updatetable(b, da, "Users");
                MessageBox.Show("xóa thành công");
                loadTK();
            }
        }

        public bool fix(string tendn, string pass, string Manv, string quen)
        {
            try
            {
                // 1. Tìm dòng dữ liệu cần xóa (Find chỉ có tác dụng khi có khóa chính)
                object[] keys = new object[1];
                keys[0] = tendn;

                DataRow fdtr = ds_Shop.Tables["Users"].Rows.Find(keys);
                // 2. Xóa dòng khỏi bảng KHOA trên dataset 
                fdtr["Password"] = pass;
                fdtr["MaNhanVien"] = Manv;
                fdtr["Quyen"] = quen;
                // 4. Update vào database 
                SqlCommandBuilder buil = new SqlCommandBuilder(da);
                // 5. 
                da.Update(ds_Shop, "Users");
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
