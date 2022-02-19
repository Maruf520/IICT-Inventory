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
            var purchase = await context.Purchasheds.Include(x => x.CashMemos).Where(x => x.Id == id).FirstOrDefaultAsync();
            return purchase;
        }

        public async Task<List<Purchashed>> GetPendingPurchased()
        {
            var purchase = await context.Purchasheds.Include(x => x.CashMemos).Where(x => x.IsConfirmed == false && x.PurchaseStatus == PurchaseStatus.Pending).ToListAsync();
            return purchase;
        }
        public async Task<List<Purchashed>> GetRejectedPurchased()
        {
            var purchase = await context.Purchasheds.Include(x => x.CashMemos).Where(x => x.IsConfirmed == false && x.PurchaseStatus == PurchaseStatus.Rejected).ToListAsync();
            return purchase;
        }
        public async Task<List<Purchashed>> GetConfirmedPurchased()
        {
            var purchase = await context.Purchasheds.Include(x => x.CashMemos).Where(x => x.IsConfirmed == true && x.PurchaseStatus == PurchaseStatus.Confirmed).ToListAsync();
            return purchase;
        }

        public async Task<List<Purchashed>> GetPurchashedByProductId(long id)
        {
            var purchased = await context.Purchasheds.Where(x => x.ProductId == id).ToListAsync();
            return purchased;  
        }       
        public async Task<List<Purchashed>> GetPurchashedByDate(int year,PaymentBy paymentBy, PaymentProcess paymentProcess)
        {
            var purchased = await context.Purchasheds.Where(x => x.CreatedAt.Date.Year == year && x.PaymentBy == paymentBy && x.PaymentProcess == paymentProcess).ToListAsync();
            return purchased;
        }
    }
}
