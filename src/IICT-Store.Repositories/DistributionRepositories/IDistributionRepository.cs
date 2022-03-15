using IICT_Store.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Repositories.DistributionRepositories
{
    public interface IDistributionRepository : IBaseRepository<Distribution>
    {
        Task<Distribution> GetDistributionById(long id);
        Task<List<Distribution>> GetDistributionByRoomNo(int roomNo);
        Task<List<Distribution>> GetDistributionByPerson(long personId);
        Task<List<Distribution>> GetAllDistributionByProductId(long productId);
        Task<List<ProductSerialNo>> GetAllSerialNo();
        Task<ProductSerialNo> GetProductByProductNoId(long id);
        Task<Distribution> GetByRoomIdAndProductId(int roomNo, long productId);
        Task<Distribution> GetByProductId(long productId);
        Task<ProductSerialNo> GetLastProductByProductNoId(long id);
    }
}
