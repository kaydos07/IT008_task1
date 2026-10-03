using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace WPFTest
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<SinhVien> DanhSachSinhVien { get; set; } = new();

        public MainWindow()
        {
            InitializeComponent();

            btnNhapLai.Click += NhapLai_Click;
            btnThemSinhVien.Click += ThemSinhVien_Click;

            lstSinhVien.ItemsSource = DanhSachSinhVien;
            lstSinhVien.MouseDoubleClick += lvSinhVien_MouseDoubleClick;
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

            var sinhVien = new SinhVien { MaSinhVien = maSinhVien, HoTen = hoTen, GioiTinh = gioiTinh, Truong = truong };

            DanhSachSinhVien.Add(sinhVien);

            MessageBox.Show("Đã thêm sinh viên");
            XoaForm();
        }
        private void lvSinhVien_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lstSinhVien.SelectedItem is SinhVien selectedSinhVien)
            {
                DetailWindow detailWin = new DetailWindow(selectedSinhVien);

                detailWin.Owner = this;

                detailWin.ShowDialog();
            }
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


            ICollectionView view = CollectionViewSource.GetDefaultView(lstSinhVien.ItemsSource);
            view.Filter = item =>
            {
                if (item is not SinhVien sv) return false;

                bool okName = name == "" || sv.HoTen.Contains(name, StringComparison.OrdinalIgnoreCase);
                bool okMssv = mssv == "" || sv.MaSinhVien.Contains(mssv, StringComparison.OrdinalIgnoreCase);
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
