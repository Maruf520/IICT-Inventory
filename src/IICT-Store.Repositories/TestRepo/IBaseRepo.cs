using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using IICT_Store.Models;

namespace IICT_Store.Repositories.TestRepo
{
    public interface IBaseRepo
    {
        IEnumerable<T> GetAll<T>() where T : BaseModel;
        T GetById<T>(long id) where T : BaseModel;
        void Insert<T>(T entity)  where T : BaseModel;
        void Update<T>(T entity) where T : BaseModel;
        void Delete<T>(long id) where T : BaseModel;
        IQueryable<T> GetItems<T>(Expression<Func<T, bool>> filter) where T : BaseModel;
    }
}