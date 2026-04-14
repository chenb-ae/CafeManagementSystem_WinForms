using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GamingCafe
{
    public partial class LichSuBanHang : UserControl
    {
        private string maHD;

        public LichSuBanHang()
        {
            InitializeComponent();

            pnlHD.BringToFront();

            taiLichSu();
        }

        public void taiLichSu()
        {
            try
            {
                using (SqlConnection con = DBConnect.getConnection())
                {
                    string sql = "SELECT MaHD, NgayTao, TongTien, NguoiTao FROM HoaDon";

                    SqlDataAdapter da = new SqlDataAdapter(sql, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvHD.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lịch sử: " + ex.Message);
            }
        }

        private void dgvHD_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvHD.Rows[e.RowIndex];
                maHD = row.Cells["MaHD"].Value.ToString();

                HienThiCTHD(maHD, row);
            }
        }

        private void HienThiCTHD(string maHD, DataGridViewRow selectedRow)
        {
            try
            {
                using (SqlConnection con = DBConnect.getConnection())
                {
                    con.Open();

                    string sqlChiTiet = "SELECT TenMon, SoLuong, DonGia, ThanhTien FROM ChiTietHoaDon WHERE MaHD = @ma";
                    SqlDataAdapter da = new SqlDataAdapter(sqlChiTiet, con);
                    da.SelectCommand.Parameters.AddWithValue("@ma", maHD);
                    DataTable dtChiTiet = new DataTable();
                    da.Fill(dtChiTiet);

                    dgvCTHD.DataSource = dtChiTiet;

                    string sqlHD = "SELECT KhachDua FROM HoaDon WHERE MaHD = @ma";
                    SqlCommand cmd = new SqlCommand(sqlHD, con);
                    cmd.Parameters.AddWithValue("@ma", maHD);

                    int khachDua = Convert.ToInt32(cmd.ExecuteScalar());
                    long tongTien = Convert.ToInt64(selectedRow.Cells["TongTien"].Value);
                    string ngayTao = selectedRow.Cells["NgayTao"].Value.ToString();
                    string nguoiTao = selectedRow.Cells["NguoiTao"].Value.ToString();

                    lblChiTiet.Text = $"Chi tiết hóa đơn: {maHD}";
                    txtNV.Text = nguoiTao;
                    txtThoiGian.Text = ngayTao;

                    txtTongTien.Text = $"{tongTien:N0} VNĐ";
                    txtKhachDua.Text = $"{khachDua:N0} VNĐ";
                    txtThoiLai.Text = $"{(khachDua - tongTien):N0} VNĐ";

                    pnlCTHD.BringToFront();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hiển thị chi tiết: {ex.Message}", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public int XoaDau(string str)
        {
            try
            {
                string cleanString = str.Replace(".", "").Replace(",", "").Replace(" VNĐ", "").Trim();
                return int.Parse(cleanString);
            }
            catch
            {
                return 0;
            }
        }


        public void InHoaDon(string maHD)
        {
            try
            {
                string hoTenNV = "";

                using (SqlConnection con = DBConnect.getConnection())
                {
                    con.Open();
                    StringBuilder hoaDon = new StringBuilder();

                    string sqlGetTen = @"SELECT n.Ho + ' ' + n.Ten 
                                   FROM HoaDon h 
                                   JOIN NhanVIen n ON h.NguoiTao = n.MaNV 
                                   WHERE h.MaHD = @ma";

                    SqlCommand cmdTen = new SqlCommand(sqlGetTen, con);
                    cmdTen.Parameters.AddWithValue("@ma", maHD);

                    object result = cmdTen.ExecuteScalar();
                    hoTenNV = result.ToString();

                    hoaDon.AppendLine("                GAMING CAFE                ");
                    hoaDon.AppendLine("-------------------------------------------");
                    hoaDon.AppendLine("             PHIẾU THANH TOÁN              ");
                    hoaDon.AppendLine($"Mã hóa đơn: {maHD}");
                    hoaDon.AppendLine($"Ngày bán:   {txtThoiGian.Text}");
                    hoaDon.AppendLine($"Nhân viên:  {hoTenNV}");
                    hoaDon.AppendLine("-------------------------------------------");
                    hoaDon.AppendLine(string.Format("{0,-16} {1,-10} {2,15}", "Số lượng", "Đơn giá", "Thành tiền"));

                    foreach (DataGridViewRow row in dgvCTHD.Rows)
                    {
                        if (row.IsNewRow) continue;

                        string tenMon = row.Cells[0].Value.ToString().ToUpper();

                        int soLuong = int.Parse(row.Cells[1].Value.ToString());
                        int donGia = XoaDau(row.Cells[2].Value.ToString());
                        int thanhTien = XoaDau(row.Cells[3].Value.ToString());

                        string strDonGia = donGia.ToString("N0") + " VNĐ";
                        string strThanhTien = thanhTien.ToString("N0") + " VNĐ";

                        hoaDon.AppendLine($"{tenMon}");
                        hoaDon.AppendLine(string.Format("{0,-16} {1,-10} {2,15}",
                            soLuong,
                            strDonGia,
                            strThanhTien));
                    }

                    hoaDon.AppendLine("-------------------------------------------");
                    hoaDon.AppendLine(string.Format("{0,-26} {1,15}", "Tổng tiền: ", txtTongTien.Text));
                    hoaDon.AppendLine(string.Format("{0,-26} {1,15}", "Khách đưa: ", txtKhachDua.Text));
                    hoaDon.AppendLine(string.Format("{0,-26} {1,15}", "Tiền thối lại: ", txtThoiLai.Text));
                    hoaDon.AppendLine("-------------------------------------------");
                    hoaDon.AppendLine("             CẢM ƠN QUÝ KHÁCH              ");

                    RichTextBox rtb = new RichTextBox();
                    rtb.Font = new Font("Consolas", 12, FontStyle.Regular);
                    rtb.Text = hoaDon.ToString();
                    rtb.ReadOnly = true;
                    rtb.BackColor = Color.White;
                    rtb.Dock = DockStyle.Fill;
                    rtb.BorderStyle = BorderStyle.None;

                    Size textSize = TextRenderer.MeasureText(rtb.Text, rtb.Font);

                    Form f = new Form();
                    f.Text = "Hóa đơn thanh toán #" + maHD;
                    f.StartPosition = FormStartPosition.CenterScreen;
                    f.MaximizeBox = false;
                    f.MinimizeBox = false;
                    f.FormBorderStyle = FormBorderStyle.FixedDialog;

                    f.ClientSize = new Size(textSize.Width, textSize.Height);

                    f.Controls.Add(rtb);
                    f.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi in hóa đơn: " + ex.Message);
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            pnlHD.BringToFront();
        }

        private void btnInHD_Click(object sender, EventArgs e)
        {
            InHoaDon(maHD);
        }
    }
}
