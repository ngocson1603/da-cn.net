using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DoAn_Shopppppp
{
    public partial class FormThongKe : Form
    {
        private Form currentchildform;
        public FormThongKe()
        {
            InitializeComponent();
        }

        private void motrangcon(Form trangcon)
        {
            if (currentchildform != null)
            {
                currentchildform.Close();

            }
            currentchildform = trangcon;
            trangcon.TopLevel = false;
            trangcon.FormBorderStyle = FormBorderStyle.None;
            trangcon.Dock = DockStyle.Fill;
            panel1.Controls.Add(trangcon);
            panel1.Tag = trangcon;
            trangcon.BringToFront();
            trangcon.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            motrangcon(new ThongKeSanPham());
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            motrangcon(new ThongKeDoanhDu());
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            motrangcon(new ThongKeHoaDonTheoNgay());
        }
    }
}
