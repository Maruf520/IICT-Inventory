using IICT_Store.Models.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Repositories.PersonRepositories
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        Task<bool> GetByEmailAndPhone(string email, string phone);
    }
}
