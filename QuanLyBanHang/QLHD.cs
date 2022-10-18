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
    public partial class QLHD : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        string query;
        public QLHD()
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
                List<tblQLHD> lstHD = new List<tblQLHD>();
                conn.Open();
                query = $"SELECT * FROM HoaDon";
                cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    tblQLHD objHD = new tblQLHD();
                    objHD.MaHD = (string)dr["MaHD"];
                    objHD.MaNV = (string)dr["MaNV"];
                    objHD.MaKH = (string)dr["MaKH"];
                    objHD.MaSP = (string)dr["MaSP"];
                    objHD.SoLuong = (double)dr["SoLuong"];
                    objHD.NgayBan = (DateTime)dr["NgayBan"];
                    objHD.ThanhTien = (double)dr["ThanhTien"];
                    lstHD.Add(objHD);
                }
                dgvDSHD.DataSource = lstHD;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string MaHD = txtMaHD.Text;
            string MaNV = txtMaNV.Text;
            string MaKH = txtMaKH.Text;
            string MaSP = txtMaSP.Text;
            string SoLuong = txtSoLuong.Text;
            string[] arr = txtNgayBan.Text.Split('/');
            string NgayBan = arr[2] + "-" + arr[1] + "-" + arr[0];
            string ThanhTien = txtThanhTien.Text;


            try
            {
                conn.Open();
                query = $"INSERT INTO HoaDon (MaHD, MaNV, MaKH, MaSP, SoLuong, NgayBan, ThanhTien) VALUES ('{MaHD}', '{MaNV}', '{MaKH}', '{MaSP}', '{SoLuong}', '{NgayBan}', '{ThanhTien}')";
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
                List<tblQLHD> list = new List<tblQLHD>();
                try
                {
                    conn.Open();
                    query = $"SELECT * FROM HoaDon WHERE MaKH LIKE '%{TimKiem}%' ";
                    cmd = new SqlCommand(query, conn);
                    SqlDataReader data = cmd.ExecuteReader();
                    while (data.Read())
                    {
                        tblQLHD hd = new tblQLHD();
                        hd.MaHD = (string)data["MaHD"];
                        hd.MaNV = (string)data["MaNV"];
                        hd.MaKH = (string)data["MaKH"];
                        hd.MaSP = (string)data["MaSP"];
                        hd.SoLuong = (double)data["SoLuong"];
                        hd.NgayBan = (DateTime)data["NgayBan"];
                        hd.ThanhTien = (double)data["ThanhTien"];
                        list.Add(hd);
                    }
                    conn.Close();
                    dgvDSHD.DataSource = list;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi khi hiển thị danh sách" + ex.Message);
                }
            }
        }

        private void dgvDSHD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = dgvDSHD.CurrentCell.RowIndex;
            txtMaHD.Text = dgvDSHD.Rows[row].Cells["MaHD"].Value.ToString();
            txtMaNV.Text = dgvDSHD.Rows[row].Cells["MaNV"].Value.ToString();
            txtMaKH.Text = dgvDSHD.Rows[row].Cells["MaKH"].Value.ToString();
            txtMaSP.Text = dgvDSHD.Rows[row].Cells["MaSP"].Value.ToString();
            txtSoLuong.Text = dgvDSHD.Rows[row].Cells["SoLuong"].Value.ToString();
            txtNgayBan.Text = dgvDSHD.Rows[row].Cells["NgayBan"].Value.ToString();
            txtThanhTien.Text = dgvDSHD.Rows[row].Cells["ThanhTien"].Value.ToString();

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string MaHD = txtMaHD.Text;
            try
            {
                conn.Open();
                query = $"DELETE FROM HoaDon WHERE MaHD = '{MaHD}'";
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
            txtMaHD.Text = "";
            txtMaNV.Text = "";
            txtMaKH.Text = "";
            txtMaSP.Text = "";
            txtSoLuong.Text = "";
            txtNgayBan.Text = "";
            txtThanhTien.Text = "";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string MaHD = txtMaHD.Text;
            string MaNV = txtMaNV.Text;
            string MaKH = txtMaKH.Text;
            string MaSP = txtMaSP.Text;
            string SoLuong = txtSoLuong.Text;
            string[] arr = txtNgayBan.Text.Split('/');
            string NgayBan = arr[2] + "-" + arr[1] + "-" + arr[0];
            string ThanhTien = txtThanhTien.Text;
            try
            {
                conn.Open();
                query = $"UPDATE HoaDon SET MaHD = '{MaHD}', MaNV = '{MaNV}', MaKH = '{MaKH}', MaSP = '{MaSP}', SoLuong = '{SoLuong}', NgayBan = '{NgayBan}', ThanhTien = '{ThanhTien}' WHERE MaHD = '{MaHD}'";
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
    }
}
