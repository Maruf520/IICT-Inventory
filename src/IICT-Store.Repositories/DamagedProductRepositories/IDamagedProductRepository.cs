using IICT_Store.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Repositories.DamagedProductRepositories
{
    public interface IDamagedProductRepository : IBaseRepository<DamagedProduct>
    {
        Task<List<DamagedProduct>> GetAllDamagedProduct();
    }
}
