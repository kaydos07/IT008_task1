using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPFTest
{
    public partial class DetailWindow : Window
    {
        public DetailWindow(SinhVien sv)
        {
            InitializeComponent();

            this.DataContext = sv;
        }

        private void BtnDong_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}