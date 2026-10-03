using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace WPFTest
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<SinhVien> DanhSachSinhVien { get; set; } = new();
        private bool isDeleteMode = false;

        public MainWindow()
        {
            InitializeComponent();

            btnNhapLai.Click += NhapLai_Click;
            btnThemSinhVien.Click += ThemSinhVien_Click;

            lstSinhVien.ItemsSource = DanhSachSinhVien;
            lstSinhVien.MouseDoubleClick += lvSinhVien_MouseDoubleClick;
            lstSinhVien.PreviewMouseLeftButtonDown += lstSinhVien_PreviewMouseLeftButtonDown;
        }

        private void ChonAnh_Click(object sender, RoutedEventArgs e)
        {
            // Mở hộp thoại để người dùng chọn một tệp ảnh.
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Tệp ảnh|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    imgAvatar.Source = new BitmapImage(new Uri(dialog.FileName));
                }
                catch (Exception)
                {
                    MessageBox.Show("Không mở được ảnh. Vui lòng chọn ảnh khác.");
                }
            }
        }

        private void XoaAnh_Click(object sender, RoutedEventArgs e)
        {
            imgAvatar.Source = null;
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

            var sinhVien = new SinhVien
            {
                MaSinhVien = maSinhVien,
                HoTen = hoTen,
                GioiTinh = gioiTinh,
                Truong = truong,
                Avatar = imgAvatar.Source
            };

            DanhSachSinhVien.Add(sinhVien);

            MessageBox.Show("Đã thêm sinh viên");
            XoaForm();
        }
        private void lvSinhVien_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (isDeleteMode)
                return;

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
            imgAvatar.Source = null;
            txtMaSinhVien.Focus();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            SetDeleteMode(!isDeleteMode);

            if (isDeleteMode)
            {
                MessageBox.Show(
                    "Hãy bấm vào sinh viên bạn muốn xóa.",
                    "Chế độ xóa",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        private void lstSinhVien_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!isDeleteMode)
                return;

            DependencyObject? source = e.OriginalSource as DependencyObject;
            while (source != null && source is not ListViewItem)
            {
                source = VisualTreeHelper.GetParent(source);
            }

            if (source is not ListViewItem item || item.DataContext is not SinhVien selectedSinhVien)
                return;

            // Chặn sự kiện chọn/double-click để không mở cửa sổ chỉnh sửa khi đang xóa.
            e.Handled = true;

            MessageBoxResult result = MessageBox.Show(
                $"Bạn có chắc muốn xóa sinh viên:\n\n" +
                $"{selectedSinhVien.HoTen}\n" +
                $"MSSV: {selectedSinhVien.MaSinhVien}?",
                "Xác nhận xóa",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                DanhSachSinhVien.Remove(selectedSinhVien);
            }

            // Sau một lần chọn sinh viên để xác nhận, tự thoát chế độ xóa.
            SetDeleteMode(false);
        }

        private void SetDeleteMode(bool enabled)
        {
            isDeleteMode = enabled;
            btnDelete.Background = enabled ? Brushes.LightCoral : Brushes.White;
            btnDelete.ToolTip = enabled ? "Đang xóa - bấm lại để hủy" : "Xóa sinh viên";
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
