using AutoMapper;
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
        private readonly IMapper mapper;
        public DamagedProductService(
            IProductSerialNoRepository productSerialNoRepository,
            IDamagedProductRepository damagedProductRepository,
            IDistributionRepository distributionRepository,
            IProductRepository productReository,
            IDamagedProductSerialNoRepository damagedProductSerialNoRepository,
            IProductNumberRepository productNumberRepository,
            IMapper mapper

            )
        {
            this.productSerialNoRepository = productSerialNoRepository;
            this.damagedProductRepository = damagedProductRepository;
            this.distributionRepository = distributionRepository;
            this.productReository = productReository;
            this.damagedProductSerialNoRepository = damagedProductSerialNoRepository;
            this.productNumberRepository = productNumberRepository;
            this.mapper = mapper;
        }

        public async Task<ServiceResponse<DamagedProductDto>> DamageProduct(CreateDamagedProductDto damagedProductDto, string userId)
        {
            ServiceResponse<DamagedProductDto> response = new();
            var product = productReository.GetById(damagedProductDto.ProductId);
            var productSerialNo = await productSerialNoRepository.GetAssignedProductSerialByProductNoId(damagedProductDto.SerialId);
            var productNo = productNumberRepository.GetById(damagedProductDto.SerialId);
            if (productNo != null)
            {
                product = productReository.GetById(productNo.ProductId);
            }
            if (productSerialNo != null)
            {
                product = productReository.GetById(productNo.ProductId);
            }
            if (productSerialNo != null && product.HasSerial == true)
            {
                var distribution = distributionRepository.GetById(productSerialNo.DistributionId);
                var serialNoToUpdate = productSerialNoRepository.GetById(productSerialNo.Id);
                var productno = productNumberRepository.GetById(damagedProductDto.SerialId);
                productno.ProductStatus = ProductStatus.Damaged;
                productNumberRepository.Update(productno);
                serialNoToUpdate.ProductStatus = ProductStatus.Damaged;
                serialNoToUpdate.UpdatedAt = DateTime.Now;
                productSerialNoRepository.Update(serialNoToUpdate);
                distribution.TotalRemainingQuantity = distribution.Quantity - 1;
                distribution.UpdatedAt = DateTime.Now;
                product.TotalQuantity = product.TotalQuantity - 1;
                distributionRepository.Update(distribution);
                DamagedProduct damagedProduct1 = new();
                damagedProduct1.PersonId = 0;
                damagedProduct1.ProductId = distribution.ProductId;
                damagedProduct1.Quantity = 1;
                if (distribution.RoomNo != null)
                {
                    damagedProduct1.RoomNo = (int)distribution.RoomNo;
                }
                if (distribution.DistributedTo != 0)
                {
                    damagedProduct1.PersonId = distribution.DistributedTo;
                }
                damagedProduct1.ReceiverId = damagedProductDto.ReceiverId;
                damagedProduct1.SenderId = damagedProductDto.SenderId;
                damagedProduct1.CreatedAt = DateTime.Now;
                damagedProduct1.CreatedBy = userId;
                damagedProductRepository.Insert(damagedProduct1);
                DamagedProductSerialNo damagedProductSerialNo = new();
                damagedProductSerialNo.CreatedAt = DateTime.Now;
                damagedProductSerialNo.DamagedProductId = damagedProduct1.Id;
                damagedProductSerialNo.ProductNoId = damagedProductDto.SerialId;
                damagedProductSerialNoRepository.Insert(damagedProductSerialNo);
                productReository.Update(product);
                response.Messages.Add("Created.");
                response.StatusCode = System.Net.HttpStatusCode.Created;
                return response;
            }
            else if (productSerialNo == null && product.HasSerial == true)
            {
                var productno = productNumberRepository.GetById(damagedProductDto.SerialId);
                productno.ProductStatus = ProductStatus.Damaged;
                productNumberRepository.Update(productno);
                // var distribution = distributionRepository.GetById(productSerialNo.DistributionId);
                //  var serialNoToUpdate = productSerialNoRepository.GetById(productSerialNo.Id);
                //serialNoToUpdate.ProductStatus = ProductStatus.Damaged;
                // serialNoToUpdate.UpdatedAt = DateTime.Now;
                //productSerialNoRepository.Update(serialNoToUpdate);
                //distribution.TotalRemainingQuantity = distribution.Quantity - 1;
                // distribution.UpdatedAt = DateTime.Now;
                //distributionRepository.Update(distribution);
                DamagedProduct damagedProduct1 = new();
                //damagedProduct1.PersonId = distribution.DistributedTo;
                //damagedProduct1.ProductId = distribution.ProductId;
                damagedProduct1.Quantity = 1;
                var productNoid = productNumberRepository.GetById(damagedProductDto.SerialId);
                var productByProductNoid = productReository.GetById(productNoid.ProductId);
                productByProductNoid.TotalQuantity = productByProductNoid.TotalQuantity - 1;
                productByProductNoid.QuantityInStock = productByProductNoid.QuantityInStock - 1;
                //if (distribution.RoomNo != null)
                //{
                //    damagedProduct1.RoomNo = (int)distribution.RoomNo;
                //}
                //if (distribution.DistributedTo != 0)
                //{
                //    damagedProduct1.PersonId = distribution.DistributedTo;
                //}
                damagedProduct1.ReceiverId = damagedProductDto.ReceiverId;
                damagedProduct1.SenderId = damagedProductDto.SenderId;
                damagedProduct1.CreatedAt = DateTime.Now;
                damagedProduct1.CreatedBy = userId;
                damagedProduct1.WasNotDistributed = true;
                damagedProduct1.ProductId = productNo.ProductId;
                damagedProductRepository.Insert(damagedProduct1);
                productReository.Update(productByProductNoid);
                DamagedProductSerialNo damagedProductSerialNo = new();
                damagedProductSerialNo.CreatedAt = DateTime.Now;
                damagedProductSerialNo.DamagedProductId = damagedProduct1.Id;
                damagedProductSerialNo.ProductNoId = damagedProductDto.SerialId;
                damagedProductSerialNoRepository.Insert(damagedProductSerialNo);
                var productNoToUpdate = productNumberRepository.GetById(damagedProductDto.SerialId);
                productNoToUpdate.ProductStatus = ProductStatus.Damaged;
                productNumberRepository.Update(productNoToUpdate);
                response.Messages.Add("Created.");
                response.StatusCode = System.Net.HttpStatusCode.Created;
                return response;
            }
            else if (distributionRepository.GetById(damagedProductDto.DistributionId) != null && product.HasSerial == false)
            {
                if (product.TotalQuantity < damagedProductDto.Quantity)
                {
                    response.SetMessage(new List<string> { new string("You have to reduce quantity") });
                    return response;
                }
                var distribution = distributionRepository.GetById(damagedProductDto.DistributionId);
                if (distribution.TotalRemainingQuantity < damagedProductDto.Quantity)
                {
                    response.SetMessage(new List<string> { new string("You have to reduce quantity") });
                    return response;
                }
                distribution.TotalRemainingQuantity = distribution.Quantity - damagedProductDto.Quantity;
                var getProduct = productReository.GetById(distribution.ProductId);
                getProduct.TotalQuantity = getProduct.TotalQuantity - damagedProductDto.Quantity;
                productReository.Update(getProduct);
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
                damagedProduct2.ReceiverId = damagedProductDto.ReceiverId;
                damagedProduct2.SenderId = damagedProductDto.SenderId;
                damagedProduct2.CreatedAt = DateTime.Now;
                damagedProduct2.CreatedBy = userId;
                damagedProduct2.WasNotDistributed = false;
                damagedProductRepository.Insert(damagedProduct2);
                response.Messages.Add("Created.");
                response.StatusCode = System.Net.HttpStatusCode.Created;
                return response;
            }
            else if (distributionRepository.GetById(damagedProductDto.DistributionId) == null && product.HasSerial == false)
            {
                if (product.TotalQuantity < damagedProductDto.Quantity)
                {
                    response.SetMessage(new List<string> { new string("You have to reduce quantity") });
                    return response;
                }
                //var product = productReository.GetById(damagedProductDto.ProductId);
                product.QuantityInStock = product.QuantityInStock - damagedProductDto.Quantity;
                product.TotalQuantity = product.TotalQuantity - damagedProductDto.Quantity;
                productReository.Update(product);
                DamagedProduct damagedProduct = new();
                damagedProduct.Quantity = 1;
                damagedProduct.ProductId = damagedProductDto.ProductId;
                damagedProduct.CreatedAt = DateTime.Now;
                damagedProduct.CreatedBy = userId;
                damagedProduct.WasNotDistributed = true;
                damagedProductRepository.Insert(damagedProduct);
                response.Messages.Add("Created.");
                response.StatusCode = System.Net.HttpStatusCode.Created;
                return response;
            }
            response.Messages.Add("Something went wrong.");
            response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
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
            var damagedProducts = await damagedProductRepository.GetAllDamagedProduct();
            if (damagedProducts.Count() == 0)
            {
                response.Messages.Add("Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            foreach (var item in damagedProducts)
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

        public async Task<ServiceResponse<List<GetDamagedProductDto>>> GetDamagedProductByProductId(long id)
        {
            ServiceResponse<List<GetDamagedProductDto>> response = new();
            List<GetDamagedProductDto> damagedProductDtos = new();
            var damagedProduct = damagedProductRepository.GetAll();
            var damagedProducts = damagedProduct.Where(x => x.ProductId == id);
            foreach (var items in damagedProducts)
            {
                GetDamagedProductDto damagedProductDto = new();
                var map = mapper.Map<GetDamagedProductDto>(items);
                damagedProductDtos.Add(map);
            }
            response.Data = damagedProductDtos;
            response.Messages.Add("All Damaged Products");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;
        }
        public async Task<ServiceResponse<GetDamagedProductDto>> GetDamagedProductProductNoId(long id)
        {
            ServiceResponse<GetDamagedProductDto> response = new();
            var damagedProductSerial = damagedProductSerialNoRepository.GetDamagedProductByProductNoId(id);
            if (damagedProductSerial == null)
            {
                response.Messages.Add("Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            var damagedProduct = damagedProductRepository.GetById(damagedProductSerial.DamagedProductId);
            if (damagedProduct == null)
            {
                response.Messages.Add("Damaged Product Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            var map = mapper.Map<GetDamagedProductDto>(damagedProduct);
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.Data = map;
            return response;
        }
    }
}
