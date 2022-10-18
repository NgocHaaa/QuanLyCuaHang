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
    public partial class DangNhap : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        string query;
        public DangNhap()
        {
            InitializeComponent();
            Connect connect = new Connect();
            conn = connect.ConnectDB();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                query = $"SELECT COUNT (*) FROM NhanVien WHERE TaiKhoan = '{txtTaiKhoan.Text}' AND MatKhau = '{txtMatKhau.Text}'";
                cmd = new SqlCommand(query, conn);
                int kq = (int)cmd.ExecuteScalar();
                if (kq == 1)
                {
                    MessageBox.Show("Dang nhap thanh cong");
                    this.Hide();
                    HeThong heThong = new HeThong(txtTaiKhoan.Text);
                    heThong.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Dang nhap khong thanh cong");
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
