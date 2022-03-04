using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using IICT_Store.Dtos.ProductDtos;
using IICT_Store.Models;
using IICT_Store.Models.Products;
using IICT_Store.Repositories.MaintananceProductSerialNoRepositories;
using IICT_Store.Repositories.MaintananceReposiotories;
using IICT_Store.Repositories.ProductNumberRepositories;
using IICT_Store.Repositories.ProductRepositories;
using IICT_Store.Repositories.ProductSerialNoRepositories;

namespace IICT_Store.Services.MaintananceProductService
{
    public class MaintananceProductService : IMaintananceProductService
    {
        private readonly IMaintanancePeoductSerialNoRepository maintanancePeoductSerialNoRepository;
        private readonly IMaintananceRepository maintananceRepository;
        private readonly IProductNumberRepository productNumberRepository;
        private readonly IProductRepository productRepository;
        private readonly IProductSerialNoRepository productSerialNoRepository;

        public MaintananceProductService(IMaintanancePeoductSerialNoRepository iMaintanancePeoductSerialNoRepository, 
            IMaintananceRepository maintananceRepository, 
            IMaintanancePeoductSerialNoRepository maintanancePeoductSerialNoRepository, 
            IProductNumberRepository productNumberRepository, 
            IProductRepository productRepository, 
            IProductSerialNoRepository productSerialNoRepository)
        {
            this.maintananceRepository = maintananceRepository;
            this.maintanancePeoductSerialNoRepository = maintanancePeoductSerialNoRepository;
            this.productNumberRepository = productNumberRepository;
            this.productRepository = productRepository;
            this.productSerialNoRepository = productSerialNoRepository;
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
                    Note = createMaintananceProduct.Note
                };
                maintananceRepository.Insert(maintananceProduct);
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
    }

    }