using IICT_Store.Dtos.DistributionDtos;
using IICT_Store.Dtos.ProductDtos;
using IICT_Store.Models;
using IICT_Store.Models.Products;
using IICT_Store.Repositories.DamagedProductRepositories;
using IICT_Store.Repositories.DistributionRepositories;
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
        public DamagedProductService(IProductSerialNoRepository productSerialNoRepository, IDamagedProductRepository damagedProductRepository
            , IDistributionRepository distributionRepository, IProductRepository productReository)
        {
            this.productSerialNoRepository = productSerialNoRepository;
            this.damagedProductRepository = damagedProductRepository;
            this.distributionRepository = distributionRepository;
            this.productReository = productReository;
        }

        public async Task<ServiceResponse<ProductSerialNoDto>> DamageProduct(long id)
        {
            ServiceResponse<ProductSerialNoDto> response = new();
            var productSerialNo = productSerialNoRepository.GetById(id);
            var distribution = distributionRepository.GetById(productSerialNo.DistributionId);
            distribution.Quantity = distribution.Quantity - 1;
            distributionRepository.Update(distribution);
            DamagedProduct damagedProduct = new();
            damagedProduct.Quantity = 1;
            damagedProduct.ProductId = distribution.ProductId;
            damagedProduct.CreatedAt = DateTime.Now;
            damagedProductRepository.Insert(damagedProduct);
            productSerialNoRepository.Delete(id);
            response.Messages.Add("Damaged product added.");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;

        }

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
    }
}
