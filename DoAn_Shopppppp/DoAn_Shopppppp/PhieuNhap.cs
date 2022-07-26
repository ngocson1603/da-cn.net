using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace DoAn_Shopppppp
{
    public class PhieuNhap
    {
        SqlConnection conn = new SqlConnection(Connection.stringSqlConnection);
        DataSet ds_QLShop = new DataSet();
        public PhieuNhap()
        {
            LoadPhieuNhap();
            LoadNhaPhanPhoi();
        }
        public void LoadPhieuNhap()
        {
            string caulenh = "Select * from PhieuNhap";

            SqlDataAdapter ds_KH = new SqlDataAdapter(caulenh, conn);

            ds_KH.Fill(ds_QLShop, "PhieuNhap");
            DataColumn[] key = new DataColumn[1];

            key[0] = ds_QLShop.Tables["PhieuNhap"].Columns[0];

            ds_QLShop.Tables["PhieuNhap"].PrimaryKey = key;
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
        public DataTable LoadDLNPP()
        {
            return ds_QLShop.Tables["NhaPhanPhoi"];
        }
        public DataTable LoadDLPhieuNhap()
        {
            return ds_QLShop.Tables["PhieuNhap"];
        }

        public bool KiemTraKhoaNgoaiNV(string ma)
        {
            string cmd = "select * from NhanVien where MaNhanVien = '" + ma + "'";
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
        public bool ThemPhieuNhap(string pMaPN, String pMaNV, String pMaNPP, String pNgayNhap)
        {
            if (KiemTraKhoaNgoaiNV(pMaNV) == false)
            {
                MessageBox.Show("Không có nhân viên này");
                return false;
            }
            else
            {
                try
                {
                    DataRow row = ds_QLShop.Tables["PhieuNhap"].NewRow();

                    row["MaPhieuNhap"] = pMaPN;
                    row["MaNhanVien"] = pMaNV;
                    row["MaNhaPhanPhoi"] = pMaNPP;
                    row["NgayNhap"] = pNgayNhap;

                    ds_QLShop.Tables["PhieuNhap"].Rows.Add(row);

                    SqlDataAdapter ds_NV = new SqlDataAdapter("Select * from PhieuNhap", conn);
                    SqlCommandBuilder build = new SqlCommandBuilder(ds_NV);
                    ds_NV.Update(ds_QLShop, "PhieuNhap");
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool KiemTraKhoaNgoai(string ma)
        {
            string cmd = "select * from ChiTietPhieuNhap where MaPhieuNhap = '" + ma + "'";
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
            if (KiemTraKhoaNgoai(pMaPN) == true)
            {
                MessageBox.Show("Phiếu này đã có");
                return false;
            }
            else
            {
                try
                {
                    DataColumn[] key = new DataColumn[1];
                    DataRow row = ds_QLShop.Tables["PhieuNhap"].Rows.Find(pMaPN);
                    if (row != null)
                    {
                        row.Delete();
                    }
                    key[0] = ds_QLShop.Tables["PhieuNhap"].Columns[0];
                    ds_QLShop.Tables["PhieuNhap"].PrimaryKey = key;
                    SqlDataAdapter ds_KH = new SqlDataAdapter("Select * from PhieuNhap", conn);
                    SqlCommandBuilder build = new SqlCommandBuilder(ds_KH);
                    ds_KH.Update(ds_QLShop, "PhieuNhap");

                    return true;

                }
                catch
                {
                    return false;
                }
            }
        }
        public bool SuaPhieuNhap(string pMaPN, String pMaNV, String pMaNPP, String pNgayNhap)
        {
            try
            {
                DataRow row = ds_QLShop.Tables["PhieuNhap"].Rows.Find(pMaPN);

                row["MaPhieuNhap"] = pMaPN;
                row["MaNhanVien"] = pMaNV;
                row["MaNhaPhanPhoi"] = pMaNPP;
                row["NgayNhap"] = pNgayNhap;


                SqlDataAdapter ds_NV = new SqlDataAdapter("Select * from PhieuNhap", conn);
                SqlCommandBuilder build = new SqlCommandBuilder(ds_NV);
                ds_NV.Update(ds_QLShop, "PhieuNhap");
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
