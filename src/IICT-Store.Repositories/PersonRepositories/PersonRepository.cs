﻿using IICT_Store.Models;
using IICT_Store.Models.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Repositories.PersonRepositories
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        private readonly IICT_StoreDbContext context;
        public PersonRepository(IICT_StoreDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<bool> GetByEmailAndPhone(string email, string phone)
        {
            var person =  context.Persons.Where(x => x.Email == email || x.Phone == phone).FirstOrDefault();
            if(person == null)
            {
                return true;
            }
            return false;
        }
    }
}
