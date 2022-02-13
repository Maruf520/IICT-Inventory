using IICT_Store.Dtos.ProductDtos;
using IICT_Store.Models;
using IICT_Store.Models.Products;
using IICT_Store.Repositories.ProductNumberRepositories;
using IICT_Store.Repositories.ProductRepositories;
using IICT_Store.Services.ProductServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Services.ProductNumberServices
{
    public class ProductNumberService : IProductNumberService
    {
        private readonly IProductRepository productRepository;
        private readonly IProductNumberRepository productNumberRepository;
        public ProductNumberService(IProductNumberRepository productNumberRepository, IProductRepository productRepository)
        {
            this.productNumberRepository = productNumberRepository;
            this.productRepository = productRepository;
        }
        public async Task<ServiceResponse<GetProductDto>> InsertProductNo(long id, CreateProductNoDto createProductNoDto)
        {
            ServiceResponse<GetProductDto> response = new();
            var product = productRepository.GetById(id);
            if(product == null)
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
                if(avaiableAmount < createProductNoDto.ProductNos.Count)
                {
                    response.Messages.Add("You have to reduce product item.");
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                    return response;
                }
            }
/*            if(product.TotalQuantity >= productCount)*/
            if(product.QuantityInStock < createProductNoDto.ProductNos.Count)
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
    }
}
