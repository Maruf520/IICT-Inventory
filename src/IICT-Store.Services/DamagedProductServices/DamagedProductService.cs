using IICT_Store.Dtos.DistributionDtos;
using IICT_Store.Dtos.ProductDtos;
using IICT_Store.Models;
using IICT_Store.Models.Products;
using IICT_Store.Repositories.DamagedProductRepositories;
using IICT_Store.Repositories.DamagedProductSerialRepositories;
using IICT_Store.Repositories.DistributionRepositories;
using IICT_Store.Repositories.ProductNumberRepositories;
using IICT_Store.Repositories.ProductRepositories;
using IICT_Store.Repositories.ProductSerialNoRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Services.DamagedProductServices
{
    public class DamagedProductService : IDamagedProductService
    {
        private readonly IProductSerialNoRepository productSerialNoRepository;
        private readonly IDamagedProductRepository damagedProductRepository;
        private readonly IDistributionRepository distributionRepository;
        private readonly IProductRepository productReository;
        private readonly IDamagedProductSerialNoRepository damagedProductSerialNoRepository;
        private readonly IProductNumberRepository productNumberRepository;
        public DamagedProductService(
            IProductSerialNoRepository productSerialNoRepository,
            IDamagedProductRepository damagedProductRepository,
            IDistributionRepository distributionRepository, 
            IProductRepository productReository,
            IDamagedProductSerialNoRepository damagedProductSerialNoRepository,
            IProductNumberRepository productNumberRepository

            )
        {
            this.productSerialNoRepository = productSerialNoRepository;
            this.damagedProductRepository = damagedProductRepository;
            this.distributionRepository = distributionRepository;
            this.productReository = productReository;
            this.damagedProductSerialNoRepository = damagedProductSerialNoRepository;
            this.productNumberRepository = productNumberRepository;
        }

        public async Task<ServiceResponse<DamagedProductDto>> DamageProduct(CreateDamagedProductDto damagedProductDto)
        {
            ServiceResponse<DamagedProductDto> response = new();
            var productSerialNo = await productSerialNoRepository.GetByProductNoId(damagedProductDto.SerialId);
            if(productSerialNo != null)
            {
                var distribution = distributionRepository.GetById(productSerialNo.DistributionId);
                var serialNoToUpdate = productSerialNoRepository.GetById(productSerialNo.Id);
                serialNoToUpdate.ProductStatus = ProductStatus.Damaged;
                serialNoToUpdate.UpdatedAt = DateTime.Now;
                productSerialNoRepository.Update(serialNoToUpdate);
                distribution.TotalRemainingQuantity = distribution.Quantity - 1;
                distribution.UpdatedAt = DateTime.Now;
                distributionRepository.Update(distribution);
                DamagedProduct damagedProduct1 = new();
                damagedProduct1.PersonId = distribution.DistributedTo;
                damagedProduct1.ProductId = distribution.ProductId;
                damagedProduct1.Quantity = 1;
                if(distribution.RoomNo !=null)
                {
                    damagedProduct1.RoomNo = (int)distribution.RoomNo;
                }
                if(distribution.DistributedTo != 0)
                {
                    damagedProduct1.PersonId = distribution.DistributedTo;
                }
                damagedProduct1.ReceiverId = 1;
                damagedProduct1.SenderId = 1;
                damagedProduct1.UpdatedAt = DateTime.Now;
                damagedProductRepository.Insert(damagedProduct1);
                DamagedProductSerialNo damagedProductSerialNo = new();
                damagedProductSerialNo.CreatedAt = DateTime.Now;
                damagedProductSerialNo.DamagedProductId = damagedProduct1.Id;
                damagedProductSerialNo.ProductNoId = damagedProductDto.SerialId;
                damagedProductSerialNoRepository.Insert(damagedProductSerialNo);

                response.Messages.Add("Created.");
                response.StatusCode = System.Net.HttpStatusCode.Created;
                return response;
            }
            else if(distributionRepository.GetById(damagedProductDto.DistributionId) != null)
            {
                var distribution = distributionRepository.GetById(damagedProductDto.DistributionId);
                distribution.TotalRemainingQuantity = distribution.Quantity - damagedProductDto.Quantity;
                distribution.UpdatedAt = DateTime.Now;
                distributionRepository.Update(distribution);
                DamagedProduct damagedProduct2 = new();
                damagedProduct2.PersonId = distribution.DistributedTo;
                damagedProduct2.ProductId = distribution.ProductId;
                damagedProduct2.Quantity = damagedProductDto.Quantity;
                if (distribution.RoomNo != null)
                {
                    damagedProduct2.RoomNo = (int)distribution.RoomNo;
                }
                if (distribution.DistributedTo != 0)
                {
                    damagedProduct2.PersonId = distribution.DistributedTo;
                }
                damagedProduct2.ReceiverId = 1;
                damagedProduct2.SenderId = 1;
                damagedProduct2.UpdatedAt = DateTime.Now;
                damagedProductRepository.Insert(damagedProduct2);
                response.Messages.Add("Created.");
                response.StatusCode = System.Net.HttpStatusCode.Created;
                return response;
            }

                var product = productReository.GetById(damagedProductDto.ProductId);
                product.QuantityInStock = product.QuantityInStock - 1;
                productReository.Update(product);
                DamagedProduct damagedProduct = new();
                damagedProduct.Quantity = 1;
                damagedProduct.ProductId = damagedProduct.ProductId;
                damagedProduct.UpdatedAt = DateTime.Now;
                damagedProductRepository.Insert(damagedProduct);
                response.Messages.Add("Created.");
                response.StatusCode = System.Net.HttpStatusCode.Created;
                return response;
       }











































/*
            distribution.Quantity = distribution.Quantity - 1;
            distributionRepository.Update(distribution);
            List<DamagedProductSerialNo> damagedProductSerialNos = new();
            DamagedProduct damagedProductt = new();
            damagedProduct.Quantity = 1;
            damagedProduct.ProductId = distribution.ProductId;
            damagedProduct.CreatedAt = DateTime.Now;
            DamagedProductSerialNo damagedProductSerialNo = new();
            damagedProductSerialNo.ProductNoId = productNo.Id;
            damagedProductSerialNo.Name = productNo.Name;
            damagedProductSerialNo.CreatedAt = DateTime.Now;
            damagedProductSerialNos.Add(damagedProductSerialNo);
            var productNos = productNumberRepository.GetById(damagedProductSerialNo.ProductNoId);
            productNos.ProductStatus = ProductStatus.Damaged;
            productNumberRepository.Update(productNos) ;
            damagedProduct.DamagedProductSerialNos = damagedProductSerialNos;
            damagedProductRepository.Insert(damagedProduct);
            productSerialNoRepository.Delete(productSerialNo.Id);
            response.Messages.Add("Damaged product added.");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;*/

        

        public async Task<ServiceResponse<List<DamagedProductDto>>> GetAllDamagedProduct()
        {
            ServiceResponse<List<DamagedProductDto>> response = new();
            List<DamagedProductDto> damagedProductDtos = new();
            var damagedProducts = damagedProductRepository.GetAll();
            foreach(var item in damagedProducts)
            {
                DamagedProductDto damagedProductDto = new();
                var product = productReository.GetById(item.ProductId);
                var x = damagedProductRepository.GetAll();
                var y = x.Where(x => x.Id == item.Id).ToList();
                damagedProductDto.Id = product.Id;
                damagedProductDto.Name = product.Name;
                damagedProductDto.Quantity = y.Count;
                damagedProductDtos.Add(damagedProductDto);

            }

            response.Data = damagedProductDtos;
            response.Messages.Add("All Damaged Products");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;
        }

        public async Task<ServiceResponse<GetDamagedProductDto>> GetDamagedProductById(long id)
        {
            ServiceResponse<GetDamagedProductDto> response = new();
            List<DamagedProductDto> damagedProductDtos = new();
            var damagedProduct = damagedProductRepository.GetAll();
            var damagedProducts = damagedProduct.Where(x => x.ProductId == id);
            foreach (var items in damagedProducts)
            {
                var damagedporductSerialNo = damagedProductSerialNoRepository.GetById(items.Id);
                DamagedProductDto damagedProductDto = new();
                damagedProductDto.Id = damagedporductSerialNo.Id;
                damagedProductDto.Name = damagedporductSerialNo.Name;
                
                damagedProductDtos.Add(damagedProductDto);

            }
            GetDamagedProductDto getDamagedProductDto = new();
            getDamagedProductDto.Quantity = damagedProductDtos.Count;
            getDamagedProductDto.DamagedProducts = damagedProductDtos;

            response.Data = getDamagedProductDto;
            response.Messages.Add("All Damaged Products");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;
        }
    }
}
