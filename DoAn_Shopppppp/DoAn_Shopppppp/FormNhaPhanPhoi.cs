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
    public partial class FormNhaPhanPhoi : Form
    {
        NhaPhanPhoi npp = new NhaPhanPhoi();
        public FormNhaPhanPhoi()
        {
            InitializeComponent();
        }

        public bool KiemTraRong()
        {
            bool kq = true;

            if (txtMaNPP.Text == string.Empty)
            {
                MessageBox.Show("Bạn Chưa Nhập Mã Nhà Phân Phối");
                txtMaNPP.Focus();
                return false;
            }
            else
                if (txtTenNPP.Text == string.Empty)
            {
                MessageBox.Show("Bạn Chưa Nhập tên Nhà Phân Phối");
                txtTenNPP.Focus();
                return false;
            }
            if (txtDiaChi.Text == string.Empty)
            {
                MessageBox.Show("Bạn Chưa Nhập địa chỉ");
                txtDiaChi.Focus();
                return false;
            }
            if (npp.kiemtraSDT(txtSDT.Text) == false)
            {
                MessageBox.Show("Bạn chưa nhập số điện thoại");
                txtSDT.Focus();
                return false;
            }
            if (npp.isEmail(txtEmail.Text) == false)
            {
                MessageBox.Show("Bạn chưa nhập email chưa chính xác");
                txtEmail.Focus();
                return false;
            }
            return kq;

        }

        public void removeall()
        {
            for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
            {
                dataGridView1.Rows.RemoveAt(i);
            }
        }

        private void FormNhaPhanPhoi_Load(object sender, EventArgs e)
        {
            btnLuu.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
            txtMaNPP.Enabled = txtDiaChi.Enabled = txtEmail.Enabled = txtSDT.Enabled = txtTenNPP.Enabled = false;
            dataGridView1.DataSource = npp.LoadDLNPP();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtDiaChi.Enabled = txtEmail.Enabled = txtSDT.Enabled = txtTenNPP.Enabled = true;
            btnLuu.Enabled = true;
            btnXoa.Enabled = btnSua.Enabled = false;
            txtTenNPP.Focus();
            txtDiaChi.Text = "";
            txtEmail.Text = "";
            txtSDT.Text = "";
            txtTenNPP.Text = "";
            txtMaNPP.Text = "";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int i = e.RowIndex;

                txtMaNPP.Text = dataGridView1.Rows[i].Cells[0].Value.ToString().Trim();
                txtTenNPP.Text = dataGridView1.Rows[i].Cells[1].Value.ToString().Trim();
                txtDiaChi.Text = dataGridView1.Rows[i].Cells[2].Value.ToString().Trim();
                txtSDT.Text = dataGridView1.Rows[i].Cells[3].Value.ToString().Trim();
                txtEmail.Text = dataGridView1.Rows[i].Cells[4].Value.ToString().Trim();

                btnXoa.Enabled = true;
                btnSua.Enabled = true;
                btnLuu.Enabled = false;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            removeall();
            if (txtDiaChi.Text.Length == 0 || txtTenNPP.Text.Length == 0 || txtSDT.Text.Length == 0 || txtEmail.Text.Length == 0)
            {
                MessageBox.Show("Chưa nhập đầy đủ thông tin");
                return;
            }

            else
            {
                npp.ThemNhaPhanPhoi(txtTenNPP.Text, txtDiaChi.Text, txtSDT.Text, txtEmail.Text, this);
                dataGridView1.Refresh();
                npp.loadNPP(dataGridView1, "NhaPhanPhoi");
            }
        }



        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Controls.OfType<TextBox>().ToList().ForEach(t => t.Clear());
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (npp.XoaNhaPhanPhoi(int.Parse(txtMaNPP.Text)) == true)
            {
                MessageBox.Show("Xóa Nhà Phân phối thành công");

                btnXoa.Enabled = false;
            }
            else
            {
                MessageBox.Show("Xóa Nhà Phân phối không thành công");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
      
            removeall();
            if (txtDiaChi.Text.Length == 0 || txtTenNPP.Text.Length == 0 || txtSDT.Text.Length == 0 || txtEmail.Text.Length == 0)
            {
                MessageBox.Show("Chưa nhập đầy đủ thông tin");
                return;
            }

            else
            {
                npp.SuaNhaPhanPhoi(int.Parse(txtMaNPP.Text), txtTenNPP.Text, txtDiaChi.Text, txtSDT.Text, txtEmail.Text, this);

                btnSua.Enabled = false;
                dataGridView1.Refresh();
                npp.loadNPP(dataGridView1, "NhaPhanPhoi");
            }
        }

        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}
