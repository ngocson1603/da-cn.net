using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn_Shopppppp
{
    class DangKy
    {
        SqlConnection conn = new SqlConnection(Connection.stringSqlConnection);
        Modify m = new Modify();

        public bool KiemTraKhoaNgoai(string ma)
        {
            string cmd = "select * from KhachHang where Gmail = '" + ma + "'";
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
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
        public bool kiemtraTonTaiEmail(String gmail)
        {
            if (m.taiKhoan1s("select * from KhachHang where Gmail ='" + gmail + "'").Count != 0)
            {
               
                return true;
            }
            return false;
        }
        public void add(string gm, string pass, string ten, DateTime ngs, string gt, string dt, string sdt, Form f)
        {
            string insert = "SET DATEFORMAT MDY INSERT [dbo].[KhachHang] ([Gmail], [Pass], [TenKhachHang], [Ngaysinh], [GioiTinh], [DiaChi], [SDT]) VALUES('" + gm + "','" + pass + "',N'" + ten + "','" + ngs + "',N'" + gt + "','" + dt + "','" + sdt + "')";
            if (KiemTraKhoaNgoai(gm) == true)
            {
                MessageBox.Show("Tài khoản này đã được đăng ký, vui lòng nhập Gmail khác");
            }
            else if (isEmail(gm) == false)
            {
                MessageBox.Show("Email sai định dạng");
            }
            else if (kiemtraSDT(sdt) == false)
            {
                MessageBox.Show("Số điện thọai sai định dạng");
            }
            else
            {
                try
                {
                    conn.Close();
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(insert, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đăng ký thành công");
                    
                }
                catch
                {
                    MessageBox.Show("Đăng ký thất bại");
                }
            }
        }
    }
}
