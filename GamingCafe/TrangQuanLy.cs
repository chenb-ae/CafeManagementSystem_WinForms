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
    public partial class TrangQuanLy : UserControl
    {
        pnlQuanLySP quanLySP;
        pnlQuanLyNV quanLyNV;
        pnlThongKe thongKe;

        public TrangQuanLy()
        {
            InitializeComponent();

            quanLySP = new pnlQuanLySP();
            quanLySP.Dock = DockStyle.Fill;
            quanLyNV = new pnlQuanLyNV();
            quanLyNV.Dock = DockStyle.Fill;
            thongKe = new pnlThongKe();
            thongKe.Dock = DockStyle.Fill;

            tabSP.Controls.Add(quanLySP);
            tabNV.Controls.Add(quanLyNV);
            tabThongKe.Controls.Add(thongKe);
        }

        public void taiLaiTrang()
        {
            if (tabctrlChung.SelectedTab == tabSP)
            {
                quanLySP.taiDanhSachSP();
            }
            else if (tabctrlChung.SelectedTab == tabNV)
            {
                quanLyNV.taiDanhSachNV();
            }
            else if (tabctrlChung.SelectedTab == tabThongKe)
            {
                thongKe.taiThongKe();
            }
        }

        private void tabctrlChung_SelectedIndexChanged(object sender, EventArgs e)
        {
            taiLaiTrang();
        }
    }
}
