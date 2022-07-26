using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn_Shopppppp
{
    public partial class FormDangNhapQA : Form
    {
        KetNoi kn = new KetNoi();
        public FormDangNhapQA()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public static string usernv = "";
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            int dem = 0;
            usernv = txtTenDangNhap.Text;
            string sql = "select count(*) from Users where TenDangNhap = '" + usernv + "'and Password = '" + txtMatKhau.Text + "' and Quyen = 'ad'";
            string sql1 = "select count(*) from Users where TenDangNhap = '" + usernv + "'and Password = '" + txtMatKhau.Text + "' and Quyen = 'own'";
            string sql2 = "select count(*) from KhachHang where Gmail = '" + usernv + "'and Pass = '" + txtMatKhau.Text + "'";
            int kq = (int)kn.ExcuteScalar(sql);
            int kq1 = (int)kn.ExcuteScalar(sql1);
            int kq2 = (int)kn.ExcuteScalar(sql2);
            if (kq >= 1)
            {
                FormChinh main = new FormChinh();
                main.Show();
                this.Hide();
            }
            else if (kq1 >= 1)
            {
                FormChinh main = new FormChinh();
                main.Show();
                main.kháchHàngToolStripMenuItem.Enabled = false;
                main.quảnLýTàiKhoảnToolStripMenuItem.Enabled = false;
                main.backupToolStripMenuItem.Enabled = false;
                this.Hide();
            }
            else if (kq2 >= 1)
            {
                frmKH main = new frmKH();
                main.Show();
                this.Hide();
            }
            else
            {
                dem++;
                MessageBox.Show("Đăng nhập thất bại,mời bạn nhập lại");
                if (dem == 3)
                {
                    MessageBox.Show("Bạn đã nhập sai 3 lần");
                    Application.Exit();
                }
            }
        }

        private void btnDangKi_Click(object sender, EventArgs e)
        {
            FormDangKy dk = new FormDangKy();
            dk.IsMdiContainer = true;
            dk.Show();
          
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormQuenMatKhau qmk = new FormQuenMatKhau();
            qmk.ShowDialog();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(txtTenDangNhap.Text!=""&&txtMatKhau.Text!="")
            {
                if(checkBox1.Checked==true)
                {
                    string users = txtTenDangNhap.Text;
                    string pwd = txtMatKhau.Text;
                    Properties.Settings.Default.username = users;
                    Properties.Settings.Default.password = pwd;
                    Properties.Settings.Default.Save();
                }    
                else
                {
                    Properties.Settings.Default.Reset();
                }    
            }    
        }

        private void FormDangNhapQA_Load(object sender, EventArgs e)
        {
            txtTenDangNhap.Text = Properties.Settings.Default.username;
            txtMatKhau.Text = Properties.Settings.Default.password;
            if(Properties.Settings.Default.username!="")
            {
                checkBox1.Checked = true;
            }   
        }
    }
}
