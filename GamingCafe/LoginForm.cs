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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.AcceptButton = btnDangNhap;
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {

            string user = txtDangNhap.Text;
            string pass = txtMatKhau.Text;

            try
            {
                using (SqlConnection con = DBConnect.getConnection())
                {
                    con.Open();
                    string sql = "SELECT * FROM NhanVien WHERE TenDangNhap = @user AND MatKhau = @pass AND TrangThai = N'Đang làm'";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@user", user);
                        cmd.Parameters.AddWithValue("@pass", pass);

                        using (SqlDataReader rs = cmd.ExecuteReader())
                        {
                            if (rs.Read())
                            {
                                NVTrucCa.maNV = rs["MaNV"].ToString();
                                NVTrucCa.vaiTro = rs["VaiTro"].ToString();
                                NVTrucCa.tenDangNhap = rs["TenDangNhap"].ToString();
                                NVTrucCa.ho = rs["Ho"].ToString();
                                NVTrucCa.ten = rs["Ten"].ToString();

                                MessageBox.Show(this, $"Đăng nhập thành công!\nXin chào {NVTrucCa.ten}");

                                this.DialogResult = DialogResult.OK;
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show(this, "Tên đăng nhập hoặc mật khẩu sai!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show(this, "Lỗi kết nối CSDL: " + ex.Message);
            }
        }
    }
}
