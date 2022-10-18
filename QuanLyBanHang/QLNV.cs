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
    public partial class QLNV : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        string query;
        public QLNV()
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
                List<tblQLNV> lstNV = new List<tblQLNV>();
                conn.Open();
                query = $"SELECT * FROM NhanVien";
                cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    tblQLNV objNV = new tblQLNV();
                    objNV.MaNV = (string)dr["MaNV"];
                    objNV.TenNV = (string)dr["TenNV"];
                    objNV.GioiTinh = (string)dr["GioiTinh"];
                    objNV.DiaChi = (string)dr["DiaChi"];
                    objNV.SDT = (string)dr["DienThoai"];
                    objNV.NgaySinh = (DateTime)dr["NgaySinh"];
                    objNV.ChucVu = (string)dr["ChucVu"];
                    objNV.TaiKhoan = (string)dr["TaiKhoan"];
                    objNV.MatKhau = (string)dr["MatKhau"];
                    lstNV.Add(objNV);
                }
                dgvDSNV.DataSource = lstNV;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string MaNV = txtMaNV.Text;
            string TenNV = txtTenNV.Text;
            string GioiTinh = txtGioiTinh.Text;
            string DiaChi = txtDiaChi.Text;
            string DienThoai = mtbSDT.Text;
            string[] arr = mskNgaySinh.Text.Split('/');
            string NgaySinh = arr[2] + "-" + arr[1] + "-" + arr[0];


            string ChucVu = txtChucVu.Text;
            string TaiKhoan = txtTaiKhoan.Text;
            string MatKhau = txtMatKhau.Text;

            try
            {
                conn.Open();
                query = $"INSERT INTO NhanVien (MaNV, TenNV, GioiTinh, DiaChi, DienThoai, NgaySinh, ChucVu, TaiKhoan, MatKhau) VALUES ('{MaNV}', N'{TenNV}', N'{GioiTinh}', N'{DiaChi}', '{DienThoai}', '{NgaySinh}', N'{ChucVu}', '{TaiKhoan}', '{MatKhau}')";
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
                List<tblQLNV> list = new List<tblQLNV>();
                try
                {
                    conn.Open();
                    query = $"SELECT * FROM NhanVien WHERE TenNV LIKE N'%{TimKiem}%' ";
                    cmd = new SqlCommand(query, conn);
                    SqlDataReader data = cmd.ExecuteReader();
                    while (data.Read())
                    {
                        tblQLNV nv = new tblQLNV();
                        nv.MaNV = (string)data["MaNV"];
                        nv.TenNV = (string)data["TenNV"];
                        nv.GioiTinh = (string)data["GioiTinh"];
                        nv.DiaChi = (string)data["DiaChi"];
                        nv.SDT = (string)data["DienThoai"];
                        nv.NgaySinh = (DateTime)data["NgaySinh"];
                        nv.ChucVu = (string)data["ChucVu"];
                        nv.TaiKhoan = (string)data["TaiKhoan"];
                        nv.MatKhau = (string)data["MatKhau"];
                        list.Add(nv);
                    }
                    conn.Close();
                    dgvDSNV.DataSource = list;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi khi hiển thị danh sách" + ex.Message);
                }
            }
        }

        private void dgvDSNV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = dgvDSNV.CurrentCell.RowIndex;
            txtMaNV.Text = dgvDSNV.Rows[row].Cells["MaNV"].Value.ToString();
            txtTenNV.Text = dgvDSNV.Rows[row].Cells["TenNV"].Value.ToString();
            txtGioiTinh.Text = dgvDSNV.Rows[row].Cells["GioiTinh"].Value.ToString();
            txtDiaChi.Text = dgvDSNV.Rows[row].Cells["DiaChi"].Value.ToString();
            mtbSDT.Text = dgvDSNV.Rows[row].Cells["SDT"].Value.ToString();
            mskNgaySinh.Text = dgvDSNV.Rows[row].Cells["NgaySinh"].Value.ToString();
            txtChucVu.Text = dgvDSNV.Rows[row].Cells["ChucVu"].Value.ToString();
            txtTaiKhoan.Text = dgvDSNV.Rows[row].Cells["TaiKhoan"].Value.ToString();
            txtMatKhau.Text = dgvDSNV.Rows[row].Cells["MatKhau"].Value.ToString();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string MaNV = txtMaNV.Text;
            string TenNV = txtTenNV.Text;
            string GioiTinh = txtGioiTinh.Text;
            string DiaChi = txtDiaChi.Text;
            string DienThoai = mtbSDT.Text;
            string[] arr = mskNgaySinh.Text.Split('/');
            string NgaySinh = arr[2] + "-" + arr[1] + "-" + arr[0];
            string ChucVu = txtChucVu.Text;
            string TaiKhoan = txtTaiKhoan.Text;
            string MatKhau = txtMatKhau.Text;
            try
            {
                conn.Open();
                query = $"UPDATE NhanVien SET MaNV = '{MaNV}', TenNV = N'{TenNV}', GioiTinh = N'{GioiTinh}', DiaChi = N'{DiaChi}', DienThoai = '{DienThoai}', NgaySinh = '{NgaySinh}', ChucVu = N'{ChucVu}', TaiKhoan = '{TaiKhoan}', MatKhau = '{MatKhau}' WHERE MaNV = '{MaNV}'";
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
            string MaNV = txtMaNV.Text;
            try
            {
                conn.Open();
                query = $"DELETE FROM NhanVien WHERE MaNV = '{MaNV}'";
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
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            txtGioiTinh.Text = "";
            txtDiaChi.Text = "";
            mtbSDT.Text = "";
            mskNgaySinh.Text = "";
            txtChucVu.Text = "";
            txtTaiKhoan.Text = "";
            txtMatKhau.Text = "";
        }
    }
}
