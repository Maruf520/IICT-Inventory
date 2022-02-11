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
        Task<List<Product>> GetProductByCategoryId(long id);
        Task<List<ProductNo>> GetAllProductNo();
        Task<List<ProductNo>> GetAllProductNoById(long id);
        Task<ProductNo> GetProductNoById(long id);
         void RemoveProductNo(long id);

        
    }
}
