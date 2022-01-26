﻿using IICT_Store.Models;
using IICT_Store.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Repositories.DamagedProductSerialRepositories
{
    public class DamagedProductSerialNoRepository : BaseRepository<DamagedProductSerialNo>, IDamagedProductSerialNoRepository
    {
        private readonly IICT_StoreDbContext context;
        public DamagedProductSerialNoRepository(IICT_StoreDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
