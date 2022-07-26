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
    public partial class FormNhanVien : Form
    {
        NhanVien nv = new NhanVien();
        public FormNhanVien()
        {
            InitializeComponent();
        }

        private void FormNhanVien_Load(object sender, EventArgs e)
        {
            dateTimeNgayVaoLam.Enabled = dateTimeNgaySinh.Enabled = false;
            txtMaNhanVien.Enabled = txtTenNhanVien.Enabled = txtDiaChi.Enabled = txtSoDienThoai.Enabled = false;
            cbbChucVu.Enabled = cbbGioiTinh.Enabled = false;
            btnLuu.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
            dataGridView1.DataSource = nv.LoadDLNV();
            cbbChucVu.DisplayMember = "TenChucVu";
            cbbChucVu.ValueMember = "MaChucVu";
            cbbChucVu.DataSource = nv.loadCbbChucVu();
            string[] GioiTinh = { "Nam", "Nữ"};
            foreach (string x in GioiTinh)
            {
                cbbGioiTinh.Items.Add(x);
            }

        }
        public bool KiemTraRong()
        {
            bool kq = true;

            if (txtMaNhanVien.Text == string.Empty)
            {
                MessageBox.Show("Bạn Chưa Nhập Mã Khách Hàng");
                txtMaNhanVien.Focus();
                return false;
            }
            else
                if (txtTenNhanVien.Text == string.Empty)
            {
                MessageBox.Show("Bạn Chưa Nhập tên Khách Hàng");
                txtTenNhanVien.Focus();
                return false;
            }
            if (txtDiaChi.Text == string.Empty)
            {
                MessageBox.Show("Bạn Chưa Nhập địa chỉ");
                txtDiaChi.Focus();
                return false;
            }
            if (txtSoDienThoai.Text == string.Empty)
            {
                MessageBox.Show("Bạn chưa nha");
                txtSoDienThoai.Focus();
                return false;
            }
            return kq;

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            dateTimeNgayVaoLam.Enabled = dateTimeNgaySinh.Enabled = true;
            txtMaNhanVien.Enabled = txtTenNhanVien.Enabled = txtDiaChi.Enabled = txtSoDienThoai.Enabled = true;
            cbbChucVu.Enabled = cbbGioiTinh.Enabled = true;
            txtMaNhanVien.Focus();
            btnLuu.Enabled = true;
            btnXoa.Enabled = btnSua.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (nv.XoaNhanVien(txtMaNhanVien.Text) == true)
            {
                MessageBox.Show("Xóa nhân viên thanh công");

                btnXoa.Enabled = false;
            }
            else
            {
                MessageBox.Show("Xóa nhân viên không thanh công");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];

                txtMaNhanVien.Text = row.Cells[0].Value.ToString().Trim();
                txtTenNhanVien.Text = row.Cells[1].Value.ToString().Trim();
                dateTimeNgaySinh.Text = row.Cells[2].Value.ToString().Trim();
                cbbGioiTinh.Text = row.Cells[3].Value.ToString().Trim();
                dateTimeNgayVaoLam.Text = row.Cells[4].Value.ToString().Trim();
                cbbChucVu.Text = row.Cells[5].Value.ToString().Trim();
                
                txtDiaChi.Text = row.Cells[6].Value.ToString().Trim();
                txtSoDienThoai.Text = row.Cells[7].Value.ToString().Trim();

                btnXoa.Enabled = btnSua.Enabled = true;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            dateTimeNgayVaoLam.Enabled = dateTimeNgaySinh.Enabled = true;
            txtTenNhanVien.Enabled = txtDiaChi.Enabled = txtSoDienThoai.Enabled = true;
            cbbChucVu.Enabled = cbbGioiTinh.Enabled = true;
            txtMaNhanVien.Enabled = false;
            btnThem.Enabled = btnXoa.Enabled = false;
            btnLuu.Enabled = true;
        }

        

private void btnLuu_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = true;
            try
            {
                if (txtMaNhanVien.Enabled == true)
                {
                    if (KiemTraRong() == true)
                    {
                        DateTime namsinh = dateTimeNgaySinh.Value;
                        int born_year = dateTimeNgaySinh.Value.Year;
                        int this_year = DateTime.Now.Year;

                        if (((this_year - born_year) < 18) || ((this_year - born_year) > 100))
                        {
                            MessageBox.Show("Tuổi phải lớn hơn 18", "Nhân Viên", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else if (nv.ThemNhanVien(txtMaNhanVien.Text, txtTenNhanVien.Text, dateTimeNgaySinh.Text, cbbGioiTinh.Text,
                            dateTimeNgayVaoLam.Text, cbbChucVu.SelectedValue.ToString(), txtDiaChi.Text, txtSoDienThoai.Text) == true)
                        {
                            MessageBox.Show("Thêm nhân viên thanh công");
                        }
                        else
                        {
                            MessageBox.Show("Thêm nhân viên không thanh công");
                        }

                    }
                }
                else
                {
                    if (KiemTraRong() == true)
                    {
                        DateTime namsinh = dateTimeNgaySinh.Value;
                        int born_year = dateTimeNgaySinh.Value.Year;
                        int this_year = DateTime.Now.Year;

                        if (((this_year - born_year) < 18) || ((this_year - born_year) > 100))
                        {
                            MessageBox.Show("Tuổi phải lớn hơn 18", "Nhân Viên", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else if (nv.SuaNhanVien(txtMaNhanVien.Text, txtTenNhanVien.Text, dateTimeNgaySinh.Text, cbbGioiTinh.Text,
                        dateTimeNgayVaoLam.Text, cbbChucVu.SelectedValue.ToString(), txtDiaChi.Text, txtSoDienThoai.Text) == true)
                        {
                            MessageBox.Show("Sửa nhân viên thanh công");
                        }
                        else
                        {
                            MessageBox.Show("Sửa nhân viên không thanh công");
                        }
                    }
                }
            }
            catch
            {

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
    }
}
