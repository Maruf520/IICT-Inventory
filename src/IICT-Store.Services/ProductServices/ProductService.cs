using AutoMapper;
using IICT_Store.Dtos.ProductDtos;
using IICT_Store.Models;
using IICT_Store.Models.Products;
using IICT_Store.Repositories.ProductRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.productRepository = productRepository;
        }

        public async Task<ServiceResponse<GetProductDto>> CreateProduct(CreateProductDto createProductDto)
        {
            ServiceResponse<GetProductDto> response = new();
            var productToCreate = mapper.Map<Product>(createProductDto);
            productToCreate.CreatedAt = DateTime.Now;
            productToCreate.CategoryId = createProductDto.CategoryId;
            productToCreate.TotalQuantity = 1;
            if (productToCreate.Description == null)
            {
                productToCreate.Description = "";
            }
            if (productToCreate.ImageUrl == null)
            {
                productToCreate.ImageUrl = "";
            }
            productRepository.Insert(productToCreate);
            var productToMap = mapper.Map<GetProductDto>(productToCreate);
            response.Messages.Add("Created");
            response.StatusCode = System.Net.HttpStatusCode.Created;
            response.Data = productToMap;
            return response;
        }

        public async Task<ServiceResponse<GetProductDto>> GetProductById(long id)
        {
            ServiceResponse<GetProductDto> response = new();
            var product = productRepository.GetById(id);
            if (product == null)
            {
                response.Messages.Add("Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }

            var productToMap = mapper.Map<GetProductDto>(product);
            productToMap.CategoryId = product.Category.Id;
            response.Data = productToMap;
            response.Messages.Add("Product");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;
        }

        public async Task<ServiceResponse<List<GetProductDto>>> GetALlProduct()
        {
            ServiceResponse<List<GetProductDto>> response = new();
            var products = productRepository.GetAll();
            if (products == null)
            {
                response.Messages.Add("Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            var productToMap = mapper.Map<List<GetProductDto>>(products);
            response.Messages.Add("All Products.");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.Data = productToMap;
            return response;
        }

        public async Task<ServiceResponse<CreateProductDto>> UpdateProduct(CreateProductDto createProductDto, long id)
        {
            ServiceResponse<CreateProductDto> response = new();
            var product = await productRepository.GetProductById(id);
            if (product == null)
            {
                response.Messages.Add("Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            product.CategoryId = createProductDto.CategoryId;
            product.Description = createProductDto.Description;
            product.Name = createProductDto.Name;
            product.UpdatedAt = DateTime.Now;
            productRepository.Update(product);

            response.Data = createProductDto;
            response.Messages.Add("Updated.");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;

        }

        public async Task<ServiceResponse<GetProductDto>> DeleteProduct(long productId)
        {
            ServiceResponse<GetProductDto> response = new();
            var product = await productRepository.GetProductById(productId);
            if (product == null)
            {
                response.Messages.Add("Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            productRepository.Delete(productId);
            var productToMap = mapper.Map<GetProductDto>(product);

            response.Data = productToMap;
            response.Messages.Add("Deleted.");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;
        }

        public async Task<ServiceResponse<GetProductDto>> InsertProductNo(long id,CreateProductNoDto createProductNoDto)
        {
            ServiceResponse<GetProductDto> response = new();
            var product =  productRepository.GetById(id);
            List<ProductNo> nos = new();
            foreach (var item in createProductNoDto.ProductNos)
            {
                ProductNo productNo = new();
                productNo.Name = item.Name;
                productNo.CreatedAt = DateTime.Now;
                nos.Add(productNo);
            }
            product.ProductNos = nos;

            productRepository.Update(product);
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.Messages.Add("Product Number Added.");
            return response;
        }

    }
}
