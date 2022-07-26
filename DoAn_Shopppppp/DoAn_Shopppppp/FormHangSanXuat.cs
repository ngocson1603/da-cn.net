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
    public partial class FormHangSanXuat : Form
    {
        HangSanXuat hsx = new HangSanXuat();
        public FormHangSanXuat()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtTenSP.Enabled = true;
            btnLuu.Enabled = true;
        }

        private void FormHangSanXuat_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = hsx.LoadDLLoaiSP();
            txtLoaiSP.Enabled = txtTenSP.Enabled = false;
            btnSua.Enabled = btnXoa.Enabled = btnLuu.Enabled = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];

                txtLoaiSP.Text = row.Cells[0].Value.ToString();
                txtTenSP.Text = row.Cells[1].Value.ToString();

                btnXoa.Enabled = btnSua.Enabled = true;
                txtTenSP.Enabled = true;
                btnLuu.Enabled = false;
            }
        }

        public bool KiemTraRong()
        {
            bool kq = true;
            if (txtTenSP.Text == string.Empty)
            {
                MessageBox.Show("Bạn Chưa Nhập tên hãng");
                txtTenSP.Focus();
                return false;
            }
            return kq;

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = btnSua.Enabled = false;
            if (MessageBox.Show("Bạn có muốn xóa không!", "Xóa", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
            {
                if (hsx.XoaLoaiSanPham(int.Parse(txtLoaiSP.Text)) == true)
                {
                    MessageBox.Show("Xóa thành công");
                    btnXoa.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Xóa không thành công");
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (KiemTraRong())
            {
                if (MessageBox.Show("Bạn có muốn thêm không!", "Thêm", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
                {
                    hsx.ThemLoaiSanPham(txtTenSP.Text, this);
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
                    if (hsx.SuaLoaiSanPham(txtLoaiSP.Text, tl) == true)
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
