using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using DynamicData;
using Microsoft.EntityFrameworkCore;
using PharmacyAIS.Models;
using PharmacyAIS.Repositories.Interfaces;
using PharmacyAIS.Services;
using ReactiveUI;
using Splat;

namespace PharmacyAIS.ViewModels;

public class HomeViewModel:ViewModelBase
{
    private readonly IClientOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    public HomeViewModel()
    {
        _orderRepository = Locator.Current.GetService<IClientOrderRepository>();
        _productRepository = Locator.Current.GetService<IProductRepository>();
        Title = "Главная";
        _ordersSource = new SourceList<Order>();
        _ordersSource.Connect().Filter(NoDoneOrderFunc()).Bind(out _orders).Subscribe();
        IList<Order> orders = _orderRepository.GetOrders().GetAwaiter().GetResult();
        _productSource = new SourceList<Product>();
        _productSource.Connect().Filter(EndProducts()).Bind(out _products).Subscribe();
        IList<Product> products = _productRepository.GetProducts().GetAwaiter().GetResult();
        _productSource.AddRange(products);
        _ordersSource.AddRange(orders);
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