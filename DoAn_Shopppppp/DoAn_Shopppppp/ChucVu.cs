using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace DoAn_Shopppppp
{
    public class ChucVu
    {
        SqlConnection conn = new SqlConnection(Connection.stringSqlConnection);
        DataSet ds_QLShop = new DataSet();
        SqlDataAdapter ds_KH = new SqlDataAdapter();
        public ChucVu()
        {
            LoadLoaiSanPham();
        }
        public void LoadLoaiSanPham()
        {
            string caulenh = "Select * from ChucVu";

            ds_KH = new SqlDataAdapter(caulenh, conn);

            ds_KH.Fill(ds_QLShop, "ChucVu");
            DataColumn[] key = new DataColumn[1];

            key[0] = ds_QLShop.Tables["ChucVu"].Columns[0];

            ds_QLShop.Tables["ChucVu"].PrimaryKey = key;
        }
        public DataTable LoadDLLoaiSP()
        {
            return ds_QLShop.Tables["ChucVu"];
        }
        public void ThemLoaiSanPham(string pTenLSP, Form f)
        {
            string insert = "SET IDENTITY_INSERT [ChucVu] OFF INSERT [dbo].[ChucVu] ([TenChucVu]) VALUES(N'" + pTenLSP + "')";
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
            string cmd = "select * from NhanVien where ChucVu = '" + ma + "'";
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
                MessageBox.Show("Có nhân viên đang giữ chức này");
                return false;
            }
            else
            {
                try
                {
                    DataColumn[] key = new DataColumn[1];
                    DataRow row = ds_QLShop.Tables["ChucVu"].Rows.Find(pMaLoaiSanPham);
                    if (row != null)
                    {
                        row.Delete();
                    }
                    key[0] = ds_QLShop.Tables["ChucVu"].Columns[0];
                    ds_QLShop.Tables["ChucVu"].PrimaryKey = key;
                    SqlDataAdapter ds_KH = new SqlDataAdapter("Select * from ChucVu", conn);
                    SqlCommandBuilder build = new SqlCommandBuilder(ds_KH);
                    ds_KH.Update(ds_QLShop, "ChucVu");

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

                DataRow fdtr = ds_QLShop.Tables["ChucVu"].Rows.Find(keys);
                // 2. Xóa dòng khỏi bảng KHOA trên dataset 
                fdtr["TenChucVu"] = pTenLSP;
                // 4. Update vào database 
                SqlCommandBuilder buil = new SqlCommandBuilder(ds_KH);
                // 5. 
                ds_KH.Update(ds_QLShop, "ChucVu");
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
