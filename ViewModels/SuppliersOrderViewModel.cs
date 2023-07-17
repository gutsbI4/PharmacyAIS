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

public class SuppliersOrderViewModel:ViewModelBase
{
    private ISupplieRepository _supplierRepository;
    public SuppliersOrderViewModel()
    {
        _supplierRepository = Locator.Current.GetService<ISupplieRepository>();
        Title = "Заказы поставщикам";
        var searchFilter = this.WhenAnyValue(x => x.SearchString).Select(SearchFunc);
        _suppliesSource = new SourceList<Supplie>();
        _suppliesSource.Connect().Filter(searchFilter).Bind(out _supplies).Subscribe();
        IList<Supplie> supplies = _supplierRepository.GetSupplies().GetAwaiter().GetResult();
        _suppliesSource.AddRange(supplies);


    }

    private SourceList<Supplie> _suppliesSource;
    private ReadOnlyObservableCollection<Supplie> _supplies;
    public ReadOnlyObservableCollection<Supplie> Supplies => _supplies;

    private string _searchString = String.Empty;

    public string SearchString
    {
        get => _searchString;
        set => this.RaiseAndSetIfChanged(ref _searchString, value);
    }

    private Func<Supplie, bool> SearchFunc(string searchString)
    {
        return supplies => supplies.SupplieId.ToString().Contains(searchString) ||
                           supplies.Supplier.Name.Contains(searchString)
                           || supplies.SupplieProduct.Select(x=>x.Product.Name).Contains(searchString);
    }
}