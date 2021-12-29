using IICT_Store.Dtos.DistributionDtos;
using IICT_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Services.DistributionServices
{
    public interface IDistributionService
    {
        Task<ServiceResponse<GetDistributionDto>> Create(CreateDistributionDto createDistributionDto);
        Task<ServiceResponse<GetDistributionDto>> GetById(long id);
        Task<ServiceResponse<List<GetDistributionDto>>> GetByRoomNo(int roomNo);
        Task<ServiceResponse<List<GetDistributionDto>>> GetByPersonId(long personId);
    }
}
