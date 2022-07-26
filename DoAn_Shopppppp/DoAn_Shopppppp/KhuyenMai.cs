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
    class KhuyenMai
    {
        SqlConnection conn = new SqlConnection(Connection.stringSqlConnection);
        DataSet ds_Shop = new DataSet();
        SqlDataAdapter da;
        SqlDataAdapter da1;
        SqlCommandBuilder b = new SqlCommandBuilder();
        public KhuyenMai()
        {
            loadTK();
            loadCTDKM();
        }
        public void loadTK()
        {
            string cmd = "select * from DotKhuyenMai";
            da = new SqlDataAdapter(cmd, conn);
            da.Fill(ds_Shop, "DotKhuyenMai");
            DataColumn[] key = new DataColumn[1];
            key[0] = ds_Shop.Tables["DotKhuyenMai"].Columns[0];
            ds_Shop.Tables["DotKhuyenMai"].PrimaryKey = key;
        }

        public DataTable loadDLTK()
        {
            return ds_Shop.Tables["DotKhuyenMai"];
        }

        public void loadCTDKM()
        {
            string cmd = "select * from ChiTietDotKhuyenMai";
            da1 = new SqlDataAdapter(cmd, conn);
            da1.Fill(ds_Shop, "ChiTietDotKhuyenMai");
            DataColumn[] key = new DataColumn[2];
            key[0] = ds_Shop.Tables["ChiTietDotKhuyenMai"].Columns[0];
            key[1] = ds_Shop.Tables["ChiTietDotKhuyenMai"].Columns[1];
            ds_Shop.Tables["ChiTietDotKhuyenMai"].PrimaryKey = key;
        }

        public DataTable loadDLCTDKM()
        {
            return ds_Shop.Tables["ChiTietDotKhuyenMai"];
        }

        public void updatetable(SqlCommandBuilder scb, SqlDataAdapter da, string tablename)
        {
            scb = new SqlCommandBuilder(da);
            da.Update(ds_Shop, tablename);
        }

        public void adddkm(DateTime ngaybd, DateTime ngaykt, Form f)
        {
            string insert = "SET DATEFORMAT MDY SET IDENTITY_INSERT [DotKhuyenMai] OFF INSERT [dbo].[DotKhuyenMai] ([NgayBD], [NgayKT]) VALUES('" + ngaybd + "','" + ngaykt + "')";
            try
            {
                conn.Close();
                conn.Open();
                SqlCommand cmd = new SqlCommand(insert, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Tạo thành công");
                loadTK();
            }
            catch
            {
                MessageBox.Show("Tạo thất bại");
            }
        }

        public bool KiemTraKhoaNgoai(int ma)
        {
            string cmd = "select * from ChiTietDotKhuyenMai where MaDot = '" + ma + "'";
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public bool deldkm(int madot, Form f)
        {
            try
            {
                object[] keys = new object[1];
                keys[0] = madot;

                DataRow fdtr = ds_Shop.Tables["DotKhuyenMai"].Rows.Find(keys);
                if (KiemTraKhoaNgoai(madot) == true)
                {
                    MessageBox.Show("đợt này đang giảm");
                    loadTK();
                    return false;
                }
                else
                {
                    fdtr.Delete();
                    updatetable(b, da, "DotKhuyenMai");
                    loadTK();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool fixdkm(int madot, DateTime ngaybd, DateTime ngaykt, Form f)
        {
            try
            {
                // 1. Tìm dòng dữ liệu cần xóa (Find chỉ có tác dụng khi có khóa chính)
                object[] keys = new object[1];
                keys[0] = madot;

                DataRow fdtr = ds_Shop.Tables["DotKhuyenMai"].Rows.Find(keys);
                // 2. Xóa dòng khỏi bảng KHOA trên dataset 
                fdtr["NgayBD"] = ngaybd;
                fdtr["NgayKT"] = ngaykt;
                // 4. Update vào database 
                SqlCommandBuilder buil = new SqlCommandBuilder(da);
                // 5. 
                da.Update(ds_Shop, "DotKhuyenMai");
                loadTK();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void add(int madot, string masp, float tlgiam, Form f)
        {
            object[] keys = new object[2];
            keys[0] = madot;
            keys[1] = masp;

            DataRow fdtr = ds_Shop.Tables["ChiTietDotKhuyenMai"].Rows.Find(keys);
            DataRow nr = ds_Shop.Tables["ChiTietDotKhuyenMai"].NewRow();
            nr["MaDot"] = madot;
            nr["MaSanPham"] = masp;
            nr["TiLeGiamGia"] = tlgiam;
            ds_Shop.Tables["ChiTietDotKhuyenMai"].Rows.Add(nr);
            updatetable(b, da1, "ChiTietDotKhuyenMai");
            MessageBox.Show("thêm thành công");
            loadCTDKM();
        }

        public void del(int manv, string masp, Form f)
        {
            object[] keys = new object[2];
            keys[0] = manv;
            keys[1] = masp;

            DataRow fdtr = ds_Shop.Tables["ChiTietDotKhuyenMai"].Rows.Find(keys);
            if (fdtr == null)
            {
                MessageBox.Show("không tồn tại");
                loadCTDKM();
            }
            else
            {
                fdtr.Delete();
                updatetable(b, da1, "ChiTietDotKhuyenMai");
                MessageBox.Show("xóa thành công");
                loadCTDKM();
            }
        }

        public bool fix(int madot, string masp, float tlgiam, Form f)
        {
            try
            {
                // 1. Tìm dòng dữ liệu cần xóa (Find chỉ có tác dụng khi có khóa chính)
                object[] keys = new object[2];
                keys[0] = madot;
                keys[1] = masp;

                DataRow fdtr = ds_Shop.Tables["ChiTietDotKhuyenMai"].Rows.Find(keys);
                // 2. Xóa dòng khỏi bảng KHOA trên dataset 
                fdtr["MaSanPham"] = masp;
                fdtr["TiLeGiamGia"] = tlgiam;
                // 4. Update vào database 
                SqlCommandBuilder buil = new SqlCommandBuilder(da1);
                // 5. 
                da1.Update(ds_Shop, "ChiTietDotKhuyenMai");
                loadCTDKM();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
