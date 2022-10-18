using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace QuanLyBanHang
{
    internal class tblQLNV
    {
        private string _MaNV;
        private string _TenNV;
        private string _GioiTinh;
        private string _DiaChi;
        private string _SDT;
        private DateTime _NgaySinh;
        private string _ChucVu;
        private string _TaiKhoan;
        private string _MatKhau;

        public string MaNV { get => _MaNV; set => _MaNV = value; }
        public string TenNV { get => _TenNV; set => _TenNV = value; }
        public string GioiTinh { get => _GioiTinh; set => _GioiTinh = value; }
        public string DiaChi { get => _DiaChi; set => _DiaChi = value; }
        public string SDT { get => _SDT; set => _SDT = value; }
        public DateTime NgaySinh { get => _NgaySinh; set => _NgaySinh = value; }
        public string ChucVu { get => _ChucVu; set => _ChucVu = value; }
        public string TaiKhoan { get => _TaiKhoan; set => _TaiKhoan = value; }
        public string MatKhau { get => _MatKhau; set => _MatKhau = value; }
    }
}
