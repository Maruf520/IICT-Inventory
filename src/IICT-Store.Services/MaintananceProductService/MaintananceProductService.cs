using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using IICT_Store.Dtos.ProductDtos;
using IICT_Store.Models;
using IICT_Store.Models.Products;
using IICT_Store.Repositories.DistributionRepositories;
using IICT_Store.Repositories.MaintananceProductSerialNoRepositories;
using IICT_Store.Repositories.MaintananceReposiotories;
using IICT_Store.Repositories.ProductNumberRepositories;
using IICT_Store.Repositories.ProductRepositories;
using IICT_Store.Repositories.ProductSerialNoRepositories;
using IICT_Store.Repositories.TestRepo;

namespace IICT_Store.Services.MaintananceProductService
{
    public class MaintananceProductService : IMaintananceProductService
    {
        private readonly IMaintanancePeoductSerialNoRepository maintanancePeoductSerialNoRepository;
        private readonly IMaintananceRepository maintananceRepository;
        private readonly IProductNumberRepository productNumberRepository;
        private readonly IProductRepository productRepository;
        private readonly IProductSerialNoRepository productSerialNoRepository;
        private readonly IDistributionRepository distributionRepository;
        private readonly IBaseRepo baseRepo;
        private readonly IMapper mapper;

        public MaintananceProductService(IMaintanancePeoductSerialNoRepository iMaintanancePeoductSerialNoRepository, 
            IMaintananceRepository maintananceRepository, 
            IMaintanancePeoductSerialNoRepository maintanancePeoductSerialNoRepository, 
            IProductNumberRepository productNumberRepository, 
            IProductRepository productRepository, 
            IProductSerialNoRepository productSerialNoRepository, IDistributionRepository distributionRepository, IBaseRepo baseRepo, IMapper mapper)
        {
            this.maintananceRepository = maintananceRepository;
            this.maintanancePeoductSerialNoRepository = maintanancePeoductSerialNoRepository;
            this.productNumberRepository = productNumberRepository;
            this.productRepository = productRepository;
            this.productSerialNoRepository = productSerialNoRepository;
            this.distributionRepository = distributionRepository;
            this.baseRepo = baseRepo;
            this.mapper = mapper;
        }

        public async Task<ServiceResponse<GetMaintananceProductDto>> Create(CreateMaintananceProductDto createMaintananceProduct)
        {
            ServiceResponse<GetMaintananceProductDto> response = new ServiceResponse<GetMaintananceProductDto>();
            try
            {
                var product = productRepository.GetById(createMaintananceProduct.ProductId);
                if (product == null)
                {
                    response.SetNotFoundMessage();
                    return response;
                }

                var productSerialNo = productSerialNoRepository.GetById(createMaintananceProduct.ProductSerialId);
                var productNo = productNumberRepository.GetById(productSerialNo.ProductNoId);
                var distribution = distributionRepository.GetById(productSerialNo.DistributionId);
                if (productNo == null)
                {
                    response.SetNotFoundMessage();
                    return response;
                }

                MaintenanceProduct maintananceProduct = new MaintenanceProduct()
                {
                    Quantity = createMaintananceProduct.Quantity,
                    ProductId = createMaintananceProduct.ProductId,
                    CreatedAt = DateTime.Now,
                    SenderId = createMaintananceProduct.SenderId,
                    ReceiverId = createMaintananceProduct.ReceiverId,
                    DistributionId = distribution.Id,
                    Note = createMaintananceProduct.Note
                };
                maintananceRepository.Insert(maintananceProduct);
                distribution.Quantity = distribution.Quantity - 1;
                distributionRepository.Update(distribution);
                productNo.ProductStatus = ProductStatus.Maintanance;
                productNo.UpdatedAt = DateTime.Now;
                productNumberRepository.Update(productNo);
                productSerialNo.ProductStatus = ProductStatus.Maintanance;
                productSerialNo.UpdatedAt = DateTime.Now;
                productSerialNoRepository.Update(productSerialNo);
                MaintenanceProductSerialNo maintenanceProductSerial = new MaintenanceProductSerialNo()
                {
                    IsRepaired = false,
                    MaintananceProductId = maintananceProduct.Id,
                    Name = productNo.Name,
                    ProductNoId = productNo.Id,
                    CreatedAt = DateTime.Now
                };
                maintanancePeoductSerialNoRepository.Insert(maintenanceProductSerial);
                response.SetOkMessage();
                return response;
            }
            catch (Exception e)
            {
                response.SetMessage(new List<string> {e.Message}, HttpStatusCode.BadRequest);
                return response;
            }
        }

