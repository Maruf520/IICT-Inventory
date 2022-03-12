using IICT_Store.Dtos.PersonDtos;
using IICT_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Services.PersonServices
{
    public interface IPersonService 
    {
        Task<ServiceResponse<GetPersonDto>> CreatePerson(CreatePersonDto createPersonDto, string userId);
        Task<ServiceResponse<GetPersonDto>> GetPersonById(long id);
        Task<ServiceResponse<GetPersonDto>> UpdatePerson(long id, CreatePersonDto createPersonDto, string userId);
        Task<ServiceResponse<GetPersonDto>> DeletePerson(long id);
        Task<ServiceResponse<List<GetPersonDto>>> GetAllPerson();
    }
}
