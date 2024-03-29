﻿using IICT_Store.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Repositories.ProductNumberRepositories
{
    public interface IProductNumberRepository : IBaseRepository<ProductNo>
    {
        Task<List<ProductNo>> GetByProductId(long id);
    }
}
