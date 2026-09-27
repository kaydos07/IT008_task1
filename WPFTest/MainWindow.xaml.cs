using System.Windows;

namespace WPFTest
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
    }
}
