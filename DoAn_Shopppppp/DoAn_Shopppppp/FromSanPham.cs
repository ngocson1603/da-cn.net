using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;
using System.Linq;

namespace DoAn_Shopppppp
{
    public partial class FromSanPham : Form
    {

        SanPham sp = new SanPham();
        public FromSanPham()
        {
            InitializeComponent();
            load();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FromSanPham_Load(object sender, EventArgs e)
        {

        }

        public void load()
        {
            dataGridView1.DataSource = sp.LoadDLSP();
            btnInLuu.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
        }

        private void btnImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofp = new OpenFileDialog();
            ofp.Filter = "Select Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";
            if ((ofp.ShowDialog() == DialogResult.OK))
            {
                pictureBox1.Image = Image.FromFile(ofp.FileName);
            }
        }
        private byte[] converImgToByte()
        {
            FileStream fs;
            fs = new FileStream(txtHinhAnh.Text, FileMode.Open, FileAccess.Read);
            byte[] picbyte = new byte[fs.Length];
            fs.Read(picbyte, 0, System.Convert.ToInt32(fs.Length));
            fs.Close();
            return picbyte;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnInLuu.Enabled = true;
        }
        public void removeall()
        {
            for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
            {
                dataGridView1.Rows.RemoveAt(i);
            }
        }

        private void btnInLuu_Click(object sender, EventArgs e)
        {
            
            MemoryStream pic = new MemoryStream();
            double gia = Convert.ToDouble(txtGiaBan.Text);
            pictureBox1.Image.Save(pic, pictureBox1.Image.RawFormat);
            if (sp.KiemTraKhoaNgoai(txtMaSanPham.Text) == true)
            {
                MessageBox.Show("Sản phẩm đã tồn tại");
                load();
            }
            else if (sp.KiemTraTen(txtTenSanPham.Text) == true)
            {
                MessageBox.Show("Tên Sản phẩm đã tồn tại");
                load();
            }
            else
            {
                removeall();
                if (sp.ThemSanPham(txtMaSanPham.Text, cbbMaNhaPhanPhoi.SelectedValue.ToString(), txtTenSanPham.Text, cbbLoaiSanPham.SelectedValue.ToString()
                               , cbbHangSanXuat.SelectedValue.ToString(), gia, txtTonKho.Text, pic) == true)
                {
                    MessageBox.Show("Thêm thành công");
                    load();
                }

                else
                {
                    MessageBox.Show("Thêm thất bại");
                }
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtMaSanPham.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                cbbMaNhaPhanPhoi.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtTenSanPham.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                cbbLoaiSanPham.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                cbbHangSanXuat.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                txtTonKho.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                txtGiaBan.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();

                byte[] pic;
                pic = (byte[])dataGridView1.CurrentRow.Cells[7].Value;
                MemoryStream picture = new MemoryStream(pic);
                pictureBox1.Image = Image.FromStream(picture);

                double value = Convert.ToDouble(txtGiaBan.Text);
                txtGiaBan.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", value);

                btnXoa.Enabled = btnSua.Enabled = true;
                btnInLuu.Enabled = false;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            MemoryStream pic = new MemoryStream();
            pictureBox1.Image.Save(pic, pictureBox1.Image.RawFormat);
            string ma = txtMaSanPham.Text.ToString();
           
            if (sp.SuaSanPham(ma, cbbMaNhaPhanPhoi.SelectedValue.ToString(), txtTenSanPham.Text, cbbLoaiSanPham.SelectedValue.ToString(), cbbHangSanXuat.SelectedValue.ToString(), float.Parse(txtGiaBan.Text), txtTonKho.Text, pic) == true)

            {

                MessageBox.Show("Sửa thành công");
                btnSua.Enabled = false;
            }
            else
            {
                MessageBox.Show("Sửa thất bại");
            }
        }

        private void btnReFesh_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Controls.OfType<TextBox>().ToList().ForEach(t => t.Clear());
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (sp.XoaPhieuNhap(txtMaSanPham.Text) == true)
            {
                MessageBox.Show("Xóa thành công");
                btnXoa.Enabled = false;
            }
            else
            {
                MessageBox.Show("Xóa không thành công");
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

       
    }
}
