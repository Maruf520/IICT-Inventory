using IICT_Store.Services.ApprovalServices;
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
    public class ApprovalController : ControllerBase
    {
        private readonly IApprovalService approvalServices;
        public ApprovalController(IApprovalService approvalServices)
        {
            this.approvalServices = approvalServices;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPurchaseById(long id)
        {
            var purchase = await approvalServices.GetById(id);
            return Ok(purchase);
        }
        [HttpGet("pending")]
        public async Task<IActionResult> GetPendingPurchase()
        {
            var purchase = await approvalServices.GetPendingPurchase();
            return Ok(purchase);
        }       
        [HttpGet("rejected")]
        public async Task<IActionResult> GetRejectedPurchase()
        {
            var purchase = await approvalServices.GetRejectedPurchase();
            return Ok(purchase);
        }      
        [HttpGet("confirmed")]
        public async Task<IActionResult> GetConfirmedPurchase()
        {
            var purchase = await approvalServices.GetConfirmedPurchase();
            return Ok(purchase);
        }
        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmPurchased(long id)
        {
            var purchase = await approvalServices.ConfirmStatus(id);
            return Ok(purchase);
        }      
        [HttpPost("reject")]
        public async Task<IActionResult> RejectPurchased(long id)
        {
            var purchase = await approvalServices.RejectStatus(id);
            return Ok(purchase);
        }

    }
}
