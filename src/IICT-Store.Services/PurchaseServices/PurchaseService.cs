using AutoMapper;
using IICT_Store.Dtos.ProductDtos;
using IICT_Store.Dtos.Purchases;
using IICT_Store.Models;
using IICT_Store.Models.Pruchashes;
using IICT_Store.Repositories.ProductRepositories;
using IICT_Store.Repositories.PurchaseRepositories;
using IICT_Store.Services.ProductServices;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Services.PurchaseServices
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IMapper mapper;
        private readonly IPurchaseRepository purchaseRepository;
        private readonly IProductService productService;
        private readonly IProductRepository productRepository;
        public PurchaseService(IPurchaseRepository purchaseRepository, IProductService productService, IMapper mapper, IProductRepository productRepository)
        {
            this.purchaseRepository = purchaseRepository;
            this.productService = productService;
            this.mapper = mapper;
            this.productRepository = productRepository;
        }
        public async Task<ServiceResponse<GetPurchaseDto>> CreatePurchase(CreatePurchasedDto createPurchaseDto)
        {
            ServiceResponse<GetPurchaseDto> response = new();

            var products = productRepository.GetById(createPurchaseDto.ProductId);
            List<CashMemo> cashMemos = new List<CashMemo>();
            if (createPurchaseDto.File != null)
            {


                var documentslist = await UploadFile(createPurchaseDto.File);
   
                foreach (var document in documentslist)
                {
                    var cashMemo = new CashMemo();
                    cashMemo.ImageUrl = document;

                    cashMemo.CreatedAt = DateTime.Now;
                    cashMemos.Add(cashMemo);
                }
            }
            var product = await productService.GetProductById(createPurchaseDto.ProductId);
            if (product == null)
            {
                response.Messages.Add("Product Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            var productToMap = mapper.Map<Purchashed>(createPurchaseDto);

            var productToReturn = mapper.Map<GetPurchaseDto>(createPurchaseDto);
            productToMap.CashMemos = cashMemos;
            products.QuantityInStock = products.QuantityInStock + createPurchaseDto.Quantity;
            products.TotalQuantity = products.TotalQuantity + createPurchaseDto.Quantity;
            productRepository.Update(products);
            purchaseRepository.Insert(productToMap);
            response.Data = productToReturn;
            response.Messages.Add("Purchased.");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;
        }

        public async Task<ServiceResponse<GetPurchaseDto>> GetPurchaseById(long id)
        {
            ServiceResponse<GetPurchaseDto> response = new();
            
            var purchase =  await purchaseRepository.GetPurchashedById(id);
            if(purchase == null)
            {
                response.Messages.Add("Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            var purchaseToMap = mapper.Map<GetPurchaseDto>(purchase);

            response.Data = purchaseToMap;
            response.Messages.Add("Purcahse.");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;
        }

        public async Task<ServiceResponse<GetPurchaseDto>> UpdatePurchase(CreatePurchasedDto createPurchaseDto, long id)
        {
            ServiceResponse<GetPurchaseDto> response = new();
            var purchase = await purchaseRepository.GetPurchashedById(id);
            if (purchase == null)
            {
                response.Messages.Add("Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }

            purchase.Note = createPurchaseDto.Note;
            purchaseRepository.Update(purchase);
            response.Messages.Add("Updated.");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;
        }

        public async Task< List<string>> UploadFile(List<IFormFile> formFiles)
        {
            List<String> medias = new();
            foreach (var file in formFiles)
            {
                if (formFiles.Count > 0)
                {
                    string fName = Path.GetRandomFileName();

                    var getext = Path.GetExtension(file.FileName);
                    var filename = Path.ChangeExtension(fName, getext);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "files/cashmemo");
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    filePath = Path.Combine(filePath, filename);
                    var pathdb = "files/cashmemo/" + filename;
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);
                        stream.Flush();
                    }
                    medias.Add(pathdb);
                    
                }
               
            }
            return medias;
        }
    }
}
