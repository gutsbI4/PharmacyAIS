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
        private void Input_Text(object sender, TextInputEventArgs e)
        {
            if (e.Text == "") LoginButton.IsEnabled = false;
            else LoginButton.IsEnabled = true;
        }
    }
}
