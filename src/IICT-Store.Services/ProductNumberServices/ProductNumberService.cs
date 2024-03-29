﻿using AutoMapper;
using IICT_Store.Dtos.ProductDtos;
using IICT_Store.Models;
using IICT_Store.Models.Products;
using IICT_Store.Repositories.DistributionRepositories;
using IICT_Store.Repositories.ProductNumberRepositories;
using IICT_Store.Repositories.ProductRepositories;
using IICT_Store.Repositories.ProductSerialNoRepositories;
using IICT_Store.Services.ProductServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IICT_Store.Repositories.TestRepo;
using IICT_Store.Repositories.UserRepositories;
using IICT_Store.Dtos.UserDtos;

namespace IICT_Store.Services.ProductNumberServices
{
    public class ProductNumberService : IProductNumberService
    {
        private readonly IProductRepository productRepository;
        private readonly IProductNumberRepository productNumberRepository;
        private readonly IMapper mapper;
        private readonly IProductSerialNoRepository productSerialNoRepository;
        private readonly IDistributionRepository distributionRepository;
        private readonly IBaseRepo baseRepo;
        private readonly IUserRepository userRepository;
        public ProductNumberService(IProductNumberRepository productNumberRepository,
            IProductRepository productRepository,
            IMapper mapper,
            IProductSerialNoRepository productSerialNoRepository,
            IDistributionRepository distributionRepository, IBaseRepo baseRepo, IUserRepository userRepository)
        {
            this.productNumberRepository = productNumberRepository;
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.productSerialNoRepository = productSerialNoRepository;
            this.distributionRepository = distributionRepository;
            this.baseRepo = baseRepo;
            this.userRepository = userRepository;
        }
        public async Task<ServiceResponse<GetProductDto>> InsertProductNo(long id, CreateProductNoDto createProductNoDto, string userId)
        {
            ServiceResponse<GetProductDto> response = new();
            var product = productRepository.GetById(id);
            if (product == null)
            {
                response.Messages.Add("Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            if (product.HasSerial == false)
            {
                response.Messages.Add("Sorry, this product doesn't have any serial No.");
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return response;
            }
            if (product.ProductNos != null)
            {
                var productCount = await productNumberRepository.GetByProductId(id);
                var avaiableAmount = product.QuantityInStock - productCount.Count;
                if (product.TotalQuantity < createProductNoDto.ProductNos.Count)
                {
                    response.Messages.Add("You have to reduce product item.");
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                    return response;
                }
            }
            /*            if(product.TotalQuantity >= productCount)*/
            if (product.TotalQuantity < createProductNoDto.ProductNos.Count)
            {
                response.Messages.Add("You have to reduce product item.");
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return response;
            }
            var checkSerialNo = CheckIfSerialNoExists(id, (List<ProductNoDto>)createProductNoDto.ProductNos);
            if (checkSerialNo.Result == false)
            {
                response.Messages.Add("Please set unique serial no.");
                return response;
            }

            foreach (var item in createProductNoDto.ProductNos)
            {
                ProductNo productNo = new();
                productNo.Name = item.Name;
                productNo.ProductId = id;
                productNo.CreatedAt = DateTime.Now;
                productNo.CreatedBy = userId;
                productNumberRepository.Insert(productNo);
            }

            response.Messages.Add("Product Number Added.");
            response.StatusCode = System.Net.HttpStatusCode.Created;
            return response;

        }

        public async Task<bool> CheckIfSerialNoExists(long productId, List<ProductNoDto> name)
        {
            var productSerialNo = await productRepository.GetAllProductNoById(productId);
            var nameCount = name.Count;
            for (int i = 0; i < nameCount; i++)
            {
                foreach (var serialno in productSerialNo)
                {
                    if (serialno.Name == name[i].Name)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public async Task<ServiceResponse<List<GetProductNoDto>>> GetProductNoByProductId(long productId)
        {
            ServiceResponse<List<GetProductNoDto>> response = new();
            try
            {
                List<GetProductNoDto> productNoDtos = new();
                var productsNo = await productNumberRepository.GetByProductId(productId);
                if (productsNo.Count == 0)
                {
                    response.Messages.Add("Not Found.");
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                    return response;
                }

                foreach (var product in productsNo)
                {
                    GetProductNoDto productNoDto = new();
                    var productserial2 = await productSerialNoRepository.GetByProductNoId(product.Id);
                    var t = baseRepo.GetItems<ProductSerialNo>(e => e.ProductNoId == product.Id).ToList();
                    var productserial = t.OrderByDescending(e => e.Id).FirstOrDefault();
                    if (productserial != null)
                    {
                        var distribution = distributionRepository.GetById(productserial.DistributionId);
                        if (distribution.RoomNo != null)
                        {
                            productNoDto.RoomNo = (int)distribution.RoomNo;
                        }

                        productNoDto.DistributedTo = distribution.DistributedTo;
                        var user = await userRepository.GetById(distribution.CreatedBy);
                        productNoDto.UpdatedByUser = mapper.Map<GetUserDto>(user);
                        productNoDto.CreatedByUser = productNoDto.UpdatedByUser;
                    }
                    productNoDto.Id = product.Id;
                    productNoDto.Name = product.Name;
                    productNoDto.ProductStatus = product.ProductStatus;
                    productNoDtos.Add(productNoDto);
                }
                var map = mapper.Map<List<GetProductNoDto>>(productsNo);
                response.Data = productNoDtos;
                response.Messages.Add("All Serial.");
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return response;
            }
            catch (Exception e)
            {
                return response;
            }
        }
    }
}
