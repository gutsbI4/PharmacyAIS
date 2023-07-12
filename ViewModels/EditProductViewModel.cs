using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using PharmacyAIS.Models;
using PharmacyAIS.Repositories.Implementations;
using PharmacyAIS.Repositories.Interfaces;
using PharmacyAIS.Services;
using PharmacyAIS.Views;
using ReactiveUI;
using Splat;
using System.Collections.Generic;
using System.Linq;

namespace PharmacyAIS.ViewModels
{
    public class EditProductViewModel : ViewModelBase
    {
        private readonly IViewModelService _viewModelService;
        private readonly IProductRepository _productRepository;
        private Product Product { get; set; }
        private Product _editingProduct;
        public Product EditingProduct
        {
            get => _editingProduct;
            set => this.RaiseAndSetIfChanged(ref _editingProduct, value);
        }
        private string _imageProductPath;
        public string ImageProductPath
        {
            get => _imageProductPath;
            set => this.RaiseAndSetIfChanged(ref _imageProductPath, value);
        }
        public List<Unit> Units { get; set; }
        public List<string> ProductsNames { get; set; }
        public List<string> ProductsManufacturers { get; set; }
        public EditProductViewModel(Product product)
        {
            _viewModelService = Locator.Current.GetService<IViewModelService>();
            _productRepository = Locator.Current.GetService<IProductRepository>();
            Product = product;
            EditingProduct = new Product
            {
                Name = product.Name,
                Manufacturer = product.Manufacturer,
                Unit = product.Unit,
                Dosage = product.Dosage,
                QuantityInStock = product.QuantityInStock,
                Price = product.Price,
                Image = product.Image
            };
            ImageProductPath = EditingProduct.Image;
            var db = Locator.Current.GetService<DataBaseContext>();
            Units = db.Unit.ToList();
            ProductsNames = db.Product.Select(p => p.Name).ToList();
            ProductsManufacturers = db.Manufacturer.Select(p => p.Name).ToList();
        }
        public void EditImage(string path)
        {
            EditingProduct.Image = path;
            ImageProductPath = path;
        }
        public void ConfirmEdit()
        {
            Product.Name = EditingProduct.Name;
            Product.Manufacturer = EditingProduct.Manufacturer;
            Product.Unit = EditingProduct.Unit;
            Product.Dosage = EditingProduct.Dosage;
            Product.Image = EditingProduct.Image;
            Product.QuantityInStock = EditingProduct.QuantityInStock;
            Product.Price = EditingProduct.Price;
            _productRepository.EditProduct(Product.ProductId,EditingProduct);
            _viewModelService.ChangeContentViewModel(_viewModelService.GetViewModel<ProductsViewModel>());
        }
        public void CancelEdit()
        {
            _viewModelService.ChangeContentViewModel(_viewModelService.GetViewModel<ProductsViewModel>());
        }
    }
}
