﻿using AutoMapper;
using Storage.Core.Models.Storage;
using Storage.WebApi.DTO;

namespace Storage.WebApi.Helpers
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<Product, ProductDTOInfoReadOnly>()
                .ForMember(m => m.ItemsCount, opt => opt.MapFrom(x => x.Items.Count()))
                .ForMember(m => m.ProductCategory, opt => opt.MapFrom(x => new ProductCategoryDTO() { Id = x.ProductCategory.Id, Name = x.ProductCategory.Name }))
                .ForMember(m => m.Items, opt => opt.MapFrom(x => x.Items
                    .Select(x => new StorageItemDTO() { Id = x.Id, ProductId = x.ProductId, CellId = x.CellId ?? 0, AddTime = x.AddTime })));

            CreateMap<Product, ProductDTOItemInfoReadOnly>();
            CreateMap<StorageItem, StorageItemDTO>();
            CreateMap<StorageItemDTO, StorageItem>();
            CreateMap<StorageItem, StorageItemDTOInfoReadOnly>();
            CreateMap<ProductCategory, ProductCategoryDTO>();

            CreateMap<CellType, CellTypeDTO>();
            CreateMap<CellTypeDTO, CellType>();
            CreateMap<SubArea, SubAreaDTOReadOnlyInfo>();
            CreateMap<Cell, CellDTOInfoReadOnly>();
            CreateMap<Cell, CellDTOShortInfoReadOnly>();
            CreateMap<Area, AreaDTO>();
        }
    }
}