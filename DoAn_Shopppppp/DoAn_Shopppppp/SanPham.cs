using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows;

namespace DoAn_Shopppppp
{

    public class SanPham
    {
        SqlConnection conn = new SqlConnection(Connection.stringSqlConnection);
        DataSet ds_QLShop = new DataSet();
        KetNoi kn = new KetNoi();

        public SanPham()
        {
            LoadSanPham();
            LoadNhaPhanPhoi();
            LoadLoaiSanPham();
            LoadHangSanXuat();
        }
        public void LoadSanPham()
        {
            string caulenh = "Select * from SanPham";

            SqlDataAdapter ds_KH = new SqlDataAdapter(caulenh, conn);

            ds_KH.Fill(ds_QLShop, "SanPham");
            DataColumn[] key = new DataColumn[1];

            key[0] = ds_QLShop.Tables["SanPham"].Columns[0];

            ds_QLShop.Tables["SanPham"].PrimaryKey = key;
        }

        public void LoadLoaiSanPham()
        {
            string caulenh = "Select * from LoaiSanPham";

            SqlDataAdapter ds_KH = new SqlDataAdapter(caulenh, conn);

            ds_KH.Fill(ds_QLShop, "LoaiSanPham");
            DataColumn[] key = new DataColumn[1];

            key[0] = ds_QLShop.Tables["LoaiSanPham"].Columns[0];

            ds_QLShop.Tables["LoaiSanPham"].PrimaryKey = key;
        }
        public void LoadNhaPhanPhoi()
        {
            string caulenh = "Select * from NhaPhanPhoi";

            SqlDataAdapter ds_KH = new SqlDataAdapter(caulenh, conn);

            ds_KH.Fill(ds_QLShop, "NhaPhanPhoi");
            DataColumn[] key = new DataColumn[1];

            key[0] = ds_QLShop.Tables["NhaPhanPhoi"].Columns[0];

            ds_QLShop.Tables["NhaPhanPhoi"].PrimaryKey = key;
        }
        public void LoadHangSanXuat()
        {
            string caulenh = "Select * from HangSanXuat";

            SqlDataAdapter ds_KH = new SqlDataAdapter(caulenh, conn);

            ds_KH.Fill(ds_QLShop, "HangSanXuat");
            DataColumn[] key = new DataColumn[1];

            key[0] = ds_QLShop.Tables["HangSanXuat"].Columns[0];

            ds_QLShop.Tables["HangSanXuat"].PrimaryKey = key;
        }
       
        public DataTable LoadDLSP()
        {
            return ds_QLShop.Tables["SanPham"];
        }
        public DataTable LoadDLNPP()
        {
            return ds_QLShop.Tables["NhaPhanPhoi"];
        }
        public DataTable LoadDLLoaiSP()
        {
            return ds_QLShop.Tables["LoaiSanPham"];
        }
        public DataTable LoadDLHangSanXuat()
        {
            return ds_QLShop.Tables["HangSanXuat"];
        }
        public bool KiemTraKhoaNgoai(string ma)
        {
            string cmd = "select * from SanPham where MaSanPham = '" + ma + "'";
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
        public bool KiemTraTen(string ma)
        {
            string cmd = "select * from SanPham where TenSanPham = '" + ma + "'";
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
        public bool ThemSanPham(String pMaSP, String pMaNPP, String pTenSP, String pLoaiSP, String pHangSX, double pGiaBan, String pSoLuong, MemoryStream pHinhAnh)
        {
            SqlCommand command = new SqlCommand("INSERT INTO SanPham ([MaSanPham], [MaNhaPhanPhoi],[TenSanPham], [LoaiSanPham], [HangSanXuat], [GiaBan], [TonKho], [Image])" + "VALUES (@msp,@mnpp,@tsp,@lsp,@hsx,@gia,@sl,@hinh)", kn.getConnection);
            command.Parameters.Add("@msp", SqlDbType.NVarChar).Value = pMaSP;
            command.Parameters.Add("@mnpp", SqlDbType.Int).Value = pMaNPP;
            command.Parameters.Add("@tsp", SqlDbType.NVarChar).Value = pTenSP;
            command.Parameters.Add("@lsp", SqlDbType.Int).Value = pLoaiSP;
            command.Parameters.Add("@hsx", SqlDbType.Int).Value = pHangSX;
            command.Parameters.Add("@gia", SqlDbType.Float).Value = pGiaBan;
            command.Parameters.Add("@sl", SqlDbType.Int).Value = pSoLuong;
            command.Parameters.Add("@hinh", SqlDbType.Image).Value = pHinhAnh.ToArray();
            kn.Mo();
            //()
            if ((command.ExecuteNonQuery() == -1))
            {
                kn.Dong();
                return false;
            }
            else
            {
                LoadSanPham();
                kn.Dong();
                return true;

            }
        }

        public bool SuaSanPham(string pMaSP, string pMaNPP, string pTenSP, string pLoaiSP, string pHangSX, double pGiaBan, string pSoLuong, MemoryStream pHinhAnh)
        {
            SqlCommand command = new SqlCommand("Update SanPham set MaNhaPhanPhoi=@mnpp,TenSanPham=@tsp,LoaiSanPham=@lsp,HangSanXuat=@hsx,GiaBan=@gia,TonKho=@sl,Image=@hinh where MaSanPham=@msp", kn.getConnection);
            command.Parameters.Add("@msp", SqlDbType.NVarChar).Value = pMaSP;
            command.Parameters.Add("@mnpp", SqlDbType.Int).Value = pMaNPP;
            command.Parameters.Add("@tsp", SqlDbType.NVarChar).Value = pTenSP;
            command.Parameters.Add("@lsp", SqlDbType.Int).Value = pLoaiSP;
            command.Parameters.Add("@hsx", SqlDbType.Int).Value = pHangSX;
            command.Parameters.Add("@gia", SqlDbType.Float).Value = pGiaBan;
            command.Parameters.Add("@sl", SqlDbType.Int).Value = pSoLuong;
            command.Parameters.Add("@hinh", SqlDbType.Image).Value = pHinhAnh.ToArray();
            kn.Mo();
            //()
            if ((command.ExecuteNonQuery() == -1))
            {
                kn.Dong();
                return false;
            }
            else
            {
                LoadSanPham();
                kn.Dong();
                return true;

            }
        }

        public bool KiemTraXoa(string ma)
        {
            string cmd = "select * from ChiTietDotKhuyenMai where MaSanPham = '" + ma + "'";
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public bool XoaPhieuNhap(string pMaPN)
        {
            if (KiemTraXoa(pMaPN) == true)
            {
                MessageBox.Show("Sản phẩm này đang giảm giá");
                return false;
            }
            else
            {
                try
                {
                    DataColumn[] key = new DataColumn[1];
                    DataRow row = ds_QLShop.Tables["SanPham"].Rows.Find(pMaPN);
                    if (row != null)
                    {
                        row.Delete();
                    }
                    key[0] = ds_QLShop.Tables["SanPham"].Columns[0];
                    ds_QLShop.Tables["SanPham"].PrimaryKey = key;
                    SqlDataAdapter ds_KH = new SqlDataAdapter("Select * from SanPham", conn);
                    SqlCommandBuilder build = new SqlCommandBuilder(ds_KH);
                    ds_KH.Update(ds_QLShop, "SanPham");

                    return true;

                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
