using System.Windows.Controls;

namespace WPFTest
{
    public partial class SinhVienControl : UserControl
    {
        public SinhVienControl()
        {
            InitializeComponent();
        }

        public SinhVienControl(string maSinhVien, string hoTen, string gioiTinh, string truong)
            : this()
        {  
            txtHoTen.Text = hoTen;
            txtThongTin.Text = "Mã SV: " + maSinhVien + "   |   Giới tính: " + gioiTinh;
            txtTruong.Text = "Trường: " + truong;
        }
    }
}
