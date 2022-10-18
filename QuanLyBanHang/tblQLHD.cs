using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace QuanLyBanHang
{
    internal class tblQLHD
    {
        private string _MaHD;
        private string _MaNV;
        private string _MaKH;
        private string _MaSP;
        private double _SoLuong;
        private DateTime _NgayBan;
        private double _ThanhTien;

        public string MaHD { get => _MaHD; set => _MaHD = value; }
        public string MaNV { get => _MaNV; set => _MaNV = value; }
        public string MaKH { get => _MaKH; set => _MaKH = value; }
        public string MaSP { get => _MaSP; set => _MaSP = value; }
        public double SoLuong { get => _SoLuong; set => _SoLuong = value; }
        public DateTime NgayBan { get => _NgayBan; set => _NgayBan = value; }
        public double ThanhTien { get => _ThanhTien; set => _ThanhTien = value; }
    }
}
