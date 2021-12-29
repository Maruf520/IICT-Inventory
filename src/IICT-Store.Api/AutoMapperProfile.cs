using AutoMapper;
using IICT_Store.Dtos.Categories;
using IICT_Store.Dtos.DistributionDtos;
using IICT_Store.Dtos.PersonDtos;
using IICT_Store.Dtos.ProductDtos;
using IICT_Store.Dtos.Purchases;
using IICT_Store.Models.Categories;
using IICT_Store.Models.Persons;
using IICT_Store.Models.Products;
using IICT_Store.Models.Pruchashes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IICT_Store.Api
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateProductDto, Product>();
            CreateMap<Product, GetProductDto>();
            CreateMap<Category,GetCategoryDto>();
            CreateMap<CreatePurchasedDto, Purchashed>();
            CreateMap<CreatePurchasedDto, GetPurchaseDto>();
            CreateMap<Purchashed, GetPurchaseDto>();
            CreateMap<CashMemo, CashMemoDtos>();
            CreateMap<Person, GetPersonDto>();
            CreateMap<CreatePersonDto, GetPersonDto>();
            CreateMap<CreatePersonDto, Person>();
            CreateMap<CreateDistributionDto, Distribution>();
            CreateMap<CreateDistributionDto, GetDistributionDto>();
            CreateMap<ProductSerialNoDto, ProductSerialNo>();
            CreateMap<ProductSerialNo, ProductSerialNoDto>();
            CreateMap<Distribution, GetDistributionDto>();
        }
    }
}
