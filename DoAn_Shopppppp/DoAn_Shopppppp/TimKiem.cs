using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace DoAn_Shopppppp
{
    class TimKiem
    {
        SqlConnection conn = new SqlConnection(Connection.stringSqlConnection);
        DataSet ds_QLShop = new DataSet();
        public TimKiem()
        {

        }
        public void loadsp(DataGridView gv, string tablename)
        {
            gv.DataSource = ds_QLShop.Tables[tablename];
        }
        public void loadbyconsp(DataGridView gv, string con)
        {
            string slkl = "select * from SanPham where MaSanPham='" + con + "'";
            SqlDataAdapter da = new SqlDataAdapter(slkl, conn);
            da.Fill(ds_QLShop, "bycondition");
            loadsp(gv, "bycondition");
        }

        public void loadbyconloai(DataGridView gv, string con)
        {
            string slkl = "select * from SanPham where LoaiSanPham='" + con + "'";
            SqlDataAdapter da = new SqlDataAdapter(slkl, conn);
            da.Fill(ds_QLShop, "bycondition");
            loadsp(gv, "bycondition");
        }

        public void loadbyconten(DataGridView gv, string con)
        {
            string slkl = "select * from SanPham where TenSanPham like '%" + con + "%'";
            SqlDataAdapter da = new SqlDataAdapter(slkl, conn);
            da.Fill(ds_QLShop, "bycondition");
            loadsp(gv, "bycondition");
        }

        public void loadbyconhang(DataGridView gv, int con)
        {
            string slkl = "select * from SanPham where HangSanXuat='" + con + "'";
            SqlDataAdapter da = new SqlDataAdapter(slkl, conn);
            da.Fill(ds_QLShop, "bycondition");
            loadsp(gv, "bycondition");
        }

        public void loadbyconnpp(DataGridView gv, int con)
        {
            string slkl = "select * from SanPham where MaNhaPhanPhoi='" + con + "'";
            SqlDataAdapter da = new SqlDataAdapter(slkl, conn);
            da.Fill(ds_QLShop, "bycondition");
            loadsp(gv, "bycondition");
        }

        public void timtheomanpp(DataGridView gv, string con)
        {
            string slkl = "select * from NhaPhanPhoi where MaNhaPhanPhoi='" + con + "'";
            SqlDataAdapter da = new SqlDataAdapter(slkl, conn);
            da.Fill(ds_QLShop, "byconnpp");
            loadsp(gv, "byconnpp");
        }

        public void timtheotennpp(DataGridView gv, string con)
        {
            string slkl = "select * from NhaPhanPhoi where TenNhaPhanPhoi like '%" + con + "%'";
            SqlDataAdapter da = new SqlDataAdapter(slkl, conn);
            da.Fill(ds_QLShop, "byconnpp");
            loadsp(gv, "byconnpp");
        }

        public void timtheoemailnpp(DataGridView gv, string con)
        {
            string slkl = "select * from NhaPhanPhoi where Email like '%" + con + "%'";
            SqlDataAdapter da = new SqlDataAdapter(slkl, conn);
            da.Fill(ds_QLShop, "byconnpp");
            loadsp(gv, "byconnpp");
        }

        public void timtheotenkh(DataGridView gv, string con)
        {
            string slkl = "select * from KhachHang where TenKhachHang like '%" + con + "%'";
            SqlDataAdapter da = new SqlDataAdapter(slkl, conn);
            da.Fill(ds_QLShop, "bycondition");
            loadsp(gv, "bycondition");
        }

        public void timtheoemailkh(DataGridView gv, string con)
        {
            string slkl = "select * from KhachHang where Gmail like '%" + con + "%'";
            SqlDataAdapter da = new SqlDataAdapter(slkl, conn);
            da.Fill(ds_QLShop, "bycondition");
            loadsp(gv, "bycondition");
        }

        public void timtheomanv(DataGridView gv, string con)
        {
            string slkl = "select * from NhanVien where MaNhanVien like '%" + con + "%'";
            SqlDataAdapter da = new SqlDataAdapter(slkl, conn);
            da.Fill(ds_QLShop, "bycondition");
            loadsp(gv, "bycondition");
        }

        public void timtheotennv(DataGridView gv, string con)
        {
            string slkl = "select * from NhanVien where TenNhanVien like '%" + con + "%'";
            SqlDataAdapter da = new SqlDataAdapter(slkl, conn);
            da.Fill(ds_QLShop, "bycondition");
            loadsp(gv, "bycondition");
        }


    }
}
