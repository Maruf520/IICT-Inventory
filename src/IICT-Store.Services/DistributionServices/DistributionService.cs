using AutoMapper;
using IICT_Store.Dtos.DistributionDtos;
using IICT_Store.Models;
using IICT_Store.Models.Products;
using IICT_Store.Repositories.DistributionRepositories;
using IICT_Store.Repositories.ProductRepositories;
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
        private readonly IProductRepository productRepository;
        public DistributionService(IDistributionRepository distributionRepository, IMapper mapper, IProductRepository productRepository)
        {
            this.distributionRepository = distributionRepository;
            this.mapper = mapper;
            this.productRepository = productRepository;
        }
        public async Task<ServiceResponse<GetDistributionDto>> Create(CreateDistributionDto createDistributionDto)
        {
            ServiceResponse<GetDistributionDto> response = new();
            var product = productRepository.GetById(createDistributionDto.ProductId);
            if(product == null)
            {
                response.Messages.Add("Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }

            if(product.QuantityInStock < createDistributionDto.Quantity)
            {
                response.Messages.Add($"Please reduce the quantity of this product. This product has {product.QuantityInStock} items in stock.");
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return response;
            }
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
            product.QuantityInStock = product.QuantityInStock - createDistributionDto.Quantity;
            productRepository.Update(product);
            distributionRepository.Insert(distributionToCreate);
            var distributionToReturn = mapper.Map<GetDistributionDto>(createDistributionDto);
            response.Data = distributionToReturn;
            response.Messages.Add("Created.");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;
        }

        public async Task<ServiceResponse<GetDistributionDto>> GetById(long id)
        {
            ServiceResponse<GetDistributionDto> response = new();
            var distribution = await distributionRepository.GetDistributionById(id);
            if(distribution == null)
            {
                response.Messages.Add("Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }

            var distributionToReturn = mapper.Map<GetDistributionDto>(distribution);
            response.Data = distributionToReturn;
            response.Messages.Add("Distribution");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;
        }

        public async Task<ServiceResponse<List<GetDistributionDto>>> GetByPersonId(long personId)
        {
            ServiceResponse<List<GetDistributionDto>> response = new();
            var distribution = await distributionRepository.GetDistributionByPerson(personId);
            if (distribution == null)
            {
                response.Messages.Add("Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }

            var distributionToReturn = mapper.Map<List<GetDistributionDto>>(distribution);
            response.Data = distributionToReturn;
            response.Messages.Add("All he takes.");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;

        }

        public async Task<ServiceResponse<List<GetDistributionDto>>> GetByRoomNo(int roomNo)
        {
            ServiceResponse<List<GetDistributionDto>> response = new();
            var product = await distributionRepository.GetDistributionByRoomNo(roomNo);
            if (product == null)
            {
                response.Messages.Add("Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            var map = mapper.Map<List<GetDistributionDto>>(product);
            response.Data = map;
            response.Messages.Add("All distribution");
            return response;
        }
    }
}
