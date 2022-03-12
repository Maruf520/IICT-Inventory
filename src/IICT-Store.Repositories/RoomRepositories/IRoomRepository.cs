using System.Collections.Generic;
using IICT_Store.Models;

namespace IICT_Store.Repositories.RoomRepositories
{
    public interface IRoomRepository
    {
        public void Delete(long id);
        public IEnumerable<Room> GetAll();
        public Room GetById(long id);
        public void Insert(Room entity);
        public void Update(Room entity);

    }
}