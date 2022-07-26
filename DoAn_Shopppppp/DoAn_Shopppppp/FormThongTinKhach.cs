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
    public partial class FormThongTinKhach : Form
    {
        InKhachHang inkh = new InKhachHang();
        public FormThongTinKhach()
        {
            InitializeComponent();
        }

        private void FormThongTinKhach_Load(object sender, EventArgs e)
        {
            CrystalReport1 rpt = new CrystalReport1();

            inkh.LoadDLSinhVienTheoMaLop(FormDangNhapQA.usernv);
            DataTable dtsv = inkh.GetDLSinhVien();

            rpt.SetDatabaseLogon("sa", "123", "ADMIN\\SQLEXPRESS", "QL_SHOP");
            rpt.SetDataSource(dtsv);

            crystalReportViewer1.ReportSource = rpt;
            crystalReportViewer1.DisplayToolbar = true;
            crystalReportViewer1.DisplayStatusBar = true;
            crystalReportViewer1.Refresh();
        }
    }
}
