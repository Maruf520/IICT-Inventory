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
        Task<ServiceResponse<GetDistributionDto>> CreateNew(CreateDistributionDto createDistributionDto);
        Task<ServiceResponse<GetDistributionDto>> GetById(long id);
        Task<ServiceResponse<List<GetDistributionDto>>> GetByRoomNo(int roomNo);
        Task<ServiceResponse<List<GetDistributionDto>>> GetByPersonId(long personId);
        Task<ServiceResponse<List<GetDistributionDto>>> GetAllDistributionByProductId(long productId);
        Task<ServiceResponse<GetDistributionDto>> GetDistributionByProductNoId(long id);

        Task<ServiceResponse<List<GetDistributionDto>>> GetDirstribution(long productId, long personId ,
            int roomNo);
    }
}
