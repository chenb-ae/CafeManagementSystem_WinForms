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
    public partial class pnlQuanLyNV : UserControl
    {
        public pnlQuanLyNV()
        {
            InitializeComponent();

            cbbVaiTro.SelectedIndex = 0;

            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnChoNghi.Enabled = false;
            btnHuy.Enabled = false;

            taiDanhSachNV();
        }

        public void taiDanhSachNV()
        {
            try
            {
                using (SqlConnection con = DBConnect.getConnection())
                {
                    string sql = "";

                    if (chkHienThiAll.Checked == true)
                    {
                        sql = "SELECT * FROM NhanVien";
                    }
                    else
                    {
                        sql = "SELECT * FROM NhanVien WHERE TrangThai = N'Đang làm'";
                    }

                    SqlDataAdapter da = new SqlDataAdapter(sql, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvNV.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi! Không tải được dữ liệu nhân viên: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string taiMaNV()
        {
            string maNV = "NV01";
            try
            {
                using (SqlConnection con = DBConnect.getConnection())
                {
                    con.Open();
                    string sql = "SELECT TOP 1 MaNV FROM NhanVien ORDER BY MaNV DESC";
                    SqlCommand cmd = new SqlCommand(sql, con);

                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        string maCu = result.ToString();
                        int phanSo = int.Parse(maCu.Substring(2)) + 1;
                        maNV = $"NV{phanSo:D2}";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi sinh mã tự động: " + ex.Message);
            }
            return maNV;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            DialogResult xacNhan = MessageBox.Show("Xác nhận thêm nhân viên mới?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (xacNhan == DialogResult.Yes)
            {
                string maNV = txtMaNV.Text;
                string tenDangNhap = txtTenDangNhap.Text.Trim();
                string matKhau = txtMatKhau.Text;
                string ho = txtHo.Text.Trim();
                string ten = txtTen.Text.Trim();
                string vaiTro = cbbVaiTro.SelectedItem?.ToString() ?? "Nhân viên";

                if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
                {
                    MessageBox.Show("Tên đăng nhập và Mật khẩu không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenDangNhap.Focus();
                    return;
                }

                try
                {
                    using (SqlConnection con = DBConnect.getConnection())
                    {
                        con.Open();
                        string sql = "INSERT INTO NhanVien (MaNV, TenDangNhap, MatKhau, Ho, Ten, VaiTro, TrangThai) " +
                                     "VALUES (@ma, @tendn, @mk, @ho, @ten, @vaitro, N'Đang làm')";

                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {
                            cmd.Parameters.AddWithValue("@ma", maNV);
                            cmd.Parameters.AddWithValue("@tendn", tenDangNhap);
                            cmd.Parameters.AddWithValue("@mk", matKhau);
                            cmd.Parameters.AddWithValue("@ho", ho);
                            cmd.Parameters.AddWithValue("@ten", ten);
                            cmd.Parameters.AddWithValue("@vaitro", vaiTro);

                            int ketQua = cmd.ExecuteNonQuery();

                            if (ketQua > 0)
                            {
                                MessageBox.Show("Đã thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                taiDanhSachNV();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi thêm nhân viên: " + ex.Message);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            DialogResult xacNhan = MessageBox.Show("Xác nhận sửa thông tin nhân viên?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (xacNhan == DialogResult.Yes)
            {
                string maNV = txtMaNV.Text;
                string matKhauMoi = txtMatKhau.Text.Trim();
                string ho = txtHo.Text.Trim();
                string ten = txtTen.Text.Trim();
                string soDienThoai = txtSDT.Text.Trim();
                string vaiTro = cbbVaiTro.SelectedItem?.ToString() ?? "Nhân viên";

                try
                {
                    using (SqlConnection con = DBConnect.getConnection())
                    {
                        con.Open();
                        string sql = "";
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = con;

                        if (string.IsNullOrEmpty(matKhauMoi))
                        {
                            sql = "UPDATE NhanVien SET Ho=@ho, Ten=@ten, VaiTro=@vaitro WHERE MaNV=@ma";
                        }
                        else
                        {
                            sql = "UPDATE NhanVien SET MatKhau=@mk, Ho=@ho, Ten=@ten, SoDienThoai=@sdt, VaiTro=@vaitro WHERE MaNV=@ma";
                            cmd.Parameters.AddWithValue("@mk", matKhauMoi);
                        }

                        cmd.CommandText = sql;
                        cmd.Parameters.AddWithValue("@ho", ho);
                        cmd.Parameters.AddWithValue("@ten", ten);
                        cmd.Parameters.AddWithValue("@sdt", soDienThoai);
                        cmd.Parameters.AddWithValue("@vaitro", vaiTro);
                        cmd.Parameters.AddWithValue("@ma", maNV);

                        int ketQua = cmd.ExecuteNonQuery();
                        if (ketQua > 0)
                        {
                            MessageBox.Show("Sửa thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            taiDanhSachNV();
                            btnHuy.PerformClick();
                        }
                    }
                }
                catch (Exception ex) {
                    MessageBox.Show("Lỗi khi sửa thông tin: " + ex.Message);
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            dgvNV.ClearSelection();

            txtMaNV.Text = taiMaNV();
            txtTenDangNhap.Clear();
            txtMatKhau.Clear();
            txtHo.Clear();
            txtTen.Clear();
            txtSDT.Clear();
            cbbVaiTro.SelectedIndex = 0;

            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnHuy.Enabled = false;
            btnChoNghi.Enabled = false;
        }

        private void dgvNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNV.Rows[e.RowIndex];

                string trangThai = row.Cells["TrangThai"].Value?.ToString();
                if (trangThai == "Đã nghỉ")
                {
                    btnChoNghi.Text = "Làm lại";
                }
                else
                {
                    btnChoNghi.Text = "Cho nghỉ";
                }

                btnThem.Enabled = false;
                btnSua.Enabled = true;
                btnHuy.Enabled = true;
                btnChoNghi.Enabled = true;

                txtMaNV.Text = row.Cells["MaNV"].Value?.ToString();
                txtTenDangNhap.Text = row.Cells["TenDangNhap"].Value?.ToString();
                txtHo.Text = row.Cells["Ho"].Value?.ToString();
                txtTen.Text = row.Cells["Ten"].Value?.ToString();
                txtSDT.Text = row.Cells["SoDienThoai"].Value?.ToString();
                cbbVaiTro.SelectedItem = row.Cells["VaiTro"].Value?.ToString();

                txtMatKhau.Clear();
            }
        }

        private void btnChoNghi_Click(object sender, EventArgs e)
        {
            string maNV = txtMaNV.Text.Trim();
            string hoTen = txtHo.Text.Trim() + " " + txtTen.Text.Trim();

            string cauHoi = "";
            string trangThaiMoi = "";

            if (btnChoNghi.Text == "Làm lại")
            {
                cauHoi = $"Bạn có muốn cho nhân viên '{hoTen}' làm lại không?";
                trangThaiMoi = "Đang làm";
            }
            else
            {
                cauHoi = $"Bạn có chắc chắn muốn cho nhân viên '{hoTen}' nghỉ việc không?";
                trangThaiMoi = "Đã nghỉ";
            }

            DialogResult xacNhan = MessageBox.Show(cauHoi, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (xacNhan == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection con = DBConnect.getConnection())
                    {
                        con.Open();
                        string sql = "UPDATE NhanVien SET TrangThai = @tt WHERE MaNV = @ma";
                        SqlCommand cmd = new SqlCommand(sql, con);

                        cmd.Parameters.AddWithValue("@ma", maNV);
                        cmd.Parameters.AddWithValue("@tt", trangThaiMoi);

                        int ketQua = cmd.ExecuteNonQuery();
                        if (ketQua > 0)
                        {
                            MessageBox.Show("Cập nhật trạng thái thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            taiDanhSachNV();
                            btnHuy.PerformClick();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi cập nhật dữ liệu: " + ex.Message);
                }
            }
        }

        private void chkHienThiAll_CheckedChanged(object sender, EventArgs e)
        {
            taiDanhSachNV();
            btnHuy_Click(sender, e);
        }
    }
}
