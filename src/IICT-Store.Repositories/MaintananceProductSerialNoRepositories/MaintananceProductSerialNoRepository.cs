using IICT_Store.Models;
using IICT_Store.Models.Products;

namespace IICT_Store.Repositories.MaintananceProductSerialNoRepositories
{
    public class MaintananceProductSerialNoRepository: BaseRepository<MaintenanceProductSerialNo>, IMaintanancePeoductSerialNoRepository
    {
        private readonly IICT_StoreDbContext context;

        public MaintananceProductSerialNoRepository(IICT_StoreDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}