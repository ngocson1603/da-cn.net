using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace DoAn_Shopppppp
{
    public partial class FormPhieuNhap : Form
    {
        PhieuNhap pn = new PhieuNhap();
        SqlConnection connect = new SqlConnection(Connection.stringSqlConnection);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader rdr;
        public static string tennv = "";
        KetNoi kn = new KetNoi();
        public FormPhieuNhap()
        {
            InitializeComponent();
        }

        private void FormPhieuNhap_Load(object sender, EventArgs e)
        {
            dateTimeNgayNhap.Enabled = false;
            cbbNhaPhanPhoi.Enabled = false;
            txtMaPheu.Enabled = false;
            txtChuThich.Enabled = false;
            txtMaNV.Enabled = false;
            btnLuu.Enabled = btnXoa.Enabled = btnSua.Enabled = false;

            dataGridView1.DataSource = pn.LoadDLPhieuNhap();
            cbbNhaPhanPhoi.DisplayMember = "MaNhaPhanPhoi";
            cbbNhaPhanPhoi.ValueMember = "MaNhaPhanPhoi";
            cbbNhaPhanPhoi.DataSource = pn.LoadDLNPP();
        

            try
            {
                connect.Open();
                cmd.CommandText = "select NhanVien.MaNhanVien from NhanVien,Users where NhanVien.MaNhanVien = Users.MaNhanVien and Users.TenDangNhap='" + FormDangNhapQA.usernv + "'";
                cmd.Connection = connect;
                rdr = cmd.ExecuteReader();
                bool temp = false;
                while (rdr.Read())
                {
                    txtMaNV.Text = rdr.GetString(0);
                    tennv = rdr.GetString(0);
                    temp = true;
                }
                if (temp == false)
                    MessageBox.Show("not found");
                connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public bool KiemTraRong()
        {
            bool kq = true;

            if (txtMaPheu.Text == string.Empty)
            {
                MessageBox.Show("Bạn Chưa Nhập Mã Phiếu");
                txtMaPheu.Focus();
                return false;
            }
            else
                if (txtMaNV.Text == string.Empty)
            {
                MessageBox.Show("Bạn Chưa Nhập mã nhân viên");
                txtMaNV.Focus();
                return false;
            }
            //if (txtChuThich.Text == string.Empty)
            //{
            //    MessageBox.Show("Bạn Chưa Nhập ch");
            //    txtChuThich.Focus();
            //    return false;
            //}

            return kq;

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            dateTimeNgayNhap.Enabled = true;
            txtMaPheu.Enabled = true;
        
            cbbNhaPhanPhoi.Enabled = true;
            btnLuu.Enabled = true;
            btnXoa.Enabled = btnSua.Enabled = false;

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];

                txtMaPheu.Text = row.Cells[0].Value.ToString();
                txtMaNV.Text = row.Cells[1].Value.ToString();
                dateTimeNgayNhap.Text = row.Cells[4].Value.ToString();
                cbbNhaPhanPhoi.Text = row.Cells[2].Value.ToString();
                txtChuThich.Text = row.Cells[3].Value.ToString();

                btnXoa.Enabled = btnSua.Enabled = true;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (pn.XoaPhieuNhap(txtMaPheu.Text) == true)
            {
                MessageBox.Show("Xóa phiếu nhập thành công");
                btnXoa.Enabled = false;
            }
            else
            {
                MessageBox.Show("Xóa phiếu nhập không thành công");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            dateTimeNgayNhap.Enabled = true;
            txtMaPheu.Enabled = false;
            txtMaNV.Enabled = true;
            cbbNhaPhanPhoi.Enabled = true;
            btnThem.Enabled = btnXoa.Enabled = false;
            
            btnLuu.Enabled = true;


        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = true;
            if (txtMaPheu.Enabled == true)
            {
                if (KiemTraRong())
                {
                    if (pn.ThemPhieuNhap(txtMaPheu.Text, txtMaNV.Text, cbbNhaPhanPhoi.Text, dateTimeNgayNhap.Text) == true)

                    {
                        MessageBox.Show("Thêm phiếu nhập thành công");
                    }
                    else
                    {
                        MessageBox.Show("Thêm phiếu nhập thất bại");
                    }
                }
            }
            else
            {
                if (KiemTraRong() == true)
                {
                    if (pn.SuaPhieuNhap(txtMaPheu.Text, txtMaNV.Text, cbbNhaPhanPhoi.Text, dateTimeNgayNhap.Text) == true)
                    {
                        MessageBox.Show("Sửa phiếu nhâp thanh công");
                    }
                    else
                    {
                        MessageBox.Show("Sửa phiếu nhập không thanh công");
                    }

                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            btnXoa.Enabled = btnSua.Enabled = true;
        }

        private void btnCTPN_Click(object sender, EventArgs e)
        {

        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefesh_Click(object sender, EventArgs e)
        {
            FormInPhieuNhap pn = new FormInPhieuNhap();
            pn.Show();
            this.Hide();
        }
    }
}
