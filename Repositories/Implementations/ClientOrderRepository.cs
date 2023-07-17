using Microsoft.EntityFrameworkCore;
using PharmacyAIS.Models;
using PharmacyAIS.Repositories.Interfaces;
using PharmacyAIS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAIS.Repositories.Implementations
{
    public class ClientOrderRepository:IClientOrderRepository
    {
        private readonly DataBaseContext _db;
        public ClientOrderRepository(DataBaseContext dataBaseContext)
        {
            _db = dataBaseContext;
        }

        public async Task<List<Order>> GetOrders()
        {
            return await _db.Order.Include(p => p.Status)
                            .Include(p => p.Customer)
                            .Include(p => p.ProductOrder).ThenInclude(p => p.Product)
                            .ToListAsync();
        }
    }
}
