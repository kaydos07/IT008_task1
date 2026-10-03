using System.Globalization;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace WPFTest
{
    public partial class DetailWindow : Window
    {
        public DetailWindow(SinhVien sv)
        {
            InitializeComponent();

            this.DataContext = sv;
        }

        private void ChonAnh_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Tệp ảnh|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    // Sửa avatar của sinh viên đang được hiển thị.
                    SinhVien sv = (SinhVien)DataContext;
                    sv.Avatar = new BitmapImage(new Uri(dialog.FileName));
                }
                catch (Exception)
                {
                    MessageBox.Show("Không mở được ảnh. Vui lòng chọn ảnh khác.");
                }
            }
        }

        private void XoaAnh_Click(object sender, RoutedEventArgs e)
        {
            SinhVien sv = (SinhVien)DataContext;
            sv.Avatar = null;
        }

        private void BtnDong_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
