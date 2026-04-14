using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GamingCafe
{
    public partial class pnlQuanLySP : UserControl
    {
        FileInfo hinhDaChon = null;

        public pnlQuanLySP()
        {
            InitializeComponent();

            cbbLoai.SelectedIndex = 0;

            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnNgungBan.Enabled = false;
            btnHuy.Enabled = false;

            taiDanhSachSP();
        }

        public void taiDanhSachSP()
        {
            try
            {
                using (SqlConnection con = DBConnect.getConnection())
                {
                    string sql = "";

                    if (chkHienThiAll.Checked == true)
                    {
                        sql = "SELECT * FROM SanPham";
                    }
                    else
                    {

                        sql = "SELECT * FROM SanPham WHERE TrangThai = N'Còn bán'";
                    }

                    SqlDataAdapter da = new SqlDataAdapter(sql, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvSP.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách sản phẩm: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string taiMaSP()
        {
            string maSP = "SP01";

            try
            {
                using (SqlConnection con = DBConnect.getConnection())
                {
                    con.Open();
                    string sql = "SELECT TOP 1 MaSP FROM SanPham ORDER BY MaSP DESC";
                    SqlCommand cmd = new SqlCommand(sql, con);

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        string maCu = result.ToString();
                        int phanSo = int.Parse(maCu.Substring(2)) + 1;
                        maSP = $"SP{phanSo:D2}";
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Lỗi sinh mã tự động: " + e.Message);
            }
            return maSP;
        }

        public int xoaDau(string str)
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

        private void btnThem_Click(object sender, EventArgs e)
        {
            DialogResult xacNhan = MessageBox.Show("Xác nhận thêm?", "Xác nhận", MessageBoxButtons.YesNo);

            if (xacNhan == DialogResult.Yes)
            {
                string maSP = txtMaSP.Text;
                string tenSP = txtTenSP.Text.Trim();
                int donGia = xoaDau(txtDonGia.Text);
                int tonKho = int.Parse(txtTonKho.Text.Trim());
                string loai = cbbLoai.SelectedItem.ToString();
                string tenHinh = "default.png";

                try
                {
                    using (SqlConnection con = DBConnect.getConnection())
                    {
                        con.Open();

                        if (hinhDaChon != null)
                        {
                            tenHinh = hinhDaChon.Name;

                            string thuMucDich = Path.Combine(Application.StartupPath, "images");
                            if (!Directory.Exists(thuMucDich)) Directory.CreateDirectory(thuMucDich);

                            string fileDich = Path.Combine(thuMucDich, tenHinh);
                            File.Copy(hinhDaChon.FullName, fileDich, true);
                        }
                        else
                        {
                            MessageBox.Show("Vui lòng chọn hình ảnh sản phẩm!");
                            return;
                        }

                        string sql = "INSERT INTO SanPham (MaSP, TenSP, Gia, TonKho, Loai, HinhAnh) VALUES (@ma, @ten, @gia, @ton, @loai, @hinh)";
                        SqlCommand cmd = new SqlCommand(sql, con);

                        cmd.Parameters.AddWithValue("@ma", maSP);
                        cmd.Parameters.AddWithValue("@ten", tenSP);
                        cmd.Parameters.AddWithValue("@gia", donGia);
                        cmd.Parameters.AddWithValue("@ton", tonKho);
                        cmd.Parameters.AddWithValue("@loai", loai);
                        cmd.Parameters.AddWithValue("@hinh", tenHinh);

                        int ketQua = cmd.ExecuteNonQuery();
                        if (ketQua > 0)
                        {
                            MessageBox.Show("Đã thêm sản phẩm thành công");
                        }

                        taiDanhSachSP();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi thêm sản phẩm: " + ex.Message);
                }
            }
        }

        private void btnTaiLen_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileChooser = new OpenFileDialog();

            fileChooser.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

            if (fileChooser.ShowDialog() == DialogResult.OK)
            {
                hinhDaChon = new FileInfo(fileChooser.FileName);

                pbHinh.Image = Image.FromFile(hinhDaChon.FullName);

                pbHinh.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void dgvSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnThem.Enabled = false;
                btnSua.Enabled = true;
                btnNgungBan.Enabled = true;
                btnHuy.Enabled = true;

                int dongDaChon = e.RowIndex;
                DataGridViewRow row = dgvSP.Rows[dongDaChon];

                string trangThai = row.Cells["TrangThai"].Value?.ToString();
                if (trangThai == "Ngừng bán")
                {
                    btnNgungBan.Text = "Bán lại";
                }
                else
                {
                    btnNgungBan.Text = "Ngừng bán";
                }

                txtMaSP.Text = row.Cells[0].Value?.ToString();
                txtTenSP.Text = row.Cells[1].Value?.ToString();

                string giaRaw = row.Cells[2].Value?.ToString() ?? "";
                txtDonGia.Text = giaRaw.Replace(".", "").Replace(" VNĐ", "");

                txtTonKho.Text = row.Cells[3].Value?.ToString();
                cbbLoai.SelectedItem = row.Cells[4].Value?.ToString();

                string tenHinh = row.Cells[5].Value?.ToString() ?? "default.png";

                string path = Path.Combine(Application.StartupPath, "images", tenHinh);

                try
                {
                    if (File.Exists(path))
                    {
                        if (pbHinh.Image != null) pbHinh.Image.Dispose();

                        using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                        {
                            pbHinh.Image = Image.FromStream(fs);
                        }
                        pbHinh.SizeMode = PictureBoxSizeMode.Zoom;
                        pbHinh.Text = "";
                    }
                    else
                    {
                        string defaultPath = Path.Combine(Application.StartupPath, "images", "default.png");
                        if (File.Exists(defaultPath))
                        {
                            pbHinh.Image = Image.FromFile(defaultPath);
                        }
                        else
                        {
                            pbHinh.Image = null;
                            pbHinh.Text = "Không có ảnh";
                        }
                    }
                }
                catch (Exception ex)
                {
                    pbHinh.Text = "Lỗi tải ảnh";
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Xác nhận sửa thông tin sản phẩm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string maSP = txtMaSP.Text;
                string tenSP = txtTenSP.Text.Trim();
                int donGia = xoaDau(txtDonGia.Text);
                int tonKho = int.Parse(txtTonKho.Text.Trim());
                string loai = cbbLoai.SelectedItem.ToString();
                string tenHinh = "default";

                if (hinhDaChon != null)
                {
                    tenHinh = hinhDaChon.Name;
                    try
                    {
                        string thuMucDich = Path.Combine(Application.StartupPath, "images");
                        if (!Directory.Exists(thuMucDich)) Directory.CreateDirectory(thuMucDich);

                        string fileDich = Path.Combine(thuMucDich, tenHinh);
                        File.Copy(hinhDaChon.FullName, fileDich, true);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi copy hình: " + ex.Message);
                    }
                }
                else
                {
                    if (dgvSP.CurrentRow != null)
                    {
                        tenHinh = dgvSP.CurrentRow.Cells[5].Value.ToString();
                    }
                }

                try
                {
                    using (SqlConnection con = DBConnect.getConnection())
                    {
                        con.Open();
                        string sql = "UPDATE SanPham SET TenSP=@ten, Gia=@gia, TonKho=@ton, Loai=@loai, HinhAnh=@hinh WHERE MaSP=@ma";

                        SqlCommand cmd = new SqlCommand(sql, con);
                        cmd.Parameters.AddWithValue("@ten", tenSP);
                        cmd.Parameters.AddWithValue("@gia", donGia);
                        cmd.Parameters.AddWithValue("@ton", tonKho);
                        cmd.Parameters.AddWithValue("@loai", loai);
                        cmd.Parameters.AddWithValue("@hinh", tenHinh);
                        cmd.Parameters.AddWithValue("@ma", maSP);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            hinhDaChon = null;
                            taiDanhSachSP();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi sửa sản phẩm: " + ex.Message);
                }
            }
        }

        

        private void btnHuy_Click(object sender, EventArgs e)
        {
            dgvSP.ClearSelection();

            txtMaSP.Text = taiMaSP();
            txtTenSP.Clear();
            txtDonGia.Clear();
            txtTonKho.Clear();
            cbbLoai.SelectedIndex = 0;

            pbHinh.Image = null;
            hinhDaChon = null;

            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnNgungBan.Enabled = false;
            btnHuy.Enabled = false;
        }

        private void NumberException_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnNgungBan_Click(object sender, EventArgs e)
        {
            {
                string maSP = txtMaSP.Text.Trim();
                string tenSP = txtTenSP.Text.Trim();

                string cauHoi = "";
                string trangThaiMoi = "";

                if (btnNgungBan.Text == "Bán lại")
                {
                    cauHoi = $"Bạn có muốn mở BÁN LẠI sản phẩm '{tenSP}' không?";
                    trangThaiMoi = "Còn bán";
                }
                else
                {
                    cauHoi = $"Bạn có chắc chắn muốn NGỪNG BÁN sản phẩm '{tenSP}' không?";
                    trangThaiMoi = "Ngừng bán";
                }

                DialogResult xacNhan = MessageBox.Show(cauHoi, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (xacNhan == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection con = DBConnect.getConnection())
                        {
                            con.Open();
                            string sql = "UPDATE SanPham SET TrangThai = @tt WHERE MaSP = @ma";
                            SqlCommand cmd = new SqlCommand(sql, con);

                            cmd.Parameters.AddWithValue("@tt", trangThaiMoi);
                            cmd.Parameters.AddWithValue("@ma", maSP);

                            int ketQua = cmd.ExecuteNonQuery();
                            if (ketQua > 0)
                            {
                                MessageBox.Show("Cập nhật trạng thái thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                taiDanhSachSP();

                                btnHuy.PerformClick();
                                btnNgungBan.Text = "Ngừng bán";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi cập nhật dữ liệu: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void chkHienThiAll_CheckedChanged(object sender, EventArgs e)
        {
            taiDanhSachSP();
            btnHuy_Click(sender, e);
        }
    }
}
