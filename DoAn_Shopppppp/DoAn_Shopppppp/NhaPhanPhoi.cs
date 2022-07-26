using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DoAn_Shopppppp
{
    public class NhaPhanPhoi
    {
        SqlConnection conn = new SqlConnection(Connection.stringSqlConnection);
        DataSet ds_QLShop = new DataSet();
        SqlDataAdapter ds_KH = new SqlDataAdapter();
        public NhaPhanPhoi()
        {
            LoadNhaPhanPhoi();
        }
        public void LoadNhaPhanPhoi()
        {
            string caulenh = "Select * from NhaPhanPhoi";

            ds_KH = new SqlDataAdapter(caulenh, conn);

            ds_KH.Fill(ds_QLShop, "NhaPhanPhoi");
            DataColumn[] key = new DataColumn[1];

            key[0] = ds_QLShop.Tables["NhaPhanPhoi"].Columns[0];

            ds_QLShop.Tables["NhaPhanPhoi"].PrimaryKey = key;
        }
        public DataTable LoadDLNPP()
        {
            return ds_QLShop.Tables["NhaPhanPhoi"];
        }

        public void loadNPP(DataGridView gv, string tablename)
        {
            gv.DataSource = ds_QLShop.Tables[tablename];
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
        public bool KiemTraKhoaNgoai(string email)
        {
            string cmd = "select * from NhaPhanPhoi where Email = '" + email + "'";
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
        public void ThemNhaPhanPhoi(String pTenNPP, String pDiaChi, String pSDT, String pEmail, Form F)
        {
            string insert = "SET IDENTITY_INSERT [LoaiSanPham] OFF INSERT [dbo].[NhaPhanPhoi] ([TenNhaPhanPhoi], [DiaChi], [SDT], [Email]) VALUES('" + pTenNPP + "',N'" + pDiaChi + "','" + pSDT + "','" + pEmail + "')";
            if (isEmail(pEmail) == false)
            {
                MessageBox.Show("email sai định dạng");
                LoadNhaPhanPhoi();
            }
            else if (kiemtraSDT(pSDT) == false)
            {
                MessageBox.Show("sdt sai định dạng");
                LoadNhaPhanPhoi();
            }
            else if (KiemTraKhoaNgoai(pEmail) == true)
            {
                MessageBox.Show("Email đã tồn tại");
                LoadNhaPhanPhoi();
            }
            else
            {
                try
                {
                    conn.Close();
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(insert, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("thêm thành công");
                    LoadNhaPhanPhoi();
                }
                catch
                {
                    MessageBox.Show("thêm thất bại");
                }
            }
        }
        public bool XoaNhaPhanPhoi(int pMaNPP)
        {
            try
            {
                DataColumn[] key = new DataColumn[1];
                DataRow row = ds_QLShop.Tables["NhaPhanPhoi"].Rows.Find(pMaNPP);
                if (row != null)
                {
                    row.Delete();
                }
                key[0] = ds_QLShop.Tables["NhaPhanPhoi"].Columns[0];
                ds_QLShop.Tables["NhaPhanPhoi"].PrimaryKey = key;
                SqlDataAdapter ds_KH = new SqlDataAdapter("Select * from NhaPhanPhoi", conn);
                SqlCommandBuilder build = new SqlCommandBuilder(ds_KH);
                ds_KH.Update(ds_QLShop, "NhaPhanPhoi");

                return true;

            }
            catch
            {
                return false;
            }
        }


        public void SuaNhaPhanPhoi(int pMaNPP, String pTenNPP, String pDiaChi, String pSDT, String pEmail, Form F)
        {
            string update = "update NhaPhanPhoi set TenNhaPhanPhoi = '" + pTenNPP + "',DiaChi = N'" + pDiaChi + "',SDT = '" + pSDT + "',Email = '" + pEmail + "' where MaNhaPhanPhoi = '" + pMaNPP + "'";
            try
            {
                conn.Close();
                conn.Open();
                SqlCommand cmd = new SqlCommand(update, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Sửa thành công");
                LoadNhaPhanPhoi();
            }
            catch
            {
                MessageBox.Show("Sửa thất bại");
            }
        }


    }
}
