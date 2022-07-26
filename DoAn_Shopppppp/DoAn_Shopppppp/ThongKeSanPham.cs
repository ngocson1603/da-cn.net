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
    public partial class ThongKeSanPham : Form
    {
        SqlConnection conn = new SqlConnection(Connection.stringSqlConnection);
        public ThongKeSanPham()
        {
            InitializeComponent();
            BangThongKe();
        }

        private void BangThongKe()
        {
            chart1.Series["SoLuong"].XValueType = (System.Windows.Forms.DataVisualization.Charting.ChartValueType)ChartValueType.DateTime;
            chart1.Series["SoLuong"].YValueType = (System.Windows.Forms.DataVisualization.Charting.ChartValueType)ChartValueType.Int32;
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "dd-MM";
            DataSet ds = new DataSet();
            conn.Open();
            SqlDataAdapter adapt = new SqlDataAdapter("select TenSanPham as Ten,TonKho as SL from SanPham", conn);

            adapt.Fill(ds);
            chart1.DataSource = ds;
            //set the member of the chart data source used to data bind to the X-values of the series  
            chart1.Series["SoLuong"].XValueMember = "Ten";
            //set the member columns of the chart data source used to data bind to the X-values of the series  
            chart1.Series["SoLuong"].YValueMembers = "SL";
            chart1.Series["SoLuong"].IsValueShownAsLabel = true;
            conn.Close();
        }
    }
}
