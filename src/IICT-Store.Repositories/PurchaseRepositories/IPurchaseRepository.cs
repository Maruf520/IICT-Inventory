using IICT_Store.Models.Pruchashes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Repositories.PurchaseRepositories
{
    public interface IPurchaseRepository : IBaseRepository<Purchashed>
    {
        Task<Purchashed> GetPurchashedById(long id);
    }
}
