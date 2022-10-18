using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace QuanLyBanHang
{
    internal class tblQLDM
    {
        private string _MaDanhMuc;
        private string _TenDanhMuc;

        public string MaDanhMuc { get => _MaDanhMuc; set => _MaDanhMuc = value; }
        public string TenDanhMuc { get => _TenDanhMuc; set => _TenDanhMuc = value; }
    }
}
