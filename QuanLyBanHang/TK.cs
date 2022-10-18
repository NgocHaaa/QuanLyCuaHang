using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QuanLyBanHang
{
    public partial class TK : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader data;
        string query;
        public TK()
        {
            InitializeComponent();

            Connect connect = new Connect();
            conn = connect.ConnectDB();
            getData();
            thongKeHomNay();

        }

        void thongKeHomNay()
        {
            try
            {
                conn.Open();
                //So hoa don
                query = "SELECT COUNT(*) FROM HoaDon";
                cmd = new SqlCommand(query, conn);
                int SoHoaDon = (int)cmd.ExecuteScalar();
                lblSoHoaDon.Text = SoHoaDon.ToString();

                //Tong doanh thu
                query = "SELECT SUM(ThanhTien) FROM HoaDon";
                cmd = new SqlCommand(query, conn);
                double TongDoanhThu = (double)cmd.ExecuteScalar();
                lblTongDoanhThu.Text = TongDoanhThu.ToString();

                //So khach hang
                query = "SELECT COUNT(MaKH) FROM HoaDon";
                cmd = new SqlCommand(query, conn);
                int SoKhachHang = (int)cmd.ExecuteScalar();
                lblSoKhachHang.Text = SoKhachHang.ToString();

                //So san pham
                query = "SELECT COUNT(MaSP) FROM HoaDon";
                cmd = new SqlCommand(query, conn);
                int SoSanPham = (int)cmd.ExecuteScalar();
                lblSoSanPham.Text = SoSanPham.ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        void getData()
        {
            try
            {
                List<tblQLSP> lstSP = new List<tblQLSP>();
                conn.Open();
                query = $"SELECT * FROM SanPham WHERE SoLuong = 0";
                cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    tblQLSP objSP = new tblQLSP();
                    objSP.MaSP = (string)dr["MaSP"];
                    objSP.TenSP = (string)dr["TenSP"];
                    objSP.MaDanhMuc = (string)dr["MaDanhMuc"];
                    objSP.SoLuong = (double)dr["SoLuong"];
                    objSP.DonGiaNhap = (double)dr["DonGiaNhap"];
                    objSP.DonGiaBan = (double)dr["DonGiaBan"];
                    lstSP.Add(objSP);
                }
                conn.Close();
                dgvSPHH.DataSource = lstSP;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TK_Load(object sender, EventArgs e)
        {
            //Tong doanh thu chart
            SqlDataAdapter ad = new SqlDataAdapter("SELECT NgayBan, SUM (ThanhTien) AS ThanhTien FROM HoaDon GROUP BY NgayBan", conn);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            chartTongDoanhThu.DataSource = dt;
            chartTongDoanhThu.ChartAreas["ChartArea1"].AxisX.Title = "Ngày bán";
            chartTongDoanhThu.ChartAreas["ChartArea1"].AxisY.Title = "Tổng tiền";

            chartTongDoanhThu.Series["Series1"].XValueMember = "NgayBan";
            chartTongDoanhThu.Series["Series1"].YValueMembers = "ThanhTien";

            //Top 5 san pham ban chay
            SqlDataAdapter adt = new SqlDataAdapter("SELECT TOP 5 MaSP, Sum(SoLuong) AS SoLuong FROM HoaDon GROUP BY MaSP ORDER BY Sum(SoLuong) DESC", conn);
            DataTable dtl = new DataTable();
            adt.Fill(dtl);
            chartTopSanPham.DataSource = dtl;
            chartTopSanPham.ChartAreas["ChartArea1"].AxisX.Title = "Mã sản phẩm";
            chartTopSanPham.ChartAreas["ChartArea1"].AxisX.Title = "Số lượng";

            chartTopSanPham.Series["Series1"].XValueMember = "MaSP";
            chartTopSanPham.Series["Series1"].YValueMembers = "SoLuong";
        }

        private void dgvSPHH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnHomNay_Click(object sender, EventArgs e)
        {
            //Tong doanh thu chart
            SqlDataAdapter ad = new SqlDataAdapter("SELECT NgayBan, SUM (ThanhTien) AS ThanhTien FROM HoaDon WHERE NgayBan < GETDATE() AND NgayBan > GETDATE() - 1 GROUP BY NgayBan", conn);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            chartTongDoanhThu.DataSource = dt;
            chartTongDoanhThu.ChartAreas["ChartArea1"].AxisX.Title = "Hôm nay";
            chartTongDoanhThu.ChartAreas["ChartArea1"].AxisX.Title = "Tổng tiền";

            chartTongDoanhThu.Series["Series1"].XValueMember = "NgayBan";
            chartTongDoanhThu.Series["Series1"].YValueMembers = "ThanhTien";

            //Top 5 san pham ban chay
            SqlDataAdapter adt = new SqlDataAdapter("SELECT TOP 5 MaSP, Sum(SoLuong) AS SoLuong FROM HoaDon WHERE NgayBan < GETDATE() AND NgayBan > GETDATE() - 1 GROUP BY MaSP ORDER BY Sum(SoLuong) DESC", conn);
            DataTable dtl = new DataTable();
            adt.Fill(dtl);
            chartTopSanPham.DataSource = dtl;
            chartTopSanPham.ChartAreas["ChartArea1"].AxisX.Title = "Mã sản phẩm";
            chartTopSanPham.ChartAreas["ChartArea1"].AxisX.Title = "Số lượng";

            chartTopSanPham.Series["Series1"].XValueMember = "MaSP";
            chartTopSanPham.Series["Series1"].YValueMembers = "SoLuong";
        }

        private void btn7NgayQua_Click(object sender, EventArgs e)
        {
            //Tong doanh thu chart
            SqlDataAdapter ad = new SqlDataAdapter("SELECT NgayBan, SUM (ThanhTien) AS ThanhTien FROM HoaDon WHERE NgayBan < GETDATE() AND NgayBan > GETDATE() - 7 GROUP BY NgayBan", conn);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            chartTongDoanhThu.DataSource = dt;
            chartTongDoanhThu.ChartAreas["ChartArea1"].AxisX.Title = "Hôm nay";
            chartTongDoanhThu.ChartAreas["ChartArea1"].AxisX.Title = "Tổng tiền";

            chartTongDoanhThu.Series["Series1"].XValueMember = "NgayBan";
            chartTongDoanhThu.Series["Series1"].YValueMembers = "ThanhTien";

            //Top 5 san pham ban chay
            SqlDataAdapter adt = new SqlDataAdapter("SELECT TOP 5 MaSP, Sum(SoLuong) AS SoLuong FROM HoaDon WHERE NgayBan < GETDATE() AND NgayBan > GETDATE() - 7 GROUP BY MaSP ORDER BY Sum(SoLuong) DESC", conn);
            DataTable dtl = new DataTable();
            adt.Fill(dtl);
            chartTopSanPham.DataSource = dtl;
            chartTopSanPham.ChartAreas["ChartArea1"].AxisX.Title = "Mã sản phẩm";
            chartTopSanPham.ChartAreas["ChartArea1"].AxisX.Title = "Số lượng";

            chartTopSanPham.Series["Series1"].XValueMember = "MaSP";
            chartTopSanPham.Series["Series1"].YValueMembers = "SoLuong";
        }

        private void btn30NgayQua_Click(object sender, EventArgs e)
        {
            //Tong doanh thu chart
            SqlDataAdapter ad = new SqlDataAdapter("SELECT NgayBan, SUM (ThanhTien) AS ThanhTien FROM HoaDon WHERE NgayBan < GETDATE() AND NgayBan > GETDATE() - 30 GROUP BY NgayBan", conn);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            chartTongDoanhThu.DataSource = dt;
            chartTongDoanhThu.ChartAreas["ChartArea1"].AxisX.Title = "Hôm nay";
            chartTongDoanhThu.ChartAreas["ChartArea1"].AxisX.Title = "Tổng tiền";

            chartTongDoanhThu.Series["Series1"].XValueMember = "NgayBan";
            chartTongDoanhThu.Series["Series1"].YValueMembers = "ThanhTien";

            //Top 5 san pham ban chay
            SqlDataAdapter adt = new SqlDataAdapter("SELECT TOP 5 MaSP, Sum(SoLuong) AS SoLuong FROM HoaDon WHERE NgayBan < GETDATE() AND NgayBan > GETDATE() - 30 GROUP BY MaSP ORDER BY Sum(SoLuong) DESC", conn);
            DataTable dtl = new DataTable();
            adt.Fill(dtl);
            chartTopSanPham.DataSource = dtl;
            chartTopSanPham.ChartAreas["ChartArea1"].AxisX.Title = "Mã sản phẩm";
            chartTopSanPham.ChartAreas["ChartArea1"].AxisX.Title = "Số lượng";

            chartTopSanPham.Series["Series1"].XValueMember = "MaSP";
            chartTopSanPham.Series["Series1"].YValueMembers = "SoLuong";
        }

        private void btnTong_Click(object sender, EventArgs e)
        {
            //Tong doanh thu chart
            SqlDataAdapter ad = new SqlDataAdapter("SELECT NgayBan, SUM (ThanhTien) AS ThanhTien FROM HoaDon GROUP BY NgayBan", conn);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            chartTongDoanhThu.DataSource = dt;
            chartTongDoanhThu.ChartAreas["ChartArea1"].AxisX.Title = "Ngày bán";
            chartTongDoanhThu.ChartAreas["ChartArea1"].AxisY.Title = "Tổng tiền";

            chartTongDoanhThu.Series["Series1"].XValueMember = "NgayBan";
            chartTongDoanhThu.Series["Series1"].YValueMembers = "ThanhTien";

            //Top 5 san pham ban chay
            SqlDataAdapter adt = new SqlDataAdapter("SELECT TOP 5 MaSP, Sum(SoLuong) AS SoLuong FROM HoaDon GROUP BY MaSP ORDER BY Sum(SoLuong) DESC", conn);
            DataTable dtl = new DataTable();
            adt.Fill(dtl);
            chartTopSanPham.DataSource = dtl;
            chartTopSanPham.ChartAreas["ChartArea1"].AxisX.Title = "Mã sản phẩm";
            chartTopSanPham.ChartAreas["ChartArea1"].AxisX.Title = "Số lượng";

            chartTopSanPham.Series["Series1"].XValueMember = "MaSP";
            chartTopSanPham.Series["Series1"].YValueMembers = "SoLuong";
        }

        private void btnThangNay_Click(object sender, EventArgs e)
        {
            //Tong doanh thu chart
            SqlDataAdapter ad = new SqlDataAdapter("SELECT NgayBan, SUM (ThanhTien) AS ThanhTien FROM HoaDon WHERE MONTH(NgayBan) = MONTH(GETDATE()) GROUP BY NgayBan", conn);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            chartTongDoanhThu.DataSource = dt;
            chartTongDoanhThu.ChartAreas["ChartArea1"].AxisX.Title = "Hôm nay";
            chartTongDoanhThu.ChartAreas["ChartArea1"].AxisX.Title = "Tổng tiền";

            chartTongDoanhThu.Series["Series1"].XValueMember = "NgayBan";
            chartTongDoanhThu.Series["Series1"].YValueMembers = "ThanhTien";

            //Top 5 san pham ban chay
            SqlDataAdapter adt = new SqlDataAdapter("SELECT TOP 5 MaSP, Sum(SoLuong) AS SoLuong FROM HoaDon WHERE MONTH(NgayBan) = MONTH(GETDATE()) GROUP BY MaSP ORDER BY Sum(SoLuong) DESC", conn);
            DataTable dtl = new DataTable();
            adt.Fill(dtl);
            chartTopSanPham.DataSource = dtl;
            chartTopSanPham.ChartAreas["ChartArea1"].AxisX.Title = "Mã sản phẩm";
            chartTopSanPham.ChartAreas["ChartArea1"].AxisX.Title = "Số lượng";

            chartTopSanPham.Series["Series1"].XValueMember = "MaSP";
            chartTopSanPham.Series["Series1"].YValueMembers = "SoLuong";
        }
    }
}
