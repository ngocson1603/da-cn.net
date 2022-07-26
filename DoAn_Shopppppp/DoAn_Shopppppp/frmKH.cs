using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DoAn_Shopppppp
{
    public partial class frmKH : Form
    {
        SqlConnection connect = new SqlConnection(Connection.stringSqlConnection);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader rdr;
        public static string tennv = "";
        KetNoi kn = new KetNoi();
        InKhachHang inkh = new InKhachHang();
        private Form currentchildform;
        public frmKH()
        {
            InitializeComponent();
        }

        public void motrangcon(Form trangcon)
        {
            if (currentchildform != null)
            {
                currentchildform.Close();

            }
            currentchildform = trangcon;
            trangcon.TopLevel = false;
            trangcon.FormBorderStyle = FormBorderStyle.None;
            trangcon.Dock = DockStyle.Fill;
            panel3.Controls.Add(trangcon);
            panel3.Tag = trangcon;
            trangcon.BringToFront();
            trangcon.Show();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            motrangcon(new FormChiTietMuaHang());
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormDangNhapQA tt = new FormDangNhapQA();
            tt.ShowDialog();
            this.Close();
        }

        private void frmKH_Load(object sender, EventArgs e)
        {
            try
            {
                connect.Open();
                cmd.CommandText = "select TenKhachHang from KhachHang where Gmail='" + FormDangNhapQA.usernv + "'";
                cmd.Connection = connect;
                rdr = cmd.ExecuteReader();
                bool temp = false;
                while (rdr.Read())
                {
                    label2.Text = rdr.GetString(0);
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

        private void iconButton5_Click(object sender, EventArgs e)
        {
            kn.exitForm();
        }
        Random random = new Random();
        int x = 158, y = 30, a = 1;

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormDoiMatKhau dmk = new FormDoiMatKhau();
            dmk.ShowDialog();
            this.Close();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            motrangcon(new FormThongTinKhach());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                x += a;
                labelKhuyenMai.Location = new Point(x, y);
                if (x >= 600)
                {
                    a = -1;
                    labelKhuyenMai.ForeColor = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
                }
                if (x <= 180)
                {
                    a = 1;
                    labelKhuyenMai.ForeColor = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
                }

            }
            catch (Exception ex)
            { }
        }
    }
}
