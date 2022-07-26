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
    public partial class FormQuenMatKhau : Form
    {
        public FormQuenMatKhau()
        {
            InitializeComponent();
            label2.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();

        }
        Modify m = new Modify();
        private void button1_Click(object sender, EventArgs e)
        {
            String gmail = textBox1.Text;
            if(gmail.Trim() == "")
            {
                MessageBox.Show("Vui long nhập gmail đang kí");
            }
            else
            {
                String query = "select * from KhachHang where Gmail = '" + gmail + "'";
                if(m.taiKhoan1s(query).Count !=0)
                {
                    label2.ForeColor = Color.Blue;
                    label2.Text = "Mật khẩu của bạn là:" + m.taiKhoan1s(query)[0].Matkhau;
                }
                else
                {
                    label2.ForeColor = Color.Red;
                    label2.Text = "Email này chưa được đăng kí";
                }    
            }    


            

        }
    }
}
