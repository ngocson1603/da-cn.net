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
    public partial class FormKhachHang : Form
    {
        KhachHang shop = new KhachHang();
        public FormKhachHang()
        {
            InitializeComponent();
        }

        public bool KiemTraRong()
        {
            bool kq = true;

            if (txtMaKhachHang.Text == string.Empty)
            {
                MessageBox.Show("Bạn Chưa Nhập Mã Khách Hàng");
                txtMaKhachHang.Focus();
                return false;
            }
            else
                if (txtTenKhachHang.Text == string.Empty)
            {
                MessageBox.Show("Bạn Chưa Nhập tên Khách Hàng");
                txtMaKhachHang.Focus();
                return false;
            }
            if (txtDiachi.Text == string.Empty)
            {
                MessageBox.Show("Bạn Chưa Nhập địa chỉ");
                txtMaKhachHang.Focus();
                return false;
            }
            if (txtSoDienThoai.Text == string.Empty)
            {
                MessageBox.Show("Bạn chưa nha");
                txtMaKhachHang.Focus();
                return false;
            }
            return kq;

        }
        private void FormKhachHang_Load(object sender, EventArgs e)
        {
            dateTimeNgaySinh.Enabled = false;
            cbbGioiTinh.Enabled = false;
            txtMaKhachHang.Enabled = txtDiachi.Enabled = txtGhiChu.Enabled = txtSoDienThoai.Enabled = txtTenKhachHang.Enabled = false;
            btnLuu.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
            dataGridView1.DataSource = shop.LoadDLKH();
            string[] GioiTinh = { "Nam", "Nữ", "Khác" };
            foreach (string x in GioiTinh)
            {
                cbbGioiTinh.Items.Add(x);
            }
        }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {

                dateTimeNgaySinh.Enabled = true;
                cbbGioiTinh.Enabled = true;
                txtMaKhachHang.Enabled = txtDiachi.Enabled = txtGhiChu.Enabled = txtSoDienThoai.Enabled = txtTenKhachHang.Enabled = true;
                txtMaKhachHang.Focus();
                btnLuu.Enabled = true;
                btnXoa.Enabled = btnSua.Enabled = false;
            }
            catch
            {

            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa không!", "Xóa", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
            {
                shop.del(txtMaKhachHang.Text);

                btnXoa.Enabled = false;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];

                txtMaKhachHang.Text = row.Cells[0].Value.ToString().Trim();
                txtTenKhachHang.Text = row.Cells[2].Value.ToString().Trim();
                dateTimeNgaySinh.Text = row.Cells[3].Value.ToString().Trim();
                cbbGioiTinh.Text = row.Cells[4].Value.ToString().Trim();
                txtDiachi.Text = row.Cells[5].Value.ToString().Trim();
                txtSoDienThoai.Text = row.Cells[6].Value.ToString().Trim();
                txtGhiChu.Text = row.Cells[1].Value.ToString().Trim();

                btnXoa.Enabled = btnSua.Enabled = true;
                btnLuu.Enabled = false;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn sửa không!", "Sửa", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
            {
                DateTime ngs = dateTimeNgaySinh.Value;
                if (shop.SuaKH(txtMaKhachHang.Text, txtGhiChu.Text, txtTenKhachHang.Text, ngs, cbbGioiTinh.Text, txtDiachi.Text, txtSoDienThoai.Text) == true)
                {
                    MessageBox.Show("Thanh Cong");
                    btnSua.Enabled = false;
                    dataGridView1.Refresh();
                }
                else
                {
                    MessageBox.Show("That Bai");
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Controls.OfType<TextBox>().ToList().ForEach(t => t.Clear());
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thêm không!", "Thêm", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
            {
                DateTime ngs = dateTimeNgaySinh.Value;
                shop.ThemKH(txtMaKhachHang.Text, txtGhiChu.Text, txtTenKhachHang.Text, ngs, cbbGioiTinh.Text, txtDiachi.Text, txtSoDienThoai.Text);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
