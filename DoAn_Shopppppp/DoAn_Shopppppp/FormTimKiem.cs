using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DoAn_Shopppppp
{
    public partial class FormTimKiem : Form
    {
        TimKiem tk = new TimKiem();
        SanPham sp = new SanPham();
        NhaPhanPhoi npp = new NhaPhanPhoi();
        KhachHang kh = new KhachHang();
        NhanVien nv = new NhanVien();
        public FormTimKiem()
        {
            InitializeComponent();
            load();
        }

        public void load()
        {
            dataGridView4.DataSource = sp.LoadDLSP();
            dataGridView3.DataSource = npp.LoadDLNPP();
            dataGridView2.DataSource = kh.LoadDLKH();
            dataGridView1.DataSource = nv.LoadDLNV();
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        public void removeall()
        {
            for (int i = dataGridView4.Rows.Count - 1; i >= 0; i--)
            {
                dataGridView4.Rows.RemoveAt(i);
            }
        }

        public void removeall1()
        {
            for (int i = dataGridView3.Rows.Count - 1; i >= 0; i--)
            {
                dataGridView3.Rows.RemoveAt(i);
            }
        }

        public void removeall2()
        {
            for (int i = dataGridView2.Rows.Count - 1; i >= 0; i--)
            {
                dataGridView2.Rows.RemoveAt(i);
            }
        }

        public void removeall3()
        {
            for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
            {
                dataGridView1.Rows.RemoveAt(i);
            }
        }

        private void btnTimKiemSP_Click(object sender, EventArgs e)
        {
            if (raMaSP.Checked == true)
            {
                removeall();
                string con = txtTimKiemSP.Text;
                tk.loadbyconsp(dataGridView4, con);
            }
            else if (raLoaiSP.Checked == true)
            {
                removeall();
                string con = txtTimKiemSP.Text;
                tk.loadbyconloai(dataGridView4, con);
            }
            else if (raTenSP.Checked == true)
            {
                removeall();
                string con = txtTimKiemSP.Text;
                tk.loadbyconten(dataGridView4, con);
            }
            else if (raHangSX.Checked == true)
            {
                removeall();
                int con = int.Parse(txtTimKiemSP.Text);
                tk.loadbyconhang(dataGridView4,con);
            }
            else
            {
                removeall();
                int con = int.Parse(txtTimKiemSP.Text);
                tk.loadbyconnpp(dataGridView4, con);
            }
        }

        private void btnTimKiemNhaPP_Click(object sender, EventArgs e)
        {
            if (raMaNPP.Checked == true)
            {
                removeall1();
                string con = txtTimKiemNPP.Text;
                tk.timtheomanpp(dataGridView3, con);
            }
            else if (raTenNhaPP.Checked == true)
            {
                removeall1();
                string con = txtTimKiemNPP.Text;
                tk.timtheotennpp(dataGridView3, con);
            }
            else
            {
                removeall1();
                string con = txtTimKiemNPP.Text;
                tk.timtheoemailnpp(dataGridView3, con);
            }
        }

        private void btnTimKiemKH_Click(object sender, EventArgs e)
        {
            if (rdMaKhachHang.Checked == true)
            {
                removeall2();
                string con = txtKhachHang.Text;
                tk.timtheoemailkh(dataGridView2, con);
            }
            else
            {
                removeall2();
                string con = txtKhachHang.Text;
                tk.timtheotennpp(dataGridView2, con);
            }
        }

        private void btnTimKiemNV_Click(object sender, EventArgs e)
        {
            if (rdbMaSinhVien.Checked == true)
            {
                removeall3();
                string con = txtTimKiemNhanVien.Text;
                tk.timtheomanv(dataGridView1, con);
            }
            else
            {
                removeall3();
                string con = txtTimKiemNhanVien.Text;
                tk.timtheotennv(dataGridView1, con);
            }
        }
    }
}
