using Avalonia.Controls;
using Avalonia.Input;

namespace PharmacyAIS.Views
{
    public partial class AuthorizeWindow : Window
    {
        public AuthorizeWindow()
        {
            InitializeComponent();
        }
        
        private void OnClick(object sender, PointerPressedEventArgs e)
        {
            this.Close();
        }
    }
}
