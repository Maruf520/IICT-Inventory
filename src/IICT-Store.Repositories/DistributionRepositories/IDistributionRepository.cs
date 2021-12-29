﻿using IICT_Store.Models.Products;
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
    }
}
