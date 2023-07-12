using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using PharmacyAIS.Views;

namespace PharmacyAIS.Services
{
    public class AvaloniaWindowService : IWindowService
    {
        public void OpenWindow<TViewModel>(TViewModel viewModel)
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                var window = new MainWindow
                {
                    DataContext = viewModel
                };
                window.Show();
                var beforeWindow = ((IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime).MainWindow;
                beforeWindow.Close();
                ((IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime).MainWindow = window;
            });
            
        }

    }
}
