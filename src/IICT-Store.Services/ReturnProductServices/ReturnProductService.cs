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

        public async Task<ServiceResponse<GetReturnProductDto>> CreateReturnProduct(CreateReturnProductDto createReturnProductDto, int id)
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
            returnedProduct.ProductId = id;
            var map = mapper.Map<ReturnedProduct>(createReturnProductDto);
           
            var distribution = await distributionRepository.GetByRoomIdAndProductId(createReturnProductDto.RoomNo,id);
            if(distribution.Quantity < createReturnProductDto.Quantity)
            {
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Messages.Add($"Please Reduce the Quantity. This room has only {distribution.Quantity} item.");
                return response;
            }
            distribution.Quantity = distribution.Quantity - createReturnProductDto.Quantity;
            distributionRepository.Update(distribution);
            returnProductRepository.Insert(map);
            product.QuantityInStock = product.QuantityInStock + createReturnProductDto.Quantity;
            productRepository.Update(product);
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.Messages.Add("Returned.");
            return response;



        }
    }
}
