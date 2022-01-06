using AutoMapper;
using IICT_Store.Dtos.PersonDtos;
using IICT_Store.Models;
using IICT_Store.Models.Persons;
using IICT_Store.Repositories.PersonRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Services.PersonServices
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository personRepository;
        private readonly IMapper mapper;
        public PersonService(IPersonRepository personRepository, IMapper mapper)
        {
            this.personRepository = personRepository;
            this.mapper = mapper;
        }

        public async Task<ServiceResponse<GetPersonDto>> CreatePerson(CreatePersonDto createPersonDto)
        {
            ServiceResponse<GetPersonDto> response = new();
            var personToCreate = mapper.Map<Person>(createPersonDto);
            personToCreate.CreatedAt = DateTime.Now;
             personRepository.Insert(personToCreate);
            var personToReturn = mapper.Map<GetPersonDto>(createPersonDto);
            response.Data = personToReturn;
            response.Messages.Add("Person Created.");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;

        }

        public async Task<ServiceResponse<GetPersonDto>> DeletePerson(long id)
        {
            ServiceResponse<GetPersonDto> response = new();
            var person = personRepository.GetById(id);
            if (person == null)
            {
                response.Messages.Add("Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }

            personRepository.Delete(id);
            var PersonToReturn = mapper.Map<GetPersonDto>(person);
            response.Data = PersonToReturn;
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.Messages.Add("Deleted.");
            return response;
        }

        public async Task<ServiceResponse<List<GetPersonDto>>> GetAllPerson()
        {
            ServiceResponse<List<GetPersonDto>> response = new();
            var person = personRepository.GetAll();
            var personToMap = mapper.Map<List<GetPersonDto>>(person);
            response.Data = personToMap;
            response.Messages.Add("All Person");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;

        }

        public async Task<ServiceResponse<GetPersonDto>> GetPersonById(long id)
        {
            ServiceResponse<GetPersonDto> response = new();
            var person =  personRepository.GetById(id);
            if(person == null)
            {
                response.Messages.Add("Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }

            var personToReturn = mapper.Map<GetPersonDto>(person);
            response.Data = personToReturn;
            response.Messages.Add("Person.");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;
        }

        public async Task<ServiceResponse<GetPersonDto>> UpdatePerson(long id, CreatePersonDto createPersonDto)
        {
            ServiceResponse<GetPersonDto> response = new();
            var person = personRepository.GetById(id);
            if (person == null)
            {
                response.Messages.Add("Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }

            person.Name = createPersonDto.Name;
            person.Phone = createPersonDto.Phone;
            person.Designation = createPersonDto.Designation;
            person.UpdatedAt = DateTime.Now;
            person.Email = createPersonDto.Email;
            personRepository.Update(person);
            var personToReturn = mapper.Map<GetPersonDto>(createPersonDto);
            response.Data = personToReturn;
            response.Messages.Add("Peson Updated.");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;
        }
    }
}
