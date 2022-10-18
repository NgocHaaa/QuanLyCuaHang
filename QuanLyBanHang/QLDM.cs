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
    public partial class QLDM : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        string query;
        public QLDM()
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
                List<tblQLDM> lstDM = new List<tblQLDM>();
                conn.Open();
                query = $"SELECT * FROM DanhMuc";
                cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    tblQLDM objDM = new tblQLDM();
                    objDM.MaDanhMuc = (string)dr["MaDanhMuc"];
                    objDM.TenDanhMuc = (string)dr["TenDanhMuc"];
                    lstDM.Add(objDM);
                }
                dgvDSDM.DataSource = lstDM;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string MaDanhMuc = txtMaDM.Text;
            string TenDanhMuc = txtTenDM.Text;

            try
            {
                conn.Open();
                query = $"INSERT INTO DanhMuc (MaDanhMuc, TenDanhMuc) VALUES ('{MaDanhMuc}', N'{TenDanhMuc}')";
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
                List<tblQLDM> list = new List<tblQLDM>();
                try
                {
                    conn.Open();
                    query = $"SELECT * FROM DanhMuc WHERE TenDanhMuc LIKE N'%{TimKiem}%' ";
                    cmd = new SqlCommand(query, conn);
                    SqlDataReader data = cmd.ExecuteReader();
                    while (data.Read())
                    {
                        tblQLDM dm = new tblQLDM();
                        dm.MaDanhMuc = (string)data["MaDanhMuc"];
                        dm.TenDanhMuc = (string)data["TenDanhMuc"];
                        list.Add(dm);
                    }
                    conn.Close();
                    dgvDSDM.DataSource = list;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi khi hiển thị danh sách" + ex.Message);
                }
            }
        }

        private void dgvDSDM_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = dgvDSDM.CurrentCell.RowIndex;
            txtMaDM.Text = dgvDSDM.Rows[row].Cells["MaDanhMuc"].Value.ToString();
            txtTenDM.Text = dgvDSDM.Rows[row].Cells["TenDanhMuc"].Value.ToString();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string MaDanhMuc = txtMaDM.Text;
            string TenDanhMuc = txtTenDM.Text;
            try
            {
                conn.Open();
                query = $"UPDATE DanhMuc SET MaDanhMuc = '{MaDanhMuc}', TenDanhMuc = N'{TenDanhMuc}' WHERE MaDanhMuc = '{MaDanhMuc}'";
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
            string MaDanhMuc = txtMaDM.Text;
            try
            {
                conn.Open();
                query = $"DELETE FROM DanhMuc WHERE MaDanhMuc = '{MaDanhMuc}'";
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
            txtMaDM.Text = "";
            txtTenDM.Text = "";
        }
    }
}
