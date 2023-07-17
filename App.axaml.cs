using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using PharmacyAIS.Repositories.Implementations;
using PharmacyAIS.Repositories.Interfaces;
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

            Locator.CurrentMutable.RegisterLazySingleton(() => new ProductRepository(Locator.Current.GetService<DataBaseContext>()), typeof(IProductRepository));
            Locator.CurrentMutable.RegisterLazySingleton(()=> new UserRepository(Locator.Current.GetService<DataBaseContext>()), typeof(IUserRepository));
            Locator.CurrentMutable.RegisterLazySingleton(() => new ClientOrderRepository(Locator.Current.GetService<DataBaseContext>()), typeof(IClientOrderRepository));
            Locator.CurrentMutable.RegisterLazySingleton(() => new ManufacturerRepository(Locator.Current.GetService<DataBaseContext>()), typeof(IManufacturerRepository));
            Locator.CurrentMutable.RegisterLazySingleton(() => new SupplieRepository(Locator.Current.GetService<DataBaseContext>()), typeof(ISupplieRepository));

            Locator.CurrentMutable.RegisterLazySingleton(()=> new AvaloniaWindowService(),typeof(IWindowService));
            Locator.CurrentMutable.RegisterLazySingleton(()=>new ViewModelService(),typeof(IViewModelService));

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