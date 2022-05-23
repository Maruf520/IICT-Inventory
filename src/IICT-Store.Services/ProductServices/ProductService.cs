using AutoMapper;
using IICT_Store.Dtos.ProductDtos;
using IICT_Store.Models;
using IICT_Store.Models.Products;
using IICT_Store.Repositories.DistributionRepositories;
using IICT_Store.Repositories.ProductNumberRepositories;
using IICT_Store.Repositories.ProductRepositories;
using IICT_Store.Repositories.ProductSerialNoRepositories;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using IICT_Store.Models.Categories;
using IICT_Store.Models.Pruchashes;
using IICT_Store.Repositories.TestRepo;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace IICT_Store.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly IDistributionRepository distributionRepository;
        private readonly IProductSerialNoRepository productSerialNoRepository;
        private readonly IProductNumberRepository productNumberRepository;
        private readonly IBaseRepo baseRepo;
        public ProductService(IProductRepository productRepository,
            IMapper mapper, IDistributionRepository distributionRepository,
            IProductSerialNoRepository productSerialNoRepository,
            IProductNumberRepository productNumberRepository, IBaseRepo baseRepo)
        {
            this.mapper = mapper;
            this.productRepository = productRepository;
            this.distributionRepository = distributionRepository;
            this.productSerialNoRepository = productSerialNoRepository;
            this.productNumberRepository = productNumberRepository;
            this.baseRepo = baseRepo;
        }

        public async Task<ServiceResponse<GetProductDto>> CreateProduct(CreateProductDto createProductDto, string userId)
        {
            ServiceResponse<GetProductDto> response = new();
            var productToCreate = mapper.Map<Product>(createProductDto);
            productToCreate.CreatedAt = DateTime.Now;
            productToCreate.CategoryId = createProductDto.CategoryId;
            if (createProductDto.Image != null)
            {
                var uploadImage = await UploadImage(createProductDto.Image);
                productToCreate.ImageUrl = uploadImage;
            }
            productToCreate.CreatedBy = userId;
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
            var productSerialNo = await productNumberRepository.GetByProductId(id);

            var damagedProductCount = productSerialNo.Where(x => x.ProductStatus != ProductStatus.Damaged).Count();
            var x = productSerialNo.Count - damagedProductCount;
            var serialNoCount = product.TotalQuantity - damagedProductCount;
            if (product == null)
            {
                response.Messages.Add("Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }

            var productToMap = mapper.Map<GetProductDto>(product);
            productToMap.CategoryId = product.CategoryId;
            if (serialNoCount < 0)
            {
                productToMap.NotSerializedProduct = 0;
            }
            else
            {
                productToMap.NotSerializedProduct = serialNoCount;
            }
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

        public async Task<ServiceResponse<CreateProductDto>> UpdateProduct(CreateProductDto createProductDto, long id, string userId)
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
            product.UpdatedBy = userId;
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

        public async Task<ServiceResponse<GetProductDto>> InsertProductNo(long id, CreateProductNoDto createProductNoDto, string userId)
        {
            ServiceResponse<GetProductDto> response = new();
            var product = productRepository.GetById(id);
            if (product == null)
            {
                response.Messages.Add("Product Not Found.");
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
                var checkSerialNo = CheckIfSerialNoExists(id, (List<ProductNoDto>)createProductNoDto.ProductNos);
                if (checkSerialNo.Result == false)
                {
                    response.Messages.Add("Please set unique serial no.");
                    return response;
                }
            }

            List<ProductNo> nos = new();
            if (product.TotalQuantity > createProductNoDto.ProductNos.Count)
            {
                response.Messages.Add("Please reduce the quantity of serial number.");
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return response;
            }
            foreach (var item in createProductNoDto.ProductNos)
            {
                ProductNo productNo = new();
                productNo.Name = item.Name;
                productNo.CreatedAt = DateTime.Now;
                productNo.ProductStatus = ProductStatus.Unassigned;
                productNo.CreatedBy = userId;
                nos.Add(productNo);
            }
            product.ProductNos = nos;
            product.UpdatedBy = userId;
            productRepository.Update(product);
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.Messages.Add("Product Number Added.");
            return response;
        }
        private static ServiceResponse<List<string>> ParseFile(string filePath)
        {
            ServiceResponse<List<string>> response = new();
            var fileInfo = new FileInfo(filePath);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            List<string> list = new();
            using (var package = new ExcelPackage(fileInfo))
            {
                var workbook = package.Workbook;
                var worksheet = workbook.Worksheets.First();
                var colCount = worksheet.Dimension.End.Column; //get Column Count
                var rowCount = worksheet.Dimension.End.Row;
                if (colCount > 1 || colCount == 0 || rowCount == 0)
                {
                    response.Messages.Add("Please enter a valid xlxs file.");
                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }

                for (var row = 2; row <= rowCount; row++)
                {
                    list.Add(worksheet.Cells[row, 1].Value.ToString());
                }
            }

            response.Data = list;
            return response;
        }

        public async Task<ServiceResponse<GetProductDto>> InsertProductNoMultiple(long id, FileUploadDto fileUploadDto, string userId)
        {
            ServiceResponse<GetProductDto> response = new();
            var product = productRepository.GetById(id);
            List<string> list = new();
            if (fileUploadDto.File.Length > 0)
            {
                var path = await UploadImage(fileUploadDto.File);
                var parseFile = ParseFile(path);
                list = parseFile.Data;
                if (parseFile.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    response.Messages = parseFile.Messages;
                    return response;
                }
            }
            if (product == null)
            {
                response.Messages.Add("Product Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            if (product.HasSerial == false)
            {
                response.Messages.Add("Sorry, this product doesn't have any serial No.");
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return response;
            }
            if (list != null)
            {
                var checkSerialNo = CheckIfSerialMultipeNoExists(id, list);
                if (checkSerialNo.Result == false)
                {
                    response.Messages.Add("Please set unique serial no.");
                    return response;
                }
            }

            List<ProductNo> nos = new();
            if (product.TotalQuantity < list.Count)
            {
                response.Messages.Add("Please reduce the quantity of serial number.");
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return response;
            }
            foreach (var item in list)
            {
                ProductNo productNo = new();
                productNo.Name = item;
                productNo.ProductId = id;
                productNo.CreatedAt = DateTime.Now;
                productNo.ProductStatus = ProductStatus.Unassigned;
                productNo.CreatedBy = userId;
                productNumberRepository.Insert(productNo);
            }
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.Messages.Add("Product Number Added.");
            return response;
        }

        public async Task<ServiceResponse<GetProductDto>> GetProductBySerialNo(long serialNo)
        {
            ServiceResponse<GetProductDto> response = new();
            return response;
        }

        public async Task<ServiceResponse<List<GetProductNoDto>>> GetAllAvailableProductno(long productId)
        {
            ServiceResponse<List<GetProductNoDto>> response = new();
            List<GetProductNoDto> productNos = new();
            var product = productRepository.GetById(productId);
            if (product.HasSerial == false)
            {
                response.Messages.Add("Sorry, this product doesn't have any serial No.");
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return response;
            }
            var distribution = await distributionRepository.GetAllSerialNo();

            var xx = await productRepository.GetAllProductNoById(productId);
            var productSerial = await productRepository.GetAllProductNoById(productId);
            foreach (var item in productSerial)
            {
                foreach (var serial in distribution)
                {
                    if (item.Id != serial.ProductNoId)
                    {
                        GetProductNoDto getProductNoDto = new();
                        getProductNoDto.Id = item.Id;
                        getProductNoDto.Name = item.Name;
                        productNos.Add(getProductNoDto);
                        xx.Remove(productSerial.Find(x => x.Id == serial.ProductNoId));
                    }
                }
            }

            List<GetProductNoDto> productNos1 = new();
            foreach (var nos in xx)
            {
                GetProductNoDto getProductNoDto = new();
                getProductNoDto.Id = nos.Id;
                getProductNoDto.Name = nos.Name;
                productNos1.Add(getProductNoDto);
            }
            response.Data = productNos1;

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
        public async Task<bool> CheckIfSerialMultipeNoExists(long productId, List<string> name)
        {
            var productSerialNo = await productRepository.GetAllProductNoById(productId);
            var nameCount = name.Count;
            for (int i = 0; i < nameCount; i++)
            {
                foreach (var serialno in productSerialNo)
                {
                    if (serialno.Name == name[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public async Task<ServiceResponse<GetProductNoDto>> ReturnProductToStore(long productNoId, string userId)
        {
            ServiceResponse<GetProductNoDto> response = new();
            var getProductNo = await productSerialNoRepository.GetByProductNoId(productNoId);
            if (getProductNo == null)
            {
                response.Messages.Add("Not  found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            productSerialNoRepository.Delete(getProductNo.Id);
            var distribution = distributionRepository.GetById(getProductNo.DistributionId);
            var product = productRepository.GetById(distribution.ProductId);
            distribution.TotalRemainingQuantity = distribution.TotalRemainingQuantity - 1;
            distribution.UpdatedBy = userId;
            distributionRepository.Update(distribution);
            product.QuantityInStock = product.QuantityInStock + 1;
            productRepository.Update(product);
            response.Messages.Add(" Product returned to stock.");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;

        }

        public async Task<string> UploadImage(IFormFile formFile)
        {
            if (formFile.Length > 0)
            {
                string fName = Path.GetRandomFileName();

                var getext = Path.GetExtension(formFile.FileName);
                var filename = Path.ChangeExtension(fName, getext);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "files");
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                filePath = Path.Combine(filePath, filename);
                var pathdb = "files/" + filename;
                using (var stream = System.IO.File.Create(filePath))
                {
                    await formFile.CopyToAsync(stream);
                    stream.Flush();
                }

                return pathdb;

            }
            return "enter valid photo";
        }

        public async Task<ServiceResponse<List<GetProductDto>>> GetProductByCategoryId(long id)
        {
            ServiceResponse<List<GetProductDto>> response = new();
            var products = await productRepository.GetProductByCategoryId(id);

            List<GetProductDto> productDtos = new();
            if (products.Count == 0)
            {
                response.Messages.Add("Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            foreach (var product in products)
            {
                var productNos = await productNumberRepository.GetByProductId(product.Id);
                var productNosWithoutDmamgedProduct = productNos.Where(x => x.ProductStatus != ProductStatus.Damaged).Count();
                var map = mapper.Map<GetProductDto>(product);
                if (product.HasSerial == true)
                {
                    map.NotSerializedProduct = product.TotalQuantity - productNosWithoutDmamgedProduct;
                }
                productDtos.Add(map);
            }
            response.Messages.Add("All products.");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.Data = productDtos;
            return response;
        }

        public async Task<ServiceResponse<List<ProductReportDto>>> GetProductDetaills(int year, int productId)
        {
            ServiceResponse<List<ProductReportDto>> response = new();
            var allProducts = await productRepository.GetAllProduct();
            List<ProductReportDto> reportDtos = new();
            if (productId > 0)
            {
                ProductReportDto productReport = new();
                var product = productRepository.GetById(productId);
                if (product == null)
                {
                    response.SetMessage(new List<string> { new string("Product Not Found.") }, HttpStatusCode.NotFound);
                    return response;
                }
                var productMap = mapper.Map<GetProductDto>(product);
                var distributedProduct = baseRepo.GetItems<Distribution>(x =>
                    x.ProductId == productId && x.TotalRemainingQuantity > 0);
                var totalRemainingQuantity = distributedProduct.Select(x => x.TotalRemainingQuantity).Sum();
                var damagedProduct = baseRepo.GetItems<DamagedProduct>(x => x.ProductId == productId);
                var totalDamagedQuantity = damagedProduct.Select(x => x.Quantity).Sum();
                var maintananceProducts = baseRepo.GetItems<MaintenanceProduct>(x => x.ProductId == productId);
                int maintananceProductQuantity = 0;
                foreach (var maintenanceProduct in maintananceProducts)
                {
                    var maintananaceproductCount = baseRepo.GetItems<MaintenanceProductSerialNo>(x =>
                        x.MaintananceProductId == maintenanceProduct.Id && x.IsRepaired == false).Count();
                    maintananceProductQuantity += maintananaceproductCount;
                }

                if (year > 0)
                {
                    var boughtProductOfYear = baseRepo.GetItems<Purchashed>(x => x.CreatedAt.Year == year && x.ProductId == productId && x.IsConfirmed).Select(x => x.Quantity).Sum();
                    productReport.TotalBoughtProduct = boughtProductOfYear;
                }
                productReport.Product = productMap;
                productReport.TotalProduct = product.TotalQuantity;
                productReport.ProductInStock = product.QuantityInStock;
                productReport.TotalDistributedProduct = totalRemainingQuantity;
                productReport.TotalDamagedProduct = totalDamagedQuantity;
                productReport.TotalMaintenanceProduct = maintananceProductQuantity;
                reportDtos.Add(productReport);
                response.SetMessage(new List<string> { new("All") });
                response.Data = reportDtos;
                return response;
            }
            foreach (var singleProduct in allProducts)
            {
                ProductReportDto productReport = new();
                var product = productRepository.GetById(singleProduct.Id);
                var productMap = mapper.Map<GetProductDto>(product);
                var distributedProduct = baseRepo.GetItems<Distribution>(x =>
                    x.ProductId == singleProduct.Id && x.TotalRemainingQuantity > 0);
                var totalRemainingQuantity = distributedProduct.Select(x => x.TotalRemainingQuantity).Sum();
                var damagedProduct = baseRepo.GetItems<DamagedProduct>(x => x.ProductId == singleProduct.Id);
                var totalDamagedQuantity = damagedProduct.Select(x => x.Quantity).Sum();
                var maintananceProducts = baseRepo.GetItems<MaintenanceProduct>(x => x.ProductId == singleProduct.Id);
                int maintananceProductQuantity = 0;
                foreach (var maintenanceProduct in maintananceProducts)
                {
                    var maintananaceproductCount = baseRepo.GetItems<MaintenanceProductSerialNo>(x =>
                        x.MaintananceProductId == maintenanceProduct.Id && x.IsRepaired == false).Count();
                    maintananceProductQuantity += maintananaceproductCount;
                }

                if (year > 0)
                {
                    var boughtProductOfYear = baseRepo.GetItems<Purchashed>(x => x.CreatedAt.Year == year && x.ProductId == singleProduct.Id && x.IsConfirmed).Select(x => x.Quantity).Sum();
                    productReport.TotalBoughtProduct = boughtProductOfYear;
                }
                productReport.Product = productMap;
                productReport.TotalProduct = product.TotalQuantity;
                productReport.ProductInStock = product.QuantityInStock;
                productReport.TotalDistributedProduct = totalRemainingQuantity;
                productReport.TotalDamagedProduct = totalDamagedQuantity;
                productReport.TotalMaintenanceProduct = maintananceProductQuantity;
                reportDtos.Add(productReport);
            }

            response.SetMessage(new List<string> { new("All") });
            response.Data = reportDtos;
            return response;
        }


        public async Task<ServiceResponse<DashboardInformationDto>> GetDashboardInformation()
        {
            ServiceResponse<DashboardInformationDto> response = new();
            var category = baseRepo.GetAll<Category>().ToList();
            var products = baseRepo.GetAll<Product>().ToList();
            var damaged = baseRepo.GetAll<DamagedProduct>();
            int totalProduct = products.Select(x => x.TotalQuantity).Sum();
            int totalUnassignedProduct = products.Select(x => x.TotalQuantity).Sum();
            int totalCategory = category.Count;
            int totalDamagedProduct = damaged.Select(x => x.Quantity).Sum();

            DashboardInformationDto informationDto = new();
            informationDto.TotalCategory = totalCategory;
            informationDto.TotalDamaged = totalDamagedProduct;
            informationDto.TotalProduct = totalProduct;
            informationDto.TotalUnAssigned = totalUnassignedProduct;
            response.Data = informationDto;
            response.SetMessage(new List<string> { new string("Product informartion") }, HttpStatusCode.OK);
            return response;
        }
    }
}
