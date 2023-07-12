using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reactive.Linq;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using Avalonia.VisualTree;
using DynamicData;
using DynamicData.Binding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PharmacyAIS.Models;
using PharmacyAIS.Services;
using PharmacyAIS.Views;
using ReactiveUI;
using Splat;

namespace PharmacyAIS.ViewModels;

// TODO: Исправить проблему с сортировкой для DataGrid, путем добавления нового листа
public class ProductsViewModel:ViewModelBase
{
    private readonly IViewModelService _viewModelService;
    public ProductsViewModel()
    {
        _viewModelService = Locator.Current.GetService<IViewModelService>();
        Title = "Лекарства";
        _manufacturers = new ObservableCollection<FilterModel<Manufacturer>>(Locator.Current.GetService<DataBaseContext>()
            .Manufacturer
            .Select(p => new FilterModel<Manufacturer>() { isChecked = true, Value = p })
            .ToList());
        var manufacturersRename = _manufacturers.ToObservableChangeSet().AutoRefresh(f => f.isChecked)
            .Filter(f => f.isChecked).ToCollection().Select(ManufacturerFilterFunc);
        var searchFilter = this.WhenAnyValue(x => x.SearchString).Select(SearchFunc);
        var sort = this.WhenAnyValue(x => x.SortSelectedIndex).Select(index =>
        {
            switch (index)
            {
                case 0:
                    return SortExpressionComparer<Product>.Ascending(b => b.Name);
                case 1:
                    return SortExpressionComparer<Product>.Descending(b => b.Name);
                case 2:
                    return SortExpressionComparer<Product>.Ascending(b => b.Price);
                case 3:
                    return SortExpressionComparer<Product>.Descending(b => b.Price);
                default:
                    return SortExpressionComparer<Product>.Ascending(b => b.ProductId);
            }
        });
        _productsSource = new SourceList<Product>();
        _productsSource.Connect().Filter(manufacturersRename).Filter(searchFilter).Sort(sort).Bind(out _products).Subscribe();
        IList<Product> products = Locator.Current.GetService<DataBaseContext>().Product
            .Include(p => p.Unit)
            .ToList();
        _productsSource.AddRange(products);
        

    }

    private SourceList<Product> _productsSource;
    private ReadOnlyObservableCollection<Product> _products;
    
    public ReadOnlyObservableCollection<Product> Products => _products;

    private ObservableCollection<FilterModel<Manufacturer>> _manufacturers;
    public ObservableCollection<FilterModel<Manufacturer>> Manufacturers => _manufacturers;

    private int _sortSelectedIndex = -1;

    public int SortSelectedIndex
    {
        get=> _sortSelectedIndex;
        set => this.RaiseAndSetIfChanged(ref _sortSelectedIndex, value);
    }

    private string _searchString = String.Empty;

    public string SearchString
    {
        get => _searchString;
        set => this.RaiseAndSetIfChanged(ref _searchString, value);
    }
    private Product _selectedProduct;
    public Product SelectedProduct
    {
        get => _selectedProduct;
        set => this.RaiseAndSetIfChanged(ref _selectedProduct, value);
    }
    private Func<Product, bool> SearchFunc(string searchString)
    {
        return product => searchString == "" || product.Name.ToLower().Contains(searchString.ToLower()) || product.Manufacturer.Name.ToLower().Contains(searchString.ToLower());
    }

    private Func<Product, bool> ManufacturerFilterFunc(IReadOnlyCollection<FilterModel<Manufacturer>> filterModels)
    {
        return products => filterModels.Count == 0 ||
                           filterModels.Select(p => p.Value.Name)
            .Contains(products.Manufacturer.Name);
    }
    public void EditRowCommand()
    {
        _viewModelService.ChangeContentViewModel(new EditProductViewModel(SelectedProduct));
    }
}