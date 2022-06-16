using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using IICT_Store.Dtos.ProductDtos;
using IICT_Store.Dtos.UserDtos;
using IICT_Store.Models;
using IICT_Store.Models.Products;
using IICT_Store.Repositories.DistributionRepositories;
using IICT_Store.Repositories.MaintananceProductSerialNoRepositories;
using IICT_Store.Repositories.MaintananceReposiotories;
using IICT_Store.Repositories.ProductNumberRepositories;
using IICT_Store.Repositories.ProductRepositories;
using IICT_Store.Repositories.ProductSerialNoRepositories;
using IICT_Store.Repositories.TestRepo;
using IICT_Store.Repositories.UserRepositories;

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
        private readonly IUserRepository userRepository;

        public MaintananceProductService(IMaintanancePeoductSerialNoRepository iMaintanancePeoductSerialNoRepository,
            IMaintananceRepository maintananceRepository,
            IMaintanancePeoductSerialNoRepository maintanancePeoductSerialNoRepository,
            IProductNumberRepository productNumberRepository,
            IProductRepository productRepository,
            IUserRepository userRepository,
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

        public async Task<ServiceResponse<GetMaintananceProductDto>> Create(CreateMaintananceProductDto createMaintananceProduct, string userId)
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
                var productNo = productNumberRepository.GetById(createMaintananceProduct.ProductSerialId);
                var productSerialNo = await productSerialNoRepository.GetByProductNoId(createMaintananceProduct.ProductSerialId);
                if (productSerialNo != null)
                {

                    var distribution = distributionRepository.GetById(productSerialNo.DistributionId);
                    if (productNo == null)
                    {
                        response.SetNotFoundMessage();
                        return response;
                    }

                    MaintenanceProduct maintananceProduct1 = new MaintenanceProduct()
                    {
                        Quantity = 1,
                        ProductId = createMaintananceProduct.ProductId,
                        CreatedAt = DateTime.Now,
                        SenderId = createMaintananceProduct.SenderId,
                        ReceiverId = createMaintananceProduct.ReceiverId,
                        DistributionId = distribution.Id,
                        Note = createMaintananceProduct.Note,
                        CreatedBy = userId
                    };
                    maintananceRepository.Insert(maintananceProduct1);
                    distribution.TotalRemainingQuantity = distribution.TotalRemainingQuantity - 1;
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
                        MaintananceProductId = maintananceProduct1.Id,
                        Name = productNo.Name,
                        ProductNoId = productNo.Id,
                        CreatedAt = DateTime.Now
                    };
                    maintanancePeoductSerialNoRepository.Insert(maintenanceProductSerial);
                    response.SetOkMessage();
                    return response;
                }
                MaintenanceProduct maintananceProduct = new MaintenanceProduct()
                {
                    CreatedBy = userId,
                    Quantity = createMaintananceProduct.Quantity,
                    ProductId = createMaintananceProduct.ProductId,
                    CreatedAt = DateTime.Now,
                    SenderId = createMaintananceProduct.SenderId,
                    ReceiverId = createMaintananceProduct.ReceiverId,
                    Note = createMaintananceProduct.Note
                };
                maintananceRepository.Insert(maintananceProduct);
                MaintenanceProductSerialNo maintenanceProductSerial1 = new MaintenanceProductSerialNo()
                {
                    IsRepaired = false,
                    MaintananceProductId = maintananceProduct.Id,
                    Name = productNo.Name,
                    ProductNoId = productNo.Id,
                    CreatedAt = DateTime.Now
                };
                maintanancePeoductSerialNoRepository.Insert(maintenanceProductSerial1);
                product.QuantityInStock = product.QuantityInStock - 1;

                productRepository.Update(product);
                productNo.ProductStatus = ProductStatus.Maintanance;
                productNo.UpdatedAt = DateTime.Now;
                productNumberRepository.Update(productNo);
                response.SetOkMessage();
                return response;
            }

            catch (Exception e)
            {
                response.SetMessage(new List<string> { e.Message }, HttpStatusCode.BadRequest);
                return response;
            }
        }


        //this method is only for repair
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
                // here is a bug. cz we doont filter the productserial which is in maintanane. we just take product by serial and it take from any status.
                //we need to filter that take only productserial with status of maintanace.
                //fixed
                var productNo = productNumberRepository.GetById(maintananceProduct.ProductSerialId);
                if (productNo == null)
                {
                    response.SetNotFoundMessage();
                    return response;
                }
                var productSerialNo = await productSerialNoRepository.GetMaintananceProductByProductNoId(maintananceProduct.ProductSerialId);
                if (productSerialNo != null)
                {
                    productNo.ProductStatus = ProductStatus.Assigned;
                    productSerialNo.ProductStatus = ProductStatus.Assigned;
                    productSerialNo.UpdatedAt = DateTime.Now;
                    productNo.UpdatedAt = DateTime.Now;
                    var distribution = distributionRepository.GetById(productSerialNo.DistributionId);
                    distribution.TotalRemainingQuantity = distribution.TotalRemainingQuantity + 1;
                    productNumberRepository.Update(productNo);
                    productSerialNoRepository.Update(productSerialNo);
                    distributionRepository.Update(distribution);
                    var maintananceProductSerial = baseRepo.GetItems<MaintenanceProductSerialNo>(x => x.ProductNoId == maintananceProduct.ProductSerialId).ToList();
                    var maintananceProductSerialToUpdate = maintananceProductSerial.OrderByDescending(x => x.ProductNoId).FirstOrDefault();
                    maintananceProductSerialToUpdate.IsRepaired = true;
                    maintanancePeoductSerialNoRepository.Update(maintananceProductSerialToUpdate);
                    response.SetOkMessage();
                    return response;
                }
                product.QuantityInStock = product.QuantityInStock + 1;
                productRepository.Update(product);
                productNo.ProductStatus = ProductStatus.Unassigned;
                productNo.UpdatedAt = DateTime.Now;
                productNumberRepository.Update(productNo);
                var maintananceProductSerial1 = baseRepo.GetItems<MaintenanceProductSerialNo>(x => x.ProductNoId == maintananceProduct.ProductSerialId).ToList();
                var maintananceProductSerialToUpdate1 = maintananceProductSerial1.OrderByDescending(x => x.ProductNoId).FirstOrDefault();
                maintananceProductSerialToUpdate1.IsRepaired = true;
                maintanancePeoductSerialNoRepository.Update(maintananceProductSerialToUpdate1);
                response.SetOkMessage();
                return response;

            }
            catch (Exception e)
            {
                response.SetMessage(new List<string> { e.Message }, HttpStatusCode.BadRequest);
                return response;
            }
        }
        /*
                public async Task<ServiceResponse<GetMaintananceProductDto>> DamagaFromMaintanance(CreateDamagedProductDto createDamagedProductDto)
                {
                    ServiceResponse<GetDamagedProductDto> response = new();
                    try
                    {
                        var product = productRepository.GetById(maintananceProduct.ProductId);
                        if (product == null)
                        {
                            response.SetNotFoundMessage();
                            return response;
                        }
                        // here is a bug. cz we doont filter the productserial which is in maintanane. we just take product by serial and it take from any status.
                        //we need to filter that take only productserial with status of maintanace.
                        //fixed
                        var productNo = productNumberRepository.GetById(maintananceProduct.ProductSerialId);

                    }
                }*/


        public async Task<ServiceResponse<GetMaintananceProductDto>> GetByProductSerial(long productId, long serialId)
        {
            ServiceResponse<GetMaintananceProductDto> response = new();
            var products = baseRepo.GetItems<MaintenanceProduct>(x => x.ProductId == productId);
            var product = products.OrderByDescending(x => x.ProductId).FirstOrDefault();
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
            productToMap.CreatedByUser = mapper.Map<GetUserDto>(await userRepository.GetById(mainTananceProduct.CreatedBy));
            response.Data = productToMap;
            response.SetOkMessage();
            return response;
        }
    }

}