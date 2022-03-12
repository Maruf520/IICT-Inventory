using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IICT_Store.Models;

namespace IICT_Store.Repositories.RoomRepositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly IICT_StoreDbContext context;
        public RoomRepository(IICT_StoreDbContext context)
        {
            this.context = context;
        }
        public void Delete(long id)
        {
            var entity = context.Rooms.Find(id);
            context.Remove(entity);
            context.SaveChanges();
        }

        public IEnumerable<Room> GetAll()
        {
            return context.Rooms.AsEnumerable();
        }

        public Room GetById(long id)
        {
            var entity = context.Rooms.FirstOrDefault(x => x.Id == id);
            return entity;
        }

        public void Insert(Room entity)
        {
            context.Rooms.Add(entity);
            context.SaveChanges();
        }

        public void Update(Room entity)
        {
            context.Rooms.Update(entity);
            context.SaveChanges();
        }
    }
}