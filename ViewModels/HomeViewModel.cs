using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using DynamicData;
using Microsoft.EntityFrameworkCore;
using PharmacyAIS.Models;
using PharmacyAIS.Services;
using ReactiveUI;
using Splat;

namespace PharmacyAIS.ViewModels;

public class HomeViewModel:ViewModelBase
{
    public HomeViewModel()
    {
        Title = "Главная";
        _ordersSource = new SourceList<Order>();
        _ordersSource.Connect().Filter(NoDoneOrderFunc()).Bind(out _orders).Subscribe();
        IList<Order> orders = Locator.Current.GetService<DataBaseContext>().Order
            .Include(p => p.Customer)
            .Include(p => p.Status)
            .Include(p => p.ProductOrder).ThenInclude(p => p.Product)
            .ToList();
            _ordersSource.AddRange(orders);

        _productSource = new SourceList<Product>();
        _productSource.Connect().Filter(EndProducts()).Bind(out _products).Subscribe();
        IList<Product> products = Locator.Current.GetService<DataBaseContext>().Product
            .Include(p=>p.Unit)
            .ToList();
        _productSource.AddRange(products);


    }

    private SourceList<Order> _ordersSource;
    private ReadOnlyObservableCollection<Order> _orders;
    public ReadOnlyObservableCollection<Order> Orders => _orders;

    private string _searchString = String.Empty;

    private SourceList<Product> _productSource;
    private ReadOnlyObservableCollection<Product> _products;
    public ReadOnlyObservableCollection<Product> Products => _products;

    private Func<Order, bool> NoDoneOrderFunc()
    {
        return order => order.Status.Name.Contains("принят");
    }

    private Func<Product, bool> EndProducts()
    {
        return product => product.QuantityInStock < 10;
    }


}