using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Text.RegularExpressions;

namespace DoAn_Shopppppp
{
    public class NhanVien
    {
        SqlConnection conn = new SqlConnection(Connection.stringSqlConnection);
        DataSet ds_QLShop = new DataSet();
        public NhanVien()
        {
            LoadNV();
            LoadChucVu();

        }
        public void LoadNV()
        {
            string caulenh = "Select * from NhanVien";

            SqlDataAdapter ds_KH = new SqlDataAdapter(caulenh, conn);

            ds_KH.Fill(ds_QLShop, "NhanVien");
            DataColumn[] key = new DataColumn[1];

            key[0] = ds_QLShop.Tables["NhanVien"].Columns[0];

            ds_QLShop.Tables["NhanVien"].PrimaryKey = key;
        }
        public void LoadChucVu()
        {
            string caulenh = "Select * from ChucVu";

            SqlDataAdapter ds_KH = new SqlDataAdapter(caulenh, conn);

            ds_KH.Fill(ds_QLShop, "ChucVu");
            DataColumn[] key = new DataColumn[1];

            key[0] = ds_QLShop.Tables["ChucVu"].Columns[0];

            ds_QLShop.Tables["ChucVu"].PrimaryKey = key;
        }

        public DataTable LoadDLNV()
        {
            return ds_QLShop.Tables["NhanVien"];
        }
        public DataTable loadCbbChucVu()
        {
            return ds_QLShop.Tables["ChucVu"];
        }

        public bool kiemtraSDT(String inputSDT)
        {
            try
            {

                return Regex.Match(inputSDT,
                    @"^([\+]?61[-]?|[0])?[1-9][0-9]{8}$").Success;

            }
            catch
            {
                return false;
            }
        }
        public bool ThemNhanVien(string pMaNV, string pTenNV, string pNgaySinh, string pGioiTinh, string pNgayVaoLam, string pChucVu, string pDiaChi, String pSDT)
        {
            object[] keys = new object[1];
            keys[0] = pMaNV;

            DataRow fdtr = ds_QLShop.Tables["NhanVien"].Rows.Find(keys);
            if (fdtr != null)
            {
                MessageBox.Show("trùng mã");
                return false;
            }
            else if (kiemtraSDT(pSDT) == false)
            {
                MessageBox.Show("Vui lòng nhập đúng số điện thoại");
                return false;
            }
            else
            {
                try
                {

                    DataRow row = ds_QLShop.Tables["NhanVien"].NewRow();

                    row["MaNhanVien"] = pMaNV;
                    row["TenNhanVien"] = pTenNV;
                    row["NgaySinh"] = pNgaySinh;
                    row["GioiTinh"] = pGioiTinh;
                    row["NgayVaoLam"] = pNgayVaoLam;
                    row["ChucVu"] = pChucVu;
                    row["DiaChi"] = pDiaChi;
                    row["SoDT"] = pSDT;

                    ds_QLShop.Tables["NhanVien"].Rows.Add(row);

                    SqlDataAdapter ds_NV = new SqlDataAdapter("Select * from NhanVien", conn);
                    SqlCommandBuilder build = new SqlCommandBuilder(ds_NV);
                    ds_NV.Update(ds_QLShop, "NhanVien");
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool XoaNhanVien(string pMaNV)
        {
            try
            {
                DataColumn[] key = new DataColumn[1];
                DataRow row = ds_QLShop.Tables["NhanVien"].Rows.Find(pMaNV);
                if (row != null)
                {
                    row.Delete();
                }
                key[0] = ds_QLShop.Tables["NhanVien"].Columns[0];
                ds_QLShop.Tables["NhanVien"].PrimaryKey = key;
                SqlDataAdapter ds_KH = new SqlDataAdapter("Select * from NhanVien", conn);
                SqlCommandBuilder build = new SqlCommandBuilder(ds_KH);
                ds_KH.Update(ds_QLShop, "NhanVien");

                return true;

            }
            catch
            {
                LoadNV();
                return false;
            }
        }
        public bool SuaNhanVien(string pMaNV, string pTenNV, string pNgaySinh, string pGioiTinh, string pNgayVaoLam, string pChucVu, string pDiaChi, String pSDT)
        {
            try
            {
                DataRow row = ds_QLShop.Tables["NhanVien"].Rows.Find(pMaNV);

                row["MaNhanVien"] = pMaNV;
                row["TenNhanVien"] = pTenNV;
                row["NgaySinh"] = pNgaySinh;
                row["GioiTinh"] = pGioiTinh;
                row["NgayVaoLam"] = pNgayVaoLam;
                row["ChucVu"] = pChucVu;
                row["DiaChi"] = pDiaChi;
                row["SoDT"] = pSDT;



                SqlDataAdapter ds_NV = new SqlDataAdapter("Select * from NhanVien", conn);
                SqlCommandBuilder build = new SqlCommandBuilder(ds_NV);
                ds_NV.Update(ds_QLShop, "NhanVien");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
