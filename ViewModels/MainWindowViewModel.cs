using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PharmacyAIS.Models;
using PharmacyAIS.Services;
using ReactiveUI;
using Splat;

namespace PharmacyAIS.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public User User { get; set; }
        public List<ViewModelBase> MainMenuViewModels { get; set; }
        private ViewModelBase _selectedViewModel;
        public virtual ViewModelBase SelectedViewModel
        {
            get => _selectedViewModel;
            set => this.RaiseAndSetIfChanged(ref _selectedViewModel, value);
        }
        public MainWindowViewModel(User user)
        {
            Title = "Главная";
            MainMenuViewModels = new List<ViewModelBase>()
            {
                new HomeViewModel(),
                new ClientsOrderViewModel(),
                new ProductsViewModel(),
                new SuppliersOrderViewModel(),
                new UsersViewModel()
            };

            User = user;
            SelectedViewModel = MainMenuViewModels[0];
        }
    }
}