using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using PharmacyAIS.Services;
using PharmacyAIS.ViewModels;
using PharmacyAIS.Views;
using Splat;

namespace PharmacyAIS
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            Locator.CurrentMutable.RegisterLazySingleton(() => new DataBaseContext(), typeof(DataBaseContext));
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new AuthorizeWindow
                {
                    DataContext = new AuthorizeViewModel(),
                    
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}