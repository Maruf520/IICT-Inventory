using AutoMapper;
using IICT_Store.Dtos.ProductDtos;
using IICT_Store.Dtos.Purchases;
using IICT_Store.Dtos.UserDtos;
using IICT_Store.Models;
using IICT_Store.Repositories.ProductRepositories;
using IICT_Store.Repositories.PurchaseRepositories;
using IICT_Store.Repositories.UserRepositories;
using IICT_Store.Services.ProductServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Services.ApprovalServices
{
    public class ApprovalService : IApprovalService
    {
        private readonly IPurchaseRepository purchaseRepository;
        private readonly IMapper mapper;
        private readonly IProductRepository productRepository;
        private readonly IUserRepository userRepository;
        public ApprovalService(IPurchaseRepository purchaseRepository, IMapper mapper, IProductRepository productRepository, IUserRepository userRepository)
        {
            this.purchaseRepository = purchaseRepository;
            this.mapper = mapper;
            this.productRepository = productRepository;
            this.userRepository = userRepository;
        }

        public async Task<ServiceResponse<List<GetPurchaseDto>>> GetPendingPurchase()
        {
            ServiceResponse<List<GetPurchaseDto>> response = new();
            List<GetPurchaseDto> getPurchaseDtos = new();
            var purchases = await purchaseRepository.GetPendingPurchased();
            if (purchases == null)
            {
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.Messages.Add("No Pending Purchase Found.");
                return response;
            }
            foreach (var purchase in purchases)
            {
                var product = await productRepository.GetProductById(purchase.ProductId);
                var productToMap = mapper.Map<GetProductDto>(product);
                var map = mapper.Map<GetPurchaseDto>(purchase);
                var x = await userRepository.GetById(purchase.CreatedBy);
                map.CreatedByUser = mapper.Map<GetUserDto>(await userRepository.GetById(purchase.CreatedBy));
                map.Product = productToMap;
                getPurchaseDtos.Add(map);
            }

            response.Data = getPurchaseDtos;
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.Messages.Add("All Pending purchase.");
            return response;
        }
        public async Task<ServiceResponse<List<GetPurchaseDto>>> GetRejectedPurchase()
        {
            ServiceResponse<List<GetPurchaseDto>> response = new();
            List<GetPurchaseDto> getPurchaseDtos = new();
            var purchases = await purchaseRepository.GetRejectedPurchased();
            if (purchases == null)
            {
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.Messages.Add("No Pending Purchase Found.");
                return response;
            }
            foreach (var purchase in purchases)
            {
                var product = await productRepository.GetProductById(purchase.ProductId);
                var productToMap = mapper.Map<GetProductDto>(product);
                var map = mapper.Map<GetPurchaseDto>(purchase);
                map.CreatedByUser = mapper.Map<GetUserDto>(await userRepository.GetById(purchase.CreatedBy));
                map.RejectedByUser = mapper.Map<GetUserDto>(await userRepository.GetById(purchase.RejectedBy));
                map.Product = productToMap;
                getPurchaseDtos.Add(map);
            }

            response.Data = getPurchaseDtos;
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.Messages.Add("All Rejected purchase.");
            return response;
        }
        public async Task<ServiceResponse<List<GetPurchaseDto>>> GetConfirmedPurchase()
        {
            ServiceResponse<List<GetPurchaseDto>> response = new();
            List<GetPurchaseDto> getPurchaseDtos = new();
            var purchases = await purchaseRepository.GetConfirmedPurchased();
            if (purchases == null)
            {
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.Messages.Add("No Pending Purchase Found.");
                return response;
            }
            foreach (var purchase in purchases)
            {
                var product = await productRepository.GetProductById(purchase.ProductId);
                var productToMap = mapper.Map<GetProductDto>(product);
                var map = mapper.Map<GetPurchaseDto>(purchase);
                map.CreatedByUser = mapper.Map<GetUserDto>(await userRepository.GetById(purchase.CreatedBy));
                map.ConfirmByUser = mapper.Map<GetUserDto>(await userRepository.GetById(purchase.ConfirmedBy));
                map.Product = productToMap;
                getPurchaseDtos.Add(map);
            }

            response.Data = getPurchaseDtos;
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.Messages.Add("All Confirmed purchase.");
            return response;
        }

        public async Task<ServiceResponse<GetPurchaseDto>> ConfirmStatus(long id, string userId)
        {
            ServiceResponse<GetPurchaseDto> response = new();
            var purchase = await purchaseRepository.GetPurchashedById(id);
            if (purchase == null)
            {
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.Messages.Add("Not Found.");
                return response;
            }

            purchase.ConfirmDate = DateTime.Now;
            purchase.ConfirmedBy = userId;
            purchase.IsConfirmed = true;
            purchase.CreatedBy = userId;
            purchase.PurchaseStatus = Models.Pruchashes.PurchaseStatus.Confirmed;
            var product = productRepository.GetById(purchase.ProductId);
            product.QuantityInStock = product.QuantityInStock + purchase.Quantity;
            product.TotalQuantity = product.TotalQuantity + purchase.Quantity;
            productRepository.Update(product);
            purchaseRepository.Update(purchase);
            var productToMap = mapper.Map<GetProductDto>(product);
            var purchaseToReturn = mapper.Map<GetPurchaseDto>(purchase);
            purchaseToReturn.CreatedByUser = mapper.Map<GetUserDto>(await userRepository.GetById(purchase.CreatedBy));
            purchaseToReturn.ConfirmByUser = mapper.Map<GetUserDto>(await userRepository.GetById(purchase.ConfirmedBy));
            purchaseToReturn.Product = productToMap;
            response.Data = purchaseToReturn;
            response.Messages.Add("Purchase Confirmed.");
            response.StatusCode = System.Net.HttpStatusCode.Created;
            return response;
        }
        public async Task<ServiceResponse<GetPurchaseDto>> RejectStatus(long id, string userId)
        {
            ServiceResponse<GetPurchaseDto> response = new();
            var purchase = purchaseRepository.GetById(id);
            if (purchase == null)
            {
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.Messages.Add("Not Found.");
                return response;
            }

            purchase.IsConfirmed = false;
            purchase.CreatedBy = userId;
            purchase.RejectedBy = userId;
            purchase.PurchaseStatus = Models.Pruchashes.PurchaseStatus.Rejected;
            purchaseRepository.Update(purchase);
            var product = productRepository.GetById(purchase.ProductId);
            var productToMap = mapper.Map<GetProductDto>(product);
            var purchaseToReturn = mapper.Map<GetPurchaseDto>(purchase);
            purchaseToReturn.CreatedByUser = mapper.Map<GetUserDto>(await userRepository.GetById(purchase.CreatedBy));
            purchaseToReturn.RejectedByUser = mapper.Map<GetUserDto>(await userRepository.GetById(purchase.RejectedBy));
            purchaseToReturn.Product = productToMap;
            response.Data = purchaseToReturn;
            response.Messages.Add("Purchase Rejected.");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;
        }

        public async Task<ServiceResponse<GetPurchaseDto>> GetById(long id)
        {
            ServiceResponse<GetPurchaseDto> response = new();
            var purchase = await purchaseRepository.GetPurchashedById(id);
            if (purchase == null)
            {
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.Messages.Add("Not Found.");
                return response;
            }
            var product = productRepository.GetById(purchase.ProductId);
            var productToMap = mapper.Map<GetProductDto>(product);
            var map = mapper.Map<GetPurchaseDto>(purchase);
            map.CreatedByUser = mapper.Map<GetUserDto>(await userRepository.GetById(purchase.CreatedBy));
            map.RejectedByUser = mapper.Map<GetUserDto>(await userRepository.GetById(purchase.RejectedBy));
            map.ConfirmByUser = mapper.Map<GetUserDto>(await userRepository.GetById(purchase.ConfirmedBy));
            map.Product = productToMap;
            response.Data = map;
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;
        }


    }
}
