using IICT_Store.Models;
using IICT_Store.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Repositories.ProductSerialNoRepositories
{
    public class ProductSerialNoRepository : BaseRepository<ProductSerialNo>, IProductSerialNoRepository
    {
        private readonly IICT_StoreDbContext context;
        public ProductSerialNoRepository(IICT_StoreDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<ProductSerialNo> GetByProductNoId(long id)
        {
            var product = context.ProductSerialNos.Where(x => x.ProductNoId == id).FirstOrDefault();
            return product;
        }
    }
}
