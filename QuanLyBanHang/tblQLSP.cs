using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace QuanLyBanHang
{
    internal class tblQLSP
    {
        private string _MaSP;
        private string _TenSP;
        private string _MaDanhMuc;
        private double _SoLuong;
        private double _DonGiaNhap;
        private double _DonGiaBan;

        public string MaSP { get => _MaSP; set => _MaSP = value; }
        public string TenSP { get => _TenSP; set => _TenSP = value; }
        public string MaDanhMuc { get => _MaDanhMuc; set => _MaDanhMuc = value; }
        public double SoLuong { get => _SoLuong; set => _SoLuong = value; }
        public double DonGiaNhap { get => _DonGiaNhap; set => _DonGiaNhap = value; }
        public double DonGiaBan { get => _DonGiaBan; set => _DonGiaBan = value; }
    }
}
