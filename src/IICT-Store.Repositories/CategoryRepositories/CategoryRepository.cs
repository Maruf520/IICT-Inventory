using IICT_Store.Models;
using IICT_Store.Models.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Repositories.CategoryRepositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly IICT_StoreDbContext context;
        public CategoryRepository(IICT_StoreDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
