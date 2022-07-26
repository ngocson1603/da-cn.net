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
    public partial class FormLoaiSanPham : Form
    {
        LoaiSanPham lsp = new LoaiSanPham();
        public FormLoaiSanPham()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        public bool KiemTraRong()
        {
            bool kq = true;
            if (txtTenSP.Text == string.Empty)
            {
                MessageBox.Show("Bạn Chưa Nhập tên sản Phẩm");
                txtTenSP.Focus();
                return false;
            }
            return kq;
        }
        private void FormLoaiSanPham_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = lsp.LoadDLLoaiSP();
            txtLoaiSP.Enabled = txtTenSP.Enabled = false;
            btnSua.Enabled = btnXoa.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtTenSP.Enabled = true;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnLuu.Enabled = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];

                txtLoaiSP.Text = row.Cells[0].Value.ToString().Trim();
                txtTenSP.Text = row.Cells[1].Value.ToString().Trim();

                btnXoa.Enabled = btnSua.Enabled = true;
                txtTenSP.Enabled = true;
                btnLuu.Enabled = false;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = btnSua.Enabled = false;
            if (MessageBox.Show("Bạn có muốn xóa không!", "Xóa", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
            {
                if (lsp.XoaLoaiSanPham(int.Parse(txtLoaiSP.Text)) == true)
                {
                    MessageBox.Show("Xóa loai sản phẩm thành công");

                    btnXoa.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Xóa loại sản phẩm không thành công");
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (KiemTraRong())
            {
                if (MessageBox.Show("Bạn có muốn thêm không!", "Thêm", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
                {
                    lsp.ThemLoaiSanPham(txtTenSP.Text, this);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn sửa không!", "Sửa", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
            {
                if (txtTenSP.Text.Length == 0)
                {
                    MessageBox.Show("Vui lòng nhập tên loại");
                }
                else
                {
                    string tl = txtTenSP.Text;
                    if (lsp.SuaLoaiSanPham(txtLoaiSP.Text, tl) == true)
                    {
                        MessageBox.Show("Sửa thành công");

                        btnSua.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Sửa thất bại");
                    }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Controls.OfType<TextBox>().ToList().ForEach(t => t.Clear());
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
