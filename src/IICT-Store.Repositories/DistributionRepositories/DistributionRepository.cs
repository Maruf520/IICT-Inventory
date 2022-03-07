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
            var distribution = await context.Distributions.Where(x => x.Id == id).FirstOrDefaultAsync();
            return distribution;
        }

        public async Task<List<Distribution>> GetDistributionByPerson(long personId)
        {
            var distribution = await context.Distributions.Where(x => x.ReceiverId == personId).ToListAsync();
            return distribution;
        }

        public async Task<List<Distribution>> GetDistributionByRoomNo(int roomNo)
        {
            var distribution = await context.Distributions.Where(x => x.RoomNo == roomNo).ToListAsync();
            return distribution;
        }

        public async Task<List<ProductSerialNo>> GetAllSerialNo()
        {
            var serialNos = await  context.ProductSerialNos.ToListAsync();
            return serialNos;
        }

        public async Task<ProductSerialNo> GetProductByProductNoId(long id)
        {
            var serial = await context.ProductSerialNos.Where(x => x.ProductNoId == id).FirstOrDefaultAsync();
            return serial;
        }

        public async Task<List<Distribution>> GetAllDistributionByProductId(long productId)
        {
            var distribution = await context.Distributions.Where(x => x.ProductId == productId).ToListAsync();
            return distribution;
        }
        public async Task<Distribution> GetByRoomIdAndProductId(int roomNo, long productId)
        {
            var distribution = await context.Distributions.Where(x => x.RoomNo == roomNo && x.ProductId == productId).FirstOrDefaultAsync();
            return distribution;
        }
        public async Task<Distribution> GetByProductId(long productId)
        {
            var distribution = await context.Distributions.Where(x => x.ProductId == productId).FirstOrDefaultAsync();
            return distribution;
        }
    }
}
