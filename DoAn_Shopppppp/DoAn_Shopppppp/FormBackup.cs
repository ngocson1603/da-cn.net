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

namespace DoAn_Shopppppp
{
    
    public partial class FormBackup : Form
    {
        SqlConnection conn = new SqlConnection("server=ADMIN\\SQLEXPRESS;database=QL_SHOP;User ID=sa ; Password=123");
        public FormBackup()
        {
            InitializeComponent();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dlg.SelectedPath;
                iconButton2.Enabled = true;
            }
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            string database = conn.Database.ToString();
            if (textBox1.Text == string.Empty)
            {
                MessageBox.Show("chọn đường dẫn");
            }
            else
            {
                string cmd = "BACKUP DATABASE [" + database + "] TO DISK = '" + textBox1.Text + "\\" + "QL_SHOP" + ".bak'";
                conn.Open();
                SqlCommand command = new SqlCommand(cmd, conn);
                command.ExecuteNonQuery();
                MessageBox.Show("thành công");
                conn.Close();
                iconButton2.Enabled = false;
            }
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "SQL SERVER database backup files|*.bak";
            dlg.Title = "Database restore";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = dlg.FileName;
                iconButton3.Enabled = true;
            }
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            string database = conn.Database.ToString();
            conn.Open();
            try
            {
                string str1 = string.Format("ALTER DATABASE[" + database + "]SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
                SqlCommand cmd1 = new SqlCommand(str1, conn);
                cmd1.ExecuteNonQuery();

                string str2 = "USE MASTER RESTORE DATABASE[" + database + "] FROM DISK='" + textBox2.Text + "' WITH REPLACE;";
                SqlCommand cmd2 = new SqlCommand(str2, conn);
                cmd2.ExecuteNonQuery();

                string str3 = string.Format("ALTER DATABASE[" + database + "]SET MULTI_USER");
                SqlCommand cmd3 = new SqlCommand(str3, conn);
                cmd3.ExecuteNonQuery();

                MessageBox.Show("thành công");
                conn.Close();
            }
            catch
            {

            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
