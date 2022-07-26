using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn_Shopppppp
{
    public partial class FormChinh : Form
    {
        SqlConnection connect = new SqlConnection(Connection.stringSqlConnection);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader rdr;
        public static string tennv = "";
        KetNoi kn = new KetNoi();
        private Form currentchildform;
        public FormChinh()
        {
            InitializeComponent();
        }
        private void motrangcon(Form trangcon)
        {
            if (currentchildform != null)
            {
                currentchildform.Close();

            }
            currentchildform = trangcon;
            trangcon.TopLevel = false;
            trangcon.FormBorderStyle = FormBorderStyle.None;
            trangcon.Dock = DockStyle.Fill;
            panel1.Controls.Add(trangcon);
            panel1.Tag = trangcon;
            trangcon.BringToFront();
            trangcon.Show();
        }

        private void nhânViênToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            motrangcon(new FromSanPham());
        }

        private void nhàPhânPhốiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            motrangcon(new FormNhaPhanPhoi());
        }

        private void loạiSảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            motrangcon(new FormLoaiSanPham());
        }

        private void hãngSảnXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            motrangcon(new FormHangSanXuat());
        }

        private void phiếuNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            motrangcon(new FormPhieuNhap());
        }

        private void đợtKhuyếnMãiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            motrangcon(new FormKhuyenMai());
        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            motrangcon(new FormNhanVien());
        }

        private void chứcVụToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            motrangcon(new FormChucVu());
        }

        private void kháchHàngToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            motrangcon(new FormKhachHang());
        }

        private void hóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            motrangcon(new FormChiTietHoaDon());
        }

        private void thốngKêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            motrangcon(new FormThongKe());
        }

        private void tìmKiếmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            motrangcon(new FormTimKiem());
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            FormDangNhapQA dn = new FormDangNhapQA();
            dn.Show();
        }

        private void quảnLýTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            motrangcon(new FormTaiKhoan());
        }

        private void FormChinh_Load(object sender, EventArgs e)
        {
            try
            {
                connect.Open();
                cmd.CommandText = "select NhanVien.TenNhanVien from NhanVien,Users where NhanVien.MaNhanVien = Users.MaNhanVien and Users.TenDangNhap='" + FormDangNhapQA.usernv + "'";
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

        private void chiTiếtPhiếuNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            motrangcon(new FormChiTietPhieuNhap());
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            motrangcon(new FormBackup());
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kn.exitForm();
        }
    }
}
