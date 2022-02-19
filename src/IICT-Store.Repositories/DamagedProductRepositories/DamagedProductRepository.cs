using IICT_Store.Models;
using IICT_Store.Models.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Repositories.DamagedProductRepositories
{
    public class DamagedProductRepository : BaseRepository<DamagedProduct>, IDamagedProductRepository
    {
        private readonly IICT_StoreDbContext context;
        public DamagedProductRepository(IICT_StoreDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<List<DamagedProduct>> GetAllDamagedProduct()
        {
            var products = await context.DamagedProducts.Include(x => x.DamagedProductSerialNos).ToListAsync();
            return products;
        }
    }
}
