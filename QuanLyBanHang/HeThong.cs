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
    public partial class HeThong : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        string query;
        public HeThong(string TaiKhoan)
        {
            InitializeComponent();
            Connect connect = new Connect();
            conn = connect.ConnectDB();
            checkUser(TaiKhoan);
        }
        void checkUser(string TaiKhoan)
        {
            try
            {
                conn.Open();
                query = $"SELECT COUNT(*) FROM NhanVien WHERE TaiKhoan = '{TaiKhoan}' AND ChucVu = 'Admin'";
                cmd = new SqlCommand(query, conn);
                int kq = (int)cmd.ExecuteScalar();
                if (kq != 1)
                {
                    btnTK.Hide();
                    btnQLNV.Hide();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void loadform(object form)
        {
            if (this.mainpanel.Controls.Count > 0)
                this.mainpanel.Controls.RemoveAt(0);
            Form f = form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.mainpanel.Controls.Add(f);
            this.mainpanel.Tag = f;
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            loadform(new TrangChu());
        }

        private void btnQLDM_Click(object sender, EventArgs e)
        {
            loadform(new QLDM());
        }

        private void btnQLKH_Click(object sender, EventArgs e)
        {
            loadform(new QLKH());
        }

        private void btnQLSP_Click(object sender, EventArgs e)
        {
            loadform(new QLSP());
        }

        private void btnQLNV_Click(object sender, EventArgs e)
        {
            loadform(new QLNV());
        }

        private void btnQLHD_Click(object sender, EventArgs e)
        {
            loadform(new QLHD());
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            this.Hide();

            DangNhap dangNhap = new DangNhap();
            dangNhap.ShowDialog();
        }

        private void btnTK_Click(object sender, EventArgs e)
        {
            loadform(new TK());
        }
    }
}
