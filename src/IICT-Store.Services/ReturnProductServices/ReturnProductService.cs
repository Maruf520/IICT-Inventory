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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using IICT_Store.Repositories.ProductNumberRepositories;
using IICT_Store.Repositories.ReturnedProductSerialNoRepositories;
using IICT_Store.Repositories.ProductSerialNoRepositories;

namespace IICT_Store.Services.ReturnProductServices
{
    public class ReturnProductService : IReturnProductService
    {
        private readonly IReturnedProductRepository returnProductRepository;
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly IDistributionRepository distributionRepository;
        private readonly IProductNumberRepository productNumberRepository;
        private readonly IReturnedProductSerialNoRepository returnedProductSerialNoRepository;
        private readonly IProductSerialNoRepository productSerialNoRepository;
        public ReturnProductService(IProductSerialNoRepository productSerialNoRepository, IReturnedProductRepository returnProductRepository, IProductRepository productRepository, IMapper mapper, IDistributionRepository distributionRepository, IProductNumberRepository productNumberRepository, IReturnedProductSerialNoRepository returnedProductSerialNoRepository)
        {
            this.returnProductRepository = returnProductRepository;
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.distributionRepository = distributionRepository;
            this.productNumberRepository = productNumberRepository;
            this.returnedProductSerialNoRepository = returnedProductSerialNoRepository;
            this.productSerialNoRepository = productSerialNoRepository;
        }

        public async Task<ServiceResponse<GetReturnProductDto>> CreateReturnProduct(CreateReturnProductDto createReturnProductDto, string userId)
        {
            ServiceResponse<GetReturnProductDto> response = new();

            var product = productRepository.GetById(createReturnProductDto.ProductId);
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
            returnedProduct.CreatedBy = userId;
            returnedProduct.Note = createReturnProductDto.Note;
            returnedProduct.ProductId = createReturnProductDto.ProductId;
            var map = mapper.Map<ReturnedProduct>(createReturnProductDto);
            returnProductRepository.Insert(map);
            if (product.HasSerial == true)
            {
                foreach (var serial in createReturnProductDto.ReturnedProductSerialNos)
                {
                    var productNumber = productNumberRepository.GetById(serial);
                    ReturnedProductSerialNo returnedProductSerialNo = new()
                    {
                        Name = productNumber.Name,
                        ProductNoId = productNumber.Id,
                        CreatedAt = DateTime.Now,
                        ReturnedProductId = map.Id
                    };
                    var productSerialNumber = await productSerialNoRepository.GetAssignedProductSerialByProductNoId(serial);
                    productSerialNumber.ProductStatus = ProductStatus.Unassigned;
                    productNumber.ProductStatus = ProductStatus.Unassigned;
                    productNumberRepository.Update(productNumber);
                    returnedProductSerialNoRepository.Insert(returnedProductSerialNo);
                    var productNo = await distributionRepository.GetLastProductByProductNoId(serial);
                    var distributions = distributionRepository.GetById(productNo.DistributionId);
                    if (distributions.Quantity < createReturnProductDto.Quantity)
                    {
                        response.StatusCode = System.Net.HttpStatusCode.OK;
                        response.Messages.Add($"Please Reduce the Quantity. This room has only {distributions.Quantity} item.");
                        return response;
                    }
                    distributions.TotalRemainingQuantity = distributions.TotalRemainingQuantity - createReturnProductDto.Quantity;
                    distributions.UpdatedAt = DateTime.Now;
                    distributionRepository.Update(distributions);
                    product.QuantityInStock = product.QuantityInStock + createReturnProductDto.Quantity;
                    productRepository.Update(product);
                }
                response.SetMessage(new List<string> { new string("Product Returned.") }, HttpStatusCode.OK);
                return response;

            }

            var distribution = await distributionRepository.GetByProductId(createReturnProductDto.ProductId);

            distribution.TotalRemainingQuantity = distribution.TotalRemainingQuantity - createReturnProductDto.Quantity;
            distributionRepository.Update(distribution);

            product.QuantityInStock = product.QuantityInStock + createReturnProductDto.Quantity;
            productRepository.Update(product);
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.Messages.Add("Returned.");
            return response;
        }
    }
}
