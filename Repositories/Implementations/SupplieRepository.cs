using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PharmacyAIS.Models;
using PharmacyAIS.Repositories.Interfaces;
using PharmacyAIS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAIS.Repositories.Implementations
{
    public class SupplieRepository : ISupplieRepository
    {
        private readonly DataBaseContext _db;
        public SupplieRepository(DataBaseContext dataBaseContext)
        {
            _db = dataBaseContext;
        }
        public async Task<List<Supplie>> GetSupplies()
        {
            return await _db.Supplie
                        .Include(p => p.Supplier)
                        .Include(p => p.SupplieProduct).ThenInclude(p => p.Product)
                        .ToListAsync();
        }
    }
}
