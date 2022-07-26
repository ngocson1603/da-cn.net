using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DoAn_Shopppppp
{

    public class LoaiSanPham
    {
        SqlConnection conn = new SqlConnection(Connection.stringSqlConnection);
        DataSet ds_QLShop = new DataSet();
        SqlDataAdapter ds_KH = new SqlDataAdapter();
        public LoaiSanPham()
        {
            LoadLoaiSanPham();
        }
        public void LoadLoaiSanPham()
        {
            string caulenh = "Select * from LoaiSanPham";

            ds_KH = new SqlDataAdapter(caulenh, conn);

            ds_KH.Fill(ds_QLShop, "LoaiSanPham");
            DataColumn[] key = new DataColumn[1];

            key[0] = ds_QLShop.Tables["LoaiSanPham"].Columns[0];

            ds_QLShop.Tables["LoaiSanPham"].PrimaryKey = key;
        }
        public DataTable LoadDLLoaiSP()
        {
            return ds_QLShop.Tables["LoaiSanPham"];
        }
        public void ThemLoaiSanPham(string pTenLSP, Form f)
        {
            string insert = "SET IDENTITY_INSERT [LoaiSanPham] OFF INSERT [dbo].[LoaiSanPham] ([TenLoaiSanPham]) VALUES(N'" + pTenLSP + "')";
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
            string cmd = "select * from SanPham where LoaiSanPham = '" + ma + "'";
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
                    DataRow row = ds_QLShop.Tables["LoaiSanPham"].Rows.Find(pMaLoaiSanPham);
                    if (row != null)
                    {
                        row.Delete();
                    }
                    key[0] = ds_QLShop.Tables["LoaiSanPham"].Columns[0];
                    ds_QLShop.Tables["LoaiSanPham"].PrimaryKey = key;
                    SqlDataAdapter ds_KH = new SqlDataAdapter("Select * from LoaiSanPham", conn);
                    SqlCommandBuilder build = new SqlCommandBuilder(ds_KH);
                    ds_KH.Update(ds_QLShop, "LoaiSanPham");

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

                DataRow fdtr = ds_QLShop.Tables["LoaiSanPham"].Rows.Find(keys);
                // 2. Xóa dòng khỏi bảng KHOA trên dataset 
                fdtr["TenLoaiSanPham"] = pTenLSP;
                // 4. Update vào database 
                SqlCommandBuilder buil = new SqlCommandBuilder(ds_KH);
                // 5. 
                ds_KH.Update(ds_QLShop, "LoaiSanPham");
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
