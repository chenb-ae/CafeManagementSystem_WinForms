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
    public partial class TrangBanHang : UserControl
    {
        public TrangBanHang()
        {
            InitializeComponent();
            TaiMenu();
        }

        public void TaiMenu()
        {
            pnlTrinhBay.Controls.Clear();

            try
            {
                using (SqlConnection con = DBConnect.getConnection())
                {
                    con.Open();
                    string sql = "SELECT * FROM SanPham WHERE TrangThai = N'Còn bán'";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        using (SqlDataReader rs = cmd.ExecuteReader())
                        {
                            int count = 0;
                            while (rs.Read())
                            {
                                count++;

                                string maSP = rs["MaSP"].ToString();
                                string tenMon = rs["TenSP"].ToString();
                                int gia = Convert.ToInt32(rs["Gia"]);
                                int tonKho = Convert.ToInt32(rs["TonKho"]);
                                string tenHinh = rs["HinhAnh"].ToString();

                                ucMenu card = new ucMenu(this, tenMon, gia, tonKho, tenHinh, maSP);

                                //thêm vào panel
                                pnlTrinhBay.Controls.Add(card);
                            }

                            int dong = (int)Math.Ceiling((double)count / 3);
                            int chieuCao = dong * 250;
                            pnlTrinhBay.Size = new Size(620, chieuCao);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải menu: " + ex.Message);
            }
        }

        public void ThemVaoGioHang(string tenMon, int soLuong, int donGia, int tonKho, string maSP)
        {

            for (int i = 0; i < dgvGioHang.Rows.Count; i++)
            {
                if (dgvGioHang.Rows[i].IsNewRow) continue;

                string monDaCo = dgvGioHang.Rows[i].Cells[0].Value.ToString();

                if (monDaCo == tenMon)
                {
                    int soLuongCu = Convert.ToInt32(dgvGioHang.Rows[i].Cells[1].Value);
                    int soLuongMoi = soLuongCu + soLuong;

                    if (soLuongMoi > tonKho)
                    {
                        MessageBox.Show(
                            $"Không đủ hàng! Trong kho chỉ còn {tonKho - soLuongCu} phần {tenMon}",
                            "Cảnh báo hết hàng",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    int tongTienMoi = soLuongMoi * donGia;
                    dgvGioHang.Rows[i].Cells[1].Value = soLuongMoi;
                    dgvGioHang.Rows[i].Cells[2].Value = tongTienMoi.ToString("N0");

                    TinhTongTien();
                    return;
                }
            }

            int tongTien = soLuong * donGia;
            dgvGioHang.Rows.Add(tenMon, soLuong, tongTien.ToString("N0"), maSP);

            TinhTongTien();
        }

        public void TinhTongTien()
        {
            long thanhTien = 0;

            foreach (DataGridViewRow row in dgvGioHang.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Cells[2].Value != null)
                {
                    string strTien = row.Cells[2].Value.ToString();
                    thanhTien += XoaDau(strTien);
                }
            }

            lblTongTien.Text = thanhTien.ToString("N0") + " VNĐ";
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

        public void inHoaDon(int maHoaDon)
        {
            try
            {
                StringBuilder hoaDon = new StringBuilder();

                hoaDon.AppendLine("                GAMING CAFE                ");
                hoaDon.AppendLine("-------------------------------------------");
                hoaDon.AppendLine("             PHIẾU THANH TOÁN              ");
                hoaDon.AppendLine($"Mã hóa đơn: {maHoaDon}");
                hoaDon.AppendLine($"Ngày in: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                hoaDon.AppendLine($"Nhân viên: {NVTrucCa.ho} {NVTrucCa.ten}");
                hoaDon.AppendLine("-------------------------------------------");

                hoaDon.AppendLine(string.Format("{0,-16} {1,-10} {2,15}", "Số lượng", "Đơn giá", "Thành tiền"));

                foreach (DataGridViewRow row in dgvGioHang.Rows)
                {
                    if (row.IsNewRow) continue;

                    string tenMon = row.Cells[0].Value.ToString();
                    string soLuong = row.Cells[1].Value.ToString();
                    int thanhTien = XoaDau(row.Cells[2].Value.ToString());

                    int donGia = thanhTien / int.Parse(soLuong);

                    string strDonGia = donGia.ToString("N0") + " VNĐ";
                    string strThanhTien = thanhTien.ToString("N0") + " VNĐ";

                    hoaDon.AppendLine(tenMon);
                    hoaDon.AppendLine(string.Format("{0,-16} {1,-10} {2,15}",
                        soLuong,
                        strDonGia,
                        strThanhTien));
                }

                hoaDon.AppendLine("-------------------------------------------");
                hoaDon.AppendLine(string.Format("{0,-26} {1,15}", "Tổng tiền: ", lblTongTien.Text));
                int khachDua = int.Parse(txtKhachDua.Text) * 1000;
                hoaDon.AppendLine(string.Format("{0,-26} {1,15}", "Khách đưa: ", khachDua.ToString("N0") + " VNĐ"));
                hoaDon.AppendLine(string.Format("{0,-26} {1,15}", "Tiền thối lại: ", lblThoiLai.Text));
                hoaDon.AppendLine("-------------------------------------------");
                hoaDon.AppendLine("             CẢM ƠN QUÝ KHÁCH              ");

                RichTextBox txtHoaDon = new RichTextBox();
                txtHoaDon.Font = new Font("Consolas", 12, FontStyle.Regular);
                txtHoaDon.Text = hoaDon.ToString();
                txtHoaDon.ReadOnly = true;
                txtHoaDon.BackColor = Color.White;
                txtHoaDon.Dock = DockStyle.Fill;

                Size textSize = TextRenderer.MeasureText(txtHoaDon.Text, txtHoaDon.Font);

                Form f = new Form();
                f.Text = "Hóa đơn thanh toán";
                f.StartPosition = FormStartPosition.CenterScreen;

                f.MaximizeBox = false;
                f.MinimizeBox = false;

                f.ClientSize = new Size(textSize.Width, textSize.Height + 20);

                f.Controls.Add(txtHoaDon);
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi in hóa đơn: " + ex.Message);
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (dgvGioHang.Rows.Count == 0 || (dgvGioHang.Rows.Count == 1 && dgvGioHang.Rows[0].IsNewRow))
            {
                MessageBox.Show("Giỏ hàng đang trống!");
                return;
            }

            string chuoiKhachDua = txtKhachDua.Text.Trim();
            if (string.IsNullOrEmpty(chuoiKhachDua))
            {
                MessageBox.Show("Lỗi: Vui lòng nhập số tiền khách đưa!");
                txtKhachDua.Focus();
                return;
            }

            int tongTien = XoaDau(lblTongTien.Text);

            int khachDua = int.Parse(chuoiKhachDua) * 1000;

            if (khachDua < tongTien)
            {
                MessageBox.Show("Lỗi: Khách chưa đưa đủ tiền!");
                return;
            }

            using (SqlConnection con = DBConnect.getConnection())
            {
                con.Open();
                SqlTransaction trans = con.BeginTransaction();

                try
                {
                    string sqlLuuHD = @"INSERT INTO HoaDon (NgayTao, TongTien, KhachDua, NguoiTao) 
                                VALUES (GETDATE(), @tong, @khach, @user);
                                SELECT SCOPE_IDENTITY();";

                    SqlCommand cmdHD = new SqlCommand(sqlLuuHD, con, trans);
                    cmdHD.Parameters.AddWithValue("@tong", tongTien);
                    cmdHD.Parameters.AddWithValue("@khach", khachDua);
                    cmdHD.Parameters.AddWithValue("@user", NVTrucCa.maNV);

                    int maHoaDon = Convert.ToInt32(cmdHD.ExecuteScalar());

                    string sqlLuuCT = "INSERT INTO ChiTietHoaDon (MaHD, MaSP, SoLuong, DonGia, ThanhTien, TenMon) VALUES (@mahd, @masp, @sl, @dg, @tt, @ten)";
                    string sqlUpdateKho = "UPDATE SanPham SET TonKho = TonKho - @sl WHERE MaSP = @masp";

                    foreach (DataGridViewRow row in dgvGioHang.Rows)
                    {
                        if (row.IsNewRow) continue;

                        string tenMon = row.Cells[0].Value.ToString();
                        int soLuong = Convert.ToInt32(row.Cells[1].Value);
                        int thanhTien = XoaDau(row.Cells[2].Value.ToString());
                        string maSP = row.Cells[3].Value.ToString();
                        int donGia = thanhTien / soLuong;

                        SqlCommand cmdCT = new SqlCommand(sqlLuuCT, con, trans);
                        cmdCT.Parameters.AddWithValue("@mahd", maHoaDon);
                        cmdCT.Parameters.AddWithValue("@masp", maSP);
                        cmdCT.Parameters.AddWithValue("@sl", soLuong);
                        cmdCT.Parameters.AddWithValue("@dg", donGia);
                        cmdCT.Parameters.AddWithValue("@tt", thanhTien);
                        cmdCT.Parameters.AddWithValue("@ten", tenMon);
                        cmdCT.ExecuteNonQuery();


                        SqlCommand cmdKho = new SqlCommand(sqlUpdateKho, con, trans);
                        cmdKho.Parameters.AddWithValue("@sl", soLuong);
                        cmdKho.Parameters.AddWithValue("@masp", maSP);
                        cmdKho.ExecuteNonQuery();
                    }

                    trans.Commit();

                    DialogResult dr = MessageBox.Show("Thanh toán thành công!\nBạn có muốn in hóa đơn không?", "Hoàn tất", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        inHoaDon(maHoaDon);
                    }
                    dgvGioHang.Rows.Clear();
                    TinhTongTien();
                    txtKhachDua.Clear();
                    TaiMenu();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show("Lỗi hệ thống: " + ex.Message);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvGioHang.Rows.Count == 0 || (dgvGioHang.Rows.Count == 1 && dgvGioHang.Rows[0].IsNewRow)) return;

            DialogResult xacNhan = MessageBox.Show("Xác nhận xóa tất cả?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (xacNhan == DialogResult.Yes)
            {
                dgvGioHang.Rows.Clear();
                TinhTongTien();
                txtKhachDua.Clear();
                lblThoiLai.Text = "0 VNĐ";
            }
        }

        private void txtKhachDua_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtKhachDua.Text))
                {
                    lblThoiLai.Text = "0 VNĐ";
                    return;
                }

                int tong = XoaDau(lblTongTien.Text);

                int khachDua = int.Parse(txtKhachDua.Text) * 1000;
                int thoiLai = khachDua - tong;

                if (tong > 0 && thoiLai > 0)
                {
                    lblThoiLai.Text = thoiLai.ToString("N0") + " VNĐ";
                }
                else
                {
                    lblThoiLai.Text = "0 VNĐ";
                }
            }
            catch (Exception)
            {
                txtKhachDua.Clear();
                txtKhachDua.Focus();
            }
        }

        private void dgvGioHang_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvGioHang.Rows[e.RowIndex].IsNewRow) return;

            string tenMon = dgvGioHang.Rows[e.RowIndex].Cells[0].Value.ToString();

            DialogResult result = MessageBox.Show(
                $"Xác nhận xóa món: {tenMon}?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                dgvGioHang.Rows.RemoveAt(e.RowIndex);

                TinhTongTien();

                if (!string.IsNullOrWhiteSpace(txtKhachDua.Text))
                {
                    txtKhachDua_TextChanged(null, null);
                }
            }
        }
    }
}
