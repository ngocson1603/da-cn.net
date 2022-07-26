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
    public partial class FormLoadding : Form
    {
        public FormLoadding()
        {
            InitializeComponent();
        }
        int x = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            x++;
            if (x == 5)
            {
                timer1.Stop();
                this.Hide();
                FormDangNhapQA dn = new FormDangNhapQA();
                dn.Show();
            }
        }
    }
}
