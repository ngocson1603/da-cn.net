using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Text.RegularExpressions;

namespace DoAn_Shopppppp
{
    public class KhachHang
    {
        SqlConnection conn = new SqlConnection(Connection.stringSqlConnection);
        DataSet ds_QLShop = new DataSet();
        SqlDataAdapter da;
        SqlCommandBuilder b = new SqlCommandBuilder();
        public KhachHang()
        {
            LoadKH();
        }
        public void LoadKH()
        {
            string caulenh = "Select * from KhachHang";

            da = new SqlDataAdapter(caulenh, conn);

            da.Fill(ds_QLShop, "KhachHang");
            DataColumn[] key = new DataColumn[1];

            key[0] = ds_QLShop.Tables["KhachHang"].Columns[0];

            ds_QLShop.Tables["KhachHang"].PrimaryKey = key;
        }
        public DataTable LoadDLKH()
        {
            return ds_QLShop.Tables["KhachHang"];
        }
        public void updatetable(SqlCommandBuilder scb, SqlDataAdapter da, string tablename)
        {
            scb = new SqlCommandBuilder(da);
            da.Update(ds_QLShop, tablename);
        }

        public bool isEmail(string inputEmail)
        {
            inputEmail = inputEmail ?? string.Empty;
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
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

        public void ThemKH(string gmail, string pass, string tenkh, DateTime ngsinh, string gioitinh, string dchi, string sdt)
        {
            object[] keys = new object[1];
            keys[0] = gmail;

            DataRow fdtr = ds_QLShop.Tables["KhachHang"].Rows.Find(keys);
            DataRow nr = ds_QLShop.Tables["KhachHang"].NewRow();
            if (fdtr != null)
            {
                MessageBox.Show("trùng mã");
                LoadKH();
            }
            else if (isEmail(gmail) == false)
            {
                MessageBox.Show("Email sai định dạng");
            }
            else if (kiemtraSDT(sdt) == false)
            {
                MessageBox.Show("Vui lòng nhập đúng số điện thoại");
            }
            else
            {
                nr["Gmail"] = gmail;
                nr["Pass"] = pass;
                nr["TenKhachHang"] = tenkh;
                nr["Ngaysinh"] = ngsinh;
                nr["GioiTinh"] = gioitinh;
                nr["DiaChi"] = dchi;
                nr["SDT"] = sdt;
                ds_QLShop.Tables["KhachHang"].Rows.Add(nr);
                updatetable(b, da, "KhachHang");
                MessageBox.Show("thêm thành công");
                LoadKH();
            }
        }

        public bool KiemTraKhoaNgoaiNV(string ma)
        {
            string cmd = "select * from ChiTietHoaDon where Gmail = '" + ma + "'";
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public void del(string manv)
        {
            object[] keys = new object[1];
            keys[0] = manv;

            DataRow fdtr = ds_QLShop.Tables["KhachHang"].Rows.Find(keys);
            if (fdtr == null)
            {
                MessageBox.Show("tài khoản không tồn tại");
                LoadKH();
            }
            else if (KiemTraKhoaNgoaiNV(manv) == true)
            {
                MessageBox.Show("không thể xóa");
                LoadKH();
            }
            else
            {
                fdtr.Delete();
                updatetable(b, da, "KhachHang");
                MessageBox.Show("xóa thành công");
                LoadKH();
            }
        }
        public bool SuaKH(string gmail, string pass, string tenkh, DateTime ngsinh, string gioitinh, string dchi, string sdt)
        {
            try
            {
                // 1. Tìm dòng dữ liệu cần xóa (Find chỉ có tác dụng khi có khóa chính)
                object[] keys = new object[1];
                keys[0] = gmail;

                DataRow fdtr = ds_QLShop.Tables["KhachHang"].Rows.Find(keys);
                // 2. Xóa dòng khỏi bảng KHOA trên dataset 
                fdtr["Pass"] = pass;
                fdtr["TenKhachHang"] = tenkh;
                fdtr["Ngaysinh"] = ngsinh;
                fdtr["GioiTinh"] = gioitinh;
                fdtr["DiaChi"] = dchi;
                fdtr["SDT"] = sdt;
                // 4. Update vào database 
                SqlCommandBuilder buil = new SqlCommandBuilder(da);
                // 5. 
                da.Update(ds_QLShop, "KhachHang");
                LoadKH();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}