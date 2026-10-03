using System.Windows.Controls;
using System.Windows.Input;

namespace WPFTest
{
    public partial class SinhVienControl : UserControl
    {
    

        public SinhVienControl()
        {
            this.MouseLeftButtonDown += SinhVienControl_MouseLeftButtonDown;
            InitializeComponent();
        }

        public SinhVienControl(SinhVien sv)
            : this()
        {
            DataContext = sv;
        }
        public event EventHandler? SinhVienClicked;

        private void SinhVienControl_MouseLeftButtonDown(
            object sender,
            MouseButtonEventArgs e)
        {
            if(DataContext is not SinhVien)
                return;
            SinhVienClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
