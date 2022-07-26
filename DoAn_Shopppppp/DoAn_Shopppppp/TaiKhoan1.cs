using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn_Shopppppp
{
    public class TaiKhoan1
    {
        private string tendangnhap;
        private string matkhau;

        public TaiKhoan1()
        {
        }

        public TaiKhoan1(string tendangnhap, string matkhau)
        {
            this.tendangnhap = tendangnhap;
            this.matkhau = matkhau;
        }

        public string Tendangnhap { get => tendangnhap; set => tendangnhap = value; }
        public string Matkhau { get => matkhau; set => matkhau = value; }
    }
}
