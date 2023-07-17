using Avalonia;
using PharmacyAIS.Repositories.Interfaces;
using PharmacyAIS.Services;
using ReactiveUI;
using Splat;
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PharmacyAIS.ViewModels
{
    internal class AuthorizeViewModel : ViewModelBase
    {
        private readonly IWindowService _windowService;
        private readonly IUserRepository _userRepository;
        private string _login;
        public string Login
        {
            get => _login;
            set => this.RaiseAndSetIfChanged(ref _login, value);
        }
        private string _password;
        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }
        private string _result;
        public string Result
        {
            get => _result;
            set => this.RaiseAndSetIfChanged(ref _result, value);
        }

        public ReactiveCommand<Unit, Unit> AuthorizeCommand { get; }
        public AuthorizeViewModel()
        {
            AuthorizeCommand = ReactiveCommand.CreateFromObservable<Unit>(AuthorizeUser);
            _windowService = Locator.Current.GetService<IWindowService>();
            _userRepository = Locator.Current.GetService<IUserRepository>();
        }
        private IObservable<Unit> AuthorizeUser()
        {
            return Observable.Start( () =>
            {
                Result = String.Empty;
                Thread.Sleep(1000);
                var user = _userRepository.Login(Login, Password).GetAwaiter().GetResult();
                if (user != null)
                {
                    var windowViewModel = new MainWindowViewModel(user);
                    _windowService.OpenWindow(windowViewModel);
                }
                else
                {
                    Result = "Такого пользователя не существует";
                }
                
            });
        }
    }
}
