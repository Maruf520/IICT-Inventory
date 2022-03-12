using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using IICT_Store.Models;
using Microsoft.EntityFrameworkCore;

namespace IICT_Store.Repositories.TestRepo
{
    public class BaseRepo : IBaseRepo
    {
        private readonly IICT_StoreDbContext context;
        // private DbSet<T> entities;
        public BaseRepo(IICT_StoreDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<T> GetAll<T>() where T : BaseModel
        {
            throw new System.NotImplementedException();
        }

        public T GetById<T>(long id) where T : BaseModel
        {
            DbSet<T> dbSet = context.Set<T>();
            T entity = dbSet.Find(id);
            return entity;
        }



        public void Insert<T>(T entity) where T : BaseModel
        {
            DbSet<T> dbSet = context.Set<T>();
            context.SaveChanges();
        }

        public void Update<T>(T entity) where T : BaseModel
        {
            throw new System.NotImplementedException();
        }

        public void Delete<T>(long id) where T : BaseModel
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<T> GetItems<T>(Expression<Func<T, bool>> filter) where T : BaseModel
        {
            DbSet<T> dbSet = context.Set<T>();
            IQueryable<T> items = dbSet.Where(filter);
            return items;
        }
    }
}