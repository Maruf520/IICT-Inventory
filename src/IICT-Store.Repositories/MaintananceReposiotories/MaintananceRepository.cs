using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IICT_Store.Models;
using IICT_Store.Models.Products;

namespace IICT_Store.Repositories.MaintananceReposiotories
{
    public class MaintananceRepository: BaseRepository<MaintenanceProduct>, IMaintananceRepository
    {
        private readonly IICT_StoreDbContext context;

        public MaintananceRepository(IICT_StoreDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
