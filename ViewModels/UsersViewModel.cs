using DynamicData;
using PharmacyAIS.Models;
using PharmacyAIS.Services;
using ReactiveUI;
using Splat;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Linq;
using System.Reactive.Linq;
using Microsoft.EntityFrameworkCore;
using PharmacyAIS.Repositories.Interfaces;

namespace PharmacyAIS.ViewModels;

public class UsersViewModel:ViewModelBase
{
    private readonly IUserRepository _userRepository;
    public UsersViewModel()
    {
        _userRepository = Locator.Current.GetService<IUserRepository>();
        Title = "Пользователи";
        var searchFilter = this.WhenAnyValue(x => x.SearchString).Select(SearchFunc);
        _usersSource = new SourceList<User>();
        _usersSource.Connect().Filter(searchFilter).Bind(out _users).Subscribe();
        IList<User> users = _userRepository.GetUsers().GetAwaiter().GetResult();
        _usersSource.AddRange(users);


    }

    private SourceList<User> _usersSource;
    private ReadOnlyObservableCollection<User> _users;
    public ReadOnlyObservableCollection<User> Users => _users;

    private string _searchString = String.Empty;

    public string SearchString
    {
        get => _searchString;
        set => this.RaiseAndSetIfChanged(ref _searchString, value);
    }

    private Func<User, bool> SearchFunc(string searchString)
    {
        return user => user.UserId.ToString().Contains(searchString) || user.Employee.Position.Contains(searchString) ||
                                                                     user.Employee.Department.Name.Contains(
                                                                         searchString)
                                                                     || user.Employee.LastName.Contains(searchString) ||
                                                                     user.Employee.FirstName.Contains(searchString);
    }
}