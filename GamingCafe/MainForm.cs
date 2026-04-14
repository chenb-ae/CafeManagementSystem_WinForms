using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GamingCafe
{
    public partial class MainForm : Form
    {
        TrangBanHang trangBanHang = new TrangBanHang();
        LichSuBanHang trangLichSu = new LichSuBanHang();
        TrangQuanLy trangQuanLy = new TrangQuanLy();

        public MainForm()
        {
            InitializeComponent();

            lblNV.Text = "👤 " + NVTrucCa.ten + "\n(" + NVTrucCa.vaiTro + ") ▼";

            trangBanHang.Dock = DockStyle.Fill;
            trangLichSu.Dock = DockStyle.Fill;
            
            pnlMain.Controls.Add(trangBanHang);
            pnlMain.Controls.Add(trangLichSu);
            
            if (NVTrucCa.vaiTro == "Admin")
            {
                trangQuanLy.Dock = DockStyle.Fill;
                pnlMain.Controls.Add(trangQuanLy);
                btnQuanLy.Visible = true;
            }

            trangBanHang.BringToFront();
        }

        private void btnTrangBanHang_Click(object sender, EventArgs e)
        {
            trangBanHang.TaiMenu();
            trangBanHang.BringToFront();
        }

        private void btnLichSu_Click(object sender, EventArgs e)
        {
            trangLichSu.taiLichSu();
            trangLichSu.BringToFront();
        }

        private void btnQuanLy_Click(object sender, EventArgs e)
        {
            trangQuanLy.taiLaiTrang();
            trangQuanLy.BringToFront();
        }

        private void lblNV_Click(object sender, EventArgs e)
        {
            Point viTriMenu = new Point(0, lblNV.Height);
            cmsNV.Show(lblNV, viTriMenu);
        }

        private void cmsiDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult xacNhan = MessageBox.Show("Bạn có muốn đăng xuất khỏi hệ thống không?",
                                           "Đăng xuất",
                                           MessageBoxButtons.YesNo,
                                           MessageBoxIcon.Question);

            if (xacNhan == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void cmsiDoiMK_Click(object sender, EventArgs e)
        {
            DoiMKForm doiMKForm = new DoiMKForm();
            doiMKForm.ShowDialog();
        }
    }
}
