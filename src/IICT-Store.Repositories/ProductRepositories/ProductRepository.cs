using IICT_Store.Models;
using IICT_Store.Models.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Repositories.ProductRepositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly IICT_StoreDbContext context;
        public ProductRepository(IICT_StoreDbContext context) : base(context)
        {
            this.context = context;
        }

        public async  Task<IEnumerable<Product>> GetAllProduct()
        {
            var products =  context.Products.Include(a => a.Category).ToList();
            return products;
        }

        public Task<Product> GetProductById(long id)
        {
            var product = context.Products.Include(a => a.Category).FirstOrDefaultAsync(x => x.Id == id);
            return product;
        }
    }
}
