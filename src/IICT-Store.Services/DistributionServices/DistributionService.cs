using AutoMapper;
using IICT_Store.Dtos.DistributionDtos;
using IICT_Store.Models;
using IICT_Store.Models.Products;
using IICT_Store.Repositories.DistributionRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Services.DistributionServices
{
    public class DistributionService : IDistributionService
    {
        private readonly IDistributionRepository distributionRepository;
        private readonly IMapper mapper;
        public DistributionService(IDistributionRepository distributionRepository, IMapper mapper)
        {
            this.distributionRepository = distributionRepository;
            this.mapper = mapper;
        }
        public async Task<ServiceResponse<GetDistributionDto>> Create(CreateDistributionDto createDistributionDto)
        {
            ServiceResponse<GetDistributionDto> response = new();
            List<ProductSerialNo> productSerialNos = new();
            foreach(var item in createDistributionDto.ProductSerialNo)
            {
                ProductSerialNo productSerialNo = new();
                productSerialNo.SerialNo = item.SerialNo;
                productSerialNo.CreatedAt = DateTime.Now;
                productSerialNos.Add(productSerialNo);

            }
            var distributionToCreate = mapper.Map<Distribution>(createDistributionDto);
            distributionToCreate.ProductSerialNo = productSerialNos;
            distributionRepository.Insert(distributionToCreate);
            var distributionToReturn = mapper.Map<GetDistributionDto>(createDistributionDto);
            response.Data = distributionToReturn;
            response.Messages.Add("Created.");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;
        }
    }
}
