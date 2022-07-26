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
    public partial class FormChiTietHoaDon : Form
    {
        ChiTietHoaDon cthd = new ChiTietHoaDon();
        SanPham sp = new SanPham();
        public FormChiTietHoaDon()
        {
            InitializeComponent();
            load();
        }

        public void load()
        {
            dataGridView1.DataSource = cthd.loadDLTK();
            cbbMaSP.DisplayMember = "TenSanPham";
            cbbMaSP.ValueMember = "MaSanPham";
            cbbMaSP.DataSource = sp.LoadDLSP();

            btnXoa.Enabled = btnSua.Enabled = btnLuu.Enabled = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];

                textBox1.Text = row.Cells[0].Value.ToString();
                cbbMaSP.Text = row.Cells[1].Value.ToString();
                txtGhiChu.Text = row.Cells[2].Value.ToString();
                txtMucGiam.Text = row.Cells[3].Value.ToString();
                txtSoLuong.Text = row.Cells[4].Value.ToString();
                txtGiaSanPham.Text = row.Cells[5].Value.ToString();
                txtTongTienHD.Text = row.Cells[6].Value.ToString();
                dateTimeNgayDat.Text = row.Cells[7].Value.ToString();

                btnXoa.Enabled = btnSua.Enabled = true;
                btnLuu.Enabled = false;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnXoa.Enabled = btnSua.Enabled = false;
            btnLuu.Enabled = true;
            txtGhiChu.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thêm không!", "Thêm", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
            {
                if (txtGhiChu.Text.Length == 0 || txtSoLuong.Text.Length == 0)
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin");
                }
                else
                {
                    string ngayd = dateTimeNgayDat.Value.ToShortDateString();
                    cthd.add(cbbMaSP.SelectedValue.ToString(), txtGhiChu.Text, int.Parse(txtSoLuong.Text), ngayd, this);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn sửa không!", "Sửa", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
            {
                if (txtGhiChu.Text.Length == 0 || txtSoLuong.Text.Length == 0)
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin");
                }
                else
                {
                    string ngayd = dateTimeNgayDat.Value.ToShortDateString();
                    if (cthd.fix(int.Parse(textBox1.Text), cbbMaSP.SelectedValue.ToString(), txtGhiChu.Text, int.Parse(txtSoLuong.Text), ngayd) == true)
                    {
                        MessageBox.Show("Thành công");
                        btnSua.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Lỗi");
                    }
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa không!", "Xóa", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
            {
                cthd.del(int.Parse(textBox1.Text), this);

                btnXoa.Enabled = false;
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Controls.OfType<TextBox>().ToList().ForEach(t => t.Clear());
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
