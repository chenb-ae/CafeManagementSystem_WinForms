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
    public partial class DoiMKForm : Form
    {
        public DoiMKForm()
        {
            InitializeComponent();
        }

        private void txtMKXacNhan_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMKMoi.Text) && string.IsNullOrEmpty(txtMKXacNhan.Text))
            {
                lblThongBao.Text = "";
                btnXacNhan.Enabled = false;
                return;
            }

            if (txtMKMoi.Text != txtMKXacNhan.Text)
            {
                lblThongBao.Text = "Mật khẩu xác nhận không khớp!";
                lblThongBao.ForeColor = Color.Red;
                btnXacNhan.Enabled = false;
            }
            else
            {
                lblThongBao.Text = "Mật khẩu hợp lệ!";
                lblThongBao.ForeColor = Color.Green;
                btnXacNhan.Enabled = true;
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            string mkCu = txtMKCu.Text;
            string mkMoi = txtMKMoi.Text;

            if (string.IsNullOrEmpty(mkCu))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu hiện tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMKCu.Focus();
                return;
            }

            if (txtMKMoi.Text == txtMKCu.Text)
            {
                MessageBox.Show("Mật khẩu mới không được trùng với mật khẩu cũ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMKMoi.Clear();
                txtMKXacNhan.Clear();
                txtMKMoi.Focus();
                return;
            }

            using (SqlConnection con = DBConnect.getConnection())
            {
                con.Open();

                string checkSql = "SELECT COUNT(*) FROM NhanVien WHERE MaNV = @MaNV AND MatKhau = @MatKhauCu";
                using (SqlCommand cmdCheck = new SqlCommand(checkSql, con))
                {
                    cmdCheck.Parameters.AddWithValue("@MaNV", NVTrucCa.maNV);
                    cmdCheck.Parameters.AddWithValue("@MatKhauCu", mkCu);

                    int tonTai = (int)cmdCheck.ExecuteScalar();

                    if (tonTai == 0)
                    {
                        MessageBox.Show("Mật khẩu hiện tại không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtMKCu.Clear();
                        txtMKCu.Focus();
                        return;
                    }
                }

                string updateSql = "UPDATE NhanVien SET MatKhau = @MatKhauMoi WHERE MaNV = @MaNV";
                using (SqlCommand cmdUpdate = new SqlCommand(updateSql, con))
                {
                    cmdUpdate.Parameters.AddWithValue("@MatKhauMoi", mkMoi);
                    cmdUpdate.Parameters.AddWithValue("@MaNV", NVTrucCa.maNV);

                    int kq = cmdUpdate.ExecuteNonQuery();
                    if (kq > 0)
                    {
                        MessageBox.Show("Cập nhật mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra, vui lòng thử lại sau.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
