﻿using BusinessLayer;
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
using TransferObject;
using BusinessLayer;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms.DataVisualization.Charting;

namespace PresentationLayer.Controllers
{
    public partial class HomeController : UserControl
    {
        public HomeController()
        {
            InitializeComponent();
        }

        //Khai báo sự kiện
        public event EventHandler CapNhatThongTin;


        NguoiDungBL nguoidungBL = new NguoiDungBL();

        ChucNangBL chucnangBL = new ChucNangBL();
        ChuyenBayBL chuyenbayBL = new ChuyenBayBL();
        string Oldpass;
        private void LoadThongTinUser(TaiKhoanNDTO User)
        {
            //Load combobox chức năng
            cbChucNang.DataSource = chucnangBL.GetChucNangList();
            cbChucNang.DisplayMember = "Ten";
            cbChucNang.ValueMember = "Id";
            //Load thông tin
            lbTenTK.Text = User.hoTenND;
            txtHoTen.Text = User.hoTenND;
            txtTenDN.Text = User.tenDangNhap;
            cbChucNang.SelectedIndex = User.ChucNangId -1;
            txtMatKhau.Text = User.matKhau;
            txtSoDT.Text = User.soDT;
            txtLinkAnh.Text = User.linkAnh;
            txtMail.Text = User.mail;
            Oldpass = User.matKhau;

            // Ảnh đại diện
            if (!string.IsNullOrEmpty(User.linkAnh) && File.Exists(User.linkAnh))
            {
                picAvatar.Image = Image.FromFile(User.linkAnh);
            }
            else
            {
                // Ảnh mặc định nếu không có
                picAvatar.Image = Properties.Resources.anh_avatar_cute_58; 
            }

        }
        private void HomeController_Load(object sender, EventArgs e)
        {
            

            if (CurrentUserTO.TaiKhoan != null)
            {
                LoadThongTinUser(CurrentUserTO.TaiKhoan);
            }

            // Thiết lập định dạng cho DateTimePicker
            datetimeNgayBay.Format = DateTimePickerFormat.Custom;
            datetimeNgayBay.CustomFormat = "MM/yyyy";
            datetimeNgayBay.ShowUpDown = true;

            DgvLoad();
            DrawChart();

        }

        private void DgvLoad()
        {
            dgvLichBay.DataSource = chuyenbayBL.GetChuyenBayListByNgay(datetimeNgayBay.Value);
            dgvLichBay.Columns["MaTB"].Visible = false;
            dgvLichBay.Columns["TienTrinhID"].Visible = false;
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string Path = openFileDialog.FileName;

                // Gán vào textbox đường dẫn
                txtLinkAnh.Text = Path;

                // Hiển thị ảnh
                picAvatar.Image = Image.FromFile(Path);
            }
        }

        //Hàm băm mật khẩu
        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(bytes);

                // Chuyển mảng byte thành chuỗi hex
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }
        private void btnCapNhatTT_Click(object sender, EventArgs e)
        {
            int chucnang = Convert.ToInt32(cbChucNang.SelectedValue);
            string hoTen = txtHoTen.Text;
            string tenDangNhap = txtTenDN.Text;
            string soDT = txtSoDT.Text;
            string linkAnh = txtLinkAnh.Text;
            string matKhau = txtMatKhau.Text;
            string mail = txtMail.Text;

            if(matKhau.Equals(Oldpass) == false)
            {
                matKhau = HashPassword(txtMatKhau.Text);
            }    
            try
            {
                if (chucnang == null || string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
                {
                    MessageBox.Show("Thông tin về Chức Năng, Tên Đăng Nhập và Mật Khẩu KHÔNG ĐƯỢC để trống!!! .");
                    return;
                }

                bool result = nguoidungBL.UpdateNuoiDung(CurrentUserTO.TaiKhoan.maND, hoTen, tenDangNhap,
                    chucnang, soDT, matKhau, linkAnh, mail);
                if (result)
                {
                    try
                    {
                        TaiKhoanNDTO acc = nguoidungBL.GetNguoiDungByID(CurrentUserTO.TaiKhoan.maND);
                        CurrentUserTO.TaiKhoan = acc;
                        MessageBox.Show("Cập nhật tài khoản người dùng thành công.");
                        LoadThongTinUser(CurrentUserTO.TaiKhoan);
                        CapNhatThongTin?.Invoke(this, EventArgs.Empty); //Gọi sự kiện nếu có sự lắng nghe đến nó

                    }
                    catch (SqlException ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Cập nhật tài khoản người dùng thất bại!");
                }
            }
            catch (SqlException ex)
            {

                MessageBox.Show("Cập nhật thất bại. LỖI: " + ex.Message);
            }
        }

        private void btnHuyThaoTac_Click(object sender, EventArgs e)
        {
            LoadThongTinUser(CurrentUserTO.TaiKhoan);
        }

        private void btnTimKiemLB_Click(object sender, EventArgs e)
        {
            DgvLoad();
            DrawChart();
        }

        // Draw Chart
        public void DrawChart()
        {
            // Chuẩn bị dữ liệu
            var data = chuyenbayBL.GetChuyenBayListByNgay(datetimeNgayBay.Value);
            var chartData = from x in data
                            group x by x.TenTB into g
                            select new
                            {
                                TuyenBay = g.Key,
                                SoChuyenBay = g.Count()
                            };

            // Xóa các phần tử cũ của chart
            chart_Home.Series.Clear();
            chart_Home.Titles.Clear();
            chart_Home.Legends.Clear();
            if (chart_Home.ChartAreas.IndexOf("HomeArea") >= 0)
            {
                chart_Home.ChartAreas.Remove(chart_Home.ChartAreas["HomeArea"]);
            }

            // Tạo biểu đồ dạng Bar
            Series series = new Series("SoChuyen");
            series.ChartType = SeriesChartType.Bar;
            series.BorderWidth = 1;
            series.Color = Color.SteelBlue;
            series.IsValueShownAsLabel = true; // Hiển thị giá trị trên các cột

            int sum = 0;
            foreach (var item in chartData)
            {
                sum += item.SoChuyenBay;
            }

            foreach (var item in chartData)
            {
                DataPoint dataPoint = new DataPoint();
                dataPoint.SetValueXY(item.TuyenBay, item.SoChuyenBay);
                dataPoint.Label = item.SoChuyenBay.ToString();
                dataPoint.LegendText = item.TuyenBay;

               
                series.Points.Add(dataPoint);
            }

            // Thêm series vào biểu đồ
            chart_Home.Series.Add(series);

            // Cấu hình tiêu đề
            chart_Home.Titles.Add("Thống kê Chuyến Bay Theo Tuyến");

            // Cấu hình trục X và Y
            ChartArea chartArea = new ChartArea("HomeArea");
            chartArea.Position.Height = 60;
            chartArea.AxisY.Title = "Số Chuyến Bay";
            chartArea.AxisX.Title = "Tuyến Bay";

            chart_Home.ChartAreas.Add(chartArea);

            // Thêm chú thích (legend)
            Legend legend = new Legend("Legend");
            legend.Docking = Docking.Right;
            legend.Alignment = StringAlignment.Center;  // Căn giữa chú thích
            chart_Home.Legends.Add(legend);

            // Cấu hình khoảng cách giữa các cột (bars)
            series["PointWidth"] = "0.8"; // Thay đổi độ rộng của các cột để chúng không bị quá sát nhau

        }
    }
}
