using System.Windows;

namespace WPFTest
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            btnNhapLai.Click += NhapLai_Click;
            btnThemSinhVien.Click += ThemSinhVien_Click;
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

            SinhVienControl sinhVien = new SinhVienControl(maSinhVien, hoTen, gioiTinh, truong);
            lstSinhVien.Items.Add(sinhVien);
            XoaForm();
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
