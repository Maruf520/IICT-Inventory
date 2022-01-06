using IICT_Store.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Repositories.ProductRepositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<Product> GetProductById(long id);
        Task<List<Product>> GetAllProduct();
        
    }
}
