using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn_Shopppppp
{
    public partial class FormKhuyenMai : Form
    {
        KhuyenMai km = new KhuyenMai();
        SanPham sp = new SanPham();
        public FormKhuyenMai()
        {
            InitializeComponent();
            load();
        }

        public void load()
        {
            dataGridView1.DataSource = km.loadDLTK();
            dataGridView2.DataSource = km.loadDLCTDKM();

            comboBox1.DisplayMember = "MaDot";
            comboBox1.ValueMember = "MaDot";
            comboBox1.DataSource = km.loadDLTK();

            comboBox2.DisplayMember = "TenSanPham";
            comboBox2.ValueMember = "MaSanPham";
            comboBox2.DataSource = sp.LoadDLSP();

            button2.Enabled = button3.Enabled = button5.Enabled = button6.Enabled = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];

                textBox1.Text = row.Cells[0].Value.ToString();
                dateTimePicker1.Text = row.Cells[1].Value.ToString();
                dateTimePicker2.Text = row.Cells[2].Value.ToString();
                button2.Enabled = button3.Enabled = true;
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex];

                comboBox1.Text = row.Cells[0].Value.ToString();
                comboBox2.Text = row.Cells[1].Value.ToString();
                textBox2.Text = row.Cells[2].Value.ToString();
                button5.Enabled = button6.Enabled = true;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Controls.OfType<TextBox>().ToList().ForEach(t => t.Clear());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime ngaybt = dateTimePicker1.Value;
            DateTime ngaykt = dateTimePicker2.Value;
            km.adddkm(ngaybt, ngaykt, this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa không!", "Xóa", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
            {
                if (km.deldkm(int.Parse(textBox1.Text), this) == true)
                {
                    MessageBox.Show("Xóa thành công");
                }
                else
                {
                    MessageBox.Show("Lỗi");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime ngaybt = dateTimePicker1.Value;
            DateTime ngaykt = dateTimePicker2.Value;
            if (km.fixdkm(int.Parse(textBox1.Text), ngaybt, ngaykt, this))
            {
                MessageBox.Show("Sửa thành công");
            }
            else
            {
                MessageBox.Show("Lỗi");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin");
            }
            else
            {
                DateTime ngaybt = dateTimePicker1.Value;
                DateTime ngaykt = dateTimePicker2.Value;
                km.add(int.Parse(comboBox1.Text), comboBox2.SelectedValue.ToString(), float.Parse(textBox2.Text), this);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa không!", "Xóa", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
            {
                km.del(int.Parse(comboBox1.Text), comboBox2.SelectedValue.ToString(), this);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (km.fix(int.Parse(comboBox1.Text), comboBox2.SelectedValue.ToString(), float.Parse(textBox2.Text), this))
            {
                MessageBox.Show("Sửa thành công");
            }
            else
            {
                MessageBox.Show("Lỗi");
            }
        }
    }
}
