using IICT_Store.Models;
using IICT_Store.Models.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Repositories.DistributionRepositories
{
    public class DistributionRepository : BaseRepository<Distribution>, IDistributionRepository
    {
        private readonly IICT_StoreDbContext context;
        public DistributionRepository(IICT_StoreDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Distribution> GetDistributionById(long id)
        {
            var distribution = await context.Distributions.Include(x => x.ProductSerialNo).Where(x => x.Id == id).FirstOrDefaultAsync();
            return distribution;
        }

        public async Task<List<Distribution>> GetDistributionByPerson(long personId)
        {
            var distribution = await context.Distributions.Include(x => x.ProductSerialNo).Where(x => x.ReceiverId == personId).ToListAsync();
            return distribution;
        }

        public async Task<List<Distribution>> GetDistributionByRoomNo(int roomNo)
        {
            var distribution = await context.Distributions.Include(x => x.ProductSerialNo).Where(x => x.RoomNo == roomNo).ToListAsync();
            return distribution;
        }

        public async Task<List<ProductSerialNo>> GetAllSerialNo()
        {
            var serialNos =  context.ProductSerialNos.ToList();
            return serialNos;
        }

        public async Task<ProductSerialNo> GetProductBySerialNo(long id)
        {
            var serial = context.ProductSerialNos.Where(x => x.ProductNoId == id).FirstOrDefault();
            return serial;
        }

        public async Task<List<Distribution>> GetAllDistributionByProductId(long productId)
        {
            var distribution = context.Distributions.Include(x => x.ProductSerialNo).Where(x => x.ProductId == productId).ToList();
            return distribution;
        }
    }
}
