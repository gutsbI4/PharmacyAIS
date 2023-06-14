using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform;
using Avalonia.Threading;
using PharmacyAIS.Services;
using PharmacyAIS.Views;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAIS.ViewModels
{
    internal class AuthorizeViewModel:ViewModelBase
    {
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
            set=>this.RaiseAndSetIfChanged(ref _result, value);
        }
        public bool IsExecuting => _isExecuting.Value;
        private ObservableAsPropertyHelper<bool> _isExecuting;

        public ReactiveCommand<Unit, Unit> AuthorizeCommand { get; }
        public AuthorizeViewModel()
        {
            AuthorizeCommand = ReactiveCommand.CreateFromObservable<Unit>(AuthorizeUser);
            AuthorizeCommand.IsExecuting.ToProperty(this, x => x.IsExecuting, out _isExecuting);
        }
        private IObservable<Unit> AuthorizeUser()
        {
            return Observable.Start(() =>
            {
                var db = Locator.Current.GetService<DataBaseContext>();
                var user = db.User.FirstOrDefault(x => x.Username == Login && x.Password == Password);
                if (user != null)
                {
                    Dispatcher.UIThread.InvokeAsync(() =>
                    {   
                        var window = new MainWindow(user);
                        window.Show();
                        ((IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime).MainWindow.Close();
                    });
                }
                else
                {
                    Result = "Такого пользователя не существует";
                }
            });
            
        }
    }
}
