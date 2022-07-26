using System;
using System.Windows.Forms;

namespace DoAn_Shopppppp
{
    public partial class FormDangKy : Form
    {
        DangKy dk = new DangKy();
        public FormDangKy()
        {
            InitializeComponent();
            radioButton1.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string gioitinh = "Nam";

            if (radioButton1.Checked)
            {
                gioitinh = "Nữ";
            }
            DateTime namsinh = dateTimePicker1.Value;
            int born_year = dateTimePicker1.Value.Year;
            int this_year = DateTime.Now.Year;

            if (txtDChi.Text.Length == 0 || txtHoTen.Text.Length == 0 || txtMatKhau.Text.Length == 0 || txtSDT.Text.Length == 0 || txtTK.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
            }
            else if (((this_year - born_year) < 18) || ((this_year - born_year) > 100))
            {
                MessageBox.Show("Tuổi phải lớn hơn 18", "Khách hàng", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                dk.add(txtTK.Text, txtMatKhau.Text, txtHoTen.Text, namsinh, gioitinh, txtDChi.Text, txtSDT.Text, this);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
         
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
