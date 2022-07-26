using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DoAn_Shopppppp
{
    public partial class FormTaiKhoan : Form
    {
        NhanVien nv = new NhanVien();
        TaiKhoan tk = new TaiKhoan();
        public FormTaiKhoan()
        {
            InitializeComponent();
            load();
        }

        public void load()
        {
            dataGridView1.DataSource = tk.loadDLTK();
            //load cbb mã nhà phân phối
            cbbPhieuNhap.DisplayMember = "MaNhanVien";
            cbbPhieuNhap.ValueMember = "MaNhanVien";
            cbbPhieuNhap.DataSource = nv.LoadDLNV();

            string[] GioiTinh = { "ad", "own" };
            foreach (string x in GioiTinh)
            {
                comboBox1.Items.Add(x);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtSoLuong.Text = txtTongTien.Text = "";
            txtSoLuong.Focus();

            button4.Enabled = true;
            button2.Enabled = button3.Enabled = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];

                cbbPhieuNhap.Text = row.Cells[2].Value.ToString();
                txtSoLuong.Text = row.Cells[0].Value.ToString();
                txtTongTien.Text = row.Cells[1].Value.ToString();
                comboBox1.Text = row.Cells[3].Value.ToString();

                button2.Enabled = button3.Enabled = true;
                button4.Enabled = false;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Controls.OfType<TextBox>().ToList().ForEach(t => t.Clear());
            txtSoLuong.Focus();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtSoLuong.Text.Length == 0 || txtTongTien.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
            }
            else
            {
                string manv = cbbPhieuNhap.SelectedValue.ToString();
                tk.add(txtSoLuong.Text, txtTongTien.Text, manv, comboBox1.Text, this);
                dataGridView1.Refresh();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtSoLuong.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng click vào bảng");
            }
            else
            {
                tk.del(txtSoLuong.Text, this);

                button2.Enabled = false;
                dataGridView1.Refresh();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string manv = cbbPhieuNhap.SelectedValue.ToString();
            if (tk.fix(txtSoLuong.Text, txtTongTien.Text, manv, comboBox1.Text) == true)
            {
                MessageBox.Show("Thanh Cong");
                button3.Enabled = false;
                dataGridView1.Refresh();
            }
            else
            {
                MessageBox.Show("That Bai");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbbPhieuNhap_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
