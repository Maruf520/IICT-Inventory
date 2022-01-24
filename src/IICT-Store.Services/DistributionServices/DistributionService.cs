using AutoMapper;
using IICT_Store.Dtos.DistributionDtos;
using IICT_Store.Dtos.ProductDtos;
using IICT_Store.Models;
using IICT_Store.Models.Products;
using IICT_Store.Repositories.DistributionRepositories;
using IICT_Store.Repositories.ProductRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Services.DistributionServices
{
    public class DistributionService : IDistributionService
    {
        private readonly IDistributionRepository distributionRepository;
        private readonly IMapper mapper;
        private readonly IProductRepository productRepository;
        public DistributionService(IDistributionRepository distributionRepository, IMapper mapper, IProductRepository productRepository)
        {
            this.distributionRepository = distributionRepository;
            this.mapper = mapper;
            this.productRepository = productRepository;
        }
        public async Task<ServiceResponse<GetDistributionDto>> Create(CreateDistributionDto createDistributionDto)
        {
            ServiceResponse<GetDistributionDto> response = new();
            var product = productRepository.GetById(createDistributionDto.ProductId);
            if(product == null)
            {
                response.Messages.Add("Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }

            if(product.QuantityInStock < createDistributionDto.Quantity)
            {
                response.Messages.Add($"Please reduce the quantity of this product. This product has {product.QuantityInStock} items in stock.");
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return response;
            }
            var serialNos = await distributionRepository.GetAllSerialNo();
            
            List<ProductSerialNo> productSerialNos = new();
            foreach(var item in createDistributionDto.ProductSerialNo)
            {
                ProductSerialNo productSerialNo = new();
                productSerialNo.ProductNoId = item.ProductNoId;
                productSerialNo.CreatedAt = DateTime.Now;
                productSerialNos.Add(productSerialNo);
            }
            if(createDistributionDto.Quantity < productSerialNos.Count)
            {
                response.Messages.Add($"Please increase the quantity of this product to {productSerialNos.Count} .");
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return response;
            }
            foreach(var item in serialNos)
            {
                foreach(var nos in productSerialNos)
                {
                    if(nos.ProductNoId == item.ProductNoId)
                    {
                        response.Messages.Add("This product already has been distributed.");
                        response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                        return response;
                    }
                }
            }
            var distributionToCreate = mapper.Map<Distribution>(createDistributionDto);
            distributionToCreate.ProductSerialNo = productSerialNos;
            product.QuantityInStock = product.QuantityInStock - createDistributionDto.Quantity;
            productRepository.Update(product);
            distributionRepository.Insert(distributionToCreate);
            var distributionToReturn = mapper.Map<GetDistributionDto>(createDistributionDto);
            response.Data = distributionToReturn;
            response.Messages.Add("Created.");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;
        }

        public async Task<ServiceResponse<List<GetDistributionDto>>> GetAllDistributionByProductId(long productId)
        {
            ServiceResponse<List<GetDistributionDto>> response = new();
            var distributions = await distributionRepository.GetAllDistributionByProductId(productId);
            List<int>rooms = new();
            List<ProductSerialNoDto> productSerialNoDtos = new();
            List<ProductSerialNo> productSerialNos = new();
            if(distributions == null)
            {
                response.Messages.Add("Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            foreach(var items in distributions)
            {
                rooms.Add((int)items.RoomNo);
            }
/*            foreach(var item in distributions)
            {
                foreach(var rom in rooms )
                {
                    if(item.RoomNo == rom)
                    {
                        var x = item.ProductSerialNo.Count;
*//*                        for(int y = 0; y < x;y++)
                        {
                            ProductSerialNoDto productSerialNo = new();
                            productSerialNo.ProductNoId = item.ProductSerialNo
                        productSerialNos.Add(item.ProductSerialNo);
                        }*//*
                        foreach(var z in item.ProductSerialNo)
                        {
                            ProductSerialNoDto productSerialNo = new();
                            productSerialNo.ProductNoId = z.ProductNoId;
                            productSerialNoDtos.Add(productSerialNo);
                            
                        }

                       *//* productSerialNoDtos.Add((ProductSerialNoDto)item.ProductSerialNo);*//*
                    }
                }
            }*/
            foreach(var rom in rooms)
            {
                var x = distributions.Where(x =>x.RoomNo == rom).ToList();
                foreach(var serial in x)
                {
                    
                    foreach(var y in serial.ProductSerialNo)
                    {
                        SerialDistributionDto serialDistributionDto = new();
                        serialDistributionDto.RoomNo = (int)serial.RoomNo;
                        
                        ProductSerialNoDto productSerialNo = new();
                        productSerialNo.ProductNoId = y.ProductNoId;
/*                        serialDistributionDto.ProductSerialNoDtos.Add(serialDistributionDto);*/
                    }
                }

            }
            var distributionTomap = mapper.Map<List<GetDistributionDto>>(distributions);
            response.Messages.Add("all products");
            response.Data = distributionTomap;
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;
            
        }

        public async Task<ServiceResponse<GetDistributionDto>> GetById(long id)
        {
            ServiceResponse<GetDistributionDto> response = new();
            var distribution = await distributionRepository.GetDistributionById(id);
            if(distribution == null)
            {
                response.Messages.Add("Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }

            var distributionToReturn = mapper.Map<GetDistributionDto>(distribution);
            response.Data = distributionToReturn;
            response.Messages.Add("Distribution");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;
        }

        public async Task<ServiceResponse<List<GetDistributionDto>>> GetByPersonId(long personId)
        {
            ServiceResponse<List<GetDistributionDto>> response = new();
            var distribution = await distributionRepository.GetDistributionByPerson(personId);
            if (distribution == null)
            {
                response.Messages.Add("Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }

            var distributionToReturn = mapper.Map<List<GetDistributionDto>>(distribution);
            response.Data = distributionToReturn;
            response.Messages.Add("All he takes.");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;

        }

        public async Task<ServiceResponse<List<GetDistributionDto>>> GetByRoomNo(int roomNo)
        {
            ServiceResponse<List<GetDistributionDto>> response = new();
            var product = await distributionRepository.GetDistributionByRoomNo(roomNo);
            if (product == null)
            {
                response.Messages.Add("Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            var map = mapper.Map<List<GetDistributionDto>>(product);
            
            response.Data = map;
            response.Messages.Add("All distribution");
            return response;
        }

    }
}
