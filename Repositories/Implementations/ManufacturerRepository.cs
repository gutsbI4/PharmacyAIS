using Microsoft.EntityFrameworkCore;
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
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly DataBaseContext _db;
        public ManufacturerRepository(DataBaseContext dataBaseContext)
        {
            _db = dataBaseContext;
        }
        public async Task<List<Manufacturer>> GetManufacturers()
        {
            return await _db.Manufacturer.ToListAsync();
        }
    }
}
