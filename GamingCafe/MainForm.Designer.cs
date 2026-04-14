namespace GamingCafe
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblNV = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnTrangBanHang = new System.Windows.Forms.Button();
            this.btnLichSu = new System.Windows.Forms.Button();
            this.btnQuanLy = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.cmsNV = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsiDoiMK = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsiDangXuat = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.cmsNV.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(202, 853);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblNV);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 696);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 155);
            this.panel3.TabIndex = 5;
            // 
            // lblNV
            // 
            this.lblNV.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblNV.Location = new System.Drawing.Point(3, 0);
            this.lblNV.Name = "lblNV";
            this.lblNV.Size = new System.Drawing.Size(187, 62);
            this.lblNV.TabIndex = 0;
            this.lblNV.Text = "Holder";
            this.lblNV.Click += new System.EventHandler(this.lblNV_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnTrangBanHang);
            this.flowLayoutPanel1.Controls.Add(this.btnLichSu);
            this.flowLayoutPanel1.Controls.Add(this.btnQuanLy);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 106);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(200, 745);
            this.flowLayoutPanel1.TabIndex = 4;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // btnTrangBanHang
            // 
            this.btnTrangBanHang.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnTrangBanHang.Location = new System.Drawing.Point(3, 42);
            this.btnTrangBanHang.Margin = new System.Windows.Forms.Padding(3, 42, 3, 21);
            this.btnTrangBanHang.Name = "btnTrangBanHang";
            this.btnTrangBanHang.Size = new System.Drawing.Size(194, 87);
            this.btnTrangBanHang.TabIndex = 1;
            this.btnTrangBanHang.Text = "Trang bán hàng";
            this.btnTrangBanHang.UseVisualStyleBackColor = true;
            this.btnTrangBanHang.Click += new System.EventHandler(this.btnTrangBanHang_Click);
            // 
            // btnLichSu
            // 
            this.btnLichSu.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnLichSu.Location = new System.Drawing.Point(3, 171);
            this.btnLichSu.Margin = new System.Windows.Forms.Padding(3, 21, 3, 21);
            this.btnLichSu.Name = "btnLichSu";
            this.btnLichSu.Size = new System.Drawing.Size(194, 87);
            this.btnLichSu.TabIndex = 2;
            this.btnLichSu.Text = "Lịch sử bán hàng";
            this.btnLichSu.UseVisualStyleBackColor = true;
            this.btnLichSu.Click += new System.EventHandler(this.btnLichSu_Click);
            // 
            // btnQuanLy
            // 
            this.btnQuanLy.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnQuanLy.Location = new System.Drawing.Point(3, 300);
            this.btnQuanLy.Margin = new System.Windows.Forms.Padding(3, 21, 3, 21);
            this.btnQuanLy.Name = "btnQuanLy";
            this.btnQuanLy.Size = new System.Drawing.Size(194, 87);
            this.btnQuanLy.TabIndex = 4;
            this.btnQuanLy.Text = "Trang quản lý";
            this.btnQuanLy.UseVisualStyleBackColor = true;
            this.btnQuanLy.Visible = false;
            this.btnQuanLy.Click += new System.EventHandler(this.btnQuanLy_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.Controls.Add(this.pbLogo);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 106);
            this.panel2.TabIndex = 0;
            // 
            // pbLogo
            // 
            this.pbLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbLogo.Image = global::GamingCafe.Properties.Resources.logo__1___1_;
            this.pbLogo.Location = new System.Drawing.Point(0, 0);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(200, 106);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbLogo.TabIndex = 0;
            this.pbLogo.TabStop = false;
            // 
            // pnlMain
            // 
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(202, 0);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(9, 11, 9, 11);
            this.pnlMain.Size = new System.Drawing.Size(832, 853);
            this.pnlMain.TabIndex = 1;
            // 
            // cmsNV
            // 
            this.cmsNV.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cmsNV.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsNV.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsiDoiMK,
            this.cmsiDangXuat});
            this.cmsNV.Name = "cmsNV";
            this.cmsNV.Size = new System.Drawing.Size(202, 68);
            // 
            // cmsiDoiMK
            // 
            this.cmsiDoiMK.Name = "cmsiDoiMK";
            this.cmsiDoiMK.Size = new System.Drawing.Size(201, 32);
            this.cmsiDoiMK.Text = "Đổi mật khẩu";
            this.cmsiDoiMK.Click += new System.EventHandler(this.cmsiDoiMK_Click);
            // 
            // cmsiDangXuat
            // 
            this.cmsiDangXuat.Name = "cmsiDangXuat";
            this.cmsiDangXuat.Size = new System.Drawing.Size(201, 32);
            this.cmsiDangXuat.Text = "Đăng xuất";
            this.cmsiDangXuat.Click += new System.EventHandler(this.cmsiDangXuat_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 853);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.cmsNV.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnLichSu;
        private System.Windows.Forms.Button btnTrangBanHang;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnQuanLy;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblNV;
        private System.Windows.Forms.ContextMenuStrip cmsNV;
        private System.Windows.Forms.ToolStripMenuItem cmsiDoiMK;
        private System.Windows.Forms.ToolStripMenuItem cmsiDangXuat;
        private System.Windows.Forms.PictureBox pbLogo;
    }
}