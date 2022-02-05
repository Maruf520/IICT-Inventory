using IICT_Store.Models;
using IICT_Store.Models.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Repositories.ProductNumberRepositories
{
    public class ProductNumberRepository : BaseRepository<ProductNo>, IProductNumberRepository
    {
        private readonly IICT_StoreDbContext context;
        public ProductNumberRepository(IICT_StoreDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<ProductNo>> GetByProductId(long id)
        {
            var productNos = await context.ProductNos.Where(x => x.ProductId == id).ToListAsync();
            return productNos;
        }
    }
}
