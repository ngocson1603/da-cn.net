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
    public partial class FormChiTietPhieuNhap : Form
    {
        PhieuNhap pn = new PhieuNhap();
        SanPham sp = new SanPham();
        ChiTietPhieuNhap ctpn = new ChiTietPhieuNhap();
        public FormChiTietPhieuNhap()
        {
            InitializeComponent();
            load();
        }

        public void load()
        {
            dataGridView1.DataSource = ctpn.loadDLTK();
            //load cbb mã nhà phân phối
            cbbPhieuNhap.DisplayMember = "MaPhieuNhap";
            cbbPhieuNhap.ValueMember = "MaPhieuNhap";
            cbbPhieuNhap.DataSource = pn.LoadDLPhieuNhap();

            cbbMaSanPham.DisplayMember = "MaSanPham";
            cbbMaSanPham.ValueMember = "MaSanPham";
            cbbMaSanPham.DataSource = sp.LoadDLSP();

            txtSoLuong.Enabled = txtTongTien.Enabled = false;
            button2.Enabled = button3.Enabled = button4.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Controls.OfType<TextBox>().ToList().ForEach(t => t.Clear());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa không!", "Xóa", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
            {
                ctpn.del(cbbPhieuNhap.Text, cbbMaSanPham.Text, this);

                button2.Enabled = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thêm không!", "Thêm", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
            {
                if (txtSoLuong.Text.Length == 0 || txtTongTien.Text.Length == 0)
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin");
                }
                else
                {
                    ctpn.add(cbbPhieuNhap.Text, cbbMaSanPham.Text, int.Parse(txtSoLuong.Text), txtTongTien.Text, this);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn sửa không!", "Sửa", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
            {
                if (ctpn.fix(cbbPhieuNhap.Text, cbbMaSanPham.Text, int.Parse(txtSoLuong.Text), txtTongTien.Text) == true)
                {
                    MessageBox.Show("Sửa thành công");
                    button3.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Lỗi");
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];

                cbbPhieuNhap.Text = row.Cells[0].Value.ToString();
                cbbMaSanPham.Text = row.Cells[1].Value.ToString();
                txtSoLuong.Text = row.Cells[2].Value.ToString();
                txtTongTien.Text = row.Cells[3].Value.ToString();

                button2.Enabled = button3.Enabled = true;
                button4.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtSoLuong.Enabled = txtTongTien.Enabled = true;
            button4.Enabled = true;
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}
