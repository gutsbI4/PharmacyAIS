using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using PharmacyAIS.Models;
using PharmacyAIS.Repositories.Implementations;
using PharmacyAIS.Repositories.Interfaces;
using PharmacyAIS.Services;
using PharmacyAIS.Views;
using ReactiveUI;
using ReactiveUI.Validation.Extensions;
using Splat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PharmacyAIS.ViewModels
{
    public class EditProductViewModel : ViewModelBase
    {
        private readonly IViewModelService _viewModelService;
        private readonly IProductRepository _productRepository;
        private string _name;
        private decimal _dosage;
        private int _quantityInStock;
        private decimal _price;
        private string _imageProductPath;
        private string _manufacturer;

        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }
        public decimal Dosage
        {
            get => _dosage;
            set => this.RaiseAndSetIfChanged(ref _dosage, value);
        }

        public int QuantityInStock
        {
            get => _quantityInStock;
            set => this.RaiseAndSetIfChanged(ref _quantityInStock, value);
        }

        public decimal Price
        {
            get => _price;
            set => this.RaiseAndSetIfChanged(ref _price, value);
        }
        public string Manufacturer
        {
            get => _manufacturer;
            set => this.RaiseAndSetIfChanged(ref _manufacturer, value);
        }
        private Product Product { get; set; }
        private Product _editingProduct;
        public Product EditingProduct
        {
            get => _editingProduct;
            set => this.RaiseAndSetIfChanged(ref _editingProduct, value);
        }
        public string ImageProductPath
        {
            get => _imageProductPath;
            set => this.RaiseAndSetIfChanged(ref _imageProductPath, value);
        }

        public List<Unit> Units { get; set; }
        public List<string> ProductsNames { get; set; }
        public List<string> ProductsManufacturers { get; set; }
        public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> ConfirmCommand { get; }
        public EditProductViewModel(Product product)
        {
            _viewModelService = Locator.Current.GetService<IViewModelService>();
            _productRepository = Locator.Current.GetService<IProductRepository>();
            Product = product;
            EditingProduct = new Product
            {
                Name = product.Name,
                Unit = product.Unit,
                Dosage = product.Dosage,
                QuantityInStock = product.QuantityInStock,
                Price = product.Price,
                Image = product.Image,
                Manufacturer = new Manufacturer { Name = product.Manufacturer.Name },
            };
            ImageProductPath = product.Image;
            Name = product.Name;
            Dosage = product.Dosage;
            QuantityInStock = product.QuantityInStock;
            Price = product.Price;
            Manufacturer = product.Manufacturer.Name;

            var db = Locator.Current.GetService<DataBaseContext>();
            Units = db.Unit.ToList();
            ProductsNames = db.Product.Select(p => p.Name).ToList();
            ProductsManufacturers = db.Manufacturer.Select(p => p.Name).ToList();

            ConfirmCommand = ReactiveCommand.CreateFromTask(ConfirmEdit, canExecute: this.IsValid());

            this.ValidationRule(vm => vm.Name, name => !string.IsNullOrWhiteSpace(name),"Поле не может быть пустым!");
            this.ValidationRule(vm => vm.Manufacturer, name => !string.IsNullOrWhiteSpace(name), "Поле не может быть пустым!");
            this.ValidationRule(vm => vm.Dosage, dosage => dosage != null && dosage > 0, "Поле не может быть пустым или нулевым!");
            this.ValidationRule(vm => vm.Price, price => price != null && price > 0, "Поле не может быть пустым или нулевым!");
            this.ValidationRule(vm => vm.QuantityInStock, quantity => quantity != null, "Поле не может быть пустым!");
        }
        public void EditImage(string path)
        {
            EditingProduct.Image = path;
            ImageProductPath = path;
        }
        private async Task ConfirmEdit()
        {
            Product.Name = Name;
            Product.Manufacturer = new Manufacturer { Name= Manufacturer};
            Product.Unit = EditingProduct.Unit;
            Product.Dosage = Dosage;
            Product.Image = ImageProductPath;
            Product.QuantityInStock = QuantityInStock;
            Product.Price = Price;
            EditingProduct = Product;
            await _productRepository.EditProduct(Product.ProductId, EditingProduct);
            _viewModelService.ChangeContentViewModel(_viewModelService.GetViewModel<ProductsViewModel>());
        }
        public void CancelEdit()
        {
            _viewModelService.ChangeContentViewModel(_viewModelService.GetViewModel<ProductsViewModel>());
        }
    }
}
