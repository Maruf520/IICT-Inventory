using IICT_Store.Models;
using IICT_Store.Models.Products;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ProductSerialNo> GetMaintananceProductByProductNoId(long id)
        {
            var product = context.ProductSerialNos.Where(x => x.ProductNoId == id && x.ProductStatus == ProductStatus.Maintanance).FirstOrDefault();
            return product;
        }
        public async Task<ProductSerialNo> GetAssignedProductSerialByProductNoId(long id)
        {
            var product = context.ProductSerialNos.Where(x => x.ProductNoId == id && x.ProductStatus == ProductStatus.Assigned || x.ProductStatus == ProductStatus.Maintanance).FirstOrDefault();
            return product;
        }
        public async Task<ProductSerialNo> GetByProductNoIdAndStatus(long id, ProductStatus productStatus)
        {
            var product = context.ProductSerialNos.Where(x => x.ProductNoId == id && x.ProductStatus == productStatus).FirstOrDefault();
            return product;
        }
        public async Task<List<ProductSerialNo>> GetProductNoIdByDistributionId(long id)
        {
            var productNoId = await context.ProductSerialNos.Where(x => x.DistributionId == id).ToListAsync();
            return productNoId;
        }
    }
}
