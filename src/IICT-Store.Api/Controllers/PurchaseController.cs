﻿using IICT_Store.Dtos.Purchases;
using IICT_Store.Models.Pruchashes;
using IICT_Store.Services.PurchaseServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IICT_Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : BaseController
    {
        private readonly IPurchaseService purchaseService;
        public PurchaseController(IPurchaseService purchaseService)
        {
            this.purchaseService = purchaseService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePurchase([FromForm]CreatePurchasedDto createPurchaseDto)
        {
            var purchase = await purchaseService.CreatePurchase(createPurchaseDto, GetuserId());
            return Ok(purchase);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPurchaseById(long id)
        {
            var purchase = await purchaseService.GetPurchaseById(id);
            return Ok(purchase);
        }
        [HttpGet("product/{id}")]
        public async Task<IActionResult> GetPurchaseByProductId(long id)
        {
            var purchase = await purchaseService.GetPurchaseByProductId(id);
            return Ok(purchase);
        }

        [HttpGet]
        public async Task<IActionResult> GetPurchaseByDate(int year, PaymentBy paymentBy, PaymentProcess paymentProcess)
        {
            var purchase = await purchaseService.GetPurchaseByDate(year, paymentBy,paymentProcess);
            return Ok(purchase);
        }


    }
}
