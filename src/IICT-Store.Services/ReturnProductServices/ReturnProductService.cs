using AutoMapper;
using IICT_Store.Dtos.ProductDtos;
using IICT_Store.Models;
using IICT_Store.Models.Products;
using IICT_Store.Repositories.DistributionRepositories;
using IICT_Store.Repositories.ProductRepositories;
using IICT_Store.Repositories.ReturnedProductRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Services.ReturnProductServices
{
    public class ReturnProductService : IReturnProductService
    {
        private readonly IReturnedProductRepository returnProductRepository;
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly IDistributionRepository distributionRepository;
        public ReturnProductService(IReturnedProductRepository returnProductRepository, IProductRepository productRepository, IMapper mapper, IDistributionRepository distributionRepository)
        {
            this.returnProductRepository = returnProductRepository;
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.distributionRepository = distributionRepository;
        }
/*
        public async Task<ServiceResponse<GetReturnProductDto>> CreateReturnProduct(CreateReturnProductDto createReturnProductDto, long id)
        {
            ServiceResponse<GetReturnProductDto> response = new();
            var product = productRepository.GetById(id);
            if (product == null)
            {
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.Messages.Add("Not Found.");
                return response;
            }
            ReturnedProduct returnedProduct = new();
            returnedProduct.Quantity = createReturnProductDto.Quantity;
            returnedProduct.ReceiverId = createReturnProductDto.ReceiverId;
            returnedProduct.SenderId = createReturnProductDto.SenderId;
            returnedProduct.CreatedAt = DateTime.Now;
            returnedProduct.Note = createReturnProductDto.Note;
            returnedProduct.ProductId = createReturnProductDto.ProductId;
            var map = mapper.Map<ReturnedProduct>(createReturnProductDto);
            returnProductRepository.Insert(map);
           var distribution = await distributionRepository.(createReturnProductDto.RoomNo);
            


        }*/

        public Task<ServiceResponse<GetReturnProductDto>> CreateReturnProduct(CreateReturnProductDto createReturnProductDto)
        {
            throw new NotImplementedException();
        }
    }
}
