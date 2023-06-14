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

namespace PharmacyAIS.ViewModels;

public class ClientsOrderViewModel:ViewModelBase
{
    public ClientsOrderViewModel()
    {
        Title = "Заказы клиентов";
        var searchFilter = this.WhenAnyValue(x => x.SearchString).Select(SearchFunc);
        _ordersSource = new SourceList<Order>();
        _ordersSource.Connect().Filter(searchFilter).Bind(out _orders).Subscribe();
        IList<Order> orders = Locator.Current.GetService<DataBaseContext>().Order
            .Include(p => p.Status)
            .Include(p=>p.Customer)
            .Include(p=>p.ProductOrder).ThenInclude(p=>p.Product)
            .ToList();
        _ordersSource.AddRange(orders);


    }

    private SourceList<Order> _ordersSource;
    private ReadOnlyObservableCollection<Order> _orders;
    public ReadOnlyObservableCollection<Order> Orders => _orders;

    private string _searchString = String.Empty;

    public string SearchString
    {
        get => _searchString;
        set => this.RaiseAndSetIfChanged(ref _searchString, value);
    }

    private Func<Order, bool> SearchFunc(string searchString)
    {
        return order => order.Customer.LastName.Contains(searchString) || order.OrderNumber.ToString().Contains(searchString);
    }
}