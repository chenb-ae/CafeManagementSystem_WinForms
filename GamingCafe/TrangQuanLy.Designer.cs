namespace GamingCafe
{
    partial class TrangQuanLy
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabctrlChung = new System.Windows.Forms.TabControl();
            this.tabSP = new System.Windows.Forms.TabPage();
            this.tabNV = new System.Windows.Forms.TabPage();
            this.tabThongKe = new System.Windows.Forms.TabPage();
            this.tabctrlChung.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabctrlChung
            // 
            this.tabctrlChung.Controls.Add(this.tabSP);
            this.tabctrlChung.Controls.Add(this.tabNV);
            this.tabctrlChung.Controls.Add(this.tabThongKe);
            this.tabctrlChung.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabctrlChung.ItemSize = new System.Drawing.Size(120, 40);
            this.tabctrlChung.Location = new System.Drawing.Point(14, 18);
            this.tabctrlChung.Name = "tabctrlChung";
            this.tabctrlChung.SelectedIndex = 0;
            this.tabctrlChung.Size = new System.Drawing.Size(1281, 1107);
            this.tabctrlChung.TabIndex = 0;
            this.tabctrlChung.SelectedIndexChanged += new System.EventHandler(this.tabctrlChung_SelectedIndexChanged);
            // 
            // tabSP
            // 
            this.tabSP.Location = new System.Drawing.Point(4, 44);
            this.tabSP.Name = "tabSP";
            this.tabSP.Padding = new System.Windows.Forms.Padding(3);
            this.tabSP.Size = new System.Drawing.Size(1273, 1059);
            this.tabSP.TabIndex = 0;
            this.tabSP.Text = "Quản lý sản phẩm";
            this.tabSP.UseVisualStyleBackColor = true;
            // 
            // tabNV
            // 
            this.tabNV.Location = new System.Drawing.Point(4, 44);
            this.tabNV.Name = "tabNV";
            this.tabNV.Padding = new System.Windows.Forms.Padding(3);
            this.tabNV.Size = new System.Drawing.Size(1273, 1059);
            this.tabNV.TabIndex = 1;
            this.tabNV.Text = "Quản lý nhân viên";
            this.tabNV.UseVisualStyleBackColor = true;
            // 
            // tabThongKe
            // 
            this.tabThongKe.Location = new System.Drawing.Point(4, 44);
            this.tabThongKe.Name = "tabThongKe";
            this.tabThongKe.Padding = new System.Windows.Forms.Padding(3);
            this.tabThongKe.Size = new System.Drawing.Size(1273, 1059);
            this.tabThongKe.TabIndex = 2;
            this.tabThongKe.Text = "Thống kê doanh thu";
            this.tabThongKe.UseVisualStyleBackColor = true;
            // 
            // TrangQuanLy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.tabctrlChung);
            this.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "TrangQuanLy";
            this.Padding = new System.Windows.Forms.Padding(14, 18, 14, 18);
            this.Size = new System.Drawing.Size(1309, 1143);
            this.tabctrlChung.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabctrlChung;
        private System.Windows.Forms.TabPage tabSP;
        private System.Windows.Forms.TabPage tabNV;
        private System.Windows.Forms.TabPage tabThongKe;
    }
}
