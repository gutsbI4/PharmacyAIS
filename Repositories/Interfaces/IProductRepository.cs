using PharmacyAIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAIS.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task EditProduct(int productId,Product editingProduct);
    }
}
