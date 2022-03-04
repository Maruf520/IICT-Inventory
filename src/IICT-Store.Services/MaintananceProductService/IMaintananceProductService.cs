﻿using System.Threading.Tasks;
using IICT_Store.Dtos.ProductDtos;
using IICT_Store.Models;

namespace IICT_Store.Services.MaintananceProductService
{
    public interface IMaintananceProductService
    {
        Task<ServiceResponse<GetMaintananceProductDto>> Create(
            CreateMaintananceProductDto createMaintananceProduct);
    }
}