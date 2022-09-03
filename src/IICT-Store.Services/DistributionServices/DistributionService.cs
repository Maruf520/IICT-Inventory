using AutoMapper;
using IICT_Store.Dtos.DistributionDtos;
using IICT_Store.Dtos.ProductDtos;
using IICT_Store.Models;
using IICT_Store.Models.Products;
using IICT_Store.Repositories.DistributionRepositories;
using IICT_Store.Repositories.ProductNumberRepositories;
using IICT_Store.Repositories.ProductRepositories;
using IICT_Store.Repositories.ProductSerialNoRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Internal;
using IICT_Store.Repositories.TestRepo;
using Microsoft.Extensions.Logging;
using IICT_Store.Models.Persons;
using IICT_Store.Dtos.UserDtos;
using IICT_Store.Repositories.UserRepositories;
using IICT_Store.Repositories.PersonRepositories;

namespace IICT_Store.Services.DistributionServices
{
    public class DistributionService : IDistributionService
    {
        private readonly IDistributionRepository distributionRepository;
        private readonly IMapper mapper;
        private readonly IProductRepository productRepository;
        private readonly IProductSerialNoRepository productSerialNoRepository;
        private readonly IProductNumberRepository productNumberRepository;
        private readonly ILogger<DistributionService> logger;
        private readonly IBaseRepo baseRepo;
        private readonly IUserRepository userRepository;
        private readonly IPersonRepository personRepository;
        public DistributionService(IPersonRepository personRepository, IDistributionRepository distributionRepository, IMapper mapper, IProductRepository productRepository, IProductSerialNoRepository productSerialNoRepository, IProductNumberRepository productNumberRepository, ILogger<DistributionService> logger, IBaseRepo baseRepo, IUserRepository userRepository)
        {
            this.distributionRepository = distributionRepository;
            this.mapper = mapper;
            this.productRepository = productRepository;
            this.productSerialNoRepository = productSerialNoRepository;
            this.productNumberRepository = productNumberRepository;
            this.logger = logger;
            this.baseRepo = baseRepo;
            this.userRepository = userRepository;
            this.personRepository = personRepository;
        }
        public async Task<ServiceResponse<GetDistributionDto>> Create(CreateDistributionDto createDistributionDto, string userId)
        {
            ServiceResponse<GetDistributionDto> response = new();
            var getSenderId = baseRepo.GetItems<Person>(x => x.Email == "maruf@mail.com").FirstOrDefault();
            if (getSenderId == null)
            {
                response.Messages.Add("Sender ID Not Found!");
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }
            var product = productRepository.GetById(createDistributionDto.ProductId);
            if (product == null)
            {
                response.Messages.Add("Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }

            if (product.QuantityInStock < createDistributionDto.Quantity)
            {
                response.Messages.Add($"Please reduce the quantity of this product. This product has {product.QuantityInStock} items in stock.");
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return response;
            }
            List<ProductSerialNo> productSerialNos = new();
            var distributionToCreate = mapper.Map<Distribution>(createDistributionDto);
            distributionToCreate.SenderId = getSenderId.Id;
            distributionToCreate.TotalRemainingQuantity = createDistributionDto.Quantity;
            distributionToCreate.CreatedAt = DateTime.Now;
            distributionToCreate.CreatedBy = userId;
            if (product.HasSerial == true)
                foreach (var item in createDistributionDto.ProductSerialNo)
                {
                    ProductSerialNo productSerialNo = new();
                    productSerialNo.ProductNoId = item.ProductNoId;
                    productSerialNo.CreatedAt = DateTime.Now;
                    productSerialNo.DistributionId = distributionToCreate.Id;
                    productSerialNo.ProductStatus = ProductStatus.Assigned;
                    productSerialNos.Add(productSerialNo);
                    ProductNo productNo = new();
                    var productno = productNumberRepository.GetById(item.ProductNoId);
                    productno.ProductStatus = ProductStatus.Assigned;
                    productNumberRepository.Update(productno);

                }
            if (product.HasSerial == true)
            {
                if (productSerialNos.Any(x => x.ProductNoId == 0))
                {
                    response.Messages.Add("Please select items to distribute.");
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                    return response;
                }
                foreach (var item in productSerialNos)
                {
                    var productNo = productNumberRepository.GetById(item.ProductNoId);
                    productNo.ProductStatus = ProductStatus.Assigned;

                    productNumberRepository.Update(productNo);
                }
            }


            //distributionToCreate.ProductSerialNo = productSerialNos;
            var IfDistributionExists = await distributionRepository.GetByRoomIdAndProductId(createDistributionDto.RoomNo, createDistributionDto.ProductId);
            if (IfDistributionExists != null && IfDistributionExists.DistributedTo == 0)
            {
                IfDistributionExists.Quantity = IfDistributionExists.Quantity + createDistributionDto.Quantity;
                IfDistributionExists.TotalRemainingQuantity = IfDistributionExists.Quantity + createDistributionDto.Quantity;
                IfDistributionExists.UpdatedAt = DateTime.Now;
                distributionRepository.Update(IfDistributionExists);
                product.QuantityInStock = product.QuantityInStock - createDistributionDto.Quantity;
                productRepository.Update(product);
                if (productSerialNos.Count > 0 && productSerialNos.Any(x => x.ProductNoId != 0))
                {
                    foreach (var prodyuctSerial in productSerialNos)
                    {
                        prodyuctSerial.DistributionId = IfDistributionExists.Id;
                        productSerialNoRepository.Insert(prodyuctSerial);
                    }
                }
                var distributionToReturn1 = mapper.Map<GetDistributionDto>(createDistributionDto);
                response.Data = distributionToReturn1;
                response.Messages.Add("Created.");
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return response;
            }
            product.QuantityInStock = product.QuantityInStock - createDistributionDto.Quantity;
            productRepository.Update(product);
            distributionRepository.Insert(distributionToCreate);
            List<ProductSerialNo> productSerialNos1 = new();
            if (product.HasSerial == true)
            {
                foreach (var item in createDistributionDto.ProductSerialNo)
                {
                    ProductSerialNo productSerialNo = new();
                    productSerialNo.ProductNoId = item.ProductNoId;
                    productSerialNo.CreatedAt = DateTime.Now;
                    productSerialNo.DistributionId = distributionToCreate.Id;
                    productSerialNo.ProductStatus = ProductStatus.Assigned;
                    productSerialNos1.Add(productSerialNo);
                    ProductNo productNo = new();
                    var productno = productNumberRepository.GetById(item.ProductNoId);
                    productno.ProductStatus = ProductStatus.Assigned;
                    productNumberRepository.Update(productno);

                }
                if (productSerialNos.Count > 0 && productSerialNos.Any(x => x.ProductNoId != 0))
                {
                    foreach (var prodyuctSerial in productSerialNos1)
                    {
                        productSerialNoRepository.Insert(prodyuctSerial);
                    }
                }
            }
            var distributionToReturn = mapper.Map<GetDistributionDto>(createDistributionDto);
            response.Data = distributionToReturn;
            response.Messages.Add("Created.");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;
        }

        public async Task<ServiceResponse<GetDistributionDto>> GetById(long id)
        {
            ServiceResponse<GetDistributionDto> response = new();
            var distribution = await distributionRepository.GetDistributionById(id);
            if (distribution == null)
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
            foreach (var products in distributionToReturn)
            {
                GetProductDto productDto = new();
                var singleProduct = productRepository.GetById(products.ProductId);
                productDto.CategoryId = singleProduct.CategoryId;
                productDto.Name = singleProduct.Name;
                productDto.Description = singleProduct.Description;
                productDto.ImageUrl = singleProduct.ImageUrl;
                productDto.HasSerial = singleProduct.HasSerial;
                productDto.Id = singleProduct.Id;
                productDto.TotalQuantity = singleProduct.TotalQuantity;
                productDto.QuantityInStock = singleProduct.QuantityInStock;
                productDto.CreatedAt = singleProduct.CreatedAt;
                productDto.UpdatedAt = singleProduct.UpdatedAt;
                products.Product = productDto;
                var productSerial = await productSerialNoRepository.GetProductNoIdByDistributionId(products.Id);
                List<GetProductSerialDto> getProductSerialDtos = new();
                foreach (var productserial in productSerial)
                {
                    GetProductSerialDto getSerialDto = new();
                    var productNo = productNumberRepository.GetById(productserial.ProductNoId);
                    getSerialDto.Name = productNo.Name;
                    getSerialDto.Id = productNo.Id;
                    getSerialDto.ProductStatus = productserial.ProductStatus;
                    getProductSerialDtos.Add(getSerialDto);
                }

                products.GetProductSerialNo = getProductSerialDtos;
            }
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
            foreach (var products in map)
            {
                GetProductDto productDto = new();
                var singleProduct = productRepository.GetById(products.ProductId);
                productDto.CategoryId = singleProduct.CategoryId;
                productDto.Name = singleProduct.Name;
                productDto.Description = singleProduct.Description;
                productDto.ImageUrl = singleProduct.ImageUrl;
                productDto.HasSerial = singleProduct.HasSerial;
                productDto.Id = singleProduct.Id;
                productDto.TotalQuantity = singleProduct.TotalQuantity;
                productDto.QuantityInStock = singleProduct.QuantityInStock;
                productDto.CreatedAt = singleProduct.CreatedAt;
                productDto.UpdatedAt = singleProduct.UpdatedAt;
                products.Product = productDto;
                var productSerial = await productSerialNoRepository.GetProductNoIdByDistributionId(products.Id);
                List<GetProductSerialDto> getProductSerialDtos = new();
                foreach (var productserial in productSerial)
                {
                    GetProductSerialDto getSerialDto = new();
                    var productNo = productNumberRepository.GetById(productserial.ProductNoId);
                    getSerialDto.Name = productNo.Name;
                    getSerialDto.Id = productNo.Id;
                    getSerialDto.ProductStatus = productserial.ProductStatus;
                    getProductSerialDtos.Add(getSerialDto);
                }

                products.GetProductSerialNo = getProductSerialDtos;
            }

            var groupby = map.GroupBy(x => x.ProductId).Select(x => x.First()).ToList();

            response.Data = map;
            response.Messages.Add("All distribution");
            return response;
        }

        public async Task<ServiceResponse<List<GetDistributionDto>>> GetAllDistributionByProductId(long id)
        {
            ServiceResponse<List<GetDistributionDto>> response = new();
            var product = productRepository.GetById(id);
            if (product == null)
            {
                response.Messages.Add("Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }

            var distributions = await distributionRepository.GetAllDistributionByProductId(id);
            var map = mapper.Map<List<GetDistributionDto>>(distributions);
            var xxx = map.GroupBy(x => x.RoomNo | x.DistributedTo).ToList();
            response.Messages.Add("All distributions.");
            response.Data = map;
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;
        }


        public async Task<ServiceResponse<List<GetDistributionDto>>> GetDirstribution(long productId = 0, long personId = 0, int roomNo = 0)
        {
            ServiceResponse<List<GetDistributionDto>> response = new();
            var distribution = distributionRepository.GetAll();
            if (personId == 0 && roomNo == 0 && productId > 0)
            {
                var distributionToPerson =
                   distribution.Where(x => x.ProductId == productId);
                if (productId == 0)
                {
                    distributionToPerson = distribution.Where(x => x.DistributedTo == personId);
                }
                var distributionToReturn = mapper.Map<List<GetDistributionDto>>(distributionToPerson);
                foreach (var products in distributionToReturn)
                {
                    GetProductDto productDto = new();
                    var singleProduct = productRepository.GetById(products.ProductId);
                    productDto.CategoryId = singleProduct.CategoryId;
                    productDto.Name = singleProduct.Name;
                    productDto.Description = singleProduct.Description;
                    productDto.ImageUrl = singleProduct.ImageUrl;
                    productDto.HasSerial = singleProduct.HasSerial;
                    productDto.Id = singleProduct.Id;
                    productDto.TotalQuantity = singleProduct.TotalQuantity;
                    productDto.QuantityInStock = singleProduct.QuantityInStock;
                    productDto.CreatedAt = singleProduct.CreatedAt;
                    productDto.UpdatedAt = singleProduct.UpdatedAt;
                    products.Product = productDto;
                    var productSerial = await productSerialNoRepository.GetProductNoIdByDistributionId(products.Id);
                    List<GetProductSerialDto> getProductSerialDtos = new();
                    foreach (var productserial in productSerial)
                    {
                        GetProductSerialDto getSerialDto = new();
                        var productNo = productNumberRepository.GetById(productserial.ProductNoId);
                        getSerialDto.Name = productNo.Name;
                        getSerialDto.Id = productNo.Id;
                        getSerialDto.ProductStatus = productserial.ProductStatus;
                        getProductSerialDtos.Add(getSerialDto);
                    }

                    products.GetProductSerialNo = getProductSerialDtos;
                }

                response.Data = distributionToReturn;
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }
            if (personId > 0)
            {
                var distributionToPerson =
                    distribution.Where(x => x.DistributedTo == personId && x.ProductId == productId);
                if (productId == 0)
                {
                    distributionToPerson = distribution.Where(x => x.DistributedTo == personId);
                }
                var distributionToReturn = mapper.Map<List<GetDistributionDto>>(distributionToPerson);
                foreach (var products in distributionToReturn)
                {
                    GetProductDto productDto = new();
                    var singleProduct = productRepository.GetById(products.ProductId);
                    productDto.CategoryId = singleProduct.CategoryId;
                    productDto.Name = singleProduct.Name;
                    productDto.Description = singleProduct.Description;
                    productDto.ImageUrl = singleProduct.ImageUrl;
                    productDto.HasSerial = singleProduct.HasSerial;
                    productDto.Id = singleProduct.Id;
                    productDto.TotalQuantity = singleProduct.TotalQuantity;
                    productDto.QuantityInStock = singleProduct.QuantityInStock;
                    productDto.CreatedAt = singleProduct.CreatedAt;
                    productDto.UpdatedAt = singleProduct.UpdatedAt;
                    products.Product = productDto;
                    var productSerial = await productSerialNoRepository.GetProductNoIdByDistributionId(products.Id);
                    List<GetProductSerialDto> getProductSerialDtos = new();
                    foreach (var productserial in productSerial)
                    {
                        GetProductSerialDto getSerialDto = new();
                        var productNo = productNumberRepository.GetById(productserial.ProductNoId);
                        getSerialDto.Name = productNo.Name;
                        getSerialDto.Id = productNo.Id;
                        getSerialDto.ProductStatus = productserial.ProductStatus;
                        getProductSerialDtos.Add(getSerialDto);
                    }

                    products.GetProductSerialNo = getProductSerialDtos;
                }

                response.Data = distributionToReturn;
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }
            if (roomNo > 0)
            {
                var distributionToRoom = distribution.Where(x => x.RoomNo == roomNo && x.ProductId == productId);
                if (productId == 0)
                {
                    distributionToRoom = distribution.Where(x => x.RoomNo == roomNo);
                }
                var distributionToReturn = mapper.Map<List<GetDistributionDto>>(distributionToRoom);
                foreach (var products in distributionToReturn)
                {
                    GetProductDto productDto = new();
                    var singleProduct = productRepository.GetById(products.ProductId);
                    productDto.CategoryId = singleProduct.CategoryId;
                    productDto.Name = singleProduct.Name;
                    productDto.Description = singleProduct.Description;
                    productDto.ImageUrl = singleProduct.ImageUrl;
                    productDto.HasSerial = singleProduct.HasSerial;
                    productDto.Id = singleProduct.Id;
                    productDto.TotalQuantity = singleProduct.TotalQuantity;
                    productDto.QuantityInStock = singleProduct.QuantityInStock;
                    productDto.CreatedAt = singleProduct.CreatedAt;
                    productDto.UpdatedAt = singleProduct.UpdatedAt;
                    products.Product = productDto;
                    var productSerial = await productSerialNoRepository.GetProductNoIdByDistributionId(products.Id);
                    List<GetProductSerialDto> getProductSerialDtos = new();
                    foreach (var productserial in productSerial)
                    {
                        GetProductSerialDto getSerialDto = new();
                        var productNo = productNumberRepository.GetById(productserial.ProductNoId);
                        getSerialDto.Name = productNo.Name;
                        getSerialDto.Id = productNo.Id;
                        getSerialDto.ProductStatus = productserial.ProductStatus;
                        getProductSerialDtos.Add(getSerialDto);
                    }

                    products.GetProductSerialNo = getProductSerialDtos;
                }

                response.Data = distributionToReturn;
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }
            response.SetNotFoundMessage();
            return response;

        }

        public async Task<ServiceResponse<GetDistributionDto>> GetDistributionByProductNoId(long id)
        {
            ServiceResponse<GetDistributionDto> response = new();
            var productNo = productNumberRepository.GetById(id);
            var productSeria = await productSerialNoRepository.GetByProductNoId(id);
            var x = baseRepo.GetItems<ProductSerialNo>(x => x.ProductNoId == productNo.Id).ToList();
            var productSerial = x.OrderByDescending(x => x.Id).FirstOrDefault();
            if (productSerial == null)
            {
                response.Messages.Add("Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            List<GetProductSerialDto> getProductSerialDtos = new();
            var distribution = distributionRepository.GetById(productSerial.DistributionId);
            var productSerials = await productSerialNoRepository.GetProductNoIdByDistributionId(distribution.Id);
            foreach (var productserial in productSerials)
            {
                var productno = productNumberRepository.GetById(productserial.ProductNoId);
                var productNoToMap = mapper.Map<GetProductSerialDto>(productno);
                getProductSerialDtos.Add(productNoToMap);
            }
            var map = mapper.Map<GetDistributionDto>(distribution);
            map.CreatedByByUser = mapper.Map<GetUserDto>(await userRepository.GetById(distribution.CreatedBy));
            map.UpdatedByUser = mapper.Map<GetUserDto>(await userRepository.GetById(distribution.UpdatedBy));
            map.GetProductSerialNo = getProductSerialDtos;
            response.Data = map;
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;

        }

        public async Task<ServiceResponse<GetDistributionDto>> CreateNew(CreateDistributionDto createDistributionDto, string userId)
        {
            this.logger.LogInformation($"CreateNew Service STARTED");
            ServiceResponse<GetDistributionDto> response = new();
            try
            {
                var getAllPerson = personRepository.GetAll().ToList();
                var getSenderId = getAllPerson.Where(x => x.Email == "md.maruf5201@gmail.com").FirstOrDefault();
                if (getSenderId == null)
                {
                    response.Messages.Add("Sender ID Not Found!");
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }
                var product = productRepository.GetById(createDistributionDto.ProductId);
                if (product == null)
                {
                    this.logger.LogInformation($"CreateNew Service ENDED with Not Found");
                    response.SetMessage();
                    return response;
                }

                if (product.QuantityInStock < createDistributionDto.Quantity)
                {
                    this.logger.LogInformation($"CreateNew Service Ended with Message: Please reduce the quantity of this product. This product has {product.QuantityInStock} items in stock.");
                    response.Messages.Add($"Please reduce the quantity of this product. This product has {product.QuantityInStock} items in stock.");
                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }
                List<ProductSerialNo> productSerialNos = new();
                var distributionToCreate = mapper.Map<Distribution>(createDistributionDto);
                distributionToCreate.SenderId = getSenderId.Id;
                distributionToCreate.TotalRemainingQuantity = createDistributionDto.Quantity;
                distributionToCreate.CreatedAt = DateTime.Now;
                distributionToCreate.CreatedBy = userId;
                if (createDistributionDto.RoomNo != 0)
                {
                    distributionToCreate.RoomNo = createDistributionDto.RoomNo;
                }

                if (createDistributionDto.DistributedTo != 0)
                {
                    distributionToCreate.DistributedTo = createDistributionDto.DistributedTo;
                }
                distributionToCreate.ReceiverId = createDistributionDto.ReceiverId;
                distributionToCreate.OrderNo = createDistributionDto.OrderNo;
                distributionToCreate.Description = createDistributionDto.Description;
                distributionToCreate.NameOfUser = createDistributionDto.NameOfUser;
                distributionRepository.Insert(distributionToCreate);
                product.QuantityInStock = product.QuantityInStock - createDistributionDto.Quantity;
                productRepository.Update(product);
                if (product.HasSerial == true)
                {
                    foreach (var item in createDistributionDto.ProductSerialNo)
                    {
                        ProductSerialNo productSerialNo = new();
                        productSerialNo.ProductNoId = item.ProductNoId;
                        productSerialNo.CreatedAt = DateTime.Now;
                        productSerialNo.DistributionId = distributionToCreate.Id;
                        productSerialNo.ProductStatus = ProductStatus.Assigned;
                        productSerialNos.Add(productSerialNo);
                        ProductNo productNo = new();
                        var productno = productNumberRepository.GetById(item.ProductNoId);
                        productno.ProductStatus = ProductStatus.Assigned;
                        productNumberRepository.Update(productno);
                        productSerialNoRepository.Insert(productSerialNo);
                    }
                }
                var distributionToReturn = mapper.Map<GetDistributionDto>(createDistributionDto);
                response.Data = distributionToReturn;
                response.Messages.Add("Created.");
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return response;
            }
            catch (Exception e)
            {
                this.logger.LogError($"CreateNew Service Exception: {e.Message}");
                response.SetMessage(new List<string> { e.Message }, HttpStatusCode.InternalServerError);
                return response;
            }
        }
    }
}
