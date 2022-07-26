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
    public partial class FormInPhieuNhap : Form
    {
        PhieuNhap pn = new PhieuNhap();
        public FormInPhieuNhap()
        {
            InitializeComponent();
        }

        private void FormInPhieuNhap_Load(object sender, EventArgs e)
        {
            ReportPN rpt = new ReportPN();

            pn.LoadPhieuNhap();
            DataTable dtsv = pn.LoadDLPhieuNhap();

            rpt.SetDatabaseLogon("sa", "123", "ADMIN\\SQLEXPRESS", "QL_SHOP");
            rpt.SetDataSource(dtsv);

            crystalReportViewer1.ReportSource = rpt;
            crystalReportViewer1.DisplayToolbar = true;
            crystalReportViewer1.DisplayStatusBar = true;
            crystalReportViewer1.Refresh();
        }
    }
}
