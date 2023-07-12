using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Microsoft.EntityFrameworkCore;
using PharmacyAIS.Events;
using PharmacyAIS.Models;
using PharmacyAIS.Services;
using ReactiveUI;
using Splat;

namespace PharmacyAIS.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IViewModelService _viewModelService;
        public User User { get; set; }
        public List<ViewModelBase> MainMenuViewModels { get; set; }
        private ViewModelBase _selectedMenuViewModel;
        public ViewModelBase SelectedMenuViewModel
        {
            get => _selectedMenuViewModel;
            set => this.RaiseAndSetIfChanged(ref _selectedMenuViewModel, value);
        }
        private ViewModelBase _selectedContentViewModel;
        public ViewModelBase SelectedContentViewModel
        {
            get => _selectedContentViewModel;
            set => this.RaiseAndSetIfChanged(ref _selectedContentViewModel, value);
        }
        public MainWindowViewModel(User user)
        {
            Title = "Главная";
            _viewModelService = Locator.Current.GetService<IViewModelService>();
            MainMenuViewModels = _viewModelService.GetMainMenuViewModels().ToList();
            this.WhenAnyValue(x => x.SelectedMenuViewModel).Select(p =>
            {
                return SelectedContentViewModel = p;
            }).Subscribe();
            User = user;
            SelectedMenuViewModel = MainMenuViewModels[0];
            SelectedContentViewModel = MainMenuViewModels[0];
            MessageBus.Current.Listen<ContentViewModelChanged>()
                              .Subscribe(x => SelectedContentViewModel = x.NewContentViewModel);
        }
    }
}