        public async Task<ServiceResponse<GetMaintananceProductDto>> RepairOrDamage(CreateMaintananceProductDto maintananceProduct)
        {
            ServiceResponse<GetMaintananceProductDto> response = new ServiceResponse<GetMaintananceProductDto>();
            try
            {
                var product = productRepository.GetById(maintananceProduct.ProductId);
                if (product == null)
                {
                    response.SetNotFoundMessage();
                    return response;
                }

                var productSerialNo = await productSerialNoRepository.GetByProductNoId(maintananceProduct.ProductSerialId);
                var productNo = productNumberRepository.GetById(productSerialNo.ProductNoId);
                if (productNo == null)
                {
                    response.SetNotFoundMessage();
                    return response;
                }
                productNo.ProductStatus = ProductStatus.Assigned;
                productSerialNo.ProductStatus = ProductStatus.Assigned;
                productSerialNo.UpdatedAt = DateTime.Now;
                productNo.UpdatedAt = DateTime.Now;
                var distribution = distributionRepository.GetById(productSerialNo.DistributionId);
                distribution.Quantity = distribution.Quantity + 1;
                productNumberRepository.Update(productNo);
                productSerialNoRepository.Update(productSerialNo);
                distributionRepository.Update(distribution);
                var maintananceProductSerial = baseRepo.GetItems<MaintenanceProductSerialNo>(x => x.ProductNoId == maintananceProduct.ProductSerialId).ToList();
                var  maintananceProductSerialToUpdate = maintananceProductSerial.OrderByDescending(x => x.ProductNoId).FirstOrDefault();
                maintananceProductSerialToUpdate.IsRepaired = true;
                maintanancePeoductSerialNoRepository.Update(maintananceProductSerialToUpdate);
                response.SetOkMessage();
                return response;
            }
            catch (Exception e)
            {
                response.SetMessage(new List<string> { e.Message }, HttpStatusCode.BadRequest);
                return response;
            }
        }

        public async Task<ServiceResponse<GetMaintananceProductDto>> GetByProductSerial(long productId, long serialId)
        {
            ServiceResponse<GetMaintananceProductDto> response = new();
            var products = baseRepo.GetItems<MaintenanceProduct>(x => x.ProductId == productId);
            var product = products.OrderByDescending(x =>x.ProductId).FirstOrDefault();
            if (product == null)
            {
                response.SetNotFoundMessage();
                return response;
            }
            var mainTananceProduct = baseRepo.GetItems<MaintenanceProduct>(x => x.ProductId == productId).FirstOrDefault();
            if (mainTananceProduct == null)
            {
                response.SetNotFoundMessage();
                return response;
            }
            var maintananceProductSerial = baseRepo.GetItems<MaintenanceProductSerialNo>(x =>
                x.ProductNoId == serialId && x.MaintananceProductId == mainTananceProduct.Id);
            var productDto = productRepository.GetById(productId);
            var productMap = mapper.Map<GetProductDto>(productDto);
            var productToMap = mapper.Map<GetMaintananceProductDto>(mainTananceProduct);
            productToMap.MaintananceProductSerial = maintananceProductSerial.ToList();
            productToMap.Product = productMap;
            response.Data = productToMap;
            response.SetOkMessage();
            return response;
        }
    }

    }