using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace WPFTest
{
    public class SinhVien : INotifyPropertyChanged
    {
        private string _maSinhVien = string.Empty;
        private string _hoTen = string.Empty;
        private string _gioiTinh = string.Empty;
        private string _truong = string.Empty;
        private ImageSource? _avatar;

        public ImageSource? Avatar
        {
            get => _avatar;
            set { _avatar = value; OnPropertyChanged(); }
        }

        public string MaSinhVien
        {
            get => _maSinhVien;
            set { _maSinhVien = value; OnPropertyChanged(); }
        }

        public string HoTen
        {
            get => _hoTen;
            set { _hoTen = value; OnPropertyChanged(); }
        }

        public string GioiTinh
        {
            get => _gioiTinh;
            set { _gioiTinh = value; OnPropertyChanged(); }
        }

        public string Truong
        {
            get => _truong;
            set { _truong = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
