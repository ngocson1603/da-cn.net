using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace DoAn_Shopppppp
{
    public class HangSanXuat
    {
        SqlConnection conn = new SqlConnection(Connection.stringSqlConnection);
        DataSet ds_QLShop = new DataSet();
        SqlDataAdapter ds_KH = new SqlDataAdapter();
        public HangSanXuat()
        {
            LoadLoaiSanPham();
        }
        public void LoadLoaiSanPham()
        {
            string caulenh = "Select * from HangSanXuat";

            ds_KH = new SqlDataAdapter(caulenh, conn);

            ds_KH.Fill(ds_QLShop, "HangSanXuat");
            DataColumn[] key = new DataColumn[1];

            key[0] = ds_QLShop.Tables["HangSanXuat"].Columns[0];

            ds_QLShop.Tables["HangSanXuat"].PrimaryKey = key;
        }
        public DataTable LoadDLLoaiSP()
        {
            return ds_QLShop.Tables["HangSanXuat"];
        }
        public void ThemLoaiSanPham(string pTenLSP, Form f)
        {
            string insert = "SET IDENTITY_INSERT [HangSanXuat] OFF INSERT [dbo].[HangSanXuat] ([TenHangSanXuat]) VALUES('" + pTenLSP + "')";
            try
            {
                conn.Close();
                conn.Open();
                SqlCommand cmd = new SqlCommand(insert, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("thêm thành công");
                LoadLoaiSanPham();
            }
            catch
            {
                MessageBox.Show("thêm thất bại");
            }
        }
        public bool KiemTraKhoaNgoai(int ma)
        {
            string cmd = "select * from SanPham where HangSanXuat = '" + ma + "'";
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public bool XoaLoaiSanPham(int pMaLoaiSanPham)
        {
            if (KiemTraKhoaNgoai(pMaLoaiSanPham) == true)
            {
                MessageBox.Show("Không thể xóa");
                return false;
            }
            else
            {
                try
                {
                    DataColumn[] key = new DataColumn[1];
                    DataRow row = ds_QLShop.Tables["HangSanXuat"].Rows.Find(pMaLoaiSanPham);
                    if (row != null)
                    {
                        row.Delete();
                    }
                    key[0] = ds_QLShop.Tables["HangSanXuat"].Columns[0];
                    ds_QLShop.Tables["HangSanXuat"].PrimaryKey = key;
                    SqlDataAdapter ds_KH = new SqlDataAdapter("Select * from HangSanXuat", conn);
                    SqlCommandBuilder build = new SqlCommandBuilder(ds_KH);
                    ds_KH.Update(ds_QLShop, "HangSanXuat");

                    return true;

                }
                catch
                {
                    return false;
                }
            }
        }
        public bool SuaLoaiSanPham(string pMaLSP, string pTenLSP)
        {
            try
            {
                // 1. Tìm dòng dữ liệu cần xóa (Find chỉ có tác dụng khi có khóa chính)
                object[] keys = new object[1];
                keys[0] = pMaLSP;

                DataRow fdtr = ds_QLShop.Tables["HangSanXuat"].Rows.Find(keys);
                // 2. Xóa dòng khỏi bảng KHOA trên dataset 
                fdtr["TenHangSanXuat"] = pTenLSP;
                // 4. Update vào database 
                SqlCommandBuilder buil = new SqlCommandBuilder(ds_KH);
                // 5. 
                ds_KH.Update(ds_QLShop, "HangSanXuat");
                LoadLoaiSanPham();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
