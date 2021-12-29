using IICT_Store.Models;
using IICT_Store.Models.Pruchashes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Repositories.PurchaseRepositories
{
    public class PurchaseRepository : BaseRepository<Purchashed>, IPurchaseRepository
    {
        private readonly IICT_StoreDbContext context;
        public PurchaseRepository(IICT_StoreDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Purchashed> GetPurchashedById(long id)
        {
            var purchase = await context.Purchasheds.Where(x => x.Id == id).Include(x => x.CashMemos).FirstOrDefaultAsync();
            return purchase;
        }
    }
}
