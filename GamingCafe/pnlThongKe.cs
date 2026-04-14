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
    public partial class pnlThongKe : UserControl
    {
        DateTime ngayMoQuan;

        public pnlThongKe()
        {
            InitializeComponent();
            ThietLapGioiHanThoiGian();

            radCoSan.Checked = true;
            cbbCoSan.SelectedItem = "Tháng này";

            btnXem.PerformClick();
        }

        public void taiThongKe()
        {
            btnXem.PerformClick();
        }

        private void capNhatTheTongQuan(DateTime tuNgay, DateTime denNgay)
        {
            try
            {
                using (SqlConnection con = DBConnect.getConnection())
                {
                    con.Open();

                    string sqlTong = @"SELECT COUNT(MaHD) AS TongSoBill, ISNULL(SUM(TongTien), 0) AS TongDoanhThu 
                               FROM HoaDon 
                               WHERE CAST(NgayTao AS DATE) >= @TuNgay AND CAST(NgayTao AS DATE) <= @DenNgay";

                    using (SqlCommand cmd = new SqlCommand(sqlTong, con))
                    {
                        cmd.Parameters.AddWithValue("@TuNgay", tuNgay.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@DenNgay", denNgay.ToString("yyyy-MM-dd"));
                        using (SqlDataReader rs = cmd.ExecuteReader())
                        {
                            if (rs.Read())
                            {
                                lblHD.Text = rs["TongSoBill"].ToString();
                                lblTong.Text = string.Format("{0:N0} VNĐ", rs["TongDoanhThu"]);
                            }
                        }
                    }
                    string sqlTopMon = @"SELECT TOP 1 TenMon 
                                 FROM ChiTietHoaDon 
                                 JOIN HoaDon ON ChiTietHoaDon.MaHD = HoaDon.MaHD
                                 WHERE CAST(NgayTao AS DATE) >= @TuNgay AND CAST(NgayTao AS DATE) <= @DenNgay
                                 GROUP BY TenMon 
                                 ORDER BY SUM(SoLuong) DESC";

                    using (SqlCommand cmd = new SqlCommand(sqlTopMon, con))
                    {
                        cmd.Parameters.AddWithValue("@TuNgay", tuNgay.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@DenNgay", denNgay.ToString("yyyy-MM-dd"));
                        object result = cmd.ExecuteScalar();
                        lblMon.Text = result != null ? result.ToString() : "Chưa có dữ liệu";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật tổng quan: " + ex.Message);
            }
        }

        private void veChartDoanhThu(DateTime tuNgay, DateTime denNgay, int soNgay)
        {
            chartDoanhThu.Series[0].Points.Clear();

            try
            {
                using (SqlConnection con = DBConnect.getConnection())
                {
                    con.Open();

                    string sqlGomNhom = "";
                    if (soNgay <= 31)
                    {
                        sqlGomNhom = "FORMAT(NgayTao, 'dd/MM/yyyy')";
                    }
                    else if (soNgay <= 120)
                    {
                        sqlGomNhom = "CONCAT(N'Tuần ', DATEPART(wk, NgayTao), ' - ', YEAR(NgayTao))";
                    }
                    else if (soNgay <= 730)
                    {
                        sqlGomNhom = "FORMAT(NgayTao, 'MM/yyyy')";
                    }
                    else
                    {
                        sqlGomNhom = "FORMAT(NgayTao, 'yyyy')";
                    }

                    string sqlDoanhThu = $@"SELECT {sqlGomNhom} AS ThoiGian, SUM(TongTien) AS DoanhThu 
                                    FROM HoaDon 
                                    WHERE CAST(NgayTao AS DATE) >= @TuNgay AND CAST(NgayTao AS DATE) <= @DenNgay
                                    GROUP BY {sqlGomNhom}
                                    ORDER BY MIN(NgayTao)";

                    using (SqlCommand cmd = new SqlCommand(sqlDoanhThu, con))
                    {
                        cmd.Parameters.AddWithValue("@TuNgay", tuNgay.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@DenNgay", denNgay.ToString("yyyy-MM-dd"));

                        using (SqlDataReader rs = cmd.ExecuteReader())
                        {
                            while (rs.Read())
                            {
                                chartDoanhThu.Series[0].Points.AddXY(rs["ThoiGian"].ToString(), Convert.ToDouble(rs["DoanhThu"]));
                            }
                        }
                    }

                    SetChartTitle(chartDoanhThu, $"Biểu đồ doanh thu từ {tuNgay:dd/MM/yyyy} đến {denNgay:dd/MM/yyyy}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi vẽ biểu đồ doanh thu: " + ex.Message);
            }
        }

        private void veChartTopMon(DateTime tuNgay, DateTime denNgay)
        {
            chartMon.Series[0].Points.Clear();

            try
            {
                using (SqlConnection con = DBConnect.getConnection())
                {
                    con.Open();

                    string sqlTop5 = @"SELECT TenMon, SUM(SoLuong) AS TongSoLuong 
                               FROM ChiTietHoaDon 
                               JOIN HoaDon ON ChiTietHoaDon.MaHD = HoaDon.MaHD
                               WHERE CAST(NgayTao AS DATE) >= @TuNgay AND CAST(NgayTao AS DATE) <= @DenNgay
                               GROUP BY TenMon 
                               ORDER BY TongSoLuong DESC";

                    using (SqlCommand cmd = new SqlCommand(sqlTop5, con))
                    {
                        cmd.Parameters.AddWithValue("@TuNgay", tuNgay.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@DenNgay", denNgay.ToString("yyyy-MM-dd"));

                        using (SqlDataReader rs = cmd.ExecuteReader())
                        {
                            int count = 0;
                            int tongKhac = 0;

                            while (rs.Read())
                            {
                                count++;
                                int soLuong = Convert.ToInt32(rs["TongSoLuong"]);
                                string tenMon = rs["TenMon"].ToString();

                                if (count <= 5)
                                {
                                    chartMon.Series[0].Points.AddXY(tenMon, soLuong);
                                }
                                else
                                {
                                    tongKhac += soLuong;
                                }
                            }

                            if (tongKhac > 0)
                            {
                                chartMon.Series[0].Points.AddXY("Khác", tongKhac);
                            }
                        }
                    }

                    SetChartTitle(chartMon, "Tỉ lệ món ăn bán chạy");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi vẽ biểu đồ món ăn: " + ex.Message);
            }
        }

        private void ThietLapGioiHanThoiGian()
        {
            try
            {
                using (SqlConnection con = DBConnect.getConnection())
                {
                    con.Open();

                    string sql = "SELECT MIN(NgayTao) FROM HoaDon";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            DateTime ngayMoQuan = Convert.ToDateTime(result).Date;
                            dtpTuNgay.MinDate = ngayMoQuan;
                            dtpDenNgay.MinDate = ngayMoQuan;

                            this.ngayMoQuan = ngayMoQuan;
                        }
                    }
                }
                dtpTuNgay.MaxDate = DateTime.Now.Date;
                dtpDenNgay.MaxDate = DateTime.Now.Date;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thiết lập giới hạn thời gian: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void thayDoiLoc(object sender, EventArgs e)
        {
            if (radCoSan.Checked)
            {
                cbbCoSan.Enabled = true;

                dtpTuNgay.Enabled = false;
                dtpDenNgay.Enabled = false;
            }
            else if (radTuyChinh.Checked)
            {
                cbbCoSan.Enabled = false;

                dtpTuNgay.Enabled = true;
                dtpDenNgay.Enabled = true;
            }
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            DateTime tuNgay;
            DateTime denNgay = DateTime.Now;

            if (radCoSan.Checked)
            {
                string luaChon = cbbCoSan.Text;

                if (luaChon == "7 ngày gần đây")
                {
                    tuNgay = DateTime.Now.AddDays(-7);
                }
                else if (luaChon == "Tháng này")
                {
                    tuNgay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                }
                else if (luaChon == "Năm này")
                {
                    tuNgay = new DateTime(DateTime.Now.Year, 1, 1);
                }
                else
                {
                    tuNgay = ngayMoQuan;
                }
            }
            else
            {
                tuNgay = dtpTuNgay.Value;
                denNgay = dtpDenNgay.Value;
            }

            TimeSpan khoangCach = denNgay.Date - tuNgay.Date;
            int soNgay = (int)khoangCach.TotalDays;

            veChartDoanhThu(tuNgay, denNgay, soNgay);
            veChartTopMon(tuNgay, denNgay);
            capNhatTheTongQuan(tuNgay, denNgay);
        }

        private void SetChartTitle(System.Windows.Forms.DataVisualization.Charting.Chart chart, string tieuDe)
        {
            chart.Titles.Clear();
            chart.Titles.Add(tieuDe);
            chart.Titles[0].Font = new Font("Arial", 12, FontStyle.Bold);
            chart.Titles[0].ForeColor = Color.DarkBlue;
        }
    }
}
