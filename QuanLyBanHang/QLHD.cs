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
using Word = Microsoft.Office.Interop.Word;

namespace QuanLyBanHang
{
    public partial class QLHD : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        string query;
        DataGridView dgvExportHoaDon = new DataGridView();
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

            try
            {
                //Ẩn nút thanh toán, hiện nút thanh toán


                //Khai báo số lượng cột có trong dgvExportHoaDon
                dgvExportHoaDon.ColumnCount = 7;
                //Thêm header vào dgvExportHoaDon
                dgvExportHoaDon.Columns[0].HeaderText = "Mã hóa đơn";
                dgvExportHoaDon.Columns[1].HeaderText = "Mã nhân viên";
                dgvExportHoaDon.Columns[2].HeaderText = "Mã khách hàng";
                dgvExportHoaDon.Columns[3].HeaderText = "Mã sản phẩm";
                dgvExportHoaDon.Columns[4].HeaderText = "Số lượng";
                dgvExportHoaDon.Columns[5].HeaderText = "Ngày bán";
                dgvExportHoaDon.Columns[6].HeaderText = "Thành tiền";

                //Lấy thông tin của dòng được click
                int rowindex = dgvDSHD.CurrentCell.RowIndex;
                string MaHD = dgvDSHD.Rows[rowindex].Cells[0].Value.ToString(); //Mã Hóa Đơn
                string MaNV = dgvDSHD.Rows[rowindex].Cells[1].Value.ToString();
                string MaKH = dgvDSHD.Rows[rowindex].Cells[2].Value.ToString();
                string MaSP = dgvDSHD.Rows[rowindex].Cells[3].Value.ToString();
                string SoLuong = dgvDSHD.Rows[rowindex].Cells[4].Value.ToString();
                string NgayBan = dgvDSHD.Rows[rowindex].Cells[5].Value.ToString();
                string ThanhTien = dgvDSHD.Rows[rowindex].Cells[6].Value.ToString();

                //Xóa tất cả các dòng cũ có trong dgvExportHoaDon
                dgvExportHoaDon.Rows.Clear();
                //Thêm dòng mới vào dgvExportHoaDon với thông tin của dòng được click
                dgvExportHoaDon.Rows.Add(MaHD, MaNV, MaKH, MaSP, SoLuong, NgayBan, ThanhTien);
                //Xóa bớt dòng trống bên cuối dgvExportHoaDon
                dgvExportHoaDon.AllowUserToAddRows = false;
                dgvExportHoaDon.AllowUserToDeleteRows = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }


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

        public void Export_Data_To_Word(DataGridView DGV, string filename)
        {
            if (DGV.Rows.Count != 0)
            {
                int RowCount = DGV.Rows.Count;
                int ColumnCount = DGV.Columns.Count;
                Object[,] DataArray = new object[RowCount + 1, ColumnCount + 1];

                //add rows
                int r = 0;
                for (int c = 0; c <= ColumnCount - 1; c++)
                {
                    for (r = 0; r <= RowCount - 1; r++)
                    {
                        DataArray[r, c] = DGV.Rows[r].Cells[c].Value;
                    } //end row loop
                } //end column loop

                Word.Document oDoc = new Word.Document();
                oDoc.Application.Visible = true;

                //page orintation
                oDoc.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;


                dynamic oRange = oDoc.Content.Application.Selection.Range;
                string oTemp = "";
                for (r = 0; r <= RowCount - 1; r++)
                {
                    for (int c = 0; c <= ColumnCount - 1; c++)
                    {
                        oTemp = oTemp + DataArray[r, c] + "\t";

                    }
                }

                //table format
                oRange.Text = oTemp;

                object Separator = Word.WdTableFieldSeparator.wdSeparateByTabs;
                object ApplyBorders = true;
                object AutoFit = true;
                object AutoFitBehavior = Word.WdAutoFitBehavior.wdAutoFitContent;

                oRange.ConvertToTable(ref Separator, ref RowCount, ref ColumnCount,
                                      Type.Missing, Type.Missing, ref ApplyBorders,
                                      Type.Missing, Type.Missing, Type.Missing,
                                      Type.Missing, Type.Missing, Type.Missing,
                                      Type.Missing, ref AutoFit, ref AutoFitBehavior, Type.Missing);

                oRange.Select();

                oDoc.Application.Selection.Tables[1].Select();
                oDoc.Application.Selection.Tables[1].Rows.AllowBreakAcrossPages = 0;
                oDoc.Application.Selection.Tables[1].Rows.Alignment = 0;
                oDoc.Application.Selection.Tables[1].Rows[1].Select();
                oDoc.Application.Selection.InsertRowsAbove(1);
                oDoc.Application.Selection.Tables[1].Rows[1].Select();

                //header row style
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Bold = 1;
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Name = "Tahoma";
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Size = 14;

                //add header row manually
                for (int c = 0; c <= ColumnCount - 1; c++)
                {
                    oDoc.Application.Selection.Tables[1].Cell(1, c + 1).Range.Text = DGV.Columns[c].HeaderText;
                }

                //table style 
                oDoc.Application.Selection.Tables[1].set_Style("Grid Table 4 - Accent 5");
                oDoc.Application.Selection.Tables[1].Rows[1].Select();
                oDoc.Application.Selection.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                //header text
                foreach (Word.Section section in oDoc.Application.ActiveDocument.Sections)
                {
                    Word.Range headerRange = section.Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    headerRange.Fields.Add(headerRange, Word.WdFieldType.wdFieldPage);
                    headerRange.Text = "";
                    headerRange.Font.Size = 16;
                    headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                }

                //save the file
                oDoc.SaveAs2(filename);

                //NASSIM LOUCHANI
            }
        }




        private void QLHD_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "Word Documents (*.docx)|*.docx";

            sfd.FileName = "hoadon.docx";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Export_Data_To_Word(dgvDSHD, sfd.FileName);
            }
        }
        
    }
}
