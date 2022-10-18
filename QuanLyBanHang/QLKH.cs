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
    public partial class QLKH : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        string query;
        public QLKH()
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
                List<tblQLKH> lstKH = new List<tblQLKH>();
                conn.Open();
                query = $"SELECT * FROM KhachHang";
                cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    tblQLKH objKH = new tblQLKH();
                    objKH.MaKH = (string)dr["MaKH"];
                    objKH.TenKH = (string)dr["TenKH"];
                    objKH.SDT = (string)dr["SoDienThoai"];
                    objKH.DiaChi = (string)dr["DiaChi"];
                    lstKH.Add(objKH);
                }
                dgvDSKH.DataSource = lstKH;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string MaKH = txtMaKH.Text;
            string TenKH = txtTenKH.Text;
            string SDT = mtbSDT.Text;
            string DiaChi = txtDiaChi.Text;


            try
            {
                conn.Open();
                query = $"INSERT INTO KhachHang (MaKH, TenKH, SoDienThoai, DiaChi) VALUES ('{MaKH}', N'{TenKH}', '{SDT}', N'{DiaChi}')";
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

        private void txtTimKiem3_TextChanged(object sender, EventArgs e)
        {
            string TimKiem = txtTimKiem3.Text;
            if (string.IsNullOrEmpty(TimKiem))
            {
                getData();
            }
            else
            {
                List<tblQLKH> list = new List<tblQLKH>();
                try
                {
                    conn.Open();
                    query = $"SELECT * FROM KhachHang WHERE TenKH LIKE N'%{TimKiem}%' ";
                    cmd = new SqlCommand(query, conn);
                    SqlDataReader data = cmd.ExecuteReader();
                    while (data.Read())
                    {
                        tblQLKH kh = new tblQLKH();
                        kh.MaKH = (string)data["MaKH"];
                        kh.TenKH = (string)data["TenKH"];
                        kh.SDT = (string)data["SoDienThoai"];
                        kh.DiaChi = (string)data["DiaChi"];
                        list.Add(kh);
                    }
                    conn.Close();
                    dgvDSKH.DataSource = list;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi khi hiển thị danh sách" + ex.Message);
                }
            }
        }

        private void dgvDSKH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = dgvDSKH.CurrentCell.RowIndex;
            txtMaKH.Text = dgvDSKH.Rows[row].Cells["MaKH"].Value.ToString();
            txtTenKH.Text = dgvDSKH.Rows[row].Cells["TenKH"].Value.ToString();
            mtbSDT.Text = dgvDSKH.Rows[row].Cells["SDT"].Value.ToString();
            txtDiaChi.Text = dgvDSKH.Rows[row].Cells["DiaChi"].Value.ToString();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string MaKH = txtMaKH.Text;
            try
            {
                conn.Open();
                query = $"DELETE FROM KhachHang WHERE MaKH = '{MaKH}'";
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
            txtMaKH.Text = "";
            txtTenKH.Text = "";
            mtbSDT.Text = "";
            txtDiaChi.Text = "";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string MaKH = txtMaKH.Text;
            string TenKH = txtTenKH.Text;
            string SDT = mtbSDT.Text;
            string DiaChi = txtDiaChi.Text;
            
            try
            {
                conn.Open();
                query = $"UPDATE KhachHang SET MaKH = '{MaKH}', TenKH = N'{TenKH}', SoDienThoai = '{SDT}', DiaChi = N'{DiaChi}' WHERE MaKH = '{MaKH}'";
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
