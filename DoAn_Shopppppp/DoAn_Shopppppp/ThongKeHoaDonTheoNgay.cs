using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DoAn_Shopppppp
{
    public partial class ThongKeHoaDonTheoNgay : Form
    {
        SqlConnection conn = new SqlConnection(Connection.stringSqlConnection);
        public ThongKeHoaDonTheoNgay()
        {
            InitializeComponent();
        }
        private void BangThongKe()
        {
            chart1.Series["VND"].XValueType = (System.Windows.Forms.DataVisualization.Charting.ChartValueType)ChartValueType.DateTime;
            chart1.Series["VND"].YValueType = (System.Windows.Forms.DataVisualization.Charting.ChartValueType)ChartValueType.Int32;
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "dd-MM";
            DataSet ds = new DataSet();
            conn.Open();
            SqlDataAdapter adapt = new SqlDataAdapter("select SanPham.MaSanPham as Ten,sum(TongTienHoaDon) as SL from ChiTietHoaDon,SanPham where ChiTietHoaDon.MaSanPham=SanPham.MaSanPham and NgayLapHoaDon = '" + dateTimePicker1.Text + "' group by SanPham.MaSanPham", conn);

            adapt.Fill(ds);
            chart1.DataSource = ds;
            //set the member of the chart data source used to data bind to the X-values of the series  
            chart1.Series["VND"].XValueMember = "Ten";
            //set the member columns of the chart data source used to data bind to the X-values of the series  
            chart1.Series["VND"].YValueMembers = "SL";
            chart1.Series["VND"].IsValueShownAsLabel = true;
            conn.Close();
        }
        private void iconButton1_Click(object sender, EventArgs e)
        {
            BangThongKe();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
