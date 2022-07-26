using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DoAn_Shopppppp
{
    public partial class FormChiTietMuaHang : Form
    {
        ChiTietHoaDon cthd = new ChiTietHoaDon();
        public FormChiTietMuaHang()
        {
            InitializeComponent();
            load();
        }

        public void load()
        {
            string con = FormDangNhapQA.usernv;
            cthd.loadbyconlop(dataGridView1, con);
            iconButton3.Enabled = false;
        }
        public void removeall()
        {
            for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
            {
                dataGridView1.Rows.RemoveAt(i);
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            FormSanPhamKhachXem tt = new FormSanPhamKhachXem();
            tt.ShowDialog();
            this.Close();
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            removeall();
            if (MessageBox.Show("Bạn có muốn hủy đơn không!", "Hủy", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
            {
                cthd.del(int.Parse(textBox1.Text), this);
                load();
            }
            else
            {
                load();
            }    
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];

                textBox1.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
                txtGhiChu.Text = row.Cells[2].Value.ToString();
                txtMucGiam.Text = row.Cells[3].Value.ToString();
                txtSoLuong.Text = row.Cells[4].Value.ToString();
                txtGiaSanPham.Text = row.Cells[5].Value.ToString();
                txtTongTienHD.Text = row.Cells[6].Value.ToString();
                dateTimePicker1.Text = row.Cells[7].Value.ToString();
                iconButton3.Enabled = true;
            }
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            if (txtGhiChu.Text.Length == 0 || txtSoLuong.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin");
            }
            else
            {
                removeall();
                string ngayd = dateTimePicker1.Value.ToShortDateString();
                cthd.add(textBox2.Text, txtGhiChu.Text, int.Parse(txtSoLuong.Text), ngayd, this);
                this.Close();
                load();
            }
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}
