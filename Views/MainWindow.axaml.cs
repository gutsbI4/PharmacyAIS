using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using PharmacyAIS.Models;
using PharmacyAIS.ViewModels;

namespace PharmacyAIS.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
           InitializeComponent();
        }
        public MainWindow(User user)
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(user);
        }

        private void Button_OnClick(object? sender, RoutedEventArgs e)
        {
            this.Close();
        }
         
        
    }
}