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
    public partial class QLSP : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        string query;
        public QLSP()
        {
            InitializeComponent();
            Connect connect = new Connect();
            conn = connect.ConnectDB();
            getData();
        }
        void getData()
        {
            try
            {
                List<tblQLSP> lstSP = new List<tblQLSP>();
                conn.Open();
                query = $"SELECT * FROM SanPham";
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
                dgvDSSP.DataSource = lstSP;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtMaKH_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string MaSP = txtMaSP.Text;
            string TenSP = txtTenSP.Text;
            string MaDanhMuc = txtMaDanhMuc.Text;
            string SoLuong = txtSoLuong.Text;
            string DonGiaNhap = txtDonGiaNhap.Text;
            string DonGiaBan = txtDonGiaBan.Text;

            try
            {
                conn.Open();
                query = $"INSERT INTO SanPham (MaSP, TenSP, MaDanhMuc, SoLuong, DonGiaNhap, DonGiaBan) VALUES ('{MaSP}', N'{TenSP}', '{MaDanhMuc}', '{SoLuong}', '{DonGiaNhap}', '{DonGiaBan}')";
                cmd = new SqlCommand(query, conn);
                int kq = cmd.ExecuteNonQuery();
                conn.Close();
                if (kq == 1)
                {
                    MessageBox.Show("Them thanh cong");
                    getData();
                }
                else
                {
                    MessageBox.Show("Them khong thanh cong");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string TimKiem = txtTimKiem.Text;
            if (string.IsNullOrEmpty(TimKiem))
            {
                getData();
            }
            else
            {
                List<tblQLSP> list = new List<tblQLSP>();
                try
                {
                    conn.Open();
                    query = $"SELECT * FROM SanPham WHERE TenSP LIKE N'%{TimKiem}%' ";
                    cmd = new SqlCommand(query, conn);
                    SqlDataReader data = cmd.ExecuteReader();
                    while (data.Read())
                    {
                        tblQLSP sp = new tblQLSP();
                        sp.MaSP = (string)data["MaSP"];
                        sp.TenSP = (string)data["TenSP"];
                        sp.MaDanhMuc = (string)data["MaDanhMuc"];
                        sp.SoLuong = (double)data["SoLuong"];
                        sp.DonGiaNhap = (double)data["DonGiaNhap"];
                        sp.DonGiaBan = (double)data["DonGiaBan"];
                        list.Add(sp);
                    }
                    conn.Close();
                    dgvDSSP.DataSource = list;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi khi hiển thị danh sách" + ex.Message);
                }
            }
        }

        private void dgvDSSP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = dgvDSSP.CurrentCell.RowIndex;
            txtMaSP.Text = dgvDSSP.Rows[row].Cells["MaSP"].Value.ToString();
            txtTenSP.Text = dgvDSSP.Rows[row].Cells["TenSP"].Value.ToString();
            txtMaDanhMuc.Text = dgvDSSP.Rows[row].Cells["MaDanhMuc"].Value.ToString();
            txtSoLuong.Text = dgvDSSP.Rows[row].Cells["SoLuong"].Value.ToString();
            txtDonGiaNhap.Text = dgvDSSP.Rows[row].Cells["DonGiaNhap"].Value.ToString();
            txtDonGiaBan.Text = dgvDSSP.Rows[row].Cells["DonGiaBan"].Value.ToString();

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string MaSP = txtMaSP.Text;
            string TenSP = txtTenSP.Text;
            string MaDanhMuc = txtMaDanhMuc.Text;
            string SoLuong = txtSoLuong.Text;
            string DonGiaNhap = txtDonGiaNhap.Text;
            string DonGiaBan = txtDonGiaBan.Text;
            try
            {
                conn.Open();
                query = $"UPDATE SanPham SET MaSP = '{MaSP}', TenSP = N'{TenSP}', MaDanhMuc = N'{MaDanhMuc}', SoLuong = N'{SoLuong}', DonGiaNhap = '{DonGiaNhap}', DonGiaBan = '{DonGiaBan}' WHERE MaSP = '{MaSP}'";
                cmd = new SqlCommand(query, conn);
                int kq = (int)cmd.ExecuteNonQuery();

                conn.Close();
                if (kq > 0)
                {
                    MessageBox.Show("Sua thanh cong");
                    getData();
                }
                else
                {
                    MessageBox.Show("Sua khong thanh cong");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string MaSP = txtMaSP.Text;
            try
            {
                conn.Open();
                query = $"DELETE FROM SanPham WHERE MaSP = '{MaSP}'";
                cmd = new SqlCommand(query, conn);
                int kq = (int)cmd.ExecuteNonQuery();

                conn.Close();
                if (kq > 0)
                {
                    MessageBox.Show("Xoa thanh cong");
                    getData();
                }
                else
                {
                    MessageBox.Show("Xoa khong thanh cong");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaSP.Text = "";
            txtTenSP.Text = "";
            txtMaDanhMuc.Text = "";
            txtSoLuong.Text = "";
            txtDonGiaNhap.Text = "";
            txtDonGiaBan.Text = "";
        }
    }
}
