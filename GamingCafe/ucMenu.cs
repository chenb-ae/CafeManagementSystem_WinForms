using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GamingCafe
{
    public partial class ucMenu : UserControl
    {
        private TrangBanHang gioHang;
        private int donGia;
        private int tonKho;
        private String maSP;

        public ucMenu(TrangBanHang gioHang, String tenMon, int gia, int tonKho, String tenHinh, String maSP)
        {
            InitializeComponent();

            this.gioHang = gioHang;

            this.donGia = gia;
            this.tonKho = tonKho;
            this.maSP = maSP;

            lblTenMon.Text = tenMon;
            lblGia.Text = gia.ToString("N0") + " VNĐ";

            ThemHinh(tenHinh);

            // Xử lý tồn kho
            if (tonKho <= 0)
            {
                btnThem.Enabled = false;
                btnThem.Text = "Hết hàng";
                btnThem.BackColor = Color.Gray;

                numSoLuong.Enabled = false;
            }
            else
            {
                numSoLuong.Minimum = 1;
                numSoLuong.Maximum = tonKho;
                numSoLuong.Value = 1;
            }
        }

        public void ThemHinh(string tenHinh)
        {
            try
            {
                string path = Path.Combine(Application.StartupPath, "images", tenHinh);

                if (File.Exists(path))
                {
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        picboxHinh.Image = Image.FromStream(fs);
                    }
                    picboxHinh.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    picboxHinh.Image = null;
                }
            }
            catch (Exception ex)
            {
                picboxHinh.Image = null;
                Console.WriteLine("Lỗi tải ảnh: " + ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string tenMon = lblTenMon.Text;
            int soLuong = (int)numSoLuong.Value;
            gioHang.ThemVaoGioHang(tenMon, soLuong, this.donGia, this.tonKho, this.maSP);
        }
    }
}
