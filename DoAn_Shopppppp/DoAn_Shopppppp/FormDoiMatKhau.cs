using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace DoAn_Shopppppp
{
    public partial class FormDoiMatKhau : Form
    {
       
        SqlConnection conn = new SqlConnection(Connection.stringSqlConnection);
        SqlDataAdapter da;
        SqlConnection connect = new SqlConnection(Connection.stringSqlConnection);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader rdr;
        public static string tennv = "";
        public FormDoiMatKhau()
        {
            InitializeComponent();

            try
            {
                connect.Open();
                cmd.CommandText = "select Gmail from KhachHang where Gmail='" + FormDangNhapQA.usernv + "'";
                cmd.Connection = connect;
                rdr = cmd.ExecuteReader();
                bool temp = false;
                while (rdr.Read())
                {
                    txtTenDangNhap.Text = rdr.GetString(0);
                    tennv = rdr.GetString(0);
                    temp = true;
                }
                if (temp == false)
                    MessageBox.Show("not found");
                connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            da = new SqlDataAdapter("Select count (*) from KhachHang where Gmail ='"+ txtTenDangNhap.Text+"' and Pass ='" +txtMatKhauCu.Text+"'",conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            errorProvider1.Clear();
            if(dt.Rows[0][0].ToString() == "1")
            {
                if(txtMatKhauMoi.Text == txtXacNhanMatKhau.Text)
                {
                    if (txtMatKhauMoi.Text.Length > 3)
                    {
                        SqlDataAdapter da1 = new SqlDataAdapter("Update KhachHang set Pass = N'" + txtMatKhauMoi.Text + "' where Gmail ='" + txtTenDangNhap.Text + "' and Pass = '" + txtMatKhauCu.Text + "'", conn);
                        DataTable dt1 = new DataTable();
                        da1.Fill(dt1);
                        MessageBox.Show("Đổi mật khẩu thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else

                    {
                        MessageBox.Show("Mật khảu của bản phải lớn hơn 3 kí tự");
                    }
                }
                else
                {
                  
                    errorProvider1.SetError(txtXacNhanMatKhau, "Mật Khẩu nhập lại không đúng!");

                }              
            }
            else
            {
                errorProvider1.SetError(txtTenDangNhap, "Gmail bạn nhập không đúng");
                errorProvider1.SetError(txtMatKhauCu, "Mật Khẩu không đúng!");
            }


        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (txtMatKhauMoi.PasswordChar == '*')
            {
                peye.BringToFront();
                txtMatKhauMoi.PasswordChar = '\0';
            }
        }

        private void peye_Click(object sender, EventArgs e)
        {
            if (txtMatKhauMoi.PasswordChar == '\0')
            {
                pictureBox2.BringToFront();
                txtMatKhauMoi.PasswordChar = '*';
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmKH kh = new frmKH();
            kh.ShowDialog();
            this.Close();
        }
    }
}
