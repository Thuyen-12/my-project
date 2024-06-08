using ALR.Data.Dto;
using ALR.Domain.Entities;
using ALR.Domain.Entities.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Infrastructure.APIConfig.Mapping
{
    public class TenantManageMappings : Profile
    {
        public TenantManageMappings() 
        {
             CreateMap<TenantManageDto, UserEntity>().ReverseMap();

            CreateMap<RoomEntity, TenantManageDto>()
               .ForMember(des => des.roomId, opt => opt.MapFrom(src =>src.roomId)).ReverseMap();

            CreateMap<MotelEntity, TenantManageDto>()
               .ForMember(des => des.motelId, opt => opt.MapFrom(src => src.motelID)).ReverseMap();
        }
    }
}
