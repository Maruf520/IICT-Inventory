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
using IICT_Store.Models.Products;
using IICT_Store.Repositories.TestRepo;
using IICT_Store.Repositories.UserRepositories;
using IICT_Store.Services.MailServices;

namespace IICT_Store.Services.PurchaseServices
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IMapper mapper;
        private readonly IPurchaseRepository purchaseRepository;
        private readonly IProductService productService;
        private readonly IProductRepository productRepository;
        private readonly IMailService mailService;
        private readonly IUserRepository userRepository;
        private readonly IBaseRepo baseRepo;
        public PurchaseService(IPurchaseRepository purchaseRepository, IProductService productService, IMapper mapper, IProductRepository productRepository, IMailService mailService, IUserRepository userRepository, IBaseRepo baseRepo)
        {
            this.purchaseRepository = purchaseRepository;
            this.productService = productService;
            this.mapper = mapper;
            this.productRepository = productRepository;
            this.mailService = mailService;
            this.userRepository = userRepository;
            this.baseRepo = baseRepo;
        }
        public async Task<ServiceResponse<GetPurchaseDto>> CreatePurchase(CreatePurchasedDto createPurchaseDto, string userId)
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
            List<CashMemoDtos> cashMemoDtos = new();
            foreach(var cashmemo in cashMemos)
            {
                CashMemoDtos cashMemoDto = new();
                cashMemoDto.CreatedAt = cashmemo.CreatedAt;
                cashMemoDto.Id = cashmemo.Id;
                cashMemoDto.ImageUrl = cashmemo.ImageUrl;
                cashMemoDto.PurchashedId = cashMemoDto.PurchashedId;
                cashMemoDtos.Add(cashMemoDto);

            }
            productToMap.CreatedAt = DateTime.Now;
            productToMap.CreatedBy = userId;
            productToMap.CashMemos = cashMemos;
            purchaseRepository.Insert(productToMap);
            var users = await userRepository.GetUserByRole("Approval Admin");
            foreach (var mail in users)
            {
                var user = await userRepository.GetByEmail(mail);
                await mailService.SendEmail(mail, "IICT Inventory", "Sir, There is a pending request on IICT Inventory.. Please check it and take your step." ,$"{user.UserName}");
            }
            var productToReturn = mapper.Map<GetPurchaseDto>(createPurchaseDto);
            productToReturn.Id = productToMap.Id;
            productToReturn.CashMemos = cashMemoDtos;
/*            products.QuantityInStock = products.QuantityInStock + createPurchaseDto.Quantity;
            products.TotalQuantity = products.TotalQuantity + createPurchaseDto.Quantity;
            productRepository.Update(products);*/

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
            var product = productRepository.GetById(purchase.ProductId);
            var map = mapper.Map<GetProductDto>(product);
            var purchaseToMap = mapper.Map<GetPurchaseDto>(purchase);
            purchaseToMap.PuchasedDate = purchase.CreatedAt;
            purchaseToMap.Product = map;

            response.Data = purchaseToMap;
            response.Messages.Add("Purcahse.");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;
        }

        public async Task<ServiceResponse<List<GetPurchaseDto>>> GetPurchaseByProductId(long id)
        {
            ServiceResponse<List<GetPurchaseDto>> response = new();
            var purchase = await purchaseRepository.GetPurchashedByProductId(id);
            if (purchase.Count == 0)
            {
                response.Messages.Add("Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            var map = mapper.Map<List<GetPurchaseDto>>(purchase);
            response.Data = map;
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.Messages.Add("Purchased Product.");
            return response;
        }

        public async Task<ServiceResponse<GetPurchaseDto>> UpdatePurchase(CreatePurchasedDto createPurchaseDto, long id, string userId)
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
            purchase.UpdatedBy = userId;
            purchaseRepository.Update(purchase);
            response.Messages.Add("Updated.");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;
        }

        public async Task<ServiceResponse<List<GetPurchaseDto>>> GetPurchaseByDate(int year,PaymentBy paymentBy, PaymentProcess paymentProcess)
        {
            ServiceResponse<List<GetPurchaseDto>> response = new();
            var purchase = await purchaseRepository.GetPurchashedByDate(year,paymentBy,paymentProcess);
            if(purchase.Count == 0)
            {
                response.Messages.Add("Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            var map = mapper.Map<List<GetPurchaseDto>>(purchase);
            response.Messages.Add("All Purchase");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.Data = map;
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

        public async Task<ServiceResponse<List<GetPurchaseHistory>>> GetPurchaseHistory(int year, int productId)
        {
            ServiceResponse<List<GetPurchaseHistory>> response = new();
            List<GetPurchaseHistory> getHistories = new();
            if (productId == 0)
            {
                var products = await productRepository.GetAllProduct();

                foreach (var product in products)
                {
                    GetPurchaseHistory getPurchaseHistory = new();
                    var singleProduct = await productRepository.GetProductById(product.Id);
                    var productToMap = mapper.Map<GetProductDto>(product);
                    var getPurchase =  baseRepo.GetItems<Purchashed>( x => x.ProductId == product.Id && x.IsConfirmed && x.CreatedAt.Year == year).ToList();
                    var totalQunatity = getPurchase.Select(x => x.Quantity).Sum();
                    decimal totalAmount = getPurchase.Select(e => e.Price * e.Quantity).Sum();
                    getPurchaseHistory.Product = productToMap;
                    getPurchaseHistory.TotalAmount = totalAmount;
                    getPurchaseHistory.TotalQuntity = totalQunatity;
                    getPurchaseHistory.Year = year;
                    getHistories.Add(getPurchaseHistory);
                }
                response.Data = getHistories;
                return response;
            }
            var individualProduct =  baseRepo.GetById<Product>(productId);
            GetPurchaseHistory getPurchaseHistories = new();
            var productToMaap = mapper.Map<GetProductDto>(individualProduct);
            var getPurchasee = baseRepo.GetItems<Purchashed>(x => x.ProductId == individualProduct.Id && x.IsConfirmed && x.CreatedAt.Year == year);
            var totalQunatity1 = getPurchasee.Select(x => x.Quantity).Sum();
            decimal totalAmount1 = getPurchasee.Select(e => e.Price * e.Quantity).Sum();
            getPurchaseHistories.Product = productToMaap;
            getPurchaseHistories.TotalAmount = totalAmount1;
            getPurchaseHistories.TotalQuntity = totalQunatity1;
            getPurchaseHistories.Year = year;
            getHistories.Add(getPurchaseHistories);
            response.Data = getHistories;
            return response;
        }
    }
}
