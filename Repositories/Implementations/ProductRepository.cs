using Microsoft.EntityFrameworkCore;
using PharmacyAIS.Models;
using PharmacyAIS.Repositories.Interfaces;
using PharmacyAIS.Services;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAIS.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataBaseContext _db;
        public ProductRepository(DataBaseContext dataBase)
        {
            _db = dataBase;
        }
        public async Task EditProduct(int productId, Product editingProduct)
        {
            var product = await _db.Product.FirstOrDefaultAsync(x => x.ProductId == productId);
            if (product != null)
            {
                product.Name = editingProduct.Name;
                var manufacturer = await _db.Manufacturer.FirstOrDefaultAsync(m => m.Name.ToLower() == editingProduct.Manufacturer.Name.ToLower());
                if (manufacturer == null)
                {
                    manufacturer = editingProduct.Manufacturer;
                    await _db.Manufacturer.AddAsync(manufacturer);
                }
                product.Manufacturer = manufacturer;
                product.Unit = editingProduct.Unit;
                product.Dosage = editingProduct.Dosage;
                product.Image = editingProduct.Image;
                product.QuantityInStock = editingProduct.QuantityInStock;
                product.Price = editingProduct.Price;
                await _db.SaveChangesAsync();
            }
        }
        public async Task<List<Product>> GetProducts()
        {
            return await _db.Product.Include(p=>p.Manufacturer)
                                    .Include(p=>p.Unit).ToListAsync();
        }
        public Task<List<Unit>> GetDosageUnits()
        {
            return  _db.Unit.ToListAsync();
        }
    }
}
