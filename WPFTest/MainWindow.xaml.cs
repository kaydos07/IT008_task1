using System.Windows;
using System.Windows.Controls;

namespace WPFTest
{
    public partial class MainWindow : Window
    {
   
        public MainWindow()
        {
            InitializeComponent();

            btnNhapLai.Click += NhapLai_Click;
            btnThemSinhVien.Click += ThemSinhVien_Click;
            btnEdit.Click += BtnEdit_Click;

        }

       
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if(lstSinhVien.IsHitTestVisible)
            lstSinhVien.IsHitTestVisible = false;
            else
            lstSinhVien.IsHitTestVisible = true;
            
        }

       

        private void ThemSinhVien_Click(object sender, RoutedEventArgs e)
        {
            string maSinhVien = txtMaSinhVien.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            string truong = txtTruong.Text.Trim();

            if (maSinhVien == "" || hoTen == "" || truong == "")
            {
                MessageBox.Show("Vui lòng nhập đủ mã sinh viên, họ tên và trường.");
                return;
            }

            string gioiTinh = "Khác";
            if (radNam.IsChecked == true)
                gioiTinh = "Nam";
            else if (radNu.IsChecked == true)
                gioiTinh = "Nữ";

            MessageBox.Show("Đã thêm sinh viên");
            SinhVien newSinhVien = new SinhVien
            {
                MaSinhVien = maSinhVien,
                HoTen = hoTen,
                GioiTinh = gioiTinh,
                Truong = truong
            };

            SinhVienControl sinhVien = new SinhVienControl();
            sinhVien.DataContext = newSinhVien;
            sinhVien.SinhVienClicked += SinhVien_Clicked;
            lstSinhVien.Items.Add(newSinhVien);
            XoaForm();
        }
        private void SinhVien_Clicked(object? sender, SinhVien sinhvien)
        {
            if sender is not SinhVienControl svControl)
                return;
            txtHoTen.Text = sinhvien.HoTen;
            txtMaSinhVien.Text = sinhvien.MaSinhVien;
            txtTruong.Text = sinhvien.Truong;
            if (sinhvien.GioiTinh == "Nam")
                radNam.IsChecked = true;
            else if (sinhvien.GioiTinh == "Nữ")
                radNu.IsChecked = true;
            else
                radKhac.IsChecked = true;
        }

        private void NhapLai_Click(object sender, RoutedEventArgs e)
        {
            XoaForm();
        }

        private void XoaForm()
        {
            txtMaSinhVien.Clear();
            txtHoTen.Clear();
            txtTruong.Clear();
            radNam.IsChecked = true;
            txtMaSinhVien.Focus();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            filterPopup.IsOpen = true;
        }

        private void btnApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            string name = txtFilterName.Text.Trim();
            string mssv = txtFilterMSSV.Text.Trim();

            string gender = "";
            if (filterNam.IsChecked == true)
                gender = "Nam";
            else if (filterNu.IsChecked == true)
                gender = "Nữ";
            else if (filterKhac.IsChecked == true)
                gender = "Khác";

            lstSinhVien.Items.Filter = item =>
            {
                if (item is not SinhVienControl sv)
                    return false;

                bool okName = name == "" ||
                    sv.HoTen.Contains(name, StringComparison.OrdinalIgnoreCase);
                bool okMssv = mssv == "" ||
                    sv.MaSinhVien.Contains(mssv, StringComparison.OrdinalIgnoreCase);
                bool okGender = gender == "" || sv.GioiTinh == gender;

                return okName && okMssv && okGender;
            };

            filterPopup.IsOpen = false;
        }

        private void btnClearFilter_Click(object sender, RoutedEventArgs e)
        {
            txtFilterName.Clear();
            txtFilterMSSV.Clear();
            filterNam.IsChecked = false;
            filterNu.IsChecked = false;
            filterKhac.IsChecked = false;

            lstSinhVien.Items.Filter = null;
            filterPopup.IsOpen = false;
        }
    }
}
