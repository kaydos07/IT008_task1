using System.Windows.Controls;

namespace WPFTest
{
    public partial class SinhVienControl : UserControl
    {
        public string MaSinhVien { get; private set; } = "";
        public string HoTen { get; private set; } = "";
        public string GioiTinh { get; private set; } = "";
        public string Truong { get; private set; } = "";

        public SinhVienControl()
        {
            InitializeComponent();
        }

        public SinhVienControl(string maSinhVien, string hoTen, string gioiTinh, string truong)
            : this()
        {
            MaSinhVien = maSinhVien;
            HoTen = hoTen;
            GioiTinh = gioiTinh;
            Truong = truong;

            txtHoTen.Text = hoTen;
            txtThongTin.Text = "Mã SV: " + maSinhVien + "   |   Giới tính: " + gioiTinh;
            txtTruong.Text = "Trường: " + truong;
        }
    }
}
