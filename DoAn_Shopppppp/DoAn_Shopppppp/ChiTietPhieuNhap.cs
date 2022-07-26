using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace DoAn_Shopppppp
{
    class ChiTietPhieuNhap
    {
        SqlConnection conn = new SqlConnection(Connection.stringSqlConnection);
        DataSet ds_Shop = new DataSet();
        SqlDataAdapter da;
        SqlCommandBuilder b = new SqlCommandBuilder();
        public ChiTietPhieuNhap()
        {
            loadTK();
        }
        public void loadTK()
        {
            string cmd = "select * from ChiTietPhieuNhap";
            da = new SqlDataAdapter(cmd, conn);
            da.Fill(ds_Shop, "ChiTietPhieuNhap");
            DataColumn[] key = new DataColumn[2];
            key[0] = ds_Shop.Tables["ChiTietPhieuNhap"].Columns[0];
            key[1] = ds_Shop.Tables["ChiTietPhieuNhap"].Columns[1];
            ds_Shop.Tables["ChiTietPhieuNhap"].PrimaryKey = key;
        }

        public DataTable loadDLTK()
        {
            return ds_Shop.Tables["ChiTietPhieuNhap"];
        }

        public void updatetable(SqlCommandBuilder scb, SqlDataAdapter da, string tablename)
        {
            scb = new SqlCommandBuilder(da);
            da.Update(ds_Shop, tablename);
        }

        public void add(string mapn, string masp, int sl, string tiennhap, Form f)
        {
            object[] keys = new object[2];
            keys[0] = mapn;
            keys[1] = masp;

            DataRow fdtr = ds_Shop.Tables["ChiTietPhieuNhap"].Rows.Find(keys);
            DataRow nr = ds_Shop.Tables["ChiTietPhieuNhap"].NewRow();
            if (fdtr != null)
            {
                MessageBox.Show("trùng mã");
                loadTK();
            }
            else
            {
                nr["MaPhieuNhap"] = mapn;
                nr["MaSanPham"] = masp;
                nr["SoLuong"] = sl;
                nr["TienNhap"] = tiennhap;
                ds_Shop.Tables["ChiTietPhieuNhap"].Rows.Add(nr);
                updatetable(b, da, "ChiTietPhieuNhap");
                MessageBox.Show("thêm thành công");
                loadTK();
            }
        }

        public void del(string mapn, string masp, Form f)
        {
            object[] keys = new object[2];
            keys[0] = mapn;
            keys[1] = masp;

            DataRow fdtr = ds_Shop.Tables["ChiTietPhieuNhap"].Rows.Find(keys);
            if (fdtr == null)
            {
                MessageBox.Show("phiếu không tồn tại");
                loadTK();
            }
            else
            {
                fdtr.Delete();
                updatetable(b, da, "ChiTietPhieuNhap");
                MessageBox.Show("xóa thành công");
                loadTK();
            }
        }

        public bool fix(string mapn, string masp, int sl, string tiennhap)
        {
            try
            {
                // 1. Tìm dòng dữ liệu cần xóa (Find chỉ có tác dụng khi có khóa chính)
                object[] keys = new object[2];
                keys[0] = mapn;
                keys[1] = masp;

                DataRow fdtr = ds_Shop.Tables["ChiTietPhieuNhap"].Rows.Find(keys);
                // 2. Xóa dòng khỏi bảng KHOA trên dataset 
                fdtr["MaSanPham"] = masp;
                fdtr["SoLuong"] = sl;
                fdtr["TienNhap"] = tiennhap;
                // 4. Update vào database 
                SqlCommandBuilder buil = new SqlCommandBuilder(da);
                // 5. 
                da.Update(ds_Shop, "ChiTietPhieuNhap");
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
