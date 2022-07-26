using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn_Shopppppp
{
    public partial class FormSanPhamKhachXem : Form
    {
        SanPham sp = new SanPham();
        TimKiem tk = new TimKiem();
        public FormSanPhamKhachXem()
        {
            InitializeComponent();
            load();
        }

        public void load()
        {
            dataGridView1.DataSource = sp.LoadDLSP();
            //load cbb mã nhà phân phối
            cbbMaNhaPhanPhoi.DisplayMember = "TenNhaPhanPhoi";
            cbbMaNhaPhanPhoi.ValueMember = "MaNhaPhanPhoi";
            cbbMaNhaPhanPhoi.DataSource = sp.LoadDLNPP();
            //load cbb loại sản phẩm
            cbbLoaiSanPham.DisplayMember = "TenLoaiSanPham";
            cbbLoaiSanPham.ValueMember = "MaLoaiSanPham";
            cbbLoaiSanPham.DataSource = sp.LoadDLLoaiSP();
            //load cbb hãng sản xuất
            cbbHangSanXuat.DisplayMember = "TenHangSanXuat";
            cbbHangSanXuat.ValueMember = "MaHangSanXuat";
            cbbHangSanXuat.DataSource = sp.LoadDLHangSanXuat();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtMaSanPham.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                cbbMaNhaPhanPhoi.DisplayMember = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtTenSanPham.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                cbbLoaiSanPham.DisplayMember = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                cbbHangSanXuat.DisplayMember = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                txtTonKho.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                txtGiaBan.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();

                byte[] pic;
                pic = (byte[])dataGridView1.CurrentRow.Cells[7].Value;
                MemoryStream picture = new MemoryStream(pic);
                pictureBox1.Image = Image.FromStream(picture);

                double value = Convert.ToDouble(txtGiaBan.Text);
                txtGiaBan.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", value);
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            FormChiTietMuaHang tt = new FormChiTietMuaHang();
            string a = txtMaSanPham.Text;
            tt.textBox2.Text = a.ToString();
            tt.ShowDialog();

            this.Close();
        }
        public void removeall()
        {
            for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
            {
                dataGridView1.Rows.RemoveAt(i);
            }
        }
        private void iconButton2_Click(object sender, EventArgs e)
        {
            removeall();
            string con = txtTenSanPham.Text;
            tk.loadbyconten(dataGridView1, con);
        }
    }
}
