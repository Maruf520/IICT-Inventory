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



        public async  Task<List<Product>> GetAllProduct()
        {
            var products = await context.Products.Include(x => x.Category).ToListAsync();
            return products;
        }

        public async Task<List<ProductNo>> GetAllProductNo()
        {
            var productNo = await context.ProductNos.ToListAsync();
            return productNo;
        }
        public async Task<List<ProductNo>> GetAllProductNoById(long id)
        {
            var productNo = await context.ProductNos.Where(x => x.ProductId == id).ToListAsync();
            return productNo;
        }

        public async Task<List<Product>> GetProductByCategoryId(long id)
        {
            var product = await context.Products.Include(x => x.Category).Where(x => x.CategoryId == id).ToListAsync();
            return product;
        }

        public Task<Product> GetProductById(long id)
        {
            var product = context.Products.Include(a => a.Category).FirstOrDefaultAsync(x => x.Id == id);
            return product;
        }

        public async Task<ProductNo> GetProductNoById(long id)
        {
            var product = await context.ProductNos.Where(x => x.Id == id).FirstOrDefaultAsync();
            return product;
        }

        public void RemoveProductNo(long id)
        {
           var product =  context.ProductNos.Where(x => x.Id == id).FirstOrDefault();
            context.Remove(product);
            context.SaveChanges();
        }
    }
}